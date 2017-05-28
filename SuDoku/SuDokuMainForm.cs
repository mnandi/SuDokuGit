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
		static readonly Color textColor=Color.Blue;				//	Cell number
		static readonly Color back0Color=Color.White;			//	Default
		static readonly Color back1Color=Color.PaleGoldenrod;	//	XCross
		static readonly Color back2Color=Color.PaleGoldenrod;	//	Actual
		static readonly Color back3Color=Color.PaleGoldenrod;	//	Error
		static public GameDef actGameDef=null;				//	Actual game definition
		//static public int tableSize;						//	tablesize of actual game
		static public GameTable gameTable;					//	Actual game table
		static public List<GameTable> gameQueue=null;		//	Previous game tables
		static SolidBrush br=new SolidBrush(borderColor);	//	Draw rectangle border
		static SolidBrush bn=new SolidBrush(textColor);		//	Item text color
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
		#endregion
		#region Game initialization & gaming
		public SuDokuForm() {
			InitializeComponent();

			//	Init game comboBox
			for(int ii=0; ii<Constants.gameDefTb.Length; ii++) {
				comboGameType.Items.Add(Constants.gameDefTb[ii].gName);
			}
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
			if(!gameState) {
				buttonStartGame.Text="Játék leállítása";
				comboGameType.Enabled=
				numericLevel.Enabled=
				comboGameName.Enabled=
				textGameComment.Enabled=false;
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
			gameTable.CheckTable();
			while(true) {
				Point? cellP=gameTable.GetNearestToComplete();
				if(cellP==null) {
					MessageBox.Show("A tábla megoldható","Megoldhatóság",MessageBoxButtons.OK,MessageBoxIcon.Information);
					break;
				}
				GameItem item=gameTable.item((Point)cellP);
				int num;
				if((item==null)||((num=GetTryValue(item))<1)) {
					MessageBox.Show("CSAK TESZT!!!\r\nA tábla nem oldható meg!","Megoldhatóság",MessageBoxButtons.OK,MessageBoxIcon.Error);
					break;
				}
				item.actNum=num;
				item.actFlag=1<<num;
				item.vFlag|=item.actFlag;
				gameTable.lastX=((Point)cellP).X;
				gameTable.lastY=((Point)cellP).Y;
				GameQueueAdd();
				List<int[]> err=SuCheck.StepValue(item);
				if(err.Count==0) {
					continue;
				} else {
					//	Step back
					int last=gameQueue.Count-1;
					if(last<0) {
						//	no result
						break;
					}
					gameTable=gameQueue[last];
					gameQueue.RemoveAt(last);
				}
			}
			if(gameQueue!=null) {
				gameTable=gameQueue[0];
				gameQueue=null;
				MessageBox.Show("A tábla nem oldható meg!","Megoldhatóság",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
		static int GetTryValue(GameItem i) {
			int num=1;
			int flag=i.vFlag;
			while(true) {
				flag>>=1;
				if((flag&1)==0)
					break;
				num++;
#if DEBUG
				if(num>gameTable.tabSize) {
					MessageBox.Show("Hibás vFlag","Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					break;
				}
#endif
			}
			return num;
		}
		private void buttonTestGame_Click(object sender,EventArgs e) {
			int conflictNb=0;
			int conflictAll=0;
			for(int yy=0; yy<gameTable.tabSize; yy++) {
				for(int xx=0; xx<gameTable.tabSize; xx++) {
					GameItem item=gameTable.item(xx,yy);
					if(item.actNum<1)
						continue;
					List<int[]> errors=SuCheck.CheckValues(item);
					int errCount=errors.Count;
					if(errCount==0)
						continue;
					conflictNb++;
					conflictAll+=errCount;
					for(int ii=0; ii<errCount; ii++) {
						//?	Show conflicts
					}
				}
			}
		}
		#endregion
		#region Game table drawing
		private void comboGameType_SelectedIndexChanged(object sender,EventArgs e) {
			actGameDef=Constants.gameDefTb[comboGameType.SelectedIndex];
			InitSuDokuTable(actGameDef);

		}
		void InitSuDokuTable(GameDef game){
			gameTable=new GameTable(game.xCells,game.yCells);
			pictureTable_Resize(null,null);
		}
		void RedrawTable() {
			gameTable.xCells=actGameDef.xCells;
			gameTable.yCells=actGameDef.yCells;
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
						DrawCell(g,br,cellSize,gameTable.item(xx,yy));
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
		void DrawCell(Graphics g,Brush brx,int size,GameItem item) {
			Rectangle rect=new Rectangle(SetLeft(item.cellX,size),SetTop(item.cellY,size),size,size);
			Brush bb=((actGameDef.xCross==(int)GameType.DIAGGAME)&&
					  ((item.cellX==item.cellY)||
					   (item.cellX==(actGameDef.xCells*actGameDef.yCells)-1-item.cellY)))?bd:bf;
			g.FillRectangle(bb,rect);
			g.DrawRectangle(new Pen(br),rect);
			if(item.actNum>0) {
				string chstr=((char)(((gameTable.tabSize>9)?0x40:0x30)+item.actNum)).ToString();
				g.DrawString(chstr,numFont,bn,new Point(rect.X,rect.Y));
			}
		}

		private void pictureTable_Resize(object sender,EventArgs e) {
			pictureTable.Image=new Bitmap(pictureTable.Width,pictureTable.Height);	//	bitmap of table
			RedrawTable();
		}

		private void pictureTable_Paint(object sender,PaintEventArgs e) {
			RedrawTable();
		}
		#endregion
		#region Numpad handling
		private void pictureTable_MouseClick(object sender,MouseEventArgs e) {
			GameItem item=null;
			int xx=gameTable.tabSize;
			while(SetLeft(xx,cellSize)>e.X)	xx--;
			if((xx<0)||(xx>=gameTable.tabSize))
				return;
			int yy=gameTable.tabSize;
			while(SetTop(yy,cellSize)>e.Y)	yy--;
			if((yy<0)||(yy>=gameTable.tabSize))
				return;
			item=gameTable.item(xx,yy);
			int num;
			int x=SetLeft(xx,cellSize)+cellSize*7/10;
			int y=SetTop(yy,cellSize)+cellSize*7/10;
			Point ptCell=((Control)sender).PointToScreen(new Point(x,y));
			if(e.Button==MouseButtons.Left) {
				num=ShowNumpad(ptCell,item,true);
			} else {
				num=ShowNumpad(ptCell,item,false);
			}
			if((num>=0)&&(item.actNum!=num)) {
				item.actNum=num;
				using(Graphics g=Graphics.FromImage(pictureTable.Image)) {
					DrawCell(g,br,cellSize,gameTable.item(xx,yy));
				}
				pictureTable.Refresh();
			}
		}
		int ShowNumpad(Point loc,GameItem item,bool all) {
			//	all	=true  - left button
			//		=false - right button
			NumPad pad=new NumPad(item,actGameDef,all);
			pad.Location=loc;
			pad.ShowDialog();
			item.actFlag=pad.numMask;
			return pad.numButton;	//	-1-Abort, 0-Delete, n-num
		}
		#endregion
		#region Save file handling Load/Save/Add
		//======================================
		//	Save file handling
		//======================================
		private void buttonAddGame(){
			string oLine=string.Format("[{0}]\t={1}{2}x{3}\t	#{4};\t{5}",comboGameName.Text,comboGameType.Text,textGameComment.Text);
			GameFile.AddGame(0,oLine);
			int ch0=(gameTable.tabSize>9)?0x40:0x30;
			for(int ii=0; ii<gameTable.tabSize; ii++) {
				oLine="";
				for(int jj=0; jj<gameTable.tabSize; jj++) {
					int num=gameTable.item(jj,ii).actNum;
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
			int gameIndex=GameFile.GetGameIndex((string)comboGameName.Items[comboGameName.SelectedIndex]);
			GameParams pars=GameFile.GetGameParameters(gameIndex);
			comboGameType.Text=string.Format("={0}{1}*{1}\t({2}*{3})",(pars.diag!=0)?"X":"",pars.x*pars.y,pars.x,pars.y);
			numericLevel.Value=pars.level;
			textGameComment.Text=pars.comment;
			if((actGameDef.xCells!=pars.x)||(actGameDef.yCells!=pars.y)||(actGameDef.xCross!=pars.diag)) {
				actGameDef=new GameDef(pars.x,pars.y,pars.diag,comboGameType.Text);
				gameTable.InitTable(pars.x,pars.y);
				//pictureTable_Resize(null,null);
			}
			for(int yy=0; yy<pars.size; yy++) {
				string rowLine=GameFile.GetGameRow(gameIndex,yy+1);
				gameTable.FillTableRow(yy,rowLine);
			}
			gameTable.CheckTable();
			pictureTable_Resize(null,null);
			int errnum=gameTable.CheckTable();
		}
		private void buttonFillGame_Click(object sender,EventArgs e) {
			//GameTable newTable=(GameTable)gameTable.DeepClone(gameTable);
			GameTable newTable=gameTable.DeepClone<GameTable>(gameTable);
			GameQueueAdd();
			MessageBox.Show("Ez még nincs kész!!","Kitöltés",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
		}
		#endregion
		static void GameQueueAdd() {
			if(gameQueue==null)
				gameQueue=new List<GameTable>();
			gameQueue.Add(gameTable.DeepClone<GameTable>(gameTable));
		}
	}
}
