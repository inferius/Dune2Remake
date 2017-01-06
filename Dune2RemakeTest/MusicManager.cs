using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using Sanford.Multimedia.Midi;

namespace Dune2RemakeTest
{
    public class MusicManager
    {
        private List<Uri> _musicList = new List<Uri>(); 
        private SoundPlayer _player = new SoundPlayer();
        private Sequence _sequence = new Sequence();
        private Sequencer _sequencer = new Sequencer();
        private int _currentTrack = 0;
        private bool _isLoaded;


        private OutputDevice outDevice;
        private int outDeviceID = 0;

        public MusicManager()
        {
            try
            {
                outDevice = new OutputDevice(outDeviceID);

                _sequencer.Position = 0;
                _sequencer.Sequence = _sequence;
                _sequencer.PlayingCompleted += (s, e) => Next();
                _sequence.LoadCompleted += (s, e) => _sequencer.Start();

                _sequencer.ChannelMessagePlayed += HandleChannelMessagePlayed;
                _sequencer.Stopped += HandleStopped;
                _sequencer.SysExMessagePlayed += HandleSysExMessagePlayed;
                _sequencer.Chased += HandleChased;
                _isLoaded = true;
            }
            catch
            {
                _isLoaded = false;
            }
        }

        public void AddMusic(string path)
        {
            _musicList.Add(new Uri(path, UriKind.Absolute));
        }

        public void AddMusic(Uri path)
        {
            _musicList.Add(path);
        }

        public void Play()
        {
            if (!_isLoaded) return;
            if (_musicList.Count > 0)
            {
                _sequence.LoadAsync(Path.Combine(Environment.CurrentDirectory, _musicList[_currentTrack].ToString()));

                //_player.SoundLocation = Path.Combine(Environment.CurrentDirectory, _musicList[0].ToString());
                //_player.Load();
                //_player.Play();
            }
        }

        public void Next()
        {
            if (!_isLoaded) return;
            int track = _currentTrack + 1;
            if (track >= _musicList.Count) track = 0;

            _sequencer.Stop();
            _sequence.LoadAsync(Path.Combine(Environment.CurrentDirectory, _musicList[track].ToString()));
        }

        public void Stop()
        {
            //_player.Stop();
            _sequencer.Stop();
        }


        private void HandleChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {

            outDevice.Send(e.Message);
        }

        private void HandleChased(object sender, ChasedEventArgs e)
        {
            foreach (ChannelMessage message in e.Messages)
            {
                outDevice.Send(message);
            }
        }

        private void HandleSysExMessagePlayed(object sender, SysExMessageEventArgs e)
        {
            //     outDevice.Send(e.Message); Sometimes causes an exception to be thrown because the output device is overloaded.
        }

        private void HandleStopped(object sender, StoppedEventArgs e)
        {
            foreach (ChannelMessage message in e.Messages)
            {
                outDevice.Send(message);
            }
        }
    }
}
