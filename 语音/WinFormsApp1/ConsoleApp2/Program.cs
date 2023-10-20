using System.Speech.Recognition;

public class Program
{
    static void Main(string[] args)
    {
        using (SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("zh-CN")))
        {
            // 创建 Choices 对象。
            Choices colors = new Choices();
            colors.Add(new string[] { "red", "green", "blue" });

            // 创建 GrammarBuilder 对象并追加 Choices 对象。
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(colors);

            // 创建 Grammar 对象并加载到 SpeechRecognitionEngine 对象。
            Grammar g = new Grammar(gb);
            recognizer.LoadGrammar(g);

            // 注册 SpeechRecognized 事件。
            recognizer.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

            // 设置输入设备并启动语音识别。
            recognizer.SetInputToDefaultAudioDevice();
            recognizer.RecognizeAsync(RecognizeMode.Multiple);

            // 保持控制台活动状态。
            while (true) ;
        }
    }

    // 处理 SpeechRecognized 事件。
    static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        Console.WriteLine("识别的文本: " + e.Result.Text);
    }
}
