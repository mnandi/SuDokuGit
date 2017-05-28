using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuDoku {
	class SuSolve {
		static public int CheckValues(GameItem item) {
			GameDef def=SuDokuForm.actGame;
			int errNb=0;
			long flag=0;
			long bits;
			int ii;
			int jj;
			int num;
			//	Test row
			for(ii=0; ii<SuDokuForm.tableSize; ii++) {
				if(item.px==ii)
					continue;
				num=SuDokuForm.gameTable[ii,item.py].actNum;
				if(num<=0)
					continue;
				bits=((long)1<<num);
				if((flag&bits)!=0)
					errNb++;
				flag|=bits;
			}
			//	Test column
			for(ii=0; ii<SuDokuForm.tableSize; ii++) {
				if(item.py==ii)
					continue;
				num=SuDokuForm.gameTable[item.px,ii].actNum;
				if(num<=0)
					continue;
				bits=((long)1<<num);
				if((flag&bits)!=0)
					errNb++;
				flag|=bits;
			}
			//	Test diagonal
			if(def.xCross>0) {
				if(item.px==item.py) {
					for(ii=0; ii<SuDokuForm.tableSize; ii++) {
						num=SuDokuForm.gameTable[ii,ii].actNum;
						if(num<=0)
							continue;
						bits=((long)1<<num);
						if((flag&bits)!=0)
							errNb++;
						flag|=bits;
					}
				}
				if(item.px==(SuDokuForm.tableSize-1-item.py)) {
					for(ii=0; ii<SuDokuForm.tableSize; ii++) {
						num=SuDokuForm.gameTable[ii,SuDokuForm.tableSize-1-ii].actNum;
						if(num<=0)
							continue;
						bits=((long)1<<num);
						if((flag&bits)!=0)
							errNb++;
						flag|=bits;
					}
				}
			}
			//	Test group
			for(int yy=0; yy<def.yCells; yy++) {
				int y=(item.py/def.yCells*def.yCells)+yy;
				for(int xx=0; xx<def.xCells; xx++) {
					int x=(item.px/def.xCells*def.xCells)+xx;
					//if((item.px==x)&&(item.py==y))
					//	continue;
					num=SuDokuForm.gameTable[x,y].actNum;
					if(num<=0)
						continue;
					bits=((long)1<<num);
					if((flag&bits)!=0)
						errNb++;
					flag|=bits;
				}
			}
			item.vFlag=flag;
			return errNb;
		}
	}
}
