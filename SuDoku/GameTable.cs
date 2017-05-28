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
		public int	cannum;		//   possible values (if not fixed)
        public int	imposs;		//1;   //   unfillable cell
        public int	selected;	//1;   //   selected cell
        public int	tried;		//1;   //   cell with a tried number
        public int	orig=0;		//1;   //   cell with original input number

		public int cellX;			//	cell X coord
		public int cellY;			//	cell Y coord
		public int fixNum=0;		//	cell value: -1=empty, 0-not used, 1-n
		public int vFlag=0;			//	bit mask of 0=available / 1=occupied numbers
		public int fixnum { get { return (fixNum<=0)?0:1<<(fixNum-1); } }
		public GameCell(int x,int y) {
			cellX=x;
			cellY=y;
		}
		public Point coords { get { return new Point(cellX,cellY); } }
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

		public int xCells;			//	cell x position in table
		public int yCells;			//	cell y position in table
		List<GameCell> cellList;	//	table of game cells
		public int tabSize { get { return xCells*yCells; } }
		public int boardsize { get { return xCells*yCells*xCells*yCells; } }
		public GameTable(int x,int y) {
			InitTable(x,y);
		}
		public void InitTable(int x,int y){
			xCells=x;
			yCells=y;
			x=y=-1;
			cellList=new List<GameCell>();
			for(int yy=0;yy<tabSize;yy++){
				for(int xx=0; xx<tabSize; xx++) {
					cellList.Add(new GameCell(xx,yy));
				}
			}
		}
		public int CoordToIndex(Point coord) { return coord.Y*tabSize+coord.X; }
		public Point IndexToCoord(int index) { return new Point(index%tabSize,index/tabSize); }
		public GameCell cell(int x) { return cellList[x]; }
		public GameCell cell(int x,int y) { return cellList[y*tabSize+x]; }
		public GameCell cell(Point pt) { return cellList[pt.Y*tabSize+pt.X]; }

		public int EndTest() {
			return cellList.FindIndex(x => x.fixnum==0);
		}

		public void FillTableRow(int yy,string rowData) {
			if(rowData.Length<tabSize)
				rowData=rowData.PadRight(tabSize);
			int bas=(tabSize>9)?Constants.chrBase:Constants.numBase;	//	show '1' if table <= 3x3 else 'A'
			for(int xx=0; xx<tabSize; xx++) {
				int num=(int)rowData[xx]-bas;
				if(num<=0)
					continue;
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
		public void ClearTable() {
			for(int ii=0; ii<cellList.Count; ii++) {
				GameCell cell=cellList[ii];
				cell.vFlag=0;
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
		public void ClearSelects(ref int selnb) {
			//if(selnb==0)
			//	return;
			for(int xx=boardsize;(xx--)>0;){
				//if(psutab->sudoku[xx].selected==1){
				//	psutab->sudoku[xx].selected=0;
				//	//RedrawCell(xx%susize,xx/susize);
				//}
				//if(psutab->sudoku[xx].imposs==1){
				//	psutab->sudoku[xx].imposs=0;
				//	//RedrawCell(xx%susize,xx/susize);
				//}
				if(this.cell(xx).selected==1){
					this.cell(xx).selected=0;
					//RedrawCell(xx%susize,xx/susize);
				}
				if(this.cell(xx).imposs==1) {
					this.cell(xx).imposs=0;
					//RedrawCell(xx%susize,xx/susize);
				}

			}
			selnb=0;
		}
		public int CheckTable() {
			int errs=0;
			for(int ii=0; ii<cellList.Count; ii++) {
				List<int[]> err=GameCheck.CheckValues(cellList[ii]);
				if(err.Count>0)
					errs++;
			}
			return errs;
		}
		public T DeepClone<T>(T obj) where T : class {
			T objResult;
			using(MemoryStream ms=new MemoryStream()) {
				BinaryFormatter bf=new BinaryFormatter();
				bf.Serialize(ms,obj);

				ms.Position=0;
				objResult=(T)bf.Deserialize(ms);
			}
			return objResult;
		}
	}
}
