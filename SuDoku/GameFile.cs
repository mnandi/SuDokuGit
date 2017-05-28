using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Text.RegularExpressions;

namespace SuDoku {
	enum GameLineType {
		none=-1,
		comment,
		game,
		end,
	}
	class GameParams{
		public string name="";
		public int diag=0;
		public string tx="3";
		public string ty="3";
		public string tlevel="0";
		public string comment="";
		public GameParams() {
		}
		public GameParams(string nam) {
			name=nam;
		}
		public int size { get { return x*y;} }
		public int x { get { return Int32.Parse(tx);} }
		public int y { get { return Int32.Parse(ty);} }
		public int level { get { return Int32.Parse(tlevel);} }
	}
	class GameFile {
		static List<List<string>> listGames=null;
		static List<string> listItem=null;
		static GameLineType gameItemType;
		static int getGameIndex=-1;
		static int getLineIndex=-1;
		static int putGameIndex=-1;

		static public void InitList(){
			listGames=new List<List<string>>();
			gameItemType=GameLineType.none;
		}

		#region Saving / Loading file
		static public List<string> LoadFile(string filename) {
			List<string> gameNames=new List<string>();
			using(StreamReader iStream=new StreamReader(filename,true)) {
				try {
					string iLine;
					GameLineType regular=GameLineType.comment;
					while((iLine=iStream.ReadLine())!=null) {
						if(iLine[0]=='*') {
							GameFile.AddLine(GameLineType.end,iLine);
							regular=GameLineType.comment;
							continue;
						}
						if(iLine[0]=='[') {
							regular=GameLineType.game;
							const string pattern=@"^\[([^[]*)\].*$";
							Match m=Regex.Match(iLine,pattern,RegexOptions.IgnoreCase);
							//if(!m.Success)
							//	return -1;
							gameNames.Add(m.Groups[1].Value);		//	Extract "[...]" from string
						}
						GameFile.AddLine(regular,iLine);
					}
					GameFile.EndItem();
				} catch(Exception ex) {
					string msg="Error: Could not read file from disk. Original error: "+ex.Message;
					//MessageBox.Show(msg);
				}
			}
			return gameNames;
		}
		static public void SaveFile(string filename) {
			try {
				using(StreamWriter oStream=File.AppendText(filename)) {
					for(int gg=0;gg<listGames.Count;gg++){
						List<string> game=listGames[gg];
						for(int ll=0;ll<game.Count;ll++){
							string line=game[ll];
							oStream.WriteLine(line);
						}
					}
				}
			} catch(Exception ex) {
				string msg="Error: Could not read file from disk. Original error: "+ex.Message;
				//MessageBox.Show(msg);
			}
		}
		public static int GetGameIndex(string game) {
			return SearchGame(game);
		}
		public static GameParams GetGameParameters(string game) {
			int indx=GetGameIndex(game);
			return GetGameParameters(indx);
		}
		public static string GetGameRow(int gIndx,int rIndx) {
			return listGames[gIndx][rIndx];
		}
		public static GameParams GetGameParameters(int indx) {
			if(indx<0)
				return null;
			GameParams gameParams=new GameParams();
			string gameid=GetGameRow(indx,0);
			const string pattern=@"^\[([^[]*)\]\s*(=((X)?([0-9]*)?[xX*]([0-9]*)))?\s*([#]([0-9]*))?\s*;(.*)?\s*$";
			//	1 name
			//	4 diagonal
			//	5 x
			//	6 y
			//	8 level
			//	9 comment
			Match m=Regex.Match(gameid,pattern,RegexOptions.IgnoreCase);
			//if(!m.Success)
			//	return null;
			gameParams.name=m.Groups[1].Value;		//	Extract "[...]" from string
			gameParams.diag=(string.IsNullOrWhiteSpace(m.Groups[4].Value)?(int)GameType.NODIAGGAME:(int)GameType.DIAGGAME);
			gameParams.tx=(string.IsNullOrWhiteSpace(m.Groups[5].Value)?"3":m.Groups[5].Value);		//	Extract "=X*y" from string
			gameParams.ty=(string.IsNullOrWhiteSpace(m.Groups[6].Value)?"3":m.Groups[6].Value);		//	Extract "=n*Y" from string
			gameParams.tlevel=(string.IsNullOrWhiteSpace(m.Groups[8].Value)?"1":m.Groups[8].Value);	//	Extract "=N" from string
			gameParams.comment=m.Groups[9].Value;	//	Extract "; XXXX" from string
			return gameParams;
		}
		#endregion

		#region Reading, Adding, Changing game
		static public List<string> GetGameNames() {
			List<string> listNames=new List<string>();
			for(int ii=0; ii<listGames.Count; ii++) {
				if(listGames[ii][0][0]!='[')	//	Game: first line begins with '['
					continue;
				listNames.Add(listGames[ii][0]);
			}
			return listNames;
		}
		static public void AddGame(int mode,string line) {
			List<string> game=new List<string>();
			if(mode==0){
				putGameIndex=SearchGame(line);
				if(putGameIndex<0) {
					putGameIndex=listGames.Count;
					listGames.Add(game);
				}
			}
			listGames[putGameIndex].Add(line);
		}
		static public string ReadGame(string gamename) {
			if(!string.IsNullOrWhiteSpace(gamename)) {
				getGameIndex=SearchGame(gamename);
				if(getGameIndex<0)
					return null;
				getLineIndex=0;
			}
			if((getGameIndex>=listGames.Count)||(getLineIndex>=listGames[getGameIndex].Count))
				return null;
			return listGames[getGameIndex][getLineIndex++];
		}
		#endregion

		#region Internal routines
		static int SearchGame(string gamename){
			const string pattern=@"^\[([^]]*)\].*$";
			for(int ii=0; ii<listGames.Count; ii++) {
				string line=listGames[ii][0];
				Match l=Regex.Match(line,pattern,RegexOptions.IgnoreCase);
				if(l.Groups[1].Value==gamename)
					return ii;
			}
			return -1;
		}
		static void AddLine(GameLineType type,string line) {
			if(type!=gameItemType) {
				if(type!=GameLineType.end) {
					EndItem();
				}
				gameItemType=type;
			}
			if(listItem==null) {
				listItem=new List<string>();
				//gameItemType=GameLineType.none;
			}
			listItem.Add(line);
		}
		static void EndItem() {
			if((listItem!=null)&&(listItem.Count>0)) {
				if(listGames==null) {
					listGames=new List<List<string>>();
				}
				listGames.Add(listItem);
				listItem=null;
			}
		}
		#endregion
	}
}
