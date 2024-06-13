using System;
using System.IO;
using Vosk;

namespace VoiceRecognize
{
    internal class Program
    {
        public static void DemoBytes(Model model)
        {
            VoskRecognizer rec = new VoskRecognizer(model, 16000.0f);
            rec.SetMaxAlternatives(0);
            rec.SetWords(true);
            using (Stream source = File.OpenRead("decoder-test.wav"))//файл должен быть с такми зарактеристиками: 16khz 16bit mono Wav

            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    if (rec.AcceptWaveform(buffer, bytesRead))
                    {
                        Console.WriteLine(rec.Result());
                    }
                    else
                    {
                        Console.WriteLine(rec.PartialResult());
                    }
                }
            }
            Console.WriteLine(rec.FinalResult());
        }
        static void Main(string[] args)
        {
            Vosk.Vosk.SetLogLevel(-1);
            Model model = new Model("C:\\Users\\goga\\Desktop\\Шняга\\VoiceRecognize\\VoiceRecognize\\vosk-model-small-ru-0.22");//Модель языка
            DemoBytes(model);
        }
    }
}
