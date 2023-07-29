namespace GMap.NET.Downloader
{
    partial class Form_RouteSet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.checkBox_FirstVisible = new System.Windows.Forms.CheckBox();
            this.checkBox_ShowTotal = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OK.Location = new System.Drawing.Point(112, 109);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 0;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.Location = new System.Drawing.Point(193, 109);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 1;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // checkBox_FirstVisible
            // 
            this.checkBox_FirstVisible.AutoSize = true;
            this.checkBox_FirstVisible.Location = new System.Drawing.Point(60, 32);
            this.checkBox_FirstVisible.Name = "checkBox_FirstVisible";
            this.checkBox_FirstVisible.Size = new System.Drawing.Size(120, 16);
            this.checkBox_FirstVisible.TabIndex = 2;
            this.checkBox_FirstVisible.Text = "是否显示开始节点";
            this.checkBox_FirstVisible.UseVisualStyleBackColor = true;
            // 
            // checkBox_ShowTotal
            // 
            this.checkBox_ShowTotal.AutoSize = true;
            this.checkBox_ShowTotal.Location = new System.Drawing.Point(60, 64);
            this.checkBox_ShowTotal.Name = "checkBox_ShowTotal";
            this.checkBox_ShowTotal.Size = new System.Drawing.Size(120, 16);
            this.checkBox_ShowTotal.TabIndex = 3;
            this.checkBox_ShowTotal.Text = "是否显示汇总数据";
            this.checkBox_ShowTotal.UseVisualStyleBackColor = true;
            // 
            // Form_RouteSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 144);
            this.Controls.Add(this.checkBox_ShowTotal);
            this.Controls.Add(this.checkBox_FirstVisible);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Name = "Form_RouteSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "属性设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.CheckBox checkBox_FirstVisible;
        private System.Windows.Forms.CheckBox checkBox_ShowTotal;
    }
}