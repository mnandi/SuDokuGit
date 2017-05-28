using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;	//	Point
using System.IO;		//	MemoryStream
using System.Runtime.Serialization.Formatters.Binary;

namespace SuDoku {
	[Serializable]
	public class GameCell: ICloneable {
		public int	cannum;		//	possible values (if not fixed)
        public int	imposs;		//	unfillable cell
        public int	selected;	//	selected cell
        public int	tried;		//	cell with a tried number
        public int	orig=0;		//	cell with original input number

		int cX;			//	cell X coord
		int cY;			//	cell Y coord

		public int inpsel=0;		//	selected input cell
		public int fixNum=0;		//	cell value: -1=empty, 0-not used, 1-n
		public int fixbitnum { get { return (fixNum<=0)?0:1<<(fixNum-1); } }

		public GameCell(int x,int y) {
			cX=x;
			cY=y;
		}
		public int cellX { get { return cX; } }
		public int cellY { get { return cY; } }
		public Point coords { get { return new Point(cX,cY); } }
		public object Clone() {
			return this.MemberwiseClone();
		}
		public int GetIndex(int blockSize) {
			return cellX+cellY*blockSize;
		}
	}
	[Serializable]
	public class GameTable {
		//	variables for backstep
		public int level=0;			//	table try level (in queue)
		public int x=-1;			//	last tryed cell x coord
		public int y=-1;			//	last tryed cell y coord
		public int tnb;				//	number of attempts

		int nummask;
		int tabsiz;
		public int diagFlag;
		public int selectedX=-1;
		public int selectedY=-1;

		public int xCells;			//	cell x position in table
		public int yCells;			//	cell y position in table
		List<GameCell> cellList;	//	table of game cells
		public int numbitmask { get { return nummask; } }
		public int tabSize { get { return tabsiz; } }
		public int boardsize { get { return tabsiz*tabsiz; } }
		public GameTable(int nx,int ny) {
			InitTable(nx,ny);
		}
		public void InitTable(int nx,int ny){
			xCells=nx;
			yCells=ny;
			tabsiz=nx*ny;
			nummask=(1<<tabsiz)-1;
			selectedX=
			selectedY=-1;
			cellList=new List<GameCell>();
			for(int yy=0;yy<tabsiz;yy++){
				for(int xx=0; xx<tabsiz; xx++) {
					cellList.Add(new GameCell(xx,yy));
				}
			}
			x=y=-1;
		}
		public int CoordToIndex(Point coord) { return coord.Y*tabSize+coord.X; }
		public Point IndexToCoord(int index) { return new Point(index%tabSize,index/tabSize); }
		public GameCell cell(int x) { return cellList[x]; }
		public GameCell cell(int x,int y) { return cellList[y*tabSize+x]; }
		public GameCell cell(Point pt) { return cellList[pt.Y*tabSize+pt.X]; }
		public void SetSelectedCell(int x,int y) {
			int ox=selectedX;
			int oy=selectedY;
			if((ox>=0)&&(oy>=0)) {
				cell(ox,oy).inpsel=0;	//	Clear previous
			}
			selectedX=x;
			selectedY=y;
			if((x>=0)&&(y>=0)) {
				cell(x,y).inpsel=1;		//	Set actual
			}
		}

		public int EndTest() {
#if DEBUG
			return cellList.FindLastIndex(x => x.fixNum==0);
#else
			return cellList.FindIndex(x => x.fixbitnum==0);
#endif
		}

		#region	Table handling (fill/load/clear) routines
		//------------------------------------------------------
		//	Filling
		//------------------------------------------------------
		public void FillTableRow(int yy,string rowData) {
			if(rowData.Length<tabSize)
				rowData=rowData.PadRight(tabSize);
			int bas=(tabSize>9)?Constants.chrBase:Constants.numBase;	//	show '1' if table <= 3x3 else 'A'
			for(int xx=0; xx<tabSize; xx++) {
				int num=(int)rowData[xx]-bas;
				if((num<0)||(num>tabSize))	//	filters the wrong numbers
					num=0;
				int ii=yy*tabSize+xx;
				cellList[ii].fixNum=num;
				cellList[ii].orig=(num==0)?0:1;
				cellList[ii].selected=
				cellList[ii].imposs=
				cellList[ii].tried=0;
			}
		}
		public void FillTable(List<string> lineData) {
			for(int yy=0;yy<tabSize;yy++){
				FillTableRow(yy,lineData[yy+1]);
			}
		}
		//------------------------------------------------------
		//	Data extracting from table
		//------------------------------------------------------
		public string ExtractLine(int lineindex) {
			if(lineindex>=tabSize)
				return "";
			string line="";
			for(int xx=0; xx<tabSize; xx++) {
				int num=cell(xx,lineindex+1).fixNum;
				line+=(num==0)?"":((tabSize<10)?'0':('A'-1)).ToString();
			}
			return line;
		}

