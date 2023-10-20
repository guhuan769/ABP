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

//要添加的引用
using NAudio.Wave;//语音录入工具
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;//处理json数据
using Vosk;//语音识别模型



namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string temp;//定义一个变量用来存储识别结果
        string temp_old;//定义一个old变量，用来判断识别的内容是否变化
        ManualResetEvent resetEvent = new ManualResetEvent(true);//创建一个通知线程的对象
        WasapiCapture waveIn = new WasapiCapture();//创建音频捕获
        private void voskRecoder(VoskRecognizer rec, WasapiCapture waveIn)//语音识别函数
        {
            waveIn.DataAvailable += (_, a) =>
            {
                if (rec.AcceptWaveform(a.Buffer, a.BytesRecorded) && (checkBox1.Checked == false))
                {
                    JObject jo = JsonConvert.DeserializeObject<JObject>(rec.FinalResult());//将识别结果转为json
                    string result = jo["text"].ToString().Replace(" ", "");
                    if (result != "") this.Invoke((EventHandler)(delegate { richTextBox1.AppendText(result + "\n"); }));
                }
                else if (checkBox1.Checked)
                {
                    temp_old = temp;
                    JObject jo = JsonConvert.DeserializeObject<JObject>(rec.PartialResult());//将识别结果转为json
                    temp = jo["partial"].ToString().Replace(" ", "");//去除partial键值里的空格
                    if (temp != temp_old) this.Invoke((EventHandler)(delegate { richTextBox1.Text = "\r" + temp; }));
                }
            };
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (button1.Text == "开始识别")
            {
                button1.Text = "停止识别";
                resetEvent.Set();
                waveIn.StartRecording();//开始录音
            }
            else
            {
                button1.Text = "开始识别";
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
                //初始化语音识别模型
                Vosk.Vosk.SetLogLevel(0);
                Vosk.Model model = new Vosk.Model("models");//加载模型

                VoskRecognizer rec = new VoskRecognizer(model, waveIn.WaveFormat.SampleRate);//初始化识别模型
                waveIn.WaveFormat = new WaveFormat(44100, 16, 1);//定义音频捕获参数
                rec.SetMaxAlternatives(0);//识别备选结果数，默认为0，代表需要一种结果
                rec.SetWords(false);//识别完成后是否需要显示每个单词出现和结束的时间
                voskRecoder(rec, waveIn);//开始执行语音识别函数
            });
        }


    }
}
