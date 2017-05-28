using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;	//	Point
using System.IO;		//	MemoryStream
using System.Runtime.Serialization.Formatters.Binary;

namespace SuDoku {
	enum CellType {
		FIXED,
		TRYED,
		FREE
	}
	[Serializable]
	public class GameCell: ICloneable {
		public int	cannum;		//   possible values (if not fixed)
		//public int	result;		//   one legal result value
        //public int	diag;		//1;   //   diagonal cell
        public int	imposs;		//1;   //   unfillable cell
        public int	selected;	//1;   //   selected cell
        public int	tried;		//1;   //   cell with a tried number
        public int	orig=0;		//1;   //   cell with original input number
        public int	fixd=0;		//1;   //   fixed cell

		public int cellX=0;			//	cell X coord
		public int cellY=0;			//	cell Y coord
		//public int occNum=0;		//	nbr of occupied values (nbr of bits in vFlag)
		public int fixNum=0;		//	cell value: -1=empty, 0-not used, 1-n
		//public int tryNum=0;		//	cell value: -1=empty, 0-not used, 1-n
		//public int actFlag=0;		//	own numeric bit mask (1 bit - according fixNum)
		public int vFlag=0;			//	bit mask of 0=available / 1=occupied numbers
		//public int cellType=(int)CellType.FREE;	//	if fixed yet in this step
		//int actNum=0;
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
		public int level;
		public int x;
		public int y;
		public int tnb;
		//	variables for backstep
		public int lastX=-1;
		public int lastY=-1;
		public int tryNb=-1;

		public int xCells;
		public int yCells;
		List<GameCell> listItems;
		public int tabSize { get { return xCells*yCells; } }
		public int boardsize { get { return xCells*yCells*xCells*yCells; } }
		public GameTable(int x,int y) {
			InitTable(x,y);
		}
		public void InitTable(int x,int y){
			xCells=x;
			yCells=y;
			listItems=new List<GameCell>();
			for(int yy=0;yy<tabSize;yy++){
				for(int xx=0; xx<tabSize; xx++) {
					listItems.Add(new GameCell(xx,yy));
				}
			}
		}
		public int CoordxToIndex(int ix,int iy) {
			return iy*tabSize+ix;
		}
		public Point IndexToPoint(int index) {
			return new Point(index%tabSize,index/tabSize);
		}
		public GameCell cell(int x){
			return listItems[x];
		}
		public GameCell cell(int x,int y) {
			return listItems[y*tabSize+x];
		}
		public GameCell cell(Point pt) {
			return listItems[pt.Y*tabSize+pt.X];
		}
		public void FillTableRow(int yy,string rowData) {
			int bas=(tabSize>9)?Constants.chrBase:Constants.numBase;	//	show '1' if table <= 3x3 else 'A'
			for(int xx=0; xx<tabSize; xx++) {
				int num=(int)rowData[xx]-bas;
				if(num<=0)
					continue;
				int ii=yy*tabSize+xx;
				listItems[ii].fixNum=num;
				//listItems[ii].fixNum=num;
				//-listItems[ii].actFlag=1<<num;
			}
		}
		public void FillTable(List<string> lineData) {
			for(int yy=0;yy<tabSize;yy++){
				FillTableRow(yy,lineData[yy+1]);
			}
		}
		public void ClearTable() {
			for(int ii=0; ii<listItems.Count; ii++) {
				GameCell cell=listItems[ii];
				//cell.fixNum=
				//cell.occNum=0;
				cell.vFlag=0;
				//cell.cellType=(int)CellType.FREE;
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
			if(selnb==0)
				return;
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
			for(int ii=0; ii<listItems.Count; ii++) {
				List<int[]> err=GameCheck.CheckValues(listItems[ii]);
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
