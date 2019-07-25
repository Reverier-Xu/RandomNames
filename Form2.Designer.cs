namespace RandomNames
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.CloseBox = new System.Windows.Forms.PictureBox();
            this.daysbox = new System.Windows.Forms.Label();
            this.hoursbox = new System.Windows.Forms.Label();
            this.minsbox = new System.Windows.Forms.Label();
            this.secsbox = new System.Windows.Forms.Label();
            this.tips = new System.Windows.Forms.Label();
            this.TimerUp = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.CloseBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseBox
            // 
            this.CloseBox.BackColor = System.Drawing.Color.Transparent;
            this.CloseBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseBox.BackgroundImage")));
            this.CloseBox.Location = new System.Drawing.Point(456, 12);
            this.CloseBox.Name = "CloseBox";
            this.CloseBox.Size = new System.Drawing.Size(32, 32);
            this.CloseBox.TabIndex = 0;
            this.CloseBox.TabStop = false;
            this.CloseBox.Click += new System.EventHandler(this.CloseBox_Click);
            this.CloseBox.MouseEnter += new System.EventHandler(this.CloseBox_MouseEnter);
            this.CloseBox.MouseLeave += new System.EventHandler(this.CloseBox_MouseLeave);
            // 
            // daysbox
            // 
            this.daysbox.AutoSize = true;
            this.daysbox.BackColor = System.Drawing.Color.Transparent;
            this.daysbox.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.daysbox.ForeColor = System.Drawing.SystemColors.Control;
            this.daysbox.Location = new System.Drawing.Point(20, 119);
            this.daysbox.Name = "daysbox";
            this.daysbox.Size = new System.Drawing.Size(68, 38);
            this.daysbox.TabIndex = 1;
            this.daysbox.Text = "666";
            // 
            // hoursbox
            // 
            this.hoursbox.AutoSize = true;
            this.hoursbox.BackColor = System.Drawing.Color.Transparent;
            this.hoursbox.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hoursbox.ForeColor = System.Drawing.SystemColors.Control;
            this.hoursbox.Location = new System.Drawing.Point(144, 119);
            this.hoursbox.Name = "hoursbox";
            this.hoursbox.Size = new System.Drawing.Size(51, 38);
            this.hoursbox.TabIndex = 2;
            this.hoursbox.Text = "23";
            // 
            // minsbox
            // 
            this.minsbox.AutoSize = true;
            this.minsbox.BackColor = System.Drawing.Color.Transparent;
            this.minsbox.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.minsbox.ForeColor = System.Drawing.SystemColors.Control;
            this.minsbox.Location = new System.Drawing.Point(259, 119);
            this.minsbox.Name = "minsbox";
            this.minsbox.Size = new System.Drawing.Size(51, 38);
            this.minsbox.TabIndex = 3;
            this.minsbox.Text = "59";
            // 
            // secsbox
            // 
            this.secsbox.AutoSize = true;
            this.secsbox.BackColor = System.Drawing.Color.Transparent;
            this.secsbox.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.secsbox.ForeColor = System.Drawing.SystemColors.Control;
            this.secsbox.Location = new System.Drawing.Point(371, 119);
            this.secsbox.Name = "secsbox";
            this.secsbox.Size = new System.Drawing.Size(51, 38);
            this.secsbox.TabIndex = 4;
            this.secsbox.Text = "59";
            // 
            // tips
            // 
            this.tips.AutoSize = true;
            this.tips.BackColor = System.Drawing.Color.Transparent;
            this.tips.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tips.ForeColor = System.Drawing.SystemColors.Control;
            this.tips.Location = new System.Drawing.Point(45, 200);
            this.tips.Name = "tips";
            this.tips.Size = new System.Drawing.Size(402, 28);
            this.tips.TabIndex = 5;
            this.tips.Text = "截止时间为您设置的当天上午9点开考时间";
            // 
            // TimerUp
            // 
            this.TimerUp.Enabled = true;
            this.TimerUp.Tick += new System.EventHandler(this.TimerUp_Tick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(500, 256);
            this.ControlBox = false;
            this.Controls.Add(this.tips);
            this.Controls.Add(this.secsbox);
            this.Controls.Add(this.minsbox);
            this.Controls.Add(this.hoursbox);
            this.Controls.Add(this.daysbox);
            this.Controls.Add(this.CloseBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CloseBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox CloseBox;
        private System.Windows.Forms.Label daysbox;
        private System.Windows.Forms.Label hoursbox;
        private System.Windows.Forms.Label minsbox;
        private System.Windows.Forms.Label secsbox;
        private System.Windows.Forms.Label tips;
        private System.Windows.Forms.Timer TimerUp;
    }
}