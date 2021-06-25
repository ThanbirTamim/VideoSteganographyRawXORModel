
namespace VideoStaganographyImtiaz
{
    partial class Form1
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
            this.btnSelectCoverVideo = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnEmbed = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmbedSecretMessage = new System.Windows.Forms.TextBox();
            this.pictureCover = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRetrieve = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRetrieveSecretMessage = new System.Windows.Forms.TextBox();
            this.pictureStego = new System.Windows.Forms.PictureBox();
            this.btnSelectStegoVideo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTextFile = new System.Windows.Forms.Button();
            this.lblselectStatus = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureStego)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectCoverVideo
            // 
            this.btnSelectCoverVideo.Location = new System.Drawing.Point(22, 98);
            this.btnSelectCoverVideo.Name = "btnSelectCoverVideo";
            this.btnSelectCoverVideo.Size = new System.Drawing.Size(153, 23);
            this.btnSelectCoverVideo.TabIndex = 0;
            this.btnSelectCoverVideo.Text = "Select a video";
            this.btnSelectCoverVideo.UseVisualStyleBackColor = true;
            this.btnSelectCoverVideo.Click += new System.EventHandler(this.btnSelectCoverVideo_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblselectStatus);
            this.splitContainer1.Panel1.Controls.Add(this.btnTextFile);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.txtLog);
            this.splitContainer1.Panel1.Controls.Add(this.btnEmbed);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.txtEmbedSecretMessage);
            this.splitContainer1.Panel1.Controls.Add(this.pictureCover);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnSelectCoverVideo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnDownload);
            this.splitContainer1.Panel2.Controls.Add(this.btnRetrieve);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.txtRetrieveSecretMessage);
            this.splitContainer1.Panel2.Controls.Add(this.pictureStego);
            this.splitContainer1.Panel2.Controls.Add(this.btnSelectStegoVideo);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(800, 552);
            this.splitContainer1.SplitterDistance = 391;
            this.splitContainer1.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 338);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Measurement Metric Log";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(22, 354);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(357, 186);
            this.txtLog.TabIndex = 8;
            // 
            // btnEmbed
            // 
            this.btnEmbed.Location = new System.Drawing.Point(114, 299);
            this.btnEmbed.Name = "btnEmbed";
            this.btnEmbed.Size = new System.Drawing.Size(153, 23);
            this.btnEmbed.TabIndex = 7;
            this.btnEmbed.Text = "Embed";
            this.btnEmbed.UseVisualStyleBackColor = true;
            this.btnEmbed.Click += new System.EventHandler(this.btnEmbed_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Secret a Text file or write some text";
            // 
            // txtEmbedSecretMessage
            // 
            this.txtEmbedSecretMessage.Location = new System.Drawing.Point(22, 207);
            this.txtEmbedSecretMessage.Multiline = true;
            this.txtEmbedSecretMessage.Name = "txtEmbedSecretMessage";
            this.txtEmbedSecretMessage.Size = new System.Drawing.Size(357, 86);
            this.txtEmbedSecretMessage.TabIndex = 5;
            // 
            // pictureCover
            // 
            this.pictureCover.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pictureCover.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureCover.Location = new System.Drawing.Point(268, 64);
            this.pictureCover.Name = "pictureCover";
            this.pictureCover.Size = new System.Drawing.Size(111, 111);
            this.pictureCover.TabIndex = 2;
            this.pictureCover.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(145, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Embed Approach";
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point(137, 207);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(153, 23);
            this.btnRetrieve.TabIndex = 14;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.UseVisualStyleBackColor = true;
            this.btnRetrieve.Click += new System.EventHandler(this.btnRetrieve_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Secret Text";
            // 
            // txtRetrieveSecretMessage
            // 
            this.txtRetrieveSecretMessage.Location = new System.Drawing.Point(36, 258);
            this.txtRetrieveSecretMessage.Multiline = true;
            this.txtRetrieveSecretMessage.Name = "txtRetrieveSecretMessage";
            this.txtRetrieveSecretMessage.Size = new System.Drawing.Size(357, 130);
            this.txtRetrieveSecretMessage.TabIndex = 12;
            // 
            // pictureStego
            // 
            this.pictureStego.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pictureStego.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureStego.Location = new System.Drawing.Point(282, 64);
            this.pictureStego.Name = "pictureStego";
            this.pictureStego.Size = new System.Drawing.Size(111, 111);
            this.pictureStego.TabIndex = 9;
            this.pictureStego.TabStop = false;
            // 
            // btnSelectStegoVideo
            // 
            this.btnSelectStegoVideo.Location = new System.Drawing.Point(36, 98);
            this.btnSelectStegoVideo.Name = "btnSelectStegoVideo";
            this.btnSelectStegoVideo.Size = new System.Drawing.Size(153, 23);
            this.btnSelectStegoVideo.TabIndex = 8;
            this.btnSelectStegoVideo.Text = "Select a video";
            this.btnSelectStegoVideo.UseVisualStyleBackColor = true;
            this.btnSelectStegoVideo.Click += new System.EventHandler(this.btnSelectStegoVideo_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Retrieve Approach";
            // 
            // btnTextFile
            // 
            this.btnTextFile.Location = new System.Drawing.Point(22, 178);
            this.btnTextFile.Name = "btnTextFile";
            this.btnTextFile.Size = new System.Drawing.Size(153, 23);
            this.btnTextFile.TabIndex = 10;
            this.btnTextFile.Text = "Select a .txt file";
            this.btnTextFile.UseVisualStyleBackColor = true;
            this.btnTextFile.Click += new System.EventHandler(this.btnTextFile_Click);
            // 
            // lblselectStatus
            // 
            this.lblselectStatus.AutoSize = true;
            this.lblselectStatus.BackColor = System.Drawing.Color.Green;
            this.lblselectStatus.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblselectStatus.Location = new System.Drawing.Point(181, 183);
            this.lblselectStatus.Name = "lblselectStatus";
            this.lblselectStatus.Size = new System.Drawing.Size(58, 13);
            this.lblselectStatus.TabIndex = 11;
            this.lblselectStatus.Text = "Selected!!!";
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(137, 394);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(153, 23);
            this.btnDownload.TabIndex = 15;
            this.btnDownload.Text = "Download Text";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 552);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Video Steganography";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureCover)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureStego)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectCoverVideo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureCover;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmbedSecretMessage;
        private System.Windows.Forms.Button btnEmbed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnRetrieve;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRetrieveSecretMessage;
        private System.Windows.Forms.PictureBox pictureStego;
        private System.Windows.Forms.Button btnSelectStegoVideo;
        private System.Windows.Forms.Button btnTextFile;
        private System.Windows.Forms.Label lblselectStatus;
        private System.Windows.Forms.Button btnDownload;
    }
}

