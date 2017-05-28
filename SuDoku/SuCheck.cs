using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuDoku {
	class SuCheck {
		#region Checking
		static public List<int[]> CheckValues(GameItem item) {
			GameDef def=SuDokuForm.actGameDef;
			int flag=0;
			int num;
			List<int[]> doubles=new List<int[]>();
			//	Own item
			num=item.actNum;
			if(num>0) {
				int bits=(1<<num);
				flag|=bits;
			}
			#region Check rows & columns
			//	Test row
			for(int xx=0; xx<SuDokuForm.gameTable.tabSize; xx++) {
				if(item.cellX==xx)
					continue;	//	exclude own cell
				SetFlag(ref flag,xx,item.cellY,doubles);
			}
			//	Test column
			for(int yy=0; yy<SuDokuForm.gameTable.tabSize; yy++) {
				if(item.cellY==yy)
					continue;	//	exclude own cell
				SetFlag(ref flag,item.cellX,yy,doubles);
			}
			#endregion

			#region Check diagonals
			//	Test diagonals
			if(def.xCross==(int)GameType.DIAGGAME) {
				if(item.cellX==item.cellY) {
					//	if item in top-left - bottom-right diagonal
					for(int ii=0; ii<SuDokuForm.gameTable.tabSize; ii++) {
						if(item.cellX==ii)
							continue;	//	exclude own cell
						SetFlag(ref flag,ii,ii,doubles);
					}
				}
				if(item.cellX==(SuDokuForm.gameTable.tabSize-1-item.cellY)) {
					//	if item in bottom-left - top-right diagonal
					for(int ii=0; ii<SuDokuForm.gameTable.tabSize; ii++) {
						if(item.cellX==ii)
							continue;	//	exclude own cell
						SetFlag(ref flag,ii,SuDokuForm.gameTable.tabSize-1-ii,doubles);
					}
				}
			}
			#endregion

			#region Test cell group
			//	Test group
			for(int yy=0; yy<def.yCells; yy++) {
				int y=(item.cellY/def.yCells*def.yCells)+yy;
				for(int xx=0; xx<def.xCells; xx++) {
					int x=(item.cellX/def.xCells*def.xCells)+xx;
					if((item.cellX==x)&&(item.cellY==y))
						continue;	//	exclude own cell
					SetFlag(ref flag,x,y,doubles);
				}
			#endregion
			}
			item.vFlag=flag;
			item.occNum=CountOccnum(flag);
			return doubles;
		}

		static void SetFlag(ref int flag,int x,int y,List<int[]> doubles) {
			int num=SuDokuForm.gameTable.item(x,y).actNum;
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
				//	item not in list
				dbl.Add(new int[] { x,y });
				return 1;
			}
			return 0;
		}
		#endregion

		#region Steppinging
		static public List<int[]> StepValue(GameItem item) {
			GameDef def=SuDokuForm.actGameDef;
			List<int[]> doubles=new List<int[]>();
			//	Own item
			int num=item.actNum;
			//	Num must greather than 0
			int flag=item.actFlag;

			#region Check rows & columns
			//	Test row
			for(int xx=0; xx<SuDokuForm.gameTable.tabSize; xx++) {
				if(item.cellX==xx)
					continue;	//	exclude own cell
				bool retOK=SetValue(flag,xx,item.cellY);
			}
			//	Test column
			for(int yy=0; yy<SuDokuForm.gameTable.tabSize; yy++) {
				if(item.cellY==yy)
					continue;	//	exclude own cell
				bool retOK=SetValue(flag,item.cellX,yy);
			}
			#endregion

			#region Check diagonals
			//	Test diagonals
			if(def.xCross==(int)GameType.DIAGGAME) {
				if(item.cellX==item.cellY) {
					//	if item in top-left - bottom-right diagonal
					for(int ii=0; ii<SuDokuForm.gameTable.tabSize; ii++) {
						if(item.cellX==ii)
							continue;	//	exclude own cell
						bool retOK=SetValue(flag,ii,ii);
					}
				}
				if(item.cellX==(SuDokuForm.gameTable.tabSize-1-item.cellY)) {
					//	if item in bottom-left - top-right diagonal
					for(int ii=0; ii<SuDokuForm.gameTable.tabSize; ii++) {
						if(item.cellX==ii)
							continue;	//	exclude own cell
						bool retOK=SetValue(flag,ii,SuDokuForm.gameTable.tabSize-1-ii);
					}
				}
			}
			#endregion

			#region Test cell group
			//	Test group
			for(int yy=0; yy<def.yCells; yy++) {
				int y=(item.cellY/def.yCells*def.yCells)+yy;
				for(int xx=0; xx<def.xCells; xx++) {
					int x=(item.cellX/def.xCells*def.xCells)+xx;
					if((item.cellX==x)&&(item.cellY==y))
						continue;	//	exclude own cell
					bool retOK=SetValue(flag,x,y);
				}
			#endregion
			}
			item.vFlag=flag;
			return doubles;
		}

		static bool SetValue(int flag,int x,int y) {
			int vFlag=SuDokuForm.gameTable.item(x,y).vFlag;
			if((vFlag&flag)!=0)
				return false;
			SuDokuForm.gameTable.item(x,y).vFlag|=flag;
			SuDokuForm.gameTable.item(x,y).occNum=CountOccnum(vFlag|flag);
			return true;
		}
		static int CountOccnum(int flag) {
			int count=0;
			while(flag!=0) {
				flag>>=1;		//	0.bit must 0
				if((flag&1)!=0)
					count++;
			}
			return count;
		}
		#endregion
	}
}
