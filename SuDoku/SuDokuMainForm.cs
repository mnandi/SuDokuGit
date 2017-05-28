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

namespace SuDoku {
	delegate void ShowTimerDelegate();

	public partial class SuDokuForm: Form {
		#region Common data
		const int cellOffs=2;
		const int groupOffs=2;
		static readonly Color pictureColor=Color.White;
		static readonly Color borderColor=Color.Black;
		static readonly Color textColor=Color.Blue;
		static readonly Color backgr1Color=Color.PaleGoldenrod;
		static readonly Color backgr2Color=Color.PaleGoldenrod;
		static public GameDef actGame=null;
		static public GameItem[,] gameTable;
		static SolidBrush br=new SolidBrush(borderColor);	//	Draw rectangle border
		static SolidBrush bf=new SolidBrush(pictureColor);	//	Clear picture
		static SolidBrush bn=new SolidBrush(textColor);		//	Item text color
		static Font numFont;
		static int baseX=0;
		static int baseY=0;
		static int cellSize;
		static public int tableSize;
		static bool gameState=false;
		static int gameCounter=0;
		static System.Timers.Timer gameTimer;
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

		private void buttonGame_Click(object sender,EventArgs e) {
			if(!gameState) {
				buttonGame.Text="Játék leállítása";
			} else {
				buttonGame.Text="--> Játék indítása";
			}
			gameState=!gameState;
		}
		#endregion
		#region Timer
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
		#region Game table handling
		private void comboGameType_SelectedIndexChanged(object sender,EventArgs e) {
			actGame=Constants.gameDefTb[comboGameType.SelectedIndex];
			InitSuDokuTable(actGame);

		}
		void InitSuDokuTable(GameDef game){
			gameTable=new GameItem[game.xCells*game.yCells,game.xCells*game.yCells];
			for(int xx=0; xx<game.xCells*game.yCells; xx++) {
				for(int yy=0; yy<game.xCells*game.yCells; yy++) {
					gameTable[xx,yy]=new GameItem(xx,yy);
				}
			}
			pictureTable_Resize(null,null);
		}
		void RedrawTable() {
			tableSize=actGame.xCells*actGame.yCells;
			int cellSizX=pictureTable.Width-((tableSize+1)*cellOffs+(actGame.yCells-1)*groupOffs);
			int cellSizY=pictureTable.Height-((tableSize+1)*cellOffs+(actGame.xCells-1)*groupOffs);
			cellSize=Math.Min(cellSizX,cellSizY)/tableSize;
			//cellSize=(Math.Min(pictureTable.Width,pictureTable.Height)-3*tableSize)/tableSize;
			int cellsSiz=tableSize*cellSize;
			baseX=baseY=0;
			baseX=(pictureTable.Width-SetLeft(tableSize,cellSize))/2;
			baseY=(pictureTable.Height-SetTop(tableSize,cellSize))/2;
			if(pictureTable.Image==null)
				pictureTable.Image=new Bitmap(pictureTable.Width,pictureTable.Height);	//	bitmap of table
			numFont=new Font("Arial",cellSize,FontStyle.Bold,GraphicsUnit.Pixel);
			using(Graphics g=Graphics.FromImage(pictureTable.Image)) {
				for(int xx=0; xx<actGame.xCells*actGame.yCells; xx++) {
					for(int yy=0; yy<actGame.xCells*actGame.yCells; yy++) {
						DrawCell(g,br,cellSize,gameTable[xx,yy]);
					}
				}
			}
		}
		int SetLeft(int xp,int siz) {
			return baseX+xp*(siz+cellOffs)+(xp/actGame.xCells)*groupOffs;
		}
		int SetTop(int yp,int siz) {
			return baseY+yp*(siz+cellOffs)+(yp/actGame.yCells)*groupOffs;
		}
		void DrawCell(Graphics g,Brush br,int size,GameItem item) {
			Rectangle rect=new Rectangle(SetLeft(item.px,size),SetTop(item.py,size),size,size);
			g.FillRectangle(bf,rect);
			g.DrawRectangle(new Pen(br),rect);
			if(item.actNum>0) {
				string chstr=((char)(((tableSize>9)?0x40:0x30)+item.actNum)).ToString();
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
			int xx=tableSize;
			while(SetLeft(xx,cellSize)>e.X)	xx--;
			if((xx<0)||(xx>=tableSize))
				return;
			int yy=tableSize;
			while(SetTop(yy,cellSize)>e.Y)	yy--;
			if((yy<0)||(yy>=tableSize))
				return;
			item=gameTable[xx,yy];
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
					DrawCell(g,br,cellSize,gameTable[xx,yy]);
				}
				pictureTable.Refresh();
			}
		}
		int ShowNumpad(Point loc,GameItem item,bool all) {
			NumPad pad=new NumPad(item,actGame,all);
			pad.Location=loc;
			pad.ShowDialog();
			item.actFlag=pad.numMask;
			return pad.numButton;	//	-1-Abort, 0-Delete, n-num
		}
		#endregion
		#region Save file handling
		//======================================
		//	Save file handling
		//======================================
		private void buttonSave_Click(object sender,EventArgs e) {
			OpenFileDialog saveFileDlg=new OpenFileDialog();
			saveFileDlg.InitialDirectory=".\\";
			saveFileDlg.Filter="txt files (*.txt)|*.txt|All files (*.*)|*.*";
			saveFileDlg.FilterIndex=2;
			saveFileDlg.RestoreDirectory=true;
			if(saveFileDlg.ShowDialog()==DialogResult.OK) {
				using(StreamWriter oStream=File.AppendText(saveFileDlg.FileName)) {
					try {
						string oLine=string.Format("[{0}]\t={1};\t{2}",textGameName.Text,comboGameType.Text,textGameComment.Text);
						oStream.WriteLine(oLine);
						int ch0=(tableSize>9)?0x40:0x30;
						for(int ii=0; ii<tableSize; ii++) {
							oLine="";
							for(int jj=0; jj<tableSize; jj++) {
								int num=gameTable[jj,ii].actNum;
								oLine+=((char)((num>0)?(num+ch0):0x20)).ToString();
							}
							oStream.WriteLine(oLine);
						}
						oStream.WriteLine("*");
						oStream.Flush();
					} catch(Exception ex) {
						MessageBox.Show("Error: Could not read file from disk. Original error: "+ex.Message);
					}
				}
			}

		}

		private void buttonLoad_Click(object sender,EventArgs e) {
			OpenFileDialog loadFileDlg=new OpenFileDialog();
			loadFileDlg.InitialDirectory=".\\";
			loadFileDlg.Filter="txt files (*.txt)|*.txt|All files (*.*)|*.*";
			loadFileDlg.FilterIndex=2;
			loadFileDlg.RestoreDirectory=true;
			if(loadFileDlg.ShowDialog()==DialogResult.OK) {
				using(StreamReader iStream=new StreamReader(loadFileDlg.FileName,true)) {
					try {
						string iLine;
						bool iFlag=false;
						while((iLine=iStream.ReadLine())!=null) {
							if(string.IsNullOrWhiteSpace(iLine))
								continue;
							if(iLine[0]=='*') {
								iFlag=false;
								continue;
							}
							if(iLine[0]=='[') {
								iFlag=true;
								continue;
							}
							if(!iFlag)
								continue;
						}
					} catch(Exception ex) {
						MessageBox.Show("Error: Could not read file from disk. Original error: "+ex.Message);
					}
				}
			}
		}
		#endregion
	}
}
