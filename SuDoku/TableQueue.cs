using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuDoku {
	//	This works as LIFO queue
	public class TableQueue {
		public List<GameTable> gameTableList=null;
		public TableQueue() {
			gameTableList=new List<GameTable>();
		}
		public int Size { get { return ((gameTableList==null)?0:gameTableList.Count); } }

		void Reinit() {
			gameTableList=new List<GameTable>();
		}

		public int Push(GameTable table) {
			gameTableList.Add(table.DeepClone<GameTable>(table));
			return gameTableList.Count;
		}

		public GameTable Pop() {
			int last=gameTableList.Count-1;
			if(last<1)
				return null;
			GameTable work=gameTableList[last];
			gameTableList.RemoveAt(last);
			return work;
		}
	}
}
