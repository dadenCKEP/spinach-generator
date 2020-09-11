namespace spinach_generator
{
    partial class Settings
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.textBox_dirPath = new System.Windows.Forms.TextBox();
            this.textBox_shuhouDir = new System.Windows.Forms.TextBox();
            this.textBox_nippouDir = new System.Windows.Forms.TextBox();
            this.button_selectDir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_create = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_userName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox_dirPath
            // 
            this.textBox_dirPath.Location = new System.Drawing.Point(116, 41);
            this.textBox_dirPath.Name = "textBox_dirPath";
            this.textBox_dirPath.ReadOnly = true;
            this.textBox_dirPath.Size = new System.Drawing.Size(150, 19);
            this.textBox_dirPath.TabIndex = 0;
            // 
            // textBox_shuhouDir
            // 
            this.textBox_shuhouDir.Location = new System.Drawing.Point(116, 97);
            this.textBox_shuhouDir.Name = "textBox_shuhouDir";
            this.textBox_shuhouDir.Size = new System.Drawing.Size(150, 19);
            this.textBox_shuhouDir.TabIndex = 3;
            // 
            // textBox_nippouDir
            // 
            this.textBox_nippouDir.Location = new System.Drawing.Point(116, 72);
            this.textBox_nippouDir.Name = "textBox_nippouDir";
            this.textBox_nippouDir.Size = new System.Drawing.Size(150, 19);
            this.textBox_nippouDir.TabIndex = 2;
            // 
            // button_selectDir
            // 
            this.button_selectDir.Location = new System.Drawing.Point(272, 39);
            this.button_selectDir.Name = "button_selectDir";
            this.button_selectDir.Size = new System.Drawing.Size(75, 23);
            this.button_selectDir.TabIndex = 1;
            this.button_selectDir.Text = "参照";
            this.button_selectDir.UseVisualStyleBackColor = true;
            this.button_selectDir.Click += new System.EventHandler(this.button_selectDir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "フォルダの設置場所";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "日報フォルダ名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "週報フォルダ名";
            // 
            // button_create
            // 
            this.button_create.Location = new System.Drawing.Point(153, 159);
            this.button_create.Name = "button_create";
            this.button_create.Size = new System.Drawing.Size(75, 23);
            this.button_create.TabIndex = 5;
            this.button_create.Text = "作成";
            this.button_create.UseVisualStyleBackColor = true;
            this.button_create.Click += new System.EventHandler(this.button_create_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(294, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "初期設定を行い、設定ファイルを作成します。";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "ユーザ名";
            // 
            // textBox_userName
            // 
            this.textBox_userName.Location = new System.Drawing.Point(116, 122);
            this.textBox_userName.Name = "textBox_userName";
            this.textBox_userName.Size = new System.Drawing.Size(150, 19);
            this.textBox_userName.TabIndex = 4;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 212);
            this.Controls.Add(this.textBox_userName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_create);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_selectDir);
            this.Controls.Add(this.textBox_nippouDir);
            this.Controls.Add(this.textBox_shuhouDir);
            this.Controls.Add(this.textBox_dirPath);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textBox_dirPath;
        private System.Windows.Forms.TextBox textBox_shuhouDir;
        private System.Windows.Forms.TextBox textBox_nippouDir;
        private System.Windows.Forms.Button button_selectDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_create;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_userName;
    }
}