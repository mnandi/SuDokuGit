using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuDoku {
	class GameCheck {
		#region Checking
		static public List<int[]> CheckValues(GameCell cell) {
			GameDef def=SuDokuForm.actGameDef;
			int flag=0;
			int num;
			List<int[]> doubles=new List<int[]>();
			//	Own cell
			num=cell.fixNum;
			if(num>0) {
				int bits=(1<<num);
				flag|=bits;
			}
			#region Check rows & columns
			//	Test row
			for(int xx=0; xx<SuDokuForm.gameTable.tabSize; xx++) {
				if(cell.cellX==xx)
					continue;	//	exclude own cell
				SetFlag(ref flag,xx,cell.cellY,doubles);
			}
			//	Test column
			for(int yy=0; yy<SuDokuForm.gameTable.tabSize; yy++) {
				if(cell.cellY==yy)
					continue;	//	exclude own cell
				SetFlag(ref flag,cell.cellX,yy,doubles);
			}
			#endregion

			#region Check diagonals
			//	Test diagonals
			if(def.xCross==(int)GameType.DIAGGAME) {
				if(cell.cellX==cell.cellY) {
					//	if cell in top-left - bottom-right diagonal
					for(int ii=0; ii<SuDokuForm.gameTable.tabSize; ii++) {
						if(cell.cellX==ii)
							continue;	//	exclude own cell
						SetFlag(ref flag,ii,ii,doubles);
					}
				}
				if(cell.cellX==(SuDokuForm.gameTable.tabSize-1-cell.cellY)) {
					//	if cell in bottom-left - top-right diagonal
					for(int ii=0; ii<SuDokuForm.gameTable.tabSize; ii++) {
						if(cell.cellX==ii)
							continue;	//	exclude own cell
						SetFlag(ref flag,ii,SuDokuForm.gameTable.tabSize-1-ii,doubles);
					}
				}
			}
			#endregion

			#region Test cell group
			//	Test group
			for(int yy=0; yy<def.yCells; yy++) {
				int y=(cell.cellY/def.yCells*def.yCells)+yy;
				for(int xx=0; xx<def.xCells; xx++) {
					int x=(cell.cellX/def.xCells*def.xCells)+xx;
					if((cell.cellX==x)&&(cell.cellY==y))
						continue;	//	exclude own cell
					SetFlag(ref flag,x,y,doubles);
				}
			#endregion
			}
			cell.vFlag=flag;
			return doubles;
		}

		static void SetFlag(ref int flag,int x,int y,List<int[]> doubles) {
			int num=SuDokuForm.gameTable.cell(x,y).fixNum;
			if(num>0){
				int bits=(1<<num);
				if((flag&bits)!=0) {
				//errNb+=AddDouble(x,y,doubles);
					AddDouble(x,y,doubles);
				} else {
					flag|=bits;
				}
			}
		}
		#endregion

		#region internal routines
		static int AddDouble(int x,int y,List<int[]> dbl) {
			int ix=dbl.FindIndex(e => ((e[0]==x)&&(e[1]==y)));
			if(ix<0) {
				//	cell not in list
				dbl.Add(new int[] { x,y });
				return 1;
			}
			return 0;
		}
		#endregion

		#region Steppinging
		//static public List<int[]> StepValue(GameCell cell) {
		//    GameDef def=SuDokuForm.actGameDef;
		//    List<int[]> doubles=new List<int[]>();
		//    //	Own cell
		//    int num=cell.fixNum;
		//    //	Num must greather than 0
		//    int flag=cell.fixnum;

		//    #region Check rows & columns
		//    //	Test row
		//    for(int xx=0; xx<SuDokuForm.gameTable.tabSize; xx++) {
		//        if(cell.cellX==xx)
		//            continue;	//	exclude own cell
		//        bool retOK=SetValue(flag,xx,cell.cellY);
		//    }
		//    //	Test column
		//    for(int yy=0; yy<SuDokuForm.gameTable.tabSize; yy++) {
		//        if(cell.cellY==yy)
		//            continue;	//	exclude own cell
		//        bool retOK=SetValue(flag,cell.cellX,yy);
		//    }
		//    #endregion

		//    #region Check diagonals
		//    //	Test diagonals
		//    if(def.xCross==(int)GameType.DIAGGAME) {
		//        if(cell.cellX==cell.cellY) {
		//            //	if cell in top-left - bottom-right diagonal
		//            for(int ii=0; ii<SuDokuForm.gameTable.tabSize; ii++) {
		//                if(cell.cellX==ii)
		//                    continue;	//	exclude own cell
		//                bool retOK=SetValue(flag,ii,ii);
		//            }
		//        }
		//        if(cell.cellX==(SuDokuForm.gameTable.tabSize-1-cell.cellY)) {
		//            //	if cell in bottom-left - top-right diagonal
		//            for(int ii=0; ii<SuDokuForm.gameTable.tabSize; ii++) {
		//                if(cell.cellX==ii)
		//                    continue;	//	exclude own cell
		//                bool retOK=SetValue(flag,ii,SuDokuForm.gameTable.tabSize-1-ii);
		//            }
		//        }
		//    }
		//    #endregion

		//    #region Test cell group
		//    //	Test group
		//    for(int yy=0; yy<def.yCells; yy++) {
		//        int y=(cell.cellY/def.yCells*def.yCells)+yy;
		//        for(int xx=0; xx<def.xCells; xx++) {
		//            int x=(cell.cellX/def.xCells*def.xCells)+xx;
		//            if((cell.cellX==x)&&(cell.cellY==y))
		//                continue;	//	exclude own cell
		//            bool retOK=SetValue(flag,x,y);
		//        }
		//    #endregion
		//    }
		//    //cell.vFlag=flag;
		//    return doubles;
		//}

		//static bool SetValue(int flag,int x,int y) {
		//    //int vFlag=SuDokuForm.gameTable.cell(x,y).vFlag;
		//    if((vFlag&flag)!=0)
		//        return false;
		//    //SuDokuForm.gameTable.cell(x,y).vFlag|=flag;
		//    return true;
		//}
		//static int CountOccnum(int flag) {
		//    int count=0;
		//    while(flag!=0) {
		//        flag>>=1;		//	0.bit must 0
		//        if((flag&1)!=0)
		//            count++;
		//    }
		//    return count;
		//}
		#endregion
	}
}
