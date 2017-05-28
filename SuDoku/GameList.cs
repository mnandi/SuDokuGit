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
	class GameList {
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
		static public void LoadFile(string filename) {
			using(StreamReader iStream=new StreamReader(filename,true)) {
				try {
					string iLine;
					GameLineType regular=GameLineType.comment;
					while((iLine=iStream.ReadLine())!=null) {
						if(iLine[0]=='*') {
							GameList.AddLine(GameLineType.end,iLine);
							regular=GameLineType.comment;
							continue;
						}
						if(iLine[0]=='[') {
							regular=GameLineType.game;
						}
						GameList.AddLine(regular,iLine);
					}
					GameList.EndItem();
				} catch(Exception ex) {
					string msg="Error: Could not read file from disk. Original error: "+ex.Message;
					//MessageBox.Show(msg);
				}
			}
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
			const string pattern=@"^(\[[^[]).*$";
			Match m=Regex.Match(gamename,pattern,RegexOptions.IgnoreCase);
			if(!m.Success)
				return -1;
			string name=m.Groups[0].Value;		//	Extract "[...]" from string
			for(int ii=0; ii<listGames.Count; ii++) {
				Match l=Regex.Match(listGames[ii][0],pattern,RegexOptions.IgnoreCase);
				if(l.Groups[0].Value==name)
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
