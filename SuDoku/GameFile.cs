﻿using System;
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
		public string pIdline;
		public string pName="";
		public int pDiag=0;
		public string ptX="3";
		public string ptY="3";
		public string tlevel="0";
		public string pComment="";
		public GameParams() {
		}
		public GameParams(string id,string nam) {
			pIdline=id;
			pName=nam;
		}
		public int pSize { get { return pX*pY;} }
		public int pX { get { return Int32.Parse(ptX);} }
		public int pY { get { return Int32.Parse(ptY);} }
		public int pLevel { get { return Int32.Parse(tlevel);} }
	}
	class GameFile {
		static List<List<string>> listGames=new List<List<string>>();
		static List<string> listItem=null;
		static GameLineType gameItemType;
		static int getGameIndex=-1;
		static int getLineIndex=-1;
		static int putGameIndex=-1;

		static public void InitList(){
			//listGames=new List<List<string>>();
			gameItemType=GameLineType.none;
		}

		static public GameParams SetGameId(GameParams par) {
			par.pIdline=SetGameIdLine(par);
			return par;
		}
		static public string SetGameIdLine(GameParams par) {
			string line=string.Format("[{0}]\t={1}{2}x{3}\t ({4}x{4})\t#{5}\t; {6}",par.pName,par.ptX,par.ptY,par.pSize,par.pLevel,par.pComment);
			return line;
		}
		#region Saving / Loading file
		static public List<string> LoadFile(string filename) {
			List<string> gameNames=new List<string>();
			using(StreamReader iStream=new StreamReader(filename,true)) {
				try {
					string iLine;
					GameLineType regular=GameLineType.comment;
					while((iLine=iStream.ReadLine())!=null) {
						if(iLine.Length>0) {
							if(iLine[0]=='*') {
								GameFile.AddLine(GameLineType.end,iLine);
								regular=GameLineType.comment;
								continue;
							}
							if(iLine[0]=='[') {
								regular=GameLineType.game;
								gameNames.Add(iLine.Replace("\t","  "));
							}
						}
						GameFile.AddLine(regular,iLine);
					}
					GameFile.EndItem();
				} catch(Exception ex) {
					string msg="Error: Could not read file from disk. Original error: "+ex.Message;
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
			}
		}
		public static int GetGameIndexFull(string gameid) {
			return SearchGame(ExtractGameName(gameid));
		}
		public static int GetGameIndex(string game) {
			return SearchGame(game);
		}
		public static GameParams GetGameParameters(string game) {
			int indx=GetGameIndex(game);
			return GetGameParameters(indx);
		}
		public static string GetGameRow(int gIndx,int rIndx) {
			rIndx++;
			return (listGames[gIndx].Count<=rIndx)?"*":listGames[gIndx][rIndx];
		}
		public static GameParams GetGameParameters(int indx) {
			if(indx<0)
				return null;
			string gameid=listGames[indx][0];
			return GetGameID(gameid);
		}
		public static GameParams GetGameID(string gameid) {
			GameParams gameParams=new GameParams();
			const string pattern=@"^\[([^[]*)\]\s*(=((X)?([0-9]*)?[xX*]([0-9]*)))?\s*([#]([0-9]*))?\s*(;\s*(.*))?\s*$";
			//	1 name
			//		2 =dx*y
			//		3 dx*y
			//	4 diagonal
			//	5 x
			//	6 y
			//		7 #n
			//	8 level
			//		9 ;comment
			// 10 comment
			Match m=Regex.Match(gameid,pattern,RegexOptions.IgnoreCase);
			gameParams.pIdline=gameid;
			gameParams.pName=m.Groups[1].Value;		//	Extract "[...]" from string
			gameParams.pDiag=(string.IsNullOrWhiteSpace(m.Groups[4].Value)?(int)GameType.NODIAGGAME:(int)GameType.DIAGGAME);
			gameParams.ptX=(string.IsNullOrWhiteSpace(m.Groups[5].Value)?"3":m.Groups[5].Value);		//	Extract "=X*y" from string
			gameParams.ptY=(string.IsNullOrWhiteSpace(m.Groups[6].Value)?"3":m.Groups[6].Value);		//	Extract "=n*Y" from string
			gameParams.tlevel=(string.IsNullOrWhiteSpace(m.Groups[8].Value)?"1":m.Groups[8].Value);		//	Extract "=N" from string
			gameParams.pComment=m.Groups[10].Value;	//	Extract "; XXXX" from string
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
		static public int AddGame(int gameindex,int mode,string line) {
			if(mode==0){
				List<string> game=new List<string>();
				if(gameindex<0) {
					//	new game
					gameindex=listGames.Count;
					listGames.Add(game);
				} else {
					//	replace old game
					listGames[gameindex]=game;
				}
			}
			listGames[gameindex].Add(line);
			return gameindex;
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
		static string ExtractGameName(string gameline) {
			const string pattern=@"^\[([^]]*)\].*$";
			Match l=Regex.Match(gameline,pattern,RegexOptions.IgnoreCase);
			return l.Groups[1].Value;
		}
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
