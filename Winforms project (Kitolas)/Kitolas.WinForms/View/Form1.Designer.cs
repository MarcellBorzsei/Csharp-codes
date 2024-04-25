namespace Kitolas
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
            this.gameTablePB = new System.Windows.Forms.PictureBox();
            this.gameSetButton3 = new System.Windows.Forms.Button();
            this.gameSetButton4 = new System.Windows.Forms.Button();
            this.gameSetButton6 = new System.Windows.Forms.Button();
            this.readyButton = new System.Windows.Forms.Button();
            this.blackStoneCounterLabel = new System.Windows.Forms.Label();
            this.whiteStoneCounterLabel = new System.Windows.Forms.Label();
            this.blackStoneSit = new System.Windows.Forms.Label();
            this.whiteStoneSit = new System.Windows.Forms.Label();
            this.playerTrackerLabel = new System.Windows.Forms.Label();
            this.gameTableLabel = new System.Windows.Forms.Label();
            this.RestartButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveStripButton = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadStripButton = new System.Windows.Forms.ToolStripMenuItem();
            this._saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.circleNumLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gameTablePB)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gameTablePB
            // 
            this.gameTablePB.Location = new System.Drawing.Point(290, 31);
            this.gameTablePB.Name = "gameTablePB";
            this.gameTablePB.Size = new System.Drawing.Size(497, 417);
            this.gameTablePB.TabIndex = 0;
            this.gameTablePB.TabStop = false;
            // 
            // gameSetButton3
            // 
            this.gameSetButton3.Location = new System.Drawing.Point(88, 63);
            this.gameSetButton3.Name = "gameSetButton3";
            this.gameSetButton3.Size = new System.Drawing.Size(94, 29);
            this.gameSetButton3.TabIndex = 1;
            this.gameSetButton3.Text = "3";
            this.gameSetButton3.UseVisualStyleBackColor = true;
            this.gameSetButton3.Click += new System.EventHandler(this.displayButton_Click);
            // 
            // gameSetButton4
            // 
            this.gameSetButton4.Location = new System.Drawing.Point(88, 100);
            this.gameSetButton4.Name = "gameSetButton4";
            this.gameSetButton4.Size = new System.Drawing.Size(94, 29);
            this.gameSetButton4.TabIndex = 2;
            this.gameSetButton4.Text = "4";
            this.gameSetButton4.UseVisualStyleBackColor = true;
            this.gameSetButton4.Click += new System.EventHandler(this.displayButton_Click);
            // 
            // gameSetButton6
            // 
            this.gameSetButton6.Location = new System.Drawing.Point(88, 135);
            this.gameSetButton6.Name = "gameSetButton6";
            this.gameSetButton6.Size = new System.Drawing.Size(94, 29);
            this.gameSetButton6.TabIndex = 3;
            this.gameSetButton6.Text = "6";
            this.gameSetButton6.UseVisualStyleBackColor = true;
            this.gameSetButton6.Click += new System.EventHandler(this.displayButton_Click);
            // 
            // readyButton
            // 
            this.readyButton.Location = new System.Drawing.Point(77, 170);
            this.readyButton.Name = "readyButton";
            this.readyButton.Size = new System.Drawing.Size(114, 49);
            this.readyButton.TabIndex = 4;
            this.readyButton.Text = "Ready";
            this.readyButton.UseVisualStyleBackColor = true;
            this.readyButton.Click += new System.EventHandler(this.gameStart);
            // 
            // blackStoneCounterLabel
            // 
            this.blackStoneCounterLabel.AutoSize = true;
            this.blackStoneCounterLabel.Location = new System.Drawing.Point(25, 433);
            this.blackStoneCounterLabel.Name = "blackStoneCounterLabel";
            this.blackStoneCounterLabel.Size = new System.Drawing.Size(0, 20);
            this.blackStoneCounterLabel.TabIndex = 5;
            // 
            // whiteStoneCounterLabel
            // 
            this.whiteStoneCounterLabel.AutoSize = true;
            this.whiteStoneCounterLabel.Location = new System.Drawing.Point(25, 469);
            this.whiteStoneCounterLabel.Name = "whiteStoneCounterLabel";
            this.whiteStoneCounterLabel.Size = new System.Drawing.Size(0, 20);
            this.whiteStoneCounterLabel.TabIndex = 6;
            // 
            // blackStoneSit
            // 
            this.blackStoneSit.AutoSize = true;
            this.blackStoneSit.Location = new System.Drawing.Point(125, 593);
            this.blackStoneSit.Name = "blackStoneSit";
            this.blackStoneSit.Size = new System.Drawing.Size(0, 20);
            this.blackStoneSit.TabIndex = 7;
            // 
            // whiteStoneSit
            // 
            this.whiteStoneSit.AutoSize = true;
            this.whiteStoneSit.Location = new System.Drawing.Point(125, 626);
            this.whiteStoneSit.Name = "whiteStoneSit";
            this.whiteStoneSit.Size = new System.Drawing.Size(0, 20);
            this.whiteStoneSit.TabIndex = 8;
            // 
            // playerTrackerLabel
            // 
            this.playerTrackerLabel.AutoSize = true;
            this.playerTrackerLabel.Location = new System.Drawing.Point(25, 382);
            this.playerTrackerLabel.Name = "playerTrackerLabel";
            this.playerTrackerLabel.Size = new System.Drawing.Size(0, 20);
            this.playerTrackerLabel.TabIndex = 9;
            // 
            // gameTableLabel
            // 
            this.gameTableLabel.AutoSize = true;
            this.gameTableLabel.Location = new System.Drawing.Point(873, 72);
            this.gameTableLabel.Name = "gameTableLabel";
            this.gameTableLabel.Size = new System.Drawing.Size(0, 20);
            this.gameTableLabel.TabIndex = 10;
            // 
            // RestartButton
            // 
            this.RestartButton.Location = new System.Drawing.Point(77, 226);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(113, 52);
            this.RestartButton.TabIndex = 12;
            this.RestartButton.Text = "Restart";
            this.RestartButton.UseVisualStyleBackColor = true;
            this.RestartButton.Click += new System.EventHandler(this.RestartButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1168, 28);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveStripButton,
            this.LoadStripButton});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // SaveStripButton
            // 
            this.SaveStripButton.Name = "SaveStripButton";
            this.SaveStripButton.Size = new System.Drawing.Size(212, 26);
            this.SaveStripButton.Text = "Save Game";
            this.SaveStripButton.Click += new System.EventHandler(this.SaveStripButton_Click);
            // 
            // LoadStripButton
            // 
            this.LoadStripButton.Name = "LoadStripButton";
            this.LoadStripButton.Size = new System.Drawing.Size(212, 26);
            this.LoadStripButton.Text = "Load Saved Game";
            this.LoadStripButton.Click += new System.EventHandler(this.LoadStripButton_Click);
            // 
            // _openFileDialog
            // 
            this._openFileDialog.FileName = "openFileDialog1";
            // 
            // circleNumLabel
            // 
            this.circleNumLabel.AutoSize = true;
            this.circleNumLabel.Location = new System.Drawing.Point(25, 530);
            this.circleNumLabel.Name = "circleNumLabel";
            this.circleNumLabel.Size = new System.Drawing.Size(0, 20);
            this.circleNumLabel.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 667);
            this.Controls.Add(this.circleNumLabel);
            this.Controls.Add(this.RestartButton);
            this.Controls.Add(this.gameTableLabel);
            this.Controls.Add(this.playerTrackerLabel);
            this.Controls.Add(this.whiteStoneSit);
            this.Controls.Add(this.blackStoneSit);
            this.Controls.Add(this.whiteStoneCounterLabel);
            this.Controls.Add(this.blackStoneCounterLabel);
            this.Controls.Add(this.readyButton);
            this.Controls.Add(this.gameSetButton6);
            this.Controls.Add(this.gameSetButton4);
            this.Controls.Add(this.gameSetButton3);
            this.Controls.Add(this.gameTablePB);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gameTablePB)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox gameTablePB;
        private Button gameSetButton3;
        private Button gameSetButton4;
        private Button gameSetButton6;
        private Button readyButton;
        private Label blackStoneCounterLabel;
        private Label whiteStoneCounterLabel;
        private Label blackStoneSit;
        private Label whiteStoneSit;
        private Label playerTrackerLabel;
        private Label gameTableLabel;
        private Button RestartButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem SaveStripButton;
        private ToolStripMenuItem LoadStripButton;
        private SaveFileDialog _saveFileDialog;
        private OpenFileDialog _openFileDialog;
        private Label circleNumLabel;
    }
}