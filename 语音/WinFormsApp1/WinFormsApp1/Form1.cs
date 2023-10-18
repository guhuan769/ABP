using System;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;


namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        static SpeechSynthesizer SS = new SpeechSynthesizer();
        private SpeechRecognitionEngine SRE = new SpeechRecognitionEngine(); //语音识别模块
        private bool SRE_listening = false;
        private int wordid;
        private string shibie;

        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq, int duration);
        //NAudioRecordHelper nAudioRecordHelper;
        public Form1()
        {
            InitializeComponent();
            // nAudioRecordHelper = new NAudioRecordHelper(0, @"D:\test\a.mp3");
        }


        public void InitVoice()  //语音识别初始化
        {
            //SS.SelectVoice("lily");
            SRE.SetInputToDefaultAudioDevice();  // 默认的语音输入设备，也可以设定为去识别一个WAV文

            GrammarBuilder GB = new GrammarBuilder();

            GB.Append(new Choices(new string[] { "中国", "美国", "英国", "吃屎" }));

            DictationGrammar DG = new DictationGrammar();

            Grammar G = new Grammar(GB);

            G.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(G_SpeechRecognized);  //注册语音识别事件

            SRE.EndSilenceTimeout = TimeSpan.FromSeconds(2);

            SRE.LoadGrammar(G);

        }

        void G_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Beep(500, 500);//已识别提示音

            string result = e.Result.Text;
            switch (result)
            {
                case "中国":
                    shibie = "中国：五星红旗";
                    choice(0);
                    break;
                case "美国":
                    shibie = "美国：星条旗";
                    choice(1);
                    break;
                case "英国":
                    shibie = "英国：米字旗";
                    choice(2);

                    break;
                case "吃屎":
                    shibie = "我已经吃了";
                    choice(3);

                    break;
            }
            lblanswer.Text = shibie;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitVoice();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SRE_listening == false)
            {
                button1.Text = "stop";
                SRE.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
                button1.Text = "start";
                SRE.RecognizeAsyncStop();
            }
            lblanswer.Text = "";
            SRE_listening = !SRE_listening;
            //nAudioRecordHelper.StartRecordAudio();
        }

        private void choice(int id)
        {
            wordid = id;

            Thread t1;
            Thread t2;

            t1 = new Thread(new ThreadStart(ShowAnswer));
            t1.Start();
            t1.Join();
            t2 = new Thread(new ThreadStart(SpeekAnswer));
            t2.Start();
        }
        void ShowAnswer()  //线程
        {
            MethodInvoker mi = new MethodInvoker(this.dosomething);
            this.BeginInvoke(mi);

        }
        void dosomething()
        {
            lblanswer.Text = shibie;
        }
        void SpeekAnswer()  //线程
        {
            switch (wordid)
            {
                case 0:
                    SS.Speak("五星红旗");

                    break;
                case 1:
                    SS.Speak("星条旗");

                    break;
                case 2:
                    SS.Speak("米字旗");

                    break;
                case 3:
                    SS.Speak("我已经吃了");
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //nAudioRecordHelper.StopRecordAudio();
        }
    }
}