namespace WinFormsApp2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richTextBox1 = new RichTextBox();
            button1 = new Button();
            button2 = new Button();
            checkBox1 = new CheckBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 81);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(1382, 357);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // button1
            // 
            button1.Location = new Point(12, 19);
            button1.Name = "button1";
            button1.Size = new Size(151, 56);
            button1.TabIndex = 1;
            button1.Text = "开始识别";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(169, 19);
            button2.Name = "button2";
            button2.Size = new Size(151, 56);
            button2.TabIndex = 2;
            button2.Text = "清空";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(332, 37);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(90, 28);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "文字流";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(452, 35);
            label1.Name = "label1";
            label1.Size = new Size(752, 24);
            label1.TabIndex = 4;
            label1.Text = "请阅读工具:汽车、小车、摩托车、微信、视频、电话、沙发、桌子、电脑、空调、饼干、电灯";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1402, 450);
            Controls.Add(label1);
            Controls.Add(checkBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(richTextBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private Button button1;
        private Button button2;
        private CheckBox checkBox1;
        private Label label1;
    }
}