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
        private SpeechRecognitionEngine SRE = new SpeechRecognitionEngine(); //����ʶ��ģ��
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


        public void InitVoice()  //����ʶ���ʼ��
        {
            //SS.SelectVoice("lily");
            SRE.SetInputToDefaultAudioDevice();  // Ĭ�ϵ����������豸��Ҳ�����趨Ϊȥʶ��һ��WAV��

            GrammarBuilder GB = new GrammarBuilder();

            GB.Append(new Choices(new string[] { "�й�", "����", "Ӣ��", "��ʺ" }));

            DictationGrammar DG = new DictationGrammar();

            Grammar G = new Grammar(GB);

            G.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(G_SpeechRecognized);  //ע������ʶ���¼�

            SRE.EndSilenceTimeout = TimeSpan.FromSeconds(2);

            SRE.LoadGrammar(G);

        }

        void G_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Beep(500, 500);//��ʶ����ʾ��

            string result = e.Result.Text;
            switch (result)
            {
                case "�й�":
                    shibie = "�й������Ǻ���";
                    choice(0);
                    break;
                case "����":
                    shibie = "������������";
                    choice(1);
                    break;
                case "Ӣ��":
                    shibie = "Ӣ����������";
                    choice(2);

                    break;
                case "��ʺ":
                    shibie = "���Ѿ�����";
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
        void ShowAnswer()  //�߳�
        {
            MethodInvoker mi = new MethodInvoker(this.dosomething);
            this.BeginInvoke(mi);

        }
        void dosomething()
        {
            lblanswer.Text = shibie;
        }
        void SpeekAnswer()  //�߳�
        {
            switch (wordid)
            {
                case 0:
                    SS.Speak("���Ǻ���");

                    break;
                case 1:
                    SS.Speak("������");

                    break;
                case 2:
                    SS.Speak("������");

                    break;
                case 3:
                    SS.Speak("���Ѿ�����");
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //nAudioRecordHelper.StopRecordAudio();
        }
    }
}