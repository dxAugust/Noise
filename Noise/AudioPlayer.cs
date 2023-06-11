using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System;
using System.IO;

namespace Noise
{
    public class AudioPlayer : IDisposable
    {
        IWavePlayer _playbackDevice;
        WaveStream _fileStream;
        string _fileName;

        public AudioPlayer()
        {
            _fileName = string.Empty;
            _playbackDevice = new WaveOut();
        }

        public TimeSpan CurrentTime
        {
            get { return _fileStream.CurrentTime; }
        }

        public double Position
        {
            get
            {
                if (_fileStream != null)
                {
                    return Convert.ToDouble(1000 * _fileStream.Position / _fileStream.Length);
                }
                return 0;
            }
            set
            {
                if (_fileStream != null)
                {
                    _fileStream.Position = Convert.ToInt64(value * _fileStream.Length / 1000);
                }
            }
        }

        public void Load(string fileName)
        {
            if (_fileName != fileName & _playbackDevice.PlaybackState != PlaybackState.Paused)
            {
                Stop();
                CloseFile();
                _playbackDevice.Dispose();
                _playbackDevice = null;
                EnsureDeviceCreated();
                OpenFile(fileName);
                _fileName = fileName;
            }
        }

        public void Play()
        {
            if (_playbackDevice != null & _fileStream != null & _playbackDevice.PlaybackState != PlaybackState.Playing)
            {
                _playbackDevice.Play();
            }
        }

        public void Pause()
        {
            if (_playbackDevice != null)
            {
                _playbackDevice.Pause();
            }
        }

        public void Stop()
        {
            if (_playbackDevice != null & _fileStream != null)
            {
                _playbackDevice.Stop();
                _fileStream.Position = 0;
            }
        }

        public void Dispose()
        {
            Stop();
            CloseFile();
            if (_playbackDevice != null)
            {
                _playbackDevice.Dispose();
                _playbackDevice = null;
            }
        }

        private void CloseFile()
        {
            if (_fileStream != null)
            {
                _fileStream.Dispose();
                _fileStream = null;
            }
        }

        private void OpenFile(string fileName)
        {
            var inputStream = CreateInputStream(fileName);
            _playbackDevice.Init(new SampleToWaveProvider(inputStream));
        }

        private ISampleProvider CreateInputStream(string fileName)
        {
            if (fileName.EndsWith(".wav"))
            {
                _fileStream = OpenWaveStream(fileName);
            }
            else if (fileName.EndsWith(".mp3"))
            {
                _fileStream = new Mp3FileReader(fileName);
            }
            else
            {
                throw new InvalidOperationException("Unsupported extension");
            }
            var inputStream = new SampleChannel(_fileStream);
            var sampleStream = new NotifyingSampleProvider(inputStream);
            return sampleStream;
        }

        private static WaveStream OpenWaveStream(string fileName)
        {
            WaveStream readerStream = new WaveFileReader(fileName);
            if (readerStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
            {
                readerStream = WaveFormatConversionStream.CreatePcmStream(readerStream);
                readerStream = new BlockAlignReductionStream(readerStream);
            }
            return readerStream;
        }

        private void EnsureDeviceCreated()
        {
            if (_playbackDevice == null)
            {
                CreateDevice();
            }
        }

        private void CreateDevice()
        {
            _playbackDevice = new WaveOut();
        }
    }
}