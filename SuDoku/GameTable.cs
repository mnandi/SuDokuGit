﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;	//	Point
using System.IO;		//	MemoryStream
using System.Runtime.Serialization.Formatters.Binary;

namespace SuDoku {
	[Serializable]
	public class GameItem: ICloneable {
		public int cx=0;			//	cell X coord
		public int cy=0;			//	cell Y coord
		public int occNum=0;		//	nbr of occupied values
		public int actNum=0;		//	cell value: -1=empty, 0-not used, 1-n
		public int actFlag=0;		//	own numeric bit mask (1 bit - according actNum)
		public int vFlag=0;			//	bit mask of 0=available / 1=occupied numbers
		public bool cFixed=false;	//	if fixed yet in this step
		public GameItem(int x,int y) {
			cx=x;
			cy=y;
		}
		public object Clone() {
			return this.MemberwiseClone();
		}
	}
	[Serializable]
	public class GameTable {
		//	variables for backstep
		public int lastX=-1;
		public int lastY=-1;
		public int tryNb=-1;

		public int xCells;
		public int yCells;
		List<GameItem> listItems;
		public int tabSize { get { return xCells*yCells; } }
		public GameTable(int x,int y) {
			InitTable(x,y);
		}
		public void InitTable(int x,int y){
			xCells=x;
			yCells=y;
			listItems=new List<GameItem>();
			for(int yy=0;yy<tabSize;yy++){
				for(int xx=0; xx<tabSize; xx++) {
					listItems.Add(new GameItem(xx,yy));
				}
			}
		}
		public GameItem item(int x){
			return listItems[x];
		}
		public GameItem item(int x,int y) {
			return listItems[y*tabSize+x];
		}
		public void FillTableRow(int yy,string rowData) {
			int bas=(tabSize>9)?0x40:0x30;
			for(int xx=0; xx<tabSize; xx++) {
				int num=(int)rowData[xx]-bas;
				if(num<=0)
					continue;
				listItems[yy*tabSize+xx].actNum=num;
				listItems[yy*tabSize+xx].actFlag=1<<num;
			}
		}
		public void FillTable(List<string> lineData) {
			for(int yy=1;yy<tabSize;yy++){
				FillTableRow(yy,lineData[yy]);
			}
		}
		public void ClearTable() {
			for(int ii=0; ii<listItems.Count; ii++) {
				GameItem item=listItems[ii];
				item.actFlag=
				item.occNum=
				item.vFlag=
				item.occNum=0;
				item.cFixed=false;
				item.actNum=-1;
			}
		}
		public Point GetNearestToComplete() {
			int xx=0;
			int yy=0;
			int max=0;
			for(int ii=0; ii<listItems.Count; ii++) {
				int act=listItems[ii].occNum;
				if((act<=max)||(act==tabSize))
					continue;
				max=act;
				xx=listItems[ii].cx;
				yy=listItems[ii].cy;
			}
			return new Point(xx,yy);
		}
		public int CheckTable() {
			int errs=0;
			for(int ii=0; ii<listItems.Count; ii++) {
				List<int[]> err=SuCheck.CheckValues(listItems[ii]);
				if(err.Count>0)
					errs++;
			}
			return errs;
		}
		//public object Clone() {
		//    return this.MemberwiseClone();
		//}
		public T DeepClone<T>(T obj) {
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
