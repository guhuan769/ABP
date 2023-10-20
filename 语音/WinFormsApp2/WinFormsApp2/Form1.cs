using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using NAudio.CoreAudioApi;

//Ҫ��ӵ�����
using NAudio.Wave;//����¼�빤��
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;//����json����
using Vosk;//����ʶ��ģ��



namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string temp;//����һ�����������洢ʶ����
        string temp_old;//����һ��old�����������ж�ʶ��������Ƿ�仯
        ManualResetEvent resetEvent = new ManualResetEvent(true);//����һ��֪ͨ�̵߳Ķ���
        WasapiCapture waveIn = new WasapiCapture();//������Ƶ����
        private void voskRecoder(VoskRecognizer rec, WasapiCapture waveIn)//����ʶ����
        {
            waveIn.DataAvailable += (_, a) =>
            {
                if (rec.AcceptWaveform(a.Buffer, a.BytesRecorded) && (checkBox1.Checked == false))
                {
                    JObject jo = JsonConvert.DeserializeObject<JObject>(rec.FinalResult());//��ʶ����תΪjson
                    string result = jo["text"].ToString().Replace(" ", "");
                    if (result != "") this.Invoke((EventHandler)(delegate { richTextBox1.AppendText(result + "\n"); }));
                }
                else if (checkBox1.Checked)
                {
                    temp_old = temp;
                    JObject jo = JsonConvert.DeserializeObject<JObject>(rec.PartialResult());//��ʶ����תΪjson
                    temp = jo["partial"].ToString().Replace(" ", "");//ȥ��partial��ֵ��Ŀո�
                    if (temp != temp_old) this.Invoke((EventHandler)(delegate { richTextBox1.Text = "\r" + temp; }));
                }
            };
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (button1.Text == "��ʼʶ��")
            {
                button1.Text = "ֹͣʶ��";
                resetEvent.Set();
                waveIn.StartRecording();//��ʼ¼��
            }
            else
            {
                button1.Text = "��ʼʶ��";
                resetEvent.Reset();
                waveIn.StopRecording();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                //��ʼ������ʶ��ģ��
                Vosk.Vosk.SetLogLevel(0);
                Vosk.Model model = new Vosk.Model("models");//����ģ��

                VoskRecognizer rec = new VoskRecognizer(model, waveIn.WaveFormat.SampleRate);//��ʼ��ʶ��ģ��
                waveIn.WaveFormat = new WaveFormat(44100, 16, 1);//������Ƶ�������
                rec.SetMaxAlternatives(0);//ʶ��ѡ�������Ĭ��Ϊ0��������Ҫһ�ֽ��
                rec.SetWords(false);//ʶ����ɺ��Ƿ���Ҫ��ʾÿ�����ʳ��ֺͽ�����ʱ��
                voskRecoder(rec, waveIn);//��ʼִ������ʶ����
            });
        }


    }
}
