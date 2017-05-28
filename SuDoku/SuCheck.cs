using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuDoku {
	class SuCheck {
		static public List<int[]> CheckValues(GameItem item) {
			GameDef def=SuDokuForm.actGameDef;
			int errNb=0;
			int flag=0;
			int bits;
			int num;
			List<int[]> doubles=new List<int[]>();
			//	Own item
			num=item.actNum;
			if(num>0) {
				bits=(1<<num);
				flag|=bits;
			}
			#region Check rows & columns
			//	Test row
			for(int xx=0; xx<SuDokuForm.tableSize; xx++) {
				if(item.cx==xx)
					continue;	//	exclude own cell
				num=SuDokuForm.gameTable.item(xx,item.cy).actNum;
				if(num<=0)
					continue;
				bits=(1<<num);
				if((item.actFlag&bits)!=0) {
					errNb+=AddDouble(xx,item.cy,doubles);
				}else{
					flag|=bits;
				}
			}
			//	Test column
			for(int yy=0; yy<SuDokuForm.tableSize; yy++) {
				if(item.cy==yy)
					continue;	//	exclude own cell
				num=SuDokuForm.gameTable.item(item.cx,yy).actNum;
				if(num<=0)
					continue;
				bits=(1<<num);
				if((item.actFlag&bits)!=0) {
					errNb+=AddDouble(item.cx,yy,doubles);
				} else {
					flag|=bits;
				}
			}
			#endregion

			#region Check diagonals
			//	Test diagonals
			if(def.xCross==(int)GameType.DIAGGAME) {
				if(item.cx==item.cy) {
					//	if item in top-left - bottom-right diagonal
					for(int ii=0; ii<SuDokuForm.tableSize; ii++) {
						if(item.cx==ii)
							continue;	//	exclude own cell
						num=SuDokuForm.gameTable.item(ii,ii).actNum;
						if(num<=0)
							continue;
						bits=(1<<num);
						if((item.actFlag&bits)!=0) {
							errNb+=AddDouble(ii,ii,doubles);
						} else {
							flag|=bits;
						}
					}
				}
				if(item.cx==(SuDokuForm.tableSize-1-item.cy)) {
					//	if item in bottom-left - top-right diagonal
					for(int ii=0; ii<SuDokuForm.tableSize; ii++) {
						if(item.cy==ii)
							continue;	//	exclude own cell
						num=SuDokuForm.gameTable.item(ii,SuDokuForm.tableSize-1-ii).actNum;
						if(num<=0)
							continue;
						bits=(1<<num);
						if((item.actFlag&bits)!=0) {
							errNb+=AddDouble(item.cx,ii,doubles);
						} else {
							flag|=bits;
						}
					}
				}
			}
			#endregion

			#region Test cell group
			//	Test group
			for(int yy=0; yy<def.yCells; yy++) {
				int y=(item.cy/def.yCells*def.yCells)+yy;
				for(int xx=0; xx<def.xCells; xx++) {
					int x=(item.cx/def.xCells*def.xCells)+xx;
					if((item.cx==x)&&(item.cy==y))
						continue;	//	exclude own cell
					num=SuDokuForm.gameTable.item(x,y).actNum;
					if(num<=0)
						continue;
					bits=(1<<num);
					if((item.actFlag&bits)!=0) {
						errNb+=AddDouble(x,y,doubles);
					} else {
						flag|=bits;
					}
				}
			#endregion
			}
			item.vFlag=flag;
			return doubles;
		}

		#region internal routines
		static int AddDouble(int x,int y,List<int[]> dbl) {
			int ix=dbl.FindIndex(e => ((e[0]==x)&&(e[1]==y)));
			if(ix<0) {
				//	item not in list
				dbl.Add(new int[] { x,y });
				return 1;
			}
			return 0;
		}
		#endregion
	}
}
