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
		public readonly int gxCells;		//	group columns
		public readonly int gyCells;		//	group rows
		public readonly int gxCross;		//	game type: 0=normal, 1=cross
		public readonly string gTypeName;	//	game type name
		public int gSumask;
		public int gtabSize { get { return gxCells*gyCells; } }
		public GameDef(int x,int y,int t,string n){
			gxCells=x;
			gyCells=y;
			gxCross=t;
			gTypeName=n;
			gSumask=(1<<x*y)-1;
		}
	}
	class Constants {
		public const int cellOffs=3;		//	pixels between neighbouring cells
		public const int groupOffs=3;		//	Extra plus pixels between neighbouring cell groups

		public const int numBase=((byte)'1')-1;		//	cell ident base at tables <= 3x3
		public const int chrBase=((byte)'A')-1;		//	cell ident base at tables >  3x3



		public static GameDef[] gameDefTb=new GameDef[]{
		new GameDef(3,3,(int)GameType.NODIAGGAME,	"=3x3    (9*9)"),
		new GameDef(3,3,(int)GameType.DIAGGAME,		"=X3x3   (9*9)"),
		new GameDef(2,3,(int)GameType.NODIAGGAME,	"=2x3    (6*3)"),
		new GameDef(2,3,(int)GameType.DIAGGAME,		"=X2x3   (6*3)"),
		new GameDef(3,4,(int)GameType.NODIAGGAME,	"=3x4    (12*12)"),
		new GameDef(3,4,(int)GameType.DIAGGAME,		"=X3x4   (12*12)"),
		new GameDef(4,4,(int)GameType.NODIAGGAME,	"=4x4    (16*16)"),
		new GameDef(4,4,(int)GameType.DIAGGAME,		"=X4x4   (16*16)"),
		new GameDef(5,5,(int)GameType.NODIAGGAME,	"=5x2    (25*25)"),
		new GameDef(5,5,(int)GameType.DIAGGAME,		"=X5x5   (25*25)"),
		new GameDef(2,5,(int)GameType.NODIAGGAME,	"=2x5    (10*10)"),
		new GameDef(2,5,(int)GameType.DIAGGAME,		"=X2x5   (10*10)"),
		new GameDef(3,5,(int)GameType.NODIAGGAME,	"=3x5    (15*15)"),
		new GameDef(3,5,(int)GameType.DIAGGAME,		"=X3x5   (15*15)"),
		new GameDef(4,5,(int)GameType.NODIAGGAME,	"=4x5    (20*20)"),
		new GameDef(4,5,(int)GameType.DIAGGAME,		"=X4x5   (20*20)"),
		new GameDef(5,2,(int)GameType.NODIAGGAME,	"=5x2    (10*10)"),
		new GameDef(5,2,(int)GameType.DIAGGAME,		"=X5x2   (10*10)"),
		new GameDef(5,3,(int)GameType.NODIAGGAME,	"=5x3    (15*15)"),
		new GameDef(5,3,(int)GameType.DIAGGAME,		"=X5x3   (15*15)"),
		new GameDef(5,4,(int)GameType.NODIAGGAME,	"=5x4    (20*20)"),
		new GameDef(5,4,(int)GameType.DIAGGAME,		"=X5x4   (20*20)"),
		new GameDef(2,4,(int)GameType.NODIAGGAME,	"=2x4    (8*8)"),
		new GameDef(2,4,(int)GameType.DIAGGAME,		"=X2x4   (8*8)"),
		new GameDef(4,3,(int)GameType.NODIAGGAME,	"=4x3    (12*12)"),
		new GameDef(4,3,(int)GameType.DIAGGAME,		"=X4x3   (12*12)"),
		new GameDef(4,2,(int)GameType.NODIAGGAME,	"=4x2    (8*8)"),
		new GameDef(4,2,(int)GameType.DIAGGAME,		"=X4x2   (8*8)"),
		new GameDef(3,2,(int)GameType.NODIAGGAME,	"=3x2    (6*6)"),
		new GameDef(3,2,(int)GameType.DIAGGAME,		"=X3x2   (6*6)"),
		new GameDef(2,2,(int)GameType.NODIAGGAME,	"=2x2    (4*4)"),
		new GameDef(2,2,(int)GameType.DIAGGAME,		"=X2x2   (4*4)")};
	}
}
