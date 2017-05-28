using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuDoku {
	enum GameType{
		NODIAGGAME,
		DIAGGAME
	}
	public enum Const {
		SUSIZEMX=25,   //   Maximal dimension of SuDoku board
	}
	public class GameDef {
		public readonly int xCells;		//	group columns
		public readonly int yCells;		//	group rows
		public readonly int xCross;		//	game type: 0=normal, 1=cross
		public readonly string gName;	//	game type name
		public int sumask;
		public GameDef(int x,int y,int t,string n){
			xCells=x;
			yCells=y;
			xCross=t;
			gName=n;
			sumask=(1<<x*y)-1;
		}
	}
	class Constants {
		public const int cellOffs=3;		//	pixels between neighbouring cells
		public const int groupOffs=3;		//	Extra plus pixels between neighbouring cell groups

		public const int numBase=((byte)'1')-1;		//	cell ident base at tables <= 3x3
		public const int chrBase=((byte)'A')-1;		//	cell ident base at tables >  3x3



		public static GameDef[] gameDefTb=new GameDef[]{
		new GameDef(3,3,(int)GameType.NODIAGGAME,	"=9x9    (3*3)"),
		new GameDef(3,3,(int)GameType.DIAGGAME,		"=X9x9   (3*3)"),
		new GameDef(2,3,(int)GameType.NODIAGGAME,	"=6x6    (2*3)"),
		new GameDef(2,3,(int)GameType.DIAGGAME,		"=X6x6   (2*3)"),
		new GameDef(3,4,(int)GameType.NODIAGGAME,	"=12x12  (3*4)"),
		new GameDef(3,4,(int)GameType.DIAGGAME,		"=X12x12 (3*4)"),
		new GameDef(4,4,(int)GameType.NODIAGGAME,	"=16x16  (4*4)"),
		new GameDef(4,4,(int)GameType.DIAGGAME,		"=X16x16 (4*4)"),
		new GameDef(5,5,(int)GameType.NODIAGGAME,	"=25x25  (5*5)"),
		new GameDef(5,5,(int)GameType.DIAGGAME,		"=X25x25 (5*5)"),
		new GameDef(2,5,(int)GameType.NODIAGGAME,	"=10x10  (2*5)"),
		new GameDef(2,5,(int)GameType.DIAGGAME,		"=X10x10 (2*5)"),
		new GameDef(3,5,(int)GameType.NODIAGGAME,	"=15x15  (3*5)"),
		new GameDef(3,5,(int)GameType.DIAGGAME,		"=X15x15 (3*5)"),
		new GameDef(4,5,(int)GameType.NODIAGGAME,	"=20x20  (4*5)"),
		new GameDef(4,5,(int)GameType.DIAGGAME,		"=X20x20 (4*5)"),
		new GameDef(5,2,(int)GameType.NODIAGGAME,	"=10x10  (5*2)"),
		new GameDef(5,2,(int)GameType.DIAGGAME,		"=X10x10 (5*2)"),
		new GameDef(5,3,(int)GameType.NODIAGGAME,	"=15x15  (5*3)"),
		new GameDef(5,3,(int)GameType.DIAGGAME,		"=X15x15 (5*3)"),
		new GameDef(5,4,(int)GameType.NODIAGGAME,	"=20x20  (5*4)"),
		new GameDef(5,4,(int)GameType.DIAGGAME,		"=X20x20 (5*4)"),
		new GameDef(2,4,(int)GameType.NODIAGGAME,	"=8x8    (2*4)"),
		new GameDef(2,4,(int)GameType.DIAGGAME,		"=X8x8   (2*4)"),
		new GameDef(4,3,(int)GameType.NODIAGGAME,	"=12x12  (4*3)"),
		new GameDef(4,3,(int)GameType.DIAGGAME,		"=X12x12 (4*3)"),
		new GameDef(4,2,(int)GameType.NODIAGGAME,	"=8x8    (4*2)"),
		new GameDef(4,2,(int)GameType.DIAGGAME,		"=X8x8   (4*2)"),
		new GameDef(3,2,(int)GameType.NODIAGGAME,	"=6x6    (3*2)"),
		new GameDef(3,2,(int)GameType.DIAGGAME,		"=X6x6   (3*2)"),
		new GameDef(2,2,(int)GameType.NODIAGGAME,	"=4x4    (2*2)"),
		new GameDef(2,2,(int)GameType.DIAGGAME,		"=X4x4   (2*2)")};
	}
}
