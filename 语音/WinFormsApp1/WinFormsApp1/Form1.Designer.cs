namespace WinFormsApp1
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
            button1 = new Button();
            lblanswer = new TextBox();
            label1 = new Label();
            button2 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(156, 98);
            button1.TabIndex = 0;
            button1.Text = "start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lblanswer
            // 
            lblanswer.Location = new Point(12, 116);
            lblanswer.Multiline = true;
            lblanswer.Name = "lblanswer";
            lblanswer.Size = new Size(1317, 322);
            lblanswer.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(354, 12);
            label1.Name = "label1";
            label1.Size = new Size(63, 24);
            label1.TabIndex = 3;
            label1.Text = "label1";
            // 
            // button2
            // 
            button2.Location = new Point(174, 12);
            button2.Name = "button2";
            button2.Size = new Size(160, 98);
            button2.TabIndex = 4;
            button2.Text = "stop";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1341, 450);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(lblanswer);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox lblanswer;
        private Label label1;
        private Button button2;
    }
}