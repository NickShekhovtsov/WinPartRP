using System;

namespace WinFormPlayer
{
    partial class RemotePlayer
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemotePlayer));
            this.button1 = new System.Windows.Forms.Button();
            this.buPrev = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buNext = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.laName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(53, 302);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(272, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buPrev
            // 
            this.buPrev.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.buPrev.ForeColor = System.Drawing.Color.White;
            this.buPrev.Location = new System.Drawing.Point(53, 332);
            this.buPrev.Name = "buPrev";
            this.buPrev.Size = new System.Drawing.Size(86, 58);
            this.buPrev.TabIndex = 1;
            this.buPrev.Text = "<<";
            this.buPrev.UseVisualStyleBackColor = false;
            this.buPrev.Click += new System.EventHandler(this.buPrev_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.button3.ForeColor = System.Drawing.Color.Transparent;
            this.button3.Location = new System.Drawing.Point(146, 332);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 58);
            this.button3.TabIndex = 2;
            this.button3.Text = "►";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(53, 58);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(273, 238);
            this.listBox1.TabIndex = 3;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
            // 
            // buNext
            // 
            this.buNext.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.buNext.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buNext.Location = new System.Drawing.Point(239, 332);
            this.buNext.Name = "buNext";
            this.buNext.Size = new System.Drawing.Size(86, 58);
            this.buNext.TabIndex = 4;
            this.buNext.Text = ">>";
            this.buNext.UseVisualStyleBackColor = false;
            this.buNext.Click += new System.EventHandler(this.buNext_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 10;
            this.trackBar1.Location = new System.Drawing.Point(355, 81);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 266);
            this.trackBar1.SmallChange = 10;
            this.trackBar1.TabIndex = 20;
            this.trackBar1.Value = 50;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // laName
            // 
            this.laName.AutoSize = true;
            this.laName.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.laName.Location = new System.Drawing.Point(29, 32);
            this.laName.Name = "laName";
            this.laName.Size = new System.Drawing.Size(20, 16);
            this.laName.TabIndex = 6;
            this.laName.Text = "...";
            this.laName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.laName.DockChanged += new System.EventHandler(this.laName_DockChanged);
            this.laName.Click += new System.EventHandler(this.laName_Click);
            // 
            // RemotePlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(408, 423);
            this.Controls.Add(this.laName);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.buNext);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buPrev);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Cornsilk;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(424, 462);
            this.MinimumSize = new System.Drawing.Size(424, 462);
            this.Name = "RemotePlayer";
            this.Text = "RemotePlayer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void laName_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buPrev;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buNext;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label laName;
    }
}


