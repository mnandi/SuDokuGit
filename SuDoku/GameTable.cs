using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuDoku {
	class GameTable {
		int xn;
		int yn;
		int tabSize;
		List<GameItem> listItems ;
		public GameTable(int x,int y) {
			xn=x;
			yn=y;
			tabSize=x*y;
			listItems=new List<GameItem>();
			for(int xx=0;xx<tabSize;xx++){
				for(int yy=0;yy<tabSize;yy++){
					listItems.Add(new GameItem(xx,yy));
				}
			}
		}
		public int tableSize {get{return tableSize;}}
		public GameItem item(int x){
			return listItems[x];
		}
		public GameItem item(int x,int y) {
			return listItems[y*tabSize+x];
		}
	}
}
