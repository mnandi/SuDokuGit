#region Source File Description
#endregion
#region References
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Timers;
#endregion
using System.Runtime.Serialization.Formatters.Binary;

namespace SuDoku {
	delegate void ShowTimerDelegate();

	public partial class SuDokuForm: Form {
		#region Common data
		static readonly Color borderColor=Color.Black;			//	Cell border
		static readonly Color text0Color=Color.Blue;			//	Original cell number
		static readonly Color text1Color=Color.LawnGreen;		//	Filled cell number
		static readonly Color text2Color=Color.Red;				//	Error cell number - imposs
		static readonly Color text3Color=Color.RosyBrown;		//	Error cell number - imposs
		static readonly Color back0Color=Color.White;			//	Default
		static readonly Color back1Color=Color.PaleGoldenrod;	//	XCross
		static public GameDef actGameDef=null;				//	Actual game definition
		static public GameTable gameTable;					//	Actual game table
		static public TableQueue tableQueue=null;			//	Previous game tables
		static SolidBrush br=new SolidBrush(borderColor);	//	Draw rectangle border
		static SolidBrush bo=new SolidBrush(text0Color);	//	Fix      Item text color
		static SolidBrush bs=new SolidBrush(text1Color);	//	Selected Item text color
		static SolidBrush be=new SolidBrush(text2Color);	//	Defected Item text color
		static SolidBrush bn=new SolidBrush(text3Color);	//	Fix defected Item text color
		static SolidBrush bf=new SolidBrush(back0Color);	//	Clear picture
		static SolidBrush bd=new SolidBrush(back1Color);	//	Clear picture diadonal
		//	Game states
		static bool gameState=false;			//	false=not gaming / true=gaming
		static System.Timers.Timer gameTimer;	//	Game play time counter
		static int gameCounter=0;				//	game time in seconds	
		//	sizing parameters
		static Font numFont;		//	Font to Write cell number
		static int baseX=0;			//	game table left position
		static int baseY=0;			//	game table top  position
		static int cellSize;		//	Cell size in pixels
		static int selnb=0;
		#endregion
		#region Game initialization & gaming
		public SuDokuForm() {
			InitializeComponent();

			//	Init game comboBox
			for(int ii=0; ii<Constants.gameDefTb.Length; ii++) {
				comboGameType.Items.Add(Constants.gameDefTb[ii].gName);
			}
			pictureTable.BackColor=Color.DimGray;
			tableQueue=new TableQueue();
			comboGameType.SelectedIndex=0;
			gameTimer=new System.Timers.Timer(1000);
			gameTimer.Elapsed+=OnTimedEvent;
			gameTimer.AutoReset=true;
			gameTimer.Enabled=true;
		}
		private void buttonExit_Click(object sender,EventArgs e) {
			this.Close();
		}

