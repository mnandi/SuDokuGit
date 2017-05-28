using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuDoku {
	class SuCheck {
		static public List<int[]> CheckValues(GameItem item) {
			GameDef def=SuDokuForm.actGameDef;
			int errNb=0;
			long flag=0;
			long bits;
			int num;
			List<int[]> doubles=new List<int[]>();
			//	Own item
			num=item.actNum;
			if(num>0) {
				bits=((long)1<<num);
				flag|=bits;
			}
			//	Test row
			for(int xx=0; xx<SuDokuForm.tableSize; xx++) {
				if(item.px==xx)
					continue;	//	exclude own cell
				num=SuDokuForm.gameTable[xx,item.py].actNum;
				if(num<=0)
					continue;
				bits=((long)1<<num);
				if((item.actFlag&bits)!=0) {
					errNb+=AddDouble(xx,item.py,doubles);
				}else{
					flag|=bits;
				}
			}
			//	Test column
			for(int yy=0; yy<SuDokuForm.tableSize; yy++) {
				if(item.py==yy)
					continue;	//	exclude own cell
				num=SuDokuForm.gameTable[item.px,yy].actNum;
				if(num<=0)
					continue;
				bits=((long)1<<num);
				if((item.actFlag&bits)!=0) {
					errNb+=AddDouble(item.px,yy,doubles);
				} else {
					flag|=bits;
				}
			}
			//	Test diagonals
			if(def.xCross>0) {
				if(item.px==item.py) {
					//	if item in top-left - bottom-right diagonal
					for(int ii=0; ii<SuDokuForm.tableSize; ii++) {
						if(item.px==ii)
							continue;	//	exclude own cell
						num=SuDokuForm.gameTable[ii,ii].actNum;
						if(num<=0)
							continue;
						bits=((long)1<<num);
						if((item.actFlag&bits)!=0) {
							errNb+=AddDouble(ii,ii,doubles);
						} else {
							flag|=bits;
						}
					}
				}
				if(item.px==(SuDokuForm.tableSize-1-item.py)) {
					//	if item in bottom-left - top-right diagonal
					for(int ii=0; ii<SuDokuForm.tableSize; ii++) {
						if(item.py==ii)
							continue;	//	exclude own cell
						num=SuDokuForm.gameTable[ii,SuDokuForm.tableSize-1-ii].actNum;
						if(num<=0)
							continue;
						bits=((long)1<<num);
						if((item.actFlag&bits)!=0) {
							errNb+=AddDouble(item.px,ii,doubles);
						} else {
							flag|=bits;
						}
					}
				}
			}
			//	Test group
			for(int yy=0; yy<def.yCells; yy++) {
				int y=(item.py/def.yCells*def.yCells)+yy;
				for(int xx=0; xx<def.xCells; xx++) {
					int x=(item.px/def.xCells*def.xCells)+xx;
					if((item.px==x)&&(item.py==y))
						continue;	//	exclude own cell
					num=SuDokuForm.gameTable[x,y].actNum;
					if(num<=0)
						continue;
					bits=((long)1<<num);
					if((item.actFlag&bits)!=0) {
						errNb+=AddDouble(x,y,doubles);
					} else {
						flag|=bits;
					}
				}
			}
			item.vFlag=flag;
			return doubles;
		}
		static int AddDouble(int x,int y,List<int[]> dbl) {
			int ix=dbl.FindIndex(e => ((e[0]==x)&&(e[1]==y)));
			if(ix<0) {
				//	item not in list
				dbl.Add(new int[] { x,y });
				return 1;
			}
			return 0;
		}
	}
}
