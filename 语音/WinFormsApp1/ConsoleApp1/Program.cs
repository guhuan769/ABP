using System;
using System.Speech.Recognition;

class Program
{
    static void Main(string[] args)
    {
        // 创建一个SpeechRecognitionEngine实例
        using (SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine())
        {
            // 创建一个GrammarBuilder实例并添加词汇
            GrammarBuilder builder = new GrammarBuilder();
            builder.AppendDictation();

            // 创建一个Grammar实例并加载到SpeechRecognitionEngine实例中
            Grammar grammar = new Grammar(builder);
            recognizer.LoadGrammar(grammar);

            // 注册一个事件处理器，当识别完成时触发
            recognizer.SpeechRecognized += recognizer_SpeechRecognized;

            // 设置输入设备并启动识别
            recognizer.SetInputToDefaultAudioDevice();
            recognizer.RecognizeAsync(RecognizeMode.Multiple);

            // 保持程序在识别过程中运行
            while (true)
            {
                Console.ReadLine();
            }
        }
    }

    static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        Console.WriteLine("Recognized text: " + e.Result.Text);
    }
}


