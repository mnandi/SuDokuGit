﻿#region Source File Description
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

using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;		//	language
using System.Reflection;
using System.Resources;
using System.Threading;
#endregion

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
		static public GameDef actGameDef=null;		//	Actual game definition
		static public GameTable gameTable=null;		//	Actual game table
		static public TableQueue tableQueue=null;	//	Previous game tables
		static public TableQueue resultList=null;	//	List of possible results
		static public List<int[]> diffList=null;	//	List of cells of multiple result differences ([ix,mask])
		static public List<int[]> buttonList;		//	List of game filled cells	([x,y,num])	
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
		static bool cancelFlag=false;
		#endregion
		#region Game initialization & gaming
		public SuDokuForm() {

			InitializeComponent();

			LanguageChange chlang=new LanguageChange();
			//chlang.ChangeLanguage("hu-HU");
			chlang.ChangeLanguage("en");
			chlang.ApplyLanguageToForm(this);

			//	Init game comboBox
			for(int ii=0; ii<Constants.gameDefTb.Length; ii++) {
				comboGameType.Items.Add(Constants.gameDefTb[ii].gTypeName);
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
				//	not gaming - check game
				int nerr=gameTable.CheckTable(true);
				if(nerr>0) {
					MessageBox.Show("A tábla hibás, nem megoldható\r\nJavítsa ki!","Hiba",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
				//	not gaming - start game
				buttonStartGame.Text="Játék leállítása";
				buttonClearGame.Text="Lépés törlése";
				comboGameType.Enabled=
				numericLevel.Enabled=
				comboGameName.Enabled=
				textGameComment.Enabled=false;
				buttonList=new List<int[]>();
				gameTable.ClearSelects();
			} else {
				//	gaming - stop game
				buttonStartGame.Text="--> Játék indítása";
				buttonClearGame.Text="Tábla törlése";
				comboGameType.Enabled=
				numericLevel.Enabled=
				comboGameName.Enabled=
				textGameComment.Enabled=true;
				//	remove gamed cells
				int count=buttonList.Count;
				for(int ii=0; ii<count; ii++) {
					gameTable.cell(buttonList[ii][0],buttonList[ii][1]).fixNum=0;
				}
				pictureTable_Resize(null,null);
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
		private int AnalyzeResult(solvetype type) {

			GameTable gameTableSave=gameTable.DeepClone(gameTable);
			sresult sret=SuSolve(type);
			//		-1	- no solution
			//		 0	- solved
			//		>0	- other solve error
			switch(sret) {
				case sresult.SOLVE_FEWDATA:
					MessageBox.Show("A tábla nincs megfelelően kitöltve\r\nTúl kevés kitöltött cellát tartalmaz!","Hiba",MessageBoxButtons.OK,MessageBoxIcon.Error);
					break;
				case sresult.SOLVE_IMPOSSIBLE:	//		"Lehetetlen megoldás"
					MessageBox.Show("A tábla nem megoldható","Hiba",MessageBoxButtons.OK,MessageBoxIcon.Error);
					break;
				case sresult.SOLVE_CANCELLED:
					MessageBox.Show("A művelet leállítva","Hiba",MessageBoxButtons.OK,MessageBoxIcon.Error);
					break;
				case sresult.SOLVE_OK:			//	-1	"Jó megoldás !"
					//	Returns:
					pictureTable_Resize(null,null);
					MessageBox.Show("A tábla megoldása\r\nBefejezem","Eredmény",MessageBoxButtons.OK,MessageBoxIcon.Information);
					break;
				case sresult.SOLVE_NORESULT:	//	 0	"Hiba, nincs megoldás"
					MessageBox.Show("A tábla nem oldható meg","Hiba",MessageBoxButtons.OK,MessageBoxIcon.Error);
					break;
				case sresult.SOLVE_ONERESULT:	//	1	"1 megoldás"
					MessageBox.Show("Egy megoldás van!","Eredmény",MessageBoxButtons.OK,MessageBoxIcon.Information);
					break;
				case sresult.SOLVE_MORERESULT:	//	>1	"Több megoldás"
					MessageBox.Show(string.Format("Több, legalább {0} megoldás is van!",(int)sret),"Eredmény",MessageBoxButtons.OK,MessageBoxIcon.Information);
					break;
				default:						//	>2	"Több megoldás"
					MessageBox.Show(string.Format("Több, {0} megoldás is van!",(int)sret),"Eredmény",MessageBoxButtons.OK,MessageBoxIcon.Information);
					break;
			}
			gameTable=gameTableSave;
			gameTableSave=null;
			gameTable.ClearSelects();
			pictureTable_Resize(null,null);
			return ((int)sret);
		}
		private void buttonCheckResolving_Click(object sender,EventArgs e) {
			int ret=AnalyzeResult(solvetype.TESTRESULTS);
			if(ret>1) {
				diffList=new List<int[]>();
				int size=gameTable.boardsize;
				int difnb=resultList.gameTableList.Count;
				for(int nn=0; nn<size; nn++) {
					GameTable firstTable=resultList.gameTableList[0];
					gameTable.cell(nn).canresmask=0;
					if(firstTable.cell(nn).orig==1)
						continue;
					int num=1<<(firstTable.cell(nn).fixNum-1);
					for(int jj=1; jj<difnb; jj++) {
						int numx=1<<((resultList.gameTableList[jj].cell(nn)).fixNum-1);
						num|=numx;
					}
					int numnb=CountBits(num);
					if(numnb<2)
						continue;
					gameTable.cell(nn).canresmask=num;
					diffList.Add(new int[]{nn,num});
				}
			}

		}
		private void buttonTestGame_Click(object sender,EventArgs e) {
			//	Test table
			if(gameState) {		//	false=not gaming / true=gaming
				//	here gaming
				int nErr=gameTable.CheckTable(false);
				if(nErr>0) {
					//	error message
				}
			} else {
				//	here not gaming, filling table
				int nErr=gameTable.CheckTable(true);
				pictureTable_Resize(null,null);
			}
		}
		private void buttonResolveTable_Click(object sender,EventArgs e) {
			AnalyzeResult(solvetype.MAKERESULT);
		}
		#endregion
		#region Game table drawing
		private void comboGameType_SelectedIndexChanged(object sender,EventArgs e) {
			actGameDef=Constants.gameDefTb[comboGameType.SelectedIndex];
			InitSuDokuTable(actGameDef);

		}
		void InitSuDokuTable(GameDef game) {
			gameTable=new GameTable(game.gxCells,game.gyCells);
			pictureTable_Resize(null,null);
		}
		void RedrawTable() {
			gameTable.xCells=actGameDef.gxCells;
			gameTable.yCells=actGameDef.gyCells;
			gameTable.diagFlag=actGameDef.gxCross;
			int cellSizX=pictureTable.Width-((gameTable.tabSize+1)*Constants.cellOffs+(actGameDef.gyCells-1)*Constants.groupOffs);
			int cellSizY=pictureTable.Height-((gameTable.tabSize+1)*Constants.cellOffs+(actGameDef.gxCells-1)*Constants.groupOffs);
			cellSize=Math.Min(cellSizX,cellSizY)/gameTable.tabSize;
			int cellsSiz=gameTable.tabSize*cellSize;
			baseX=baseY=0;
			baseX=(pictureTable.Width-SetLeft(gameTable.tabSize,cellSize))/2;
			baseY=(pictureTable.Height-SetTop(gameTable.tabSize,cellSize))/2;
			if(pictureTable.Image==null)
				pictureTable.Image=new Bitmap(pictureTable.Width,pictureTable.Height);	//	bitmap of table
			numFont=new Font("Arial",cellSize,FontStyle.Bold,GraphicsUnit.Pixel);
			using(Graphics g=Graphics.FromImage(pictureTable.Image)) {
				for(int yy=0; yy<actGameDef.gxCells*actGameDef.gyCells; yy++) {
					for(int xx=0; xx<actGameDef.gxCells*actGameDef.gyCells; xx++) {
						DrawCell(g,br,cellSize,gameTable.cell(xx,yy));
					}
				}
			}
		}
		int SetLeft(int xp,int siz) {
			return baseX+xp*(siz+Constants.cellOffs)+(xp/actGameDef.gxCells)*Constants.groupOffs;
		}
		int SetTop(int yp,int siz) {
			return baseY+yp*(siz+Constants.cellOffs)+(yp/actGameDef.gyCells)*Constants.groupOffs;
		}
		void DrawCell(Graphics g,Brush brx,int size,GameCell item) {
			Rectangle rect=new Rectangle(SetLeft(item.cellX,size),SetTop(item.cellY,size),size,size);
			Brush bb=((actGameDef.gxCross==(int)GameType.DIAGGAME)&&
					  ((item.cellX==item.cellY)||
					   (item.cellX==(actGameDef.gxCells*actGameDef.gyCells)-1-item.cellY)))?bd:bf;
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
			pictureTable.Image=new Bitmap(pictureTable.Width,pictureTable.Height);	//	bitmap of table
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
			gameTable.SetSelectedCell(xx,yy);
			item=gameTable.cell(xx,yy);
			int x=SetLeft(xx,cellSize)+cellSize*7/10;
			int y=SetTop(yy,cellSize)+cellSize*7/10;
			Point ptCell=((Control)sender).PointToScreen(new Point(x,y));
			int num;
			if(e.Button==MouseButtons.Left) {
				num=ShowNumpad(ptCell,item,true);
				pictureTable.Focus();
			} else {
				num=ShowNumpad(ptCell,item,false);
				pictureTable.Focus();
			}
			if(num>=0) {
				int nerr=0;
				if(!gameState) {		//	false=not gaming / true=gaming
					//	here not gaming
					item.orig=1;
					item.fixNum=num;
				} else {
					//	here gaming
					item.orig=0;
					buttonList.Add(new int[] { xx,yy,item.fixNum });
					item.fixNum=num;
				}
				gameTable.ClearTableErrors();
				nerr=gameTable.CheckTable(item.orig==1);
				pictureTable_Resize(null,null);
				using(Graphics g=Graphics.FromImage(pictureTable.Image)) {
					DrawCell(g,br,cellSize,gameTable.cell(xx,yy));
				}
				pictureTable.Refresh();
				if((gameState)&&(gameTable.EndTest()<0)){
					if(nerr>0) {
						MessageBox.Show("Hibás kitöltés!\r\nA tábla nincs jól megoldva!","Eredmény",MessageBoxButtons.OK,MessageBoxIcon.Error);
					} else {
						MessageBox.Show("Gratulálok!\r\nA tábla megoldva!","Eredmény",MessageBoxButtons.OK,MessageBoxIcon.Information);
					}
				}
			}
		}
		int ShowNumpad(Point loc,GameCell item,bool all) {
			//	all	=true  - left button
			//		=false - right button
			NumPad pad=new NumPad(item,actGameDef,gameState,all);
			pad.Location=loc;
			pad.ShowDialog();
			return pad.numButton;	//	-1-Abort, 0-Delete, n-num
		}
		#endregion
		#region Save file handling Load/Save/Add
		//======================================
		//	Save file handling
		//======================================
		//	Saving catual game
		private void buttonSaveGame_Click(object sender,EventArgs e) {
			string gameName=textGameName.Text;
			if(string.IsNullOrWhiteSpace(gameName)) {
				MessageBox.Show("A játék nevet ki kell tölteni!","Hiba",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			int gameIndex=GameFile.GetGameIndex(gameName);
			string oLine=string.Format("[{0}]\t={1}{2}*{3}\t({4}*{4}\t#{5}\t; {6})",
													textGameName.Text,
													(actGameDef.gxCross!=0)?"X":"",
													actGameDef.gxCells,
													actGameDef.gyCells,
													actGameDef.gtabSize,
													numericLevel.Value,
													textGameComment.Text);
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
			if(!gameState) {		//	false=not gaming / true=gaming
				gameTable.ClearTable();
				pictureTable_Resize(null,null);
			} else {
				//	Remove only lasts
				int count=buttonList.Count;
				if(count>0) {
					count--;
					gameTable.cell(buttonList[count][0],buttonList[count][1]).fixNum=buttonList[count][2];
					buttonList.RemoveAt(count);
					pictureTable_Resize(null,null);
				}
			}
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
			textGameName.Text=pars.pName;
			comboGameType.Text=string.Format("={0}{1}*{2}\t({3}*{3})",(pars.pDiag!=0)?"X":"",pars.pX,pars.pY,pars.pSize);
			numericLevel.Value=pars.pLevel;
			textGameComment.Text=pars.pComment;
			if((actGameDef.gxCells!=pars.pX)||(actGameDef.gyCells!=pars.pY)||(actGameDef.gxCross!=pars.pDiag)) {
				actGameDef=new GameDef(pars.pX,pars.pY,pars.pDiag,comboGameType.Text);
				gameTable.InitTable(pars.pX,pars.pY,pars.pDiag);
			}
			for(int yy=0; yy<pars.pSize; yy++) {
				string rowLine=GameFile.GetGameRow(gameIndex,yy);
				gameTable.FillTableRow(yy,((rowLine.Length>0)&&(rowLine[0]!='*'))?rowLine:"");
			}
			int nErr=gameTable.CheckTable(true);
			pictureTable_Resize(null,null);
			//int errnum=gameTable.CheckTable(true);
		}
		private void buttonFillGame_Click(object sender,EventArgs e) {
			MessageBox.Show("Ez még nincs kész!!","Kitöltés",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
		}
		#endregion

		#region XXXX
		private void buttonCancel_Click(object sender,EventArgs e) {
			cancelFlag=true;
		}

		protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e) {
			e.IsInputKey=true;
			base.OnPreviewKeyDown(e);
		}
    
		private void pictureTable_PreviewKeyDown(object sender,PreviewKeyDownEventArgs e) {
			switch(e.KeyCode){
				case Keys.Down:
				case Keys.Up:
				case Keys.Left:
				case Keys.Right:
				case Keys.NumPad0:
				case Keys.Enter:
				case Keys.Delete:
				case Keys.Space:
					e.IsInputKey=true;
					break;
				default:
					//if(e.KeyCode==Keys.A)
					e.IsInputKey=true;
					break;
			}
			e.IsInputKey=true;
		}

		private void SuDokuForm_KeyDown(object sender,KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.Down:
				case Keys.Up:
				case Keys.Left:
				case Keys.Right:
				case Keys.NumPad0:
				case Keys.Enter:
				case Keys.Delete:
				case Keys.Space:
					break;
				default:
					//if(e.KeyCode==Keys.A)
					break;
			}

		}

		private void comboLanguage_SelectedIndexChanged(object sender,EventArgs e) {
			int selx=comboLanguage.SelectedIndex;
			string langid="";
			switch(selx) {
				case 0:
					langid="hu-HU";
					break;
				case 1:
					langid="en";
					break;
			}
			if(!string.IsNullOrEmpty(langid)) {
				LanguageChange chlang=new LanguageChange();
				chlang.ChangeLanguage(langid);
				chlang.ApplyLanguageToForm(this);
			}
		}
		#endregion
	}
}
