﻿using Bazaar.resources;
using pigeon.core;
using pigeon.sound;
using pigeon.data;
using System.Collections.Generic;
using pigeon.rand;
using pigeon.gameobject;
using System;
using System.Linq;

namespace BlueSun.src.worlds {

    internal class Album {
        public string AlbumName;
        private List<string> songFilenames;
        private List<string> friendlySongNames;

        public void Initialize() {
            songFilenames = GameData.GetFileList(@"music\" + AlbumName + @"\*.nsf");
            friendlySongNames = songFilenames.Select(filename => filename.Substring(0, filename.Length - 4)).ToList();
        }

        public int SongCount => songFilenames.Count;

        public bool HasSong(string friendlySongName) {
            return FindSongIndex(friendlySongName) != -1;
        }

        public int FindSongIndex(string friendlySongName) {
            return friendlySongNames.IndexOf(friendlySongName);
        }

        public string GetFriendlySongName(int index) {
            return friendlySongNames[index];
        }

        public string GetFullPathForSong(string songName) {
            return GetFullPathForSongIndex(FindSongIndex(songName));
        }

        public string GetFullPathForSongIndex(int i) {
            return getFullSongPath(songFilenames[i]);
        }

        private string getFullSongPath(string relativeSongPath) {
            return $@"music\{AlbumName}\{relativeSongPath}";
        }
    }
}
