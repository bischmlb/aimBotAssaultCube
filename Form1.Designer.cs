namespace bischExe
{
    partial class bischExe
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GameChoice = new System.Windows.Forms.ComboBox();
            this.ProcessTMR = new System.Windows.Forms.Timer(this.components);
            this.xPosLabel = new System.Windows.Forms.Label();
            this.yPosLabel = new System.Windows.Forms.Label();
            this.zPosLabel = new System.Windows.Forms.Label();
            this.xMouse = new System.Windows.Forms.Label();
            this.yMouse = new System.Windows.Forms.Label();
            this.playerHealth = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MV Boli", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(313, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = " Aimbot";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(96, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "Vælg Client";
            // 
            // GameChoice
            // 
            this.GameChoice.BackColor = System.Drawing.Color.White;
            this.GameChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GameChoice.FormattingEnabled = true;
            this.GameChoice.Location = new System.Drawing.Point(111, 121);
            this.GameChoice.Name = "GameChoice";
            this.GameChoice.Size = new System.Drawing.Size(103, 21);
            this.GameChoice.TabIndex = 4;
            this.GameChoice.SelectedIndexChanged += new System.EventHandler(this.GameChoice_SelectedIndexChanged);
            this.GameChoice.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GameChoice_Click);
            // 
            // ProcessTMR
            // 
            this.ProcessTMR.Interval = 10;
            this.ProcessTMR.Tick += new System.EventHandler(this.ProcessTMR_Tick);
            // 
            // xPosLabel
            // 
            this.xPosLabel.AutoSize = true;
            this.xPosLabel.Font = new System.Drawing.Font("Lucida Console", 15.75F, System.Drawing.FontStyle.Bold);
            this.xPosLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.xPosLabel.Location = new System.Drawing.Point(406, 131);
            this.xPosLabel.Name = "xPosLabel";
            this.xPosLabel.Size = new System.Drawing.Size(66, 21);
            this.xPosLabel.TabIndex = 7;
            this.xPosLabel.Text = "xPos";
            this.xPosLabel.Click += new System.EventHandler(this.XPosLabel_Click);
            // 
            // yPosLabel
            // 
            this.yPosLabel.AutoSize = true;
            this.yPosLabel.Font = new System.Drawing.Font("Lucida Console", 15.75F, System.Drawing.FontStyle.Bold);
            this.yPosLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.yPosLabel.Location = new System.Drawing.Point(405, 152);
            this.yPosLabel.Name = "yPosLabel";
            this.yPosLabel.Size = new System.Drawing.Size(66, 21);
            this.yPosLabel.TabIndex = 8;
            this.yPosLabel.Text = "yPos";
            // 
            // zPosLabel
            // 
            this.zPosLabel.AutoSize = true;
            this.zPosLabel.Font = new System.Drawing.Font("Lucida Console", 15.75F, System.Drawing.FontStyle.Bold);
            this.zPosLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.zPosLabel.Location = new System.Drawing.Point(406, 173);
            this.zPosLabel.Name = "zPosLabel";
            this.zPosLabel.Size = new System.Drawing.Size(66, 21);
            this.zPosLabel.TabIndex = 9;
            this.zPosLabel.Text = "zPos";
            // 
            // xMouse
            // 
            this.xMouse.AutoSize = true;
            this.xMouse.Font = new System.Drawing.Font("Lucida Console", 15.75F, System.Drawing.FontStyle.Bold);
            this.xMouse.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.xMouse.Location = new System.Drawing.Point(406, 210);
            this.xMouse.Name = "xMouse";
            this.xMouse.Size = new System.Drawing.Size(94, 21);
            this.xMouse.TabIndex = 10;
            this.xMouse.Text = "xMouse";
            this.xMouse.Click += new System.EventHandler(this.XMouse_Click);
            // 
            // yMouse
            // 
            this.yMouse.AutoSize = true;
            this.yMouse.Font = new System.Drawing.Font("Lucida Console", 15.75F, System.Drawing.FontStyle.Bold);
            this.yMouse.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.yMouse.Location = new System.Drawing.Point(406, 231);
            this.yMouse.Name = "yMouse";
            this.yMouse.Size = new System.Drawing.Size(94, 21);
            this.yMouse.TabIndex = 11;
            this.yMouse.Text = "yMouse";
            // 
            // playerHealth
            // 
            this.playerHealth.AutoSize = true;
            this.playerHealth.Font = new System.Drawing.Font("Lucida Console", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerHealth.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.playerHealth.Location = new System.Drawing.Point(405, 97);
            this.playerHealth.Name = "playerHealth";
            this.playerHealth.Size = new System.Drawing.Size(94, 21);
            this.playerHealth.TabIndex = 12;
            this.playerHealth.Text = "Health";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MV Boli", 20.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(-4, 301);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 34);
            this.label3.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(777, 334);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.playerHealth);
            this.Controls.Add(this.yMouse);
            this.Controls.Add(this.xMouse);
            this.Controls.Add(this.zPosLabel);
            this.Controls.Add(this.yPosLabel);
            this.Controls.Add(this.xPosLabel);
            this.Controls.Add(this.GameChoice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "bisches\' AssaultCube Aimbot";
            this.Load += new System.EventHandler(this.CustomAimbot_Load);
            this.Click += new System.EventHandler(this.GameChoice_SelectedIndexChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox GameChoice;
        private System.Windows.Forms.Timer ProcessTMR;
        private System.Windows.Forms.Label xPosLabel;
        private System.Windows.Forms.Label yPosLabel;
        private System.Windows.Forms.Label zPosLabel;
        private System.Windows.Forms.Label xMouse;
        private System.Windows.Forms.Label yMouse;
        private System.Windows.Forms.Label playerHealth;
        private System.Windows.Forms.Label label3;
    }
}

