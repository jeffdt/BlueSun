using GameMusicEmuSharp;
using NAudio.Wave;
using pigeon.utilities.extensions;

namespace pigeon.sound {
    public static class Music {
        public const int P1 = 0;
        public const int P2 = 1;
        public const int TRI = 2;
        public const int NOISE = 3;
        public const int DPCM = 4;
        public const int SAW = 5;
        public const int P3 = 6;
        public const int P4 = 7;

        public enum States { Playing, Stopped, Paused }

        public static int Track;

        private static GmeReader reader;
        private static IWavePlayer player;

        public static States State { get; private set; }

        public static float Volume { get; set; } = 1.0f;

        public static void Initialize() { }

        public static void Load(string filename) {
            reader = new GmeReader(filename);

            player = new WaveOut();
            player.Init(reader);
        }

        public static void PlayTrack(int trackNum) {
            int clampedTrackNum = trackNum.Clamp(0, reader.TrackCount - 1);

            if (trackNum < 0 || trackNum > reader.TrackCount - 1) {
                Pigeon.Console.LogError(string.Format("trackNum {0} not valid for trackCount range [0, {1}]. defaulting to {2}.", trackNum, reader.TrackCount - 1, clampedTrackNum));
            }

            reader.SetTrack(clampedTrackNum);
            Play();
        }

        public static void Play() {
            State = States.Playing;
            player.Play();
            StereoDepth = .4f;
        }

        public static void Stop() {
            State = States.Stopped;
            player?.Stop();
        }

        public static void Pause() {
            State = States.Paused;
            player.Pause();
        }

        public static void MuteVoice(int voiceIndex) {
            SetVoiceMute(voiceIndex, 1);
        }

        public static void MuteVoices(params int[] voiceIndexes) {
            foreach (int voiceIndex in voiceIndexes) {
                MuteVoice(voiceIndex);
            }
        }

        public static void UnmuteVoice(int voiceIndex) {
            SetVoiceMute(voiceIndex, 0);
        }

        public static void UnmuteVoices(params int[] voiceIndexes) {
            foreach (int voiceIndex in voiceIndexes) {
                UnmuteVoice(voiceIndex);
            }
        }

        public static void SetVoiceMute(int voiceIndex, int mute) {
            reader.MuteVoice(voiceIndex, mute);
        }

        public static void MaskMuteVoices(int mutingMask) {
            reader.MuteVoices(mutingMask);
        }

        public static void ResetEqualizer() {
            reader.SetEqualizer(0, 90);
        }

        private static double _treble;
        public static double Treble {
            get { return _treble; }
            set {
                _treble = value.Clamp(-50, 5);
                // reader.SetEqualizer(_treble)
            }
        }

        private static double _bass;
        public static double Bass {
            get { return _bass; }
            set {
                _bass = value.Clamp(1, 16000);
                reader.SetEqualizer(_treble, _bass);
            }
        }

        private static double _stereoDepth;
        public static double StereoDepth {
            get { return _stereoDepth; }
            set {
                _stereoDepth = value.Clamp(0, 1);
                reader.SetStereoDepth(_stereoDepth);
            }
        }

        private static int _fade;
        public static int Fade {
            get { return _fade; }
            set {
                _fade = value;
                reader.SetFade(_fade);
            }
        }

        private static double _tempo;
        public static double Tempo {
            get { return _tempo; }
            set {
                _tempo = value;
                reader.SetTempo(_tempo);
            }
        }
    }
}
