using NAudio.Wave;
using System;
using System.Threading;
using Vosk;

namespace VoiceRecognize
{
    internal class Program
    {
        static VoskRecognizer rec;
        static void Main(string[] args)
        {
            Model model = new Model("C:\\Users\\goga\\Desktop\\Шняга\\VoiceRecognize\\VoiceRecognize\\vosk-model-small-ru-0.22");//Модель языка
            rec = new VoskRecognizer(model, 16000.0f);
            Vosk.Vosk.SetLogLevel(-1);
            WaveInEvent waveIn = new WaveInEvent();
            waveIn.WaveFormat = new WaveFormat(16000, 1);
            waveIn.DataAvailable += WaveInOnDataAvailable;
            waveIn.StartRecording();
            while (true) { Thread.Sleep(1000); }
        }

        private static void WaveInOnDataAvailable(object sender, WaveInEventArgs e)
        {
            try
            {
                Console.WriteLine(e.Buffer.ToString(), 0, e.BytesRecorded);

                if (rec.AcceptWaveform(e.Buffer, e.BytesRecorded)) Console.WriteLine(rec.Result());
                else Console.WriteLine(rec.PartialResult());
            }
            catch { }
        }
    }
}
