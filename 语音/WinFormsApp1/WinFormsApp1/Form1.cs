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
        private SpeechRecognitionEngine SRE = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("zh-CN")); //����ʶ��ģ�� 
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
        string[] text = new string[] { "�й�", "����", "Ӣ��", "���", "�ұ�", "��ǰ��", "��ǰ��", "�Ұ���", "�Է�" };

        public void InitVoice()  //����ʶ���ʼ��
        {
            //SS.SelectVoice("lily");
            label1.Text = string.Empty;
            foreach (string s in text)
            {
                label1.Text += $"{s} -";
            }
            label1.Text = label1.Text.TrimEnd('-');
            SRE.SetInputToDefaultAudioDevice();  // Ĭ�ϵ����������豸��Ҳ�����趨Ϊȥʶ��һ��WAV��

            GrammarBuilder GB = new GrammarBuilder();

            GB.Append(new Choices(text));
            //GB.AppendDictation();

            DictationGrammar DG = new DictationGrammar();

            //��������ʶ���﷨������ʱ����Ӧ�ó��������֮����������ʶ��Լ����
            Grammar G = new Grammar(GB);

            //ע������ʶ����¼�
            G.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(G_SpeechRecognized);  //ע������ʶ���¼�

            //������ֹʶ���������0-10�� Ĭ��150����
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
                    case "�й�":
                        Beep(500, 500);//��ʶ����ʾ��
                        shibie = "�й������Ǻ���";
                        choice(0);
                        break;
                    case "����":
                        Beep(500, 500);//��ʶ����ʾ��
                        shibie = "������";
                        choice(1);
                        break;
                    case "Ӣ��":
                        Beep(500, 500);//��ʶ����ʾ��
                        shibie = "Ӣ����������";
                        choice(2);

                        break;
                    case "���":
                        Beep(500, 500);//��ʶ����ʾ��
                        shibie = "�����������ʻ";
                        choice(3);
                        break;
                    case "�ұ�":
                        Beep(500, 500);//��ʶ����ʾ��
                        shibie = "�������ұ���ʻ";
                        choice(4);
                        break;
                    case "��ǰ��":
                        Beep(500, 500);//��ʶ����ʾ��
                        shibie = "��������ǰ����ʻ";
                        choice(5);
                        break;
                    case "��ǰ��":
                        Beep(500, 500);//��ʶ����ʾ��
                        shibie = "��������ǰ����ʻ";
                        choice(6);
                        break;
                    case "�Ұ���":
                        Beep(500, 500);//��ʶ����ʾ��
                        shibie = "��Ҳ����";
                        choice(7);
                        break;
                    case "hello":
                        Beep(500, 500);//��ʶ����ʾ��
                        shibie = "i am fine!";
                        choice(8);
                        break;
                    case "�Է�":
                        Beep(500, 500);//��ʶ����ʾ��
                        shibie = "�������鲻�ò������";
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

        void SpeekAnswer()  //�߳�
        {
            switch (wordid)
            {
                case 0:
                    SS.Speak("���Ǻ���");

                    break;
                case 1:
                    SS.Speak("����");

                    break;
                case 2:
                    SS.Speak("������");

                    break;
                case 3:
                    SS.Speak("�����������ʻ");
                    break;
                case 4:
                    SS.Speak("�������ұ���ʻ");
                    break;
                case 5:
                    SS.Speak("��������ǰ����ʻ");
                    break;
                case 6:
                    SS.Speak("��������ǰ����ʻ");
                    break;
                case 7:
                    SS.Speak("��Ҳ����");
                    break;
                case 8:
                    SS.Speak("i am fine!");
                    break;
                case 9:
                    SS.Speak("�������鲻�ò������");
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
        void ShowAnswer()  //�߳�
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