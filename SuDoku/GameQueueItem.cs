using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuDoku {
	public class GameQueueItem {
		//	variables for backstep
		public int lastX=-1;
		public int lastY=-1;
		public int tryNb=-1;
		public GameTable gameTable;
		public GameQueueItem() {
		}
	}
}
