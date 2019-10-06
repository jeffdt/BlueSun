using Microsoft.Xna.Framework.Input;
using pigeon.gameobject;
using pigeon.gfx.drawable.text;
using pigeon.input;
using pigeon.rand;
using pigeon.sound.music;
using System.Collections.Generic;

namespace BlueSun.src.worlds {
    internal class SongPlayer : Component {
        private List<Album> albums;

        private TextRenderer songText;
        private TextRenderer albumText;

        private int currAlbumIndex;
        private int currSongIndex;

        private int totalSongCount;

        protected override void Initialize() {
            songText = Object.FindChild("songtext").GetComponent<TextRenderer>();
            albumText = Object.FindChild("albumtext").GetComponent<TextRenderer>();

            albums = new List<Album> {
                new Album() { AlbumName = "Bazaar" },
                new Album() { AlbumName = "Covers" },
                new Album() { AlbumName = "LilLoops" },
                new Album() { AlbumName = "MetallicWing" },
                new Album() { AlbumName = "RandomJams" },
                new Album() { AlbumName = "SuperFORE!" },
                new Album() { AlbumName = "UnfinishedBusiness" }
            };

            albums.ForEach(it => it.Initialize());

            foreach (var folder in albums) {
                totalSongCount += folder.SongCount;
            }

            playRandomSong();
            //playSpecificSong("morse");
        }

        protected override void Update() {
            if (RawKeyboardInput.IsPressed(Keys.Space)) {
                playRandomSong();
            } else if (RawKeyboardInput.IsPressed(Keys.R)) {
                restartSong();
            } else if (RawKeyboardInput.IsPressed(Keys.Up)) {
                playPreviousAlbum();
            } else if (RawKeyboardInput.IsPressed(Keys.Down)) {
                playNextAlbum();
            } else if (RawKeyboardInput.IsPressed(Keys.Left)) {
                playPreviousSong();
            } else if (RawKeyboardInput.IsPressed(Keys.Right)) {
                playNextSong();
            }
        }

        private void playPreviousSong() {
            Album album = albums[currAlbumIndex];
            int nextSongIndex = (currSongIndex == 0) ? album.SongCount - 1 : (currSongIndex - 1);
            playSong(currAlbumIndex, nextSongIndex);
        }

        private void playPreviousAlbum() {
            int nextAlbumIndex = (currAlbumIndex == 0) ? albums.Count - 1 : (currAlbumIndex - 1);
            playSong(nextAlbumIndex, albums[nextAlbumIndex].SongCount.Random());
        }

        private void playNextSong() {
            Album album = albums[currAlbumIndex];
            int nextSongIndex = (currSongIndex == album.SongCount - 1) ? 0 : (currSongIndex + 1);
            playSong(currAlbumIndex, nextSongIndex);
        }

        private void playNextAlbum() {
            int nextAlbumIndex = (currAlbumIndex == albums.Count - 1) ? 0 : (currAlbumIndex + 1);
            playSong(nextAlbumIndex, albums[nextAlbumIndex].SongCount.Random());
        }

        private void playRandomSong() {
            int randomSong = totalSongCount.Random();

            for (int i = 0; i < albums.Count; i++) {
                if (albums[i].SongCount < randomSong) {
                    randomSong -= albums[i].SongCount;
                } else {
                    playSong(i, randomSong);
                    break;
                }
            }
        }

        private void playSpecificSong(string song) {
            for (int i = 0; i < albums.Count; i++) {
                int nextSongIndex = albums[i].FindSongIndex(song);

                if (nextSongIndex != -1) {
                    playSong(i, nextSongIndex);
                    break;
                }
            }
        }

        private void restartSong() {
            playSong(currAlbumIndex, currSongIndex);
        }

        private void playSong(int nextAlbumIndex, int nextSongIndex) {
            currAlbumIndex = nextAlbumIndex;
            currSongIndex = nextSongIndex;

            Album songFolder = albums[currAlbumIndex];

            Music.Stop();
            Music.Load(songFolder.GetFullPathForSongIndex(currSongIndex));
            Music.PlayTrack(0);
            Music.StereoDepth = 1f;

            songText.Text = songFolder.GetFriendlySongName(currSongIndex);
            albumText.Text = songFolder.AlbumName;
        }
    }
}