		//------------------------------------------------------
		//	Clear table
		//------------------------------------------------------
		public void ClearTable() {
			for(int ii=0; ii<cellList.Count; ii++) {
				GameCell cell=cellList[ii];
				cell.orig=0;
				cell.cannum=0;
				cell.imposs=0;
				cell.selected=0;
				cell.fixNum=0;
			}
		}
		//======================================================================
		//	Clearing all select and impossible bits on board
		//======================================================================
		public void ClearSelects() {
			for(int xx=boardsize;(xx--)>0;){
				if(this.cell(xx).selected==1){
					this.cell(xx).selected=0;
				}
				if(this.cell(xx).imposs==1) {
					this.cell(xx).imposs=0;
				}

			}
		}
		#endregion

		#region Cell counting/checking routines
		//------------------------------------------------------
		//	Checking table errors
		//------------------------------------------------------
		public int CheckTable(bool flag) {
			//	flag=false	only result need
			//		=true	show problems
			int errs=0;
			for(int ii=0; ii<cellList.Count; ii++) {
				CellResult lErr=CountCellFlag(cellList[ii]);
				if(lErr.errList.Count>0) {
					errs++;
					if(flag) {
						cell(ii).imposs=1;
						foreach(int[] ee in lErr.errList) {
							cell(ee[0],ee[1]).imposs=1;
						}
					}
				}
			}
			return errs;
		}
		public void ClearTableErrors() {
			List<GameCell> errs=cellList.FindAll(x => x.imposs==1);
			foreach(GameCell cell in errs) {
				cell.imposs=0;
			}
		}

		//------------------------------------------------------
		//	Count forbidden values for a cell
		//------------------------------------------------------
		int CountCellFlag(int indx) {
			//		returns: the bitflag of a cell impossible values
			//					by the collection of bit of numbers cell x,y
			//					on row, column, block and optionally diagonals

			int x,y;
			x=indx%tabSize;
			y=indx/tabSize;
			return CountCellFlag(x,y);
		}

		//------------------------------------------------------
		//	Count forbidden values for a cell
		//------------------------------------------------------
		public CellResult CountCellFlag(GameCell cell) {
			return CountCellFlag(cell.cellX,cell.cellY,cell.fixbitnum);
		}

		//------------------------------------------------------
		//	Count forbidden values for a cell
		//------------------------------------------------------
		public int CountCellFlag(int x,int y) {
			CellResult res=CountCellFlag(x,y,0);
			return res.cellnums;
		}

		//------------------------------------------------------
		//	Count forbidden values for a cell
		//------------------------------------------------------
		public CellResult CountCellFlag(int x,int y,int cellnum=0) {
			//		returns: the bitflag of a cell impossible values
			//					by the collection of bit of numbers cell x,y
			//					on row, column, block and optionally diagonals
			int val=0;
			int ix,iy;
			CellResult cRes=new CellResult();
			int y0=y/yCells*yCells;
			int x0=x/xCells*xCells;
			//	Collects bits of column
			ix=x;
			for(iy=tabSize; (iy--)>0; ) {
				if(iy==y)
					continue;			//	Skip at actual cell
				val|=TestFlag(cellnum,ix,iy,cRes);
			}
			//	Collects bits of row
			iy=y;
			for(ix=tabSize; (ix--)>0; ) {
				if(ix==x)
					continue;			//	Skip at actual cell
				val|=TestFlag(cellnum,ix,iy,cRes);
			}
			//	Collects bits of block
			for(iy=y0+yCells; (iy--)>y0; ) {
				for(ix=x0+xCells; (ix--)>x0; ) {
					if((iy==y)&&(ix==x))
						continue;		//	Skip at actual cell
					val|=TestFlag(cellnum,ix,iy,cRes);
				}
			}
			//	If diagonals are observed
			if(diagFlag==(int)GameType.DIAGGAME) {
				if(x==y) {
					//	Collects bits of top-down diagonal
					for(ix=tabSize; (ix--)>0; ) {
						if(ix==x)
							continue;	//	Skip at actual cell
						iy=ix;
						val|=TestFlag(cellnum,ix,iy,cRes);
					}
				}
				if((x+y+1)==tabSize) {
					//	Collects bits of bottom-up diagonal
					for(ix=tabSize; (ix--)>0; ) {
						if(ix==x)
							continue;	//	Skip at actual cell
						iy=tabSize-ix-1;
						val|=TestFlag(cellnum,ix,iy,cRes);
					}
				}
			}
			cRes.cellnums=val&numbitmask;
			return cRes;
		}
		//	test conflict
		int TestFlag(int flag,int ix,int iy,CellResult res) {
			int bits=cell(ix,iy).fixbitnum;
			if((flag&bits)!=0) {
				AddToDoubles(ix,iy,res.errList);
			}
			return bits;
		}
		//	build conflict list
		static void AddToDoubles(int x,int y,List<int[]> dbl) {
			int ix=dbl.FindIndex(e => ((e[0]==x)&&(e[1]==y)));
			if(ix<0) {
				//	cell not in list
				dbl.Add(new int[] { x,y });
			}
		}
		#endregion

		#region Cloning
		public T DeepClone<T>(T obj) where T: class {
			T objResult;
			using(MemoryStream ms=new MemoryStream()) {
				BinaryFormatter bf=new BinaryFormatter();
				bf.Serialize(ms,obj);

				ms.Position=0;
				objResult=(T)bf.Deserialize(ms);
			}
			return objResult;
		}
		#endregion
	}
}
