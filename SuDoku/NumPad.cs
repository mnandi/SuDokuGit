using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SuDoku {
	public partial class NumPad: Form {
		static int bas;
		static int siz;
		GameCell cell;
		public int numMask=0;
		public int numButton=-1;
		public NumPad(GameCell cll,GameDef def,bool mode) {
			//	all	=true  - left button	- view all num
			//		=false - right button	- view available nums
			InitializeComponent();

			cell=cll;
			siz=def.xCells*def.yCells;
			bas=(siz>9)?Constants.chrBase:Constants.numBase;	//	show '1' if table <= 3x3 else 'A'

			CellResult rerr=SuDokuForm.gameTable.CountCellFlag(cell);
			int nC=Math.Max(def.xCells,def.yCells);
			int nR=Math.Min(def.xCells,def.yCells);

			const int offs=2;
			int buttSiz=30;
			this.Size=new Size(buttSiz*nC+offs,buttSiz*(nR+1)+offs);
			Button[,] butts=new Button[nC,nR];
			Button butt;
			for(int yy=0;yy<nR;yy++){
				for(int xx=0;xx<nC;xx++){
					butt=new Button();
					butt.Size=new Size(buttSiz-offs,buttSiz-offs);
					butt.Location=new Point(xx*buttSiz+offs,yy*buttSiz+offs);
					int num=yy*nC+xx;
					butt.Text=((char)(num+((SuDokuForm.gameTable.tabSize>=10)?0x41:0x31))).ToString();
					butt.Tag=num+1;
					if(!mode) {
						if((rerr.cellnums&(1<<(num)))!=0)
							butt.Enabled=false;
					}
					butt.Click+=new System.EventHandler(this.button_Click);
					butt.KeyPress+=new KeyPressEventHandler(this.NumPad_KeyPress);
					butt.DialogResult=DialogResult.OK;
					this.Controls.Add(butt);
				}
			}
			//	Cancel
			butt=new Button();
			butt.Size=new Size(buttSiz*(nC-1)-offs,buttSiz-offs);
			butt.Location=new Point(offs,nR*buttSiz+offs);
			butt.Text="- -";
			butt.Click+=new System.EventHandler(this.button_Click);
			butt.DialogResult=DialogResult.Cancel;
			butt.Tag=(int)0;
			this.Controls.Add(butt);
			//	Abort
			butt=new Button();
			butt.Size=new Size(buttSiz-offs,buttSiz-offs);
			butt.Location=new Point((nC-1)*buttSiz+offs,nR*buttSiz+offs);
			butt.Text="x";
			butt.DialogResult=DialogResult.Abort;
			butt.Click+=new System.EventHandler(this.button_Click);
			butt.Tag=(int)(-1);
			this.Controls.Add(butt);
		}
		private void button_Click(object sender,EventArgs e) {
			numButton=(int)(((Button)sender).Tag);
			switch(numButton) {
				case -1:
					numMask=-1;
					break;
				case 0:
					numMask=0;
					break;
				default:
					numMask=(1<<(numButton-1));
					break;
			}
			CellResult rerr=SuDokuForm.gameTable.CountCellFlag(cell);
		}

		private void NumPad_KeyPress(object sender,KeyPressEventArgs e) {
			int chrnum=e.KeyChar;
			numButton=0;
			numMask=0;
			if(chrnum==0x1B){
				//	Escape
				numMask=-1;
			} else if(chrnum==' '){
			} else {
				if(siz>9) {
					chrnum&=0xDF;
					if((chrnum>='A')&&(chrnum<('A'+siz))) {
						numButton=chrnum-0x40;
						numMask=(1<<(numButton-1));
					} else {
						return;
					}
				} else {
					if((chrnum>='1')&&(chrnum<('1'+siz))) {
						numButton=chrnum-0x30;
						numMask=(1<<(numButton-1));
					} else {
						return;
					}
				}
			}
			this.Close();
		}
	}
}
