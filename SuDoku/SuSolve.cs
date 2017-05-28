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
	enum solvetype {
		MAKERESULT=-1,
		TESTRESULTS
	};
	enum sresult {
		SOLVE_CANCELLED=-3,
		SOLVE_IMPOSSIBLE=-2,//		"Lehetetlen megold�s"
		SOLVE_OK=-1,		//	-1	"J� megold�s !"
		SOLVE_NORESULT=0,	//	 0	"Hiba, nincs megold�s"
		SOLVE_ONERESULT,
		SOLVE_MORERESULT=15,
							//	>0	"A megold�sok sz�ma"
	};
	enum PHASE {
		PHcreate=0,	//	Create	game
		PHplay		//	Play	game
	};
	public class DataErrorException: Exception {
		int value;
		public DataErrorException(int val, string messg) : base(messg){
			value=val;
		}
	}
	public class CellResult {
		public int cellnums=0;
		public List<int[]> errList=new List<int[]>();
	}
	public partial class SuDokuForm {
		static readonly byte[] defnumnb={		//	Number of number list
		//	0 1 2 3 4 5 6 7 8 9 a b c d e f
			0,1,1,2,1,2,2,3,1,2,2,3,2,3,3,4,	//	00	 0
			1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,	//	10	+1
			1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,	//	20	+1
			2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,	//	30	+2
			1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,	//	40	+1
			2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,	//	50	+2
			2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,	//	60	+2
			3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,	//	70	+3
			1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,	//	80	+1
			2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,	//	90	+2
			2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,	//	A0	+2
			3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,	//	B0	+3
			2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,	//	C0	+2
			3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,	//	D0	+3
			3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,	//	E0	+3
			4,5,5,6,5,6,6,7,5,6,6,7,6,7,7,8		//	F0	+4
		//	0 1 2 3 4 5 6 7 8 9 a b c d e f
		};

		static int level=0;			//	Solving (iteration) level
		static int trynb=0;			//	Nbr of effective trying
		static int maxlev=0;		//	Maximal queue level

		sresult SuSolve(solvetype type) {
			//******************************************************
			//	Controlling the game solving
			//******************************************************
			//	Flg:	-1	- MAKERESULT	Solve game
			//			=0	- TESTRESULTS	Count solutions
			//	Returns:
			//		<0	- No solution
			//		=0	- Solution found
			//		>0	- More than one solution exist
			int nbresults=0;
			TableQueue resultList=new TableQueue();
			trynb=0;
			maxlev=0;
			cancelFlag=false;
			//	Solving table
			while(true) {
Recount: if(cancelFlag)
					return sresult.SOLVE_CANCELLED;
				int ret=SolveAll();
				//	ret	<0 - no solution
				//		=0 - no new fixed value
				//		>0 - new fixed value found
BackTry:		if(ret>=0) {
					//	Ellen�rizni hogy v�ge-e, vagy tov�bb kell sz�molni
					//if(EndTest()!=0) {
						//	=1 - all cells are fixed
					int eret=gameTable.EndTest();
					if(eret<0) {
						//	>=0 - there are fixable cells
						//	=-1 - all cells are fixed
						if(type==(int)solvetype.TESTRESULTS) {
						////    SUTAB* tmptab;
						////    if(nbresults==0) {
						////        while(pretab!=NULL) {
						////            tmptab=pretab->prevtab;
						////            free(pretab);
						////            pretab=tmptab;
						////        }
						////        nbresults++;
						////        pretab=(SUTAB*)malloc(sizeof(SUTAB));
						////        memcpy(pretab,psutab,sizeof(SUTAB));
						////        pretab->prevtab=NULL;
						////        if(psutab->prevtab==NULL) {
						////            //	No empty cell in game table
						////            break;
						////        }
						////        goto BackStep;
						////    } else {
						////        nbresults++;
						////        tmptab=pretab;
						////        pretab=(SUTAB*)malloc(sizeof(SUTAB));
						////        memcpy(pretab,psutab,sizeof(SUTAB));
						////        pretab->prevtab=tmptab;
						////        goto BackStep;
						////        //	return SOLVE_MORE;
						////    }
						////}
							if(nbresults==0) {
								int qSize=tableQueue.gameTableList.Count;
								if(qSize==0) {
									nbresults++;
									break;
								}
							}
#if DEBUG
#endif
							nbresults++;
							resultList.Push(gameTable);
							ret=-1;
							goto BackTry;
						} else {
							//	task: found solution
							//	step out from loop and return by OK
							break;
						}
					}
					if(ret>0)	//	Tal�lt megold�st, �jra sz�molni
						goto Recount;	//	continue;
				} else {
					//	<0 - here no solution
BackStep:			//	Hiba, visszal�pni majd k�vetkez� pr�b�t keresni
					//if(gameTable.prevtab!=NULL){
					if(tableQueue.Size>0) {
						//    SUTAB* wsutab=gameTable.prevtab;
						//    free(psutab);
						//    psutab=wsutab;
						gameTable=tableQueue.Pop();
						--level;
						textActLevel.Text=level.ToString();
						textActLevel.Refresh();
					} else {
						//	Itt a v�ge, nincs megold�s
						switch(nbresults) {
							case 0:
								return sresult.SOLVE_NORESULT;
							default:
								if(nbresults<(int)sresult.SOLVE_MORERESULT)
									//if(gameTable.prevtab!=NULL)
									if(tableQueue.Size>0)
										goto BackStep;
								return (sresult)(Math.Min(nbresults,(int)sresult.SOLVE_MORERESULT));
						}
					}
				}

				//	K�vetkez� pr�b�lkoz�s
				if(gameTable.x>=0) {
					//int cellx=GetTableX(gameTable.x,gameTable.y);
					if(gameTable.cell(gameTable.x,gameTable.y).cannum==0) {
						//goto BackStep;
						ret=-1;
						goto BackTry;
					}
				} else {
					SearchNext();
				}

				//	Be�ll�tani a pr�b�t
				if(SetTryNum()<=0) {
					//	Lehetetlen
					return sresult.SOLVE_IMPOSSIBLE;
				}

				++level;
				textActLevel.Text=level.ToString();
				textActLevel.Refresh();
				maxlev=Math.Max(maxlev,level);
				textMaxLevel.Text=maxlev.ToString();
				textMaxLevel.Refresh();
				trynb++;
				textTryNb.Text=trynb.ToString();
				textTryNb.Refresh();
				//SUTAB* wsutab=(SUTAB*)malloc(sizeof(SUTAB));
				//memcpy(wsutab,psutab,sizeof(SUTAB));
				//wsutab->prevtab=psutab;
				//psutab=wsutab;
				tableQueue.Push(gameTable);
				gameTable.y=
				gameTable.x=-1;
				gameTable.tnb=0;
				gameTable.level=level;
			}
			return sresult.SOLVE_OK;	//	"J� megold�s !"
		}

		//======================================================
		//	Searches the next tryable cell
		//======================================================
		void SearchNext() {
			//	Egy pr�b�t keresni
			int ix,iy;
			//	this arrays contains the indices of 1st cell with [n] possible value cases
			int[] ypos=new int[(int)Const.SUSIZEMX+1];				//	+1 because 0 index exist but not used
			int[] xpos=new int[(int)Const.SUSIZEMX+1];				//	+1 because 0 index exist but not used
			//memset(xpos,-1,sizeof(xpos));	//	Set all to -1
			for(int ii=0; ii<xpos.Length; ii++) {
				xpos[ii]=-1;
			}
			for(iy=gameTable.tabSize; (iy--)>0; ) {
				for(ix=gameTable.tabSize; (ix--)>0; ) {
					//int cellx=GetTableX(ix,iy);	///	iy*susize+ix;
					int val=gameTable.cell(ix,iy).cannum;
					if(val==0)
						continue;	//	no possible value
					if(gameTable.cell(ix,iy).tried==1)
						continue;	//	cell was tried
					int cannotnb=CountBits(val);
					//	mis= 0,1 - impossible
					//		 2	 - can try 2 cases
					if(xpos[cannotnb]<0) {	//	Store the indices of 1st other cases
						ypos[cannotnb]=iy;
						xpos[cannotnb]=ix;
					}
					if(cannotnb==(gameTable.tabSize-2))
						goto EndSearch;	//	Go to trying if only 2 cases possible
				}
			}

EndSearch:	//	Searches the 1st best case
			for(int iz=0; iz<=gameTable.tabSize; iz++) {
				if(xpos[iz]>=0) {
					int yy=gameTable.y=ypos[iz];
					int xx=gameTable.x=xpos[iz];
					//int cellx=GetTableX(gameTable.x,gameTable.y);
					gameTable.cell(xx,yy).tried=1;
					break;
					//?return;
				}
			}
		}

		//******************************************************
		//	Checks table consistency
		//******************************************************
		int CheckAll() {
			//	returns: 0 - if table consistent
			//			 1 - table is inconsistent,  unresolvable
			int ix,iy,flg=0;
			int val;
			for(iy=gameTable.tabSize; (iy--)>0; ) {
				for(ix=gameTable.tabSize; (ix--)>0; ) {
					//int cellx=GetTableX(ix,iy);	///	iy*susize+ix;
					if((gameTable.cell(ix,iy).fixbitnum)==0)
						continue;	//	Empty input item
					val=gameTable.CountCellFlag(ix,iy);	//	val: Impossibles in ix,iy
					if((val&gameTable.cell(ix,iy).fixbitnum)!=0) {
						//if((gameTable.cell(ix,iy).orig==0)||(phase==(int)PHASE.PHcreate)) {
						if((gameTable.cell(ix,iy).orig==0)||(gameState==false)) {
							gameTable.cell(ix,iy).selected=1;
							gameTable.cell(ix,iy).imposs=1;
							//?RedrawCell(ix,iy);
						}
						flg=1;
					}
				}
			}
			return flg;
		}

		//******************************************************
		//	Counts the all cells in a table
		//******************************************************
		int SolveAll() {
			//	returns	-1	- wrong value
			//			 0	- no new fixed value
			//			 1	- fixes a number

			int ix,iy,ret,flg=0;
			for(iy=gameTable.tabSize; (iy--)>0; ) {
				for(ix=gameTable.tabSize; (ix--)>0; ) {
					//int cellx=GetTableX(ix,iy);
					if((gameTable.cell(ix,iy).fixbitnum)!=0)
						continue;
					ret=SolveOne(ix,iy);	//	Sets the possible values in a cell
					//			ret	-1	- wrong value
					//				 0	- no new fixed value
					//				 1	- fixes a number
					if(ret<0)
						return ret;	//	Hiba, visszal�pni az utols� pr�b�hoz
					flg|=ret;
				}
			}
			return flg;
		}

		//======================================================
		//	Sets the possible values in a cell
		//======================================================
		int SolveOne(int x,int y) {
			//	returns	-1	- wrong value
			//			 0	- no new fixed value
			//			 1	- fixes a number

			int val=gameTable.CountCellFlag(x,y);	//	val: Impossibles in iy,ix
			int lst;
			//int	cellx=GetTableX(x,y);	///	y*susize+x;
			if((val&gameTable.cell(x,y).fixbitnum)!=0) {
				//ASSERT(0);
				return -2;	//	There are the same number as own
			}
			switch(gameTable.tabSize-CountBits(val)) {
				case 0:		//	Wrong table (no possible value for cell)
					return -1;
				case 1:		//	Only one possible number exist
OnlyOne:			gameTable.cell(x,y).fixNum=BitToNum((~val&actGameDef.sumask));
					gameTable.cell(x,y).cannum=0;
					return 1;	//	fixed a new number
				default:	//	Set the cell possible values
					lst=TestList(x,y,val);
					//	returns with bit list of possible cell values
					if(lst!=0) {
						if(CountBits(lst)==1) {
							val=(~lst)&actGameDef.sumask;
							goto OnlyOne;
						} else {
							//	More than one fixable
						}
					}
					gameTable.cell(x,y).cannum=(~val&actGameDef.sumask);
					return 0;	//	only possible values exist
			}
		}

		//------------------------------------------------------
		//	Count possible values in a cell
		//------------------------------------------------------
		int CheckOne(int x,int y) {
			//	returns: the nbr of possible valuex in cell x,y
			int val=gameTable.CountCellFlag(x,y);		//	val: Impossibles in iy,ix
			int fix=TestList(x,y,0);	//
			if((val&fix)!=0)
				return actGameDef.sumask;
			switch(CountBits(fix)) {
				case 0:
					break;
				case 1:
					return (~fix)&actGameDef.sumask;
				default:
					//int cellx=GetTableX(x,y);
					gameTable.cell(x,y).imposs=1;
					return actGameDef.sumask;
			}
			return val;
		}

		//------------------------------------------------------
		//	Count possible fixable value in a cell
		//------------------------------------------------------
		int TestList(int x,int y,int val) {
			//	returns with bit list of possible cell values of x,y cell
			int ix,iy,iz,nn;
			int ret=0;
			int y0=y/actGameDef.yCells*actGameDef.yCells;
			int x0=x/actGameDef.xCells*actGameDef.xCells;

			//	Counts the filled cells in a block column
			int yval=val;
			for(nn=0,iy=y0+actGameDef.yCells; --iy>=y0; ) {
				if(iy==y)
					continue;				//	skips the actual cell
				int cellv;
				//int cellx=GetTableX(x,iy);
				if((cellv=gameTable.cell(x,iy).fixbitnum)!=0) {
					++nn;
					yval|=cellv;			//	set if own block line
				}
			}
			if(nn==(actGameDef.yCells-1)) {
				//	if only y,x isn't filled
				for(iz=gameTable.tabSize+1; (--iz)>0; ) {
					int sunum=NumToBit(iz);
					if((yval&sunum)!=0)
						continue;			//	skips the not asked values
					nn=0;
					//	Counts the value in other lines
					for(ix=x0+actGameDef.xCells; --ix>=x0; ) {
						if(ix==x)
							continue;		//	skips the actual line
						for(iy=gameTable.tabSize; (iy--)>0; ) {
							if((iy>=y0)&&(iy<(y0+actGameDef.yCells)))
								continue;	//	skip in own bloxk
							//int cellx=GetTableX(ix,iy);
							if(gameTable.cell(ix,iy).fixbitnum==sunum)
								nn++;
						}
					}
					if(nn>=(actGameDef.xCells-1)) {
						//	if all other columns has the actual valus
						ret|=sunum;
					}
				}
			}

			//	Counts the filled cells in a block line
			int xval=val;
			for(nn=0,ix=x0+actGameDef.xCells; --ix>=x0; ) {
				if(ix==x)
					continue;				//	skips the actual cell
				//int cellx=GetTableX(ix,y);	//	y*susize+ix;
				int cellv;
				if((cellv=gameTable.cell(ix,y).fixbitnum)!=0) {
					++nn;
					xval|=cellv;			//	set if own block line
				}
			}
			if(nn==(actGameDef.xCells-1)) {
				//	if only y,x isn't filled
				for(iz=gameTable.tabSize+1; (--iz)>0; ) {
					int sunum=NumToBit(iz);
					if((xval&sunum)!=0)
						continue;			//	skips the not asked values
					nn=0;
					//	Counts the value in other columns
					for(iy=y0+actGameDef.yCells; --iy>=y0; ) {
						if(iy==y)
							continue;		//	skips the actual column
						for(ix=gameTable.tabSize; (ix--)>0; ) {
							if((ix>=x0)&&(ix<(x0+actGameDef.xCells)))
								continue;	//	skip in own bloxk
							//int cellx=GetTableX(ix,iy);
							if(gameTable.cell(ix,iy).fixbitnum==sunum)
								nn++;
						}
					}
					if(nn>=(actGameDef.yCells-1)) {
						//	if all other columns has the actual valus
						ret|=sunum;
					}
				}
			}
			return ret;
		}

		//------------------------------------------------------
		//	Egy pr�basz�m megkeres�se
		//------------------------------------------------------
		int SetTryNum() {
			int iz;
			int y=gameTable.y;
			int x=gameTable.x;
			if(x<0)
				return -1;		//	No try happened
			//int cellx=GetTableX(x,y);
			int val=gameTable.cell(x,y).cannum;
			if(val==0) {
				gameTable.y=
				gameTable.x=-1;
				return 0;		//	No tryable number
			}
			for(iz=gameTable.tabSize; iz>0; iz--) {
				int sunum=NumToBit(iz);
				if((val&sunum)!=0) {
					//int cellx=GetTableX(x,y);	///y*susize+x;
					gameTable.cell(x,y).cannum&=~sunum;			//	remove bit of num to possible value list
					//gameTable.cell(x,y).fixNum=BitToNum(sunum);
					gameTable.cell(x,y).fixNum=iz;
					break;
				}
			}
			return ++trynb;
		}

		//------------------------------------------------------
		//	Decode to human readable number
		//------------------------------------------------------
		public static int BitToNum(int val) {
			int num;
			if(val==0)
				return 0;
			for(num=1; (val&1)==0; ++num) {
				val>>=1;
			}
#if DEBUG
			if(val!=1) {
				string msg=string.Format("Hib�s bit form�tum� sz�m: {0} -> {1}",val,num);
				throw new DataErrorException(0,msg);
			}
			if(num>gameTable.tabSize) {
				string msg=string.Format("Hib�s, t�l nagy sz�m: {0}",num);
				throw new DataErrorException(0,msg);
			}
#endif
			return num;
		}

		//------------------------------------------------------
		//	Convert from human readable number to internal form
		//------------------------------------------------------
		public static int NumToBit(int num) {
			return (num==0)?0:(1<<(num-1));
		}

		//------------------------------------------------------
		//	Count bits in a bitlist
		//------------------------------------------------------
		public static int CountBits(int val) {
			return (defnumnb[(val>>24)&0xFF]+
					defnumnb[(val>>16)&0xFF]+
					defnumnb[(val>>8)&0xFF]+
					defnumnb[val&0xFF]);
		}
	}
}
