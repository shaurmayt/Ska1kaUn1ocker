namespace SkalkaUnlocker
{
    partial class about
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(about));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sitelnk = new System.Windows.Forms.PictureBox();
            this.githublnk = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.sitelnk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.githublnk)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(542, 264);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(210, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(277, 39);
            this.label2.TabIndex = 2;
            this.label2.Text = "Да, это анлокер";
            // 
            // sitelnk
            // 
            this.sitelnk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sitelnk.Image = global::SkalkaUnlocker.Properties.Resources.site;
            this.sitelnk.Location = new System.Drawing.Point(644, 321);
            this.sitelnk.Name = "sitelnk";
            this.sitelnk.Size = new System.Drawing.Size(45, 50);
            this.sitelnk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.sitelnk.TabIndex = 4;
            this.sitelnk.TabStop = false;
            this.sitelnk.Click += new System.EventHandler(this.sitelnk_Click);
            // 
            // githublnk
            // 
            this.githublnk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.githublnk.Image = global::SkalkaUnlocker.Properties.Resources.github;
            this.githublnk.Location = new System.Drawing.Point(644, 377);
            this.githublnk.Name = "githublnk";
            this.githublnk.Size = new System.Drawing.Size(45, 50);
            this.githublnk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.githublnk.TabIndex = 3;
            this.githublnk.TabStop = false;
            this.githublnk.Click += new System.EventHandler(this.githublnk_Click);
            // 
            // about
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 426);
            this.Controls.Add(this.sitelnk);
            this.Controls.Add(this.githublnk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "about";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "about";
            this.Load += new System.EventHandler(this.about_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sitelnk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.githublnk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox githublnk;
        private System.Windows.Forms.PictureBox sitelnk;
    }
}