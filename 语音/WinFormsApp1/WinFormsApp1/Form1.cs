using System;
using System.Reflection.Emit;
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
        private SpeechRecognitionEngine SRE = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("zh-CN")); //语音识别模块 
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
        string[] text = new string[] { "中国", "测试", "英国", "左边", "右边", "左前方", "右前方", "我爱你", "吃饭" };

        public void InitVoice()  //语音识别初始化
        {
            //SS.SelectVoice("lily");
            label1.Text = string.Empty;
            foreach (string s in text)
            {
                label1.Text += $"{s} -";
            }
            label1.Text = label1.Text.TrimEnd('-');
            SRE.SetInputToDefaultAudioDevice();  // 默认的语音输入设备，也可以设定为去识别一个WAV文

            GrammarBuilder GB = new GrammarBuilder();

            GB.Append(new Choices(text));
            //GB.AppendDictation();

            DictationGrammar DG = new DictationGrammar();

            //引用语音识别语法的运行时对象，应用程序可以用之来定义语音识别约束。
            Grammar G = new Grammar(GB);

            //注册语音识别后事件
            G.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(G_SpeechRecognized);  //注册语音识别事件

            //设置终止识别声音间隔0-10秒 默认150毫秒
            //SRE.EndSilenceTimeout = TimeSpan.FromSeconds(2);

            SRE.LoadGrammar(G);

        }

        void G_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string result = e.Result.Text;
            var obj = text.Where(r => r.Trim() == result).SingleOrDefault();
            //if (obj != null)
            {
                lblanswer.Invoke(() => {
                    lblanswer.Text = result.ToString();
                });
                switch (result)
                {
                    case "中国":
                        Beep(500, 500);//已识别提示音
                        shibie = "中国：五星红旗";
                        choice(0);
                        break;
                    case "测试":
                        Beep(500, 500);//已识别提示音
                        shibie = "测试了";
                        choice(1);
                        break;
                    case "英国":
                        Beep(500, 500);//已识别提示音
                        shibie = "英国：米字旗";
                        choice(2);

                        break;
                    case "左边":
                        Beep(500, 500);//已识别提示音
                        shibie = "正在向左边行驶";
                        choice(3);
                        break;
                    case "右边":
                        Beep(500, 500);//已识别提示音
                        shibie = "正在向右边行驶";
                        choice(4);
                        break;
                    case "左前方":
                        Beep(500, 500);//已识别提示音
                        shibie = "正在向左前方行驶";
                        choice(5);
                        break;
                    case "右前方":
                        Beep(500, 500);//已识别提示音
                        shibie = "正在向右前方行驶";
                        choice(6);
                        break;
                    case "我爱你":
                        Beep(500, 500);//已识别提示音
                        shibie = "我也爱你";
                        choice(7);
                        break;
                    case "hello":
                        Beep(500, 500);//已识别提示音
                        shibie = "i am fine!";
                        choice(8);
                        break;
                    case "吃饭":
                        Beep(500, 500);//已识别提示音
                        shibie = "今天心情不好不想吃了";
                        choice(9);
                        break;
                }
            }

            //button1.Invoke(() =>
            //{
            //    button1.Text = "start";
            //    SRE.RecognizeAsyncStop();
            //});

        }

        void SpeekAnswer()  //线程
        {
            switch (wordid)
            {
                case 0:
                    SS.Speak("五星红旗");

                    break;
                case 1:
                    SS.Speak("测试");

                    break;
                case 2:
                    SS.Speak("米字旗");

                    break;
                case 3:
                    SS.Speak("正在向左边行驶");
                    break;
                case 4:
                    SS.Speak("正在向右边行驶");
                    break;
                case 5:
                    SS.Speak("正在向左前方行驶");
                    break;
                case 6:
                    SS.Speak("正在向右前方行驶");
                    break;
                case 7:
                    SS.Speak("我也爱你");
                    break;
                case 8:
                    SS.Speak("i am fine!");
                    break;
                case 9:
                    SS.Speak("今天心情不好不想吃了");
                    break;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitVoice();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (SRE.AudioState == AudioState.Stopped)
            {
                button1.Text = "stop";
                SRE.RecognizeAsync(RecognizeMode.Multiple);
            }

            //if (SRE_listening == false)
            //{


            //}
            //else
            //{
            //    button1.Text = "start";
            //    SRE.RecognizeAsyncStop();
            //}
            //lblanswer.Text = "";
            //SRE_listening = !SRE_listening;
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

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Text = "start";
            SRE.RecognizeAsyncStop();
        }
    }
}