		private void buttonStartGame_Click(object sender,EventArgs e) {
			if(!gameState) {		//	false=not gaming / true=gaming
				buttonStartGame.Text="Játék leállítása";
				comboGameType.Enabled=
				numericLevel.Enabled=
				comboGameName.Enabled=
				textGameComment.Enabled=false;
				gameTable.ClearSelects(ref selnb);
			} else {
				buttonStartGame.Text="--> Játék indítása";
				comboGameType.Enabled=
				numericLevel.Enabled=
				comboGameName.Enabled=
				textGameComment.Enabled=true;
			}
			gameState=!gameState;
		}
		#endregion
		#region Game Timer
		void OnTimedEvent(object source,ElapsedEventArgs e) {
			if(gameState) {
				gameCounter++;
				ShowTimer();
			}

		}
		void ShowTimer() {
			if(this.InvokeRequired) {
				try {
					// This is a worker thread so delegate the task.
					this.Invoke(new ShowTimerDelegate(this.ShowTimer));
				} catch {
					//	ObjectDisposedException
					return;
				}
			} else {
				string aTime=string.Format("{0:D2}:{1:D2}:{2:D2}",(gameCounter/360)%60,(gameCounter/60)%60,gameCounter%60);
				textTimer.Text=aTime;
			}
		}
		#endregion
		#region Game testing buttons
		private void buttonCheckResolving_Click(object sender,EventArgs e) {
			gameTable.ClearSelects(ref selnb);
			GameTable gameTableSave=gameTable.DeepClone(gameTable);
			sresult sret=SuSolve(solvetype.MAKERESULT/*TESTRESULTS*/);
			switch(sret) {
				case sresult.SOLVE_OK:
					var ret=MessageBox.Show("A tábla megoldható\r\nMutassam a végeredményt?","Megoldhatóság",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
					if(ret==DialogResult.Yes) {
						pictureTable_Resize(null,null);
						MessageBox.Show("Tovább","Kilépés",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					}
					break;
				default:
					MessageBox.Show("A tábla nem oldható meg!","Megoldhatóság",MessageBoxButtons.OK,MessageBoxIcon.Error);
					break;
			}
			gameTable=gameTableSave;
			gameTableSave=null;
			gameTable.ClearSelects(ref selnb);
			pictureTable_Resize(null,null);
		}
		private void buttonTestGame_Click(object sender,EventArgs e) {
			int conflictNb=0;
			int conflictAll=0;
			gameTable.ClearSelects(ref selnb);
			for(int yy=0; yy<gameTable.tabSize; yy++) {
				for(int xx=0; xx<gameTable.tabSize; xx++) {
					GameCell cell=gameTable.cell(xx,yy);
					if(cell.fixNum<1)
						continue;
					CellResult cErrors=gameTable.CountCellFlag(cell);
					int errCount=cErrors.errList.Count;
					if(errCount==0)
						continue;
					conflictNb++;
					conflictAll+=errCount;
					for(int ii=0; ii<errCount; ii++) {
						//?	Show conflicts
					}
				}
			}
			gameTable.ClearSelects(ref selnb);
		}
		#endregion
		#region Game table drawing
		private void comboGameType_SelectedIndexChanged(object sender,EventArgs e) {
			actGameDef=Constants.gameDefTb[comboGameType.SelectedIndex];
			InitSuDokuTable(actGameDef);

		}
		void InitSuDokuTable(GameDef game) {
			gameTable=new GameTable(game.xCells,game.yCells);
			pictureTable_Resize(null,null);
		}
		void RedrawTable() {
			gameTable.xCells=actGameDef.xCells;
			gameTable.yCells=actGameDef.yCells;
			gameTable.diagFlag=actGameDef.xCross;
			int cellSizX=pictureTable.Width-((gameTable.tabSize+1)*Constants.cellOffs+(actGameDef.yCells-1)*Constants.groupOffs);
			int cellSizY=pictureTable.Height-((gameTable.tabSize+1)*Constants.cellOffs+(actGameDef.xCells-1)*Constants.groupOffs);
			cellSize=Math.Min(cellSizX,cellSizY)/gameTable.tabSize;
			//cellSize=(Math.Min(pictureTable.Width,pictureTable.Height)-3*gameTable.tabSize)/gameTable.tabSize;
			int cellsSiz=gameTable.tabSize*cellSize;
			baseX=baseY=0;
			baseX=(pictureTable.Width-SetLeft(gameTable.tabSize,cellSize))/2;
			baseY=(pictureTable.Height-SetTop(gameTable.tabSize,cellSize))/2;
			if(pictureTable.Image==null)
				pictureTable.Image=new Bitmap(pictureTable.Width,pictureTable.Height);	//	bitmap of table
			numFont=new Font("Arial",cellSize,FontStyle.Bold,GraphicsUnit.Pixel);
			using(Graphics g=Graphics.FromImage(pictureTable.Image)) {
				for(int yy=0; yy<actGameDef.xCells*actGameDef.yCells; yy++) {
					for(int xx=0; xx<actGameDef.xCells*actGameDef.yCells; xx++) {
						DrawCell(g,br,cellSize,gameTable.cell(xx,yy));
					}
				}
			}
		}
		int SetLeft(int xp,int siz) {
			return baseX+xp*(siz+Constants.cellOffs)+(xp/actGameDef.xCells)*Constants.groupOffs;
		}
		int SetTop(int yp,int siz) {
			return baseY+yp*(siz+Constants.cellOffs)+(yp/actGameDef.yCells)*Constants.groupOffs;
		}
		void DrawCell(Graphics g,Brush brx,int size,GameCell item) {
			Rectangle rect=new Rectangle(SetLeft(item.cellX,size),SetTop(item.cellY,size),size,size);
			Brush bb=((actGameDef.xCross==(int)GameType.DIAGGAME)&&
					  ((item.cellX==item.cellY)||
					   (item.cellX==(actGameDef.xCells*actGameDef.yCells)-1-item.cellY)))?bd:bf;
			g.FillRectangle(bb,rect);
			g.DrawRectangle(new Pen(br),rect);
			if(item.fixbitnum!=0) {
				string chstr=((char)(((gameTable.tabSize>9)?Constants.chrBase:Constants.numBase)+item.fixNum)).ToString();
				if(item.imposs==1){
					if(item.orig==1)
						g.DrawString(chstr,numFont,be,new Point(rect.X,rect.Y));
					else
						g.DrawString(chstr,numFont,bn,new Point(rect.X,rect.Y));
				}else if(item.orig==1)
					g.DrawString(chstr,numFont,bo,new Point(rect.X,rect.Y));
				else
					g.DrawString(chstr,numFont,bs,new Point(rect.X,rect.Y));
			}
		}

		private void pictureTable_Resize(object sender,EventArgs e) {
			if(this.WindowState!=FormWindowState.Normal)
				return;
			//if(sender!=null) {
			pictureTable.Image=new Bitmap(pictureTable.Width,pictureTable.Height);	//	bitmap of table
			//}
			RedrawTable();
		}

		private void pictureTable_Paint(object sender,PaintEventArgs e) {
			RedrawTable();
		}
		#endregion
		#region Numpad handling
		private void pictureTable_MouseClick(object sender,MouseEventArgs e) {
			GameCell item=null;
			int xx=gameTable.tabSize;
			while(SetLeft(xx,cellSize)>e.X)
				xx--;
			if((xx<0)||(xx>=gameTable.tabSize))
				return;
			int yy=gameTable.tabSize;
			while(SetTop(yy,cellSize)>e.Y)
				yy--;
			if((yy<0)||(yy>=gameTable.tabSize))
				return;
			item=gameTable.cell(xx,yy);
			int x=SetLeft(xx,cellSize)+cellSize*7/10;
			int y=SetTop(yy,cellSize)+cellSize*7/10;
			Point ptCell=((Control)sender).PointToScreen(new Point(x,y));
			int num;
			if(e.Button==MouseButtons.Left) {
				num=ShowNumpad(ptCell,item,true);
			} else {
				num=ShowNumpad(ptCell,item,false);
			}
			if(num>=0) {
				item.fixNum=num;
				item.orig=(num==0)?0:1;
				using(Graphics g=Graphics.FromImage(pictureTable.Image)) {
					DrawCell(g,br,cellSize,gameTable.cell(xx,yy));
				}
				pictureTable.Refresh();
			}
		}
		int ShowNumpad(Point loc,GameCell item,bool all) {
			//	all	=true  - left button
			//		=false - right button
			NumPad pad=new NumPad(item,actGameDef,all);
			pad.Location=loc;
			pad.ShowDialog();
			return pad.numButton;	//	-1-Abort, 0-Delete, n-num
		}
		#endregion
		#region Save file handling Load/Save/Add
		//======================================
		//	Save file handling
		//======================================
		private void buttonAddGame() {
			string oLine=string.Format("[{0}]\t={1}{2}x{3}\t	#{4};\t{5}",comboGameName.Text,comboGameType.Text,textGameComment.Text);
			GameFile.AddGame(0,oLine);
			int ch0=(gameTable.tabSize>9)?Constants.chrBase:Constants.numBase;	//	show '1' if table <= 3x3 else 'A'
			for(int ii=0; ii<gameTable.tabSize; ii++) {
				oLine="";
				for(int jj=0; jj<gameTable.tabSize; jj++) {
					int num=gameTable.cell(jj,ii).fixNum;
					oLine+=((char)((num>0)?(num+ch0):0x20)).ToString();
				}
				GameFile.AddGame(1,oLine);
			}
			GameFile.AddGame(2,"*");
		}
		private void buttonSaveFile_Click(object sender,EventArgs e) {
			OpenFileDialog saveFileDlg=new OpenFileDialog();
			saveFileDlg.InitialDirectory=".\\";
			saveFileDlg.Filter="txt files (*.txt)|*.txt|All files (*.*)|*.*";
			saveFileDlg.FilterIndex=2;
			saveFileDlg.RestoreDirectory=true;
			if(saveFileDlg.ShowDialog()==DialogResult.OK) {
				GameFile.SaveFile(saveFileDlg.FileName);
			}
		}
		private void buttonLoadFile_Click(object sender,EventArgs e) {
			OpenFileDialog loadFileDlg=new OpenFileDialog();
			loadFileDlg.InitialDirectory=".\\";
			loadFileDlg.Filter="txt files (*.txt)|*.txt|All files (*.*)|*.*";
			loadFileDlg.FilterIndex=2;
			loadFileDlg.RestoreDirectory=true;
			if(loadFileDlg.ShowDialog()==DialogResult.OK) {
				List<string> names=GameFile.LoadFile(loadFileDlg.FileName);
				if(names.Count>0) {
					comboGameName.Items.Clear();
					comboGameName.Items.AddRange(names.ToArray());
					comboGameName.SelectedIndex=0;
				}
			}
		}
		#endregion
		#region Game handling buttons
		private void buttonClearGame_Click(object sender,EventArgs e) {
			gameTable.ClearTable();
			pictureTable_Resize(null,null);
		}

		private void comboGameName_SelectedIndexChanged(object sender,EventArgs e) {
			if(comboGameName.SelectedIndex<0)
				return;
			int gameIndex=GameFile.GetGameIndexFull((string)comboGameName.Items[comboGameName.SelectedIndex]);
			if(gameIndex<0)
				return;
			GameParams pars=GameFile.GetGameParameters(gameIndex);
			if(pars==null)
				return;
			textGameName.Text=pars.name;
			comboGameType.Text=string.Format("={0}{1}*{2}\t({3}*{3})",(pars.diag!=0)?"X":"",pars.x,pars.y,pars.x*pars.y);
			numericLevel.Value=pars.level;
			textGameComment.Text=pars.comment;
			if((actGameDef.xCells!=pars.x)||(actGameDef.yCells!=pars.y)||(actGameDef.xCross!=pars.diag)) {
				actGameDef=new GameDef(pars.x,pars.y,pars.diag,comboGameType.Text);
				gameTable.InitTable(pars.x,pars.y);
				//pictureTable_Resize(null,null);
			}
			for(int yy=0; yy<pars.size; yy++) {
				string rowLine=GameFile.GetGameRow(gameIndex,yy+1);
				gameTable.FillTableRow(yy,((rowLine.Length>0)&&(rowLine[0]!='*'))?rowLine:"");
			}
			gameTable.CheckTable();
			pictureTable_Resize(null,null);
			int errnum=gameTable.CheckTable();
		}
		private void buttonFillGame_Click(object sender,EventArgs e) {
			MessageBox.Show("Ez még nincs kész!!","Kitöltés",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
		}
		#endregion

		private void buttonResolveTable_Click(object sender,EventArgs e) {
			// TODO: Add your control notification handler code here
			//if(timerId==TIMERID){
			//    KillTimer(timerId);
			//    timerId=0;
			//}
			gameTable.ClearSelects(ref selnb);
			GameTable gameTableSave=gameTable.DeepClone(gameTable);

			//CString secs(" 0:00:00");
			//CStatic* pTM=(CStatic*)GetDlgItem(IDC_TIMER);
			//pTM->SetWindowText(secs);

			int ret;
			if((ret=CheckAll())!=0) {
				pictureTable_Resize(null,null);
				MessageBox.Show("A tábla nem megoldható","Eredmény",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}

			//RedrawTable();
			string msg;
			sresult tret=SuSolve(/*solvetype.MAKERESULT*/solvetype.TESTRESULTS);
			//		-1	- no solution
			//		 0	- solved
			//		>0	- other solve error
			pictureTable_Resize(null,null);
			switch(tret){
				case sresult.SOLVE_MORE:		//	-9	"More then one solution"
				case sresult.SOLVE_NOMEM1:		//		"No enough memory space"
				case sresult.SOLVE_UNPOSSIBLE:	//		"Lehetetlen megoldás"
				case sresult.SOLVE_NOMEM2:		//		"No enough memory space"
				case sresult.SOLVE_FILE:		//		"Wrong file parameter"
				case sresult.SOLVE_PARAM:		//		"Wrong game parameter"
				case sresult.SOLVE_TABLE:		//		"Not enough game lines"
				case sresult.SOLVE_DATA:		//	-2	"Data error"
				case sresult.SOLVE_NORESULT:	//	 0	"Hiba, nincs megoldás"
					MessageBox.Show(string.Format("A több {0} megoldása is van!",tret),"Eredmény",MessageBoxButtons.OK,MessageBoxIcon.Information);
					break;
				case sresult.SOLVE_OK:		//	-1	"Jó megoldás !"
					//	Returns:
					MessageBox.Show("A tábla megoldása\r\nBefejezem","Eredmény",MessageBoxButtons.OK,MessageBoxIcon.Information);
					break;
				default:
					MessageBox.Show("A tábla nem megoldható","Eredmény",MessageBoxButtons.OK,MessageBoxIcon.Error);
					//AfxMessageBox(msg,MB_OK);
					break;
			}
			gameTable=gameTableSave;
			gameTableSave=null;
			pictureTable_Resize(null,null);
		}

		private void buttonSaveGame_Click(object sender,EventArgs e) {
			string gameName=textGameName.Text;
			if(string.IsNullOrWhiteSpace(gameName)) {
				MessageBox.Show("A játék nevet ki kell tölteni!","Hiba",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			int gameIndex=GameFile.GetGameIndex(gameName);
			if(gameIndex<0) {
				//	New game - create game definition
				GameParams pars=GameFile.GetGameParameters(gameIndex);
				if(pars==null)
					return;
				comboGameType.Text=string.Format("={0}{1}*{2}\t({3}*{3})",(pars.diag!=0)?"X":"",pars.x,pars.y,pars.x*pars.y);
				numericLevel.Value=pars.level;
				textGameComment.Text=pars.comment;
				if((actGameDef.xCells!=pars.x)||(actGameDef.yCells!=pars.y)||(actGameDef.xCross!=pars.diag)) {
					actGameDef=new GameDef(pars.x,pars.y,pars.diag,comboGameType.Text);
					gameTable.InitTable(pars.x,pars.y);
					//pictureTable_Resize(null,null);
				}
			} else {
				//	Replace old game - replace game definition
			}
			for(int yy=1; yy<actGameDef.tableSize; yy++) {
				string rowLine=gameTable.ExtractLine(gameIndex);
				gameTable.FillTableRow(yy,(rowLine[0]=='*')?"":rowLine);
			}
		}
	}
}
