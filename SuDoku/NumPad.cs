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
		GameItem item;
		public int numMask=0;
		public int numButton=-1;
		public NumPad(GameItem itm,GameDef def,bool mode) {
			//	all	=true  - left button	- view all num
			//		=false - right button	- view available nums
			InitializeComponent();

			item=itm;
			List<int[]> errList=SuCheck.CheckValues(item);
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
						if((item.vFlag&(1<<(num+1)))!=0)
							butt.Enabled=false;
					}
					butt.Click+=new System.EventHandler(this.button_Click);
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
			if(numButton>=0) {
				if(numButton==0) {
					numMask=0;
				} else {
					numMask=(1<<numButton);
				}
				List<int[]> errList=SuCheck.CheckValues(item);
			}
		}
	}
}
