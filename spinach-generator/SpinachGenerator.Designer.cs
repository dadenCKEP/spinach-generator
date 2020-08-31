namespace spinach_generator
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_nippou = new System.Windows.Forms.Button();
            this.button_shuhou = new System.Windows.Forms.Button();
            this.label_date = new System.Windows.Forms.Label();
            this.button_shuho_cp = new System.Windows.Forms.Button();
            this.button_nippo_cp = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_nippou
            // 
            this.button_nippou.Location = new System.Drawing.Point(8, 71);
            this.button_nippou.Name = "button_nippou";
            this.button_nippou.Size = new System.Drawing.Size(64, 64);
            this.button_nippou.TabIndex = 1;
            this.button_nippou.Text = "日報作成";
            this.button_nippou.UseVisualStyleBackColor = true;
            this.button_nippou.Click += new System.EventHandler(this.button_nippou_Click);
            // 
            // button_shuhou
            // 
            this.button_shuhou.Location = new System.Drawing.Point(78, 71);
            this.button_shuhou.Name = "button_shuhou";
            this.button_shuhou.Size = new System.Drawing.Size(64, 64);
            this.button_shuhou.TabIndex = 2;
            this.button_shuhou.Text = "週報作成";
            this.button_shuhou.UseVisualStyleBackColor = true;
            this.button_shuhou.Click += new System.EventHandler(this.button_shuhou_Click);
            // 
            // label_date
            // 
            this.label_date.AutoSize = true;
            this.label_date.Font = new System.Drawing.Font("MS UI Gothic", 18F);
            this.label_date.Location = new System.Drawing.Point(12, 9);
            this.label_date.Name = "label_date";
            this.label_date.Size = new System.Drawing.Size(130, 24);
            this.label_date.TabIndex = 3;
            this.label_date.Text = "1970/01/01";
            // 
            // button_shuho_cp
            // 
            this.button_shuho_cp.Location = new System.Drawing.Point(78, 141);
            this.button_shuho_cp.Name = "button_shuho_cp";
            this.button_shuho_cp.Size = new System.Drawing.Size(64, 64);
            this.button_shuho_cp.TabIndex = 5;
            this.button_shuho_cp.Text = "週報コピー";
            this.button_shuho_cp.UseVisualStyleBackColor = true;
            this.button_shuho_cp.Click += new System.EventHandler(this.button_shuho_cp_Click);
            // 
            // button_nippo_cp
            // 
            this.button_nippo_cp.Location = new System.Drawing.Point(8, 141);
            this.button_nippo_cp.Name = "button_nippo_cp";
            this.button_nippo_cp.Size = new System.Drawing.Size(64, 64);
            this.button_nippo_cp.TabIndex = 4;
            this.button_nippo_cp.Text = "日報コピー";
            this.button_nippo_cp.UseVisualStyleBackColor = true;
            this.button_nippo_cp.Click += new System.EventHandler(this.button_nippo_cp_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(148, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 193);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 217);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_shuho_cp);
            this.Controls.Add(this.button_nippo_cp);
            this.Controls.Add(this.label_date);
            this.Controls.Add(this.button_shuhou);
            this.Controls.Add(this.button_nippou);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Spinach Generator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_nippou;
        private System.Windows.Forms.Button button_shuhou;
        private System.Windows.Forms.Label label_date;
        private System.Windows.Forms.Button button_shuho_cp;
        private System.Windows.Forms.Button button_nippo_cp;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

