namespace SuDoku {
	partial class SuDokuForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components=null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing&&(components!=null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SuDokuForm));
            this.panelTiles = new System.Windows.Forms.Panel();
            this.pictureTable = new System.Windows.Forms.PictureBox();
            this.panelControl = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.comboLanguage = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textMaxLevel = new System.Windows.Forms.TextBox();
            this.textTryNb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textActLevel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textGameName = new System.Windows.Forms.TextBox();
            this.buttonResolveTable = new System.Windows.Forms.Button();
            this.buttonCheckResolving = new System.Windows.Forms.Button();
            this.buttonSaveGame = new System.Windows.Forms.Button();
            this.comboGameName = new System.Windows.Forms.ComboBox();
            this.textTimer = new System.Windows.Forms.TextBox();
            this.textGameComment = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericLevel = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.buttonClearGame = new System.Windows.Forms.Button();
            this.buttonTestGame = new System.Windows.Forms.Button();
            this.buttonLoadFile = new System.Windows.Forms.Button();
            this.buttonFillGame = new System.Windows.Forms.Button();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboGameType = new System.Windows.Forms.ComboBox();
            this.panelTiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureTable)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTiles
            // 
            resources.ApplyResources(this.panelTiles, "panelTiles");
            this.panelTiles.Controls.Add(this.pictureTable);
            this.panelTiles.Name = "panelTiles";
            // 
            // pictureTable
            // 
            this.pictureTable.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.pictureTable, "pictureTable");
            this.pictureTable.Name = "pictureTable";
            this.pictureTable.TabStop = false;
            this.pictureTable.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureTable_Paint);
            this.pictureTable.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureTable_MouseClick);
            this.pictureTable.Resize += new System.EventHandler(this.pictureTable_Resize);
            // 
            // panelControl
            // 
            resources.ApplyResources(this.panelControl, "panelControl");
            this.panelControl.Controls.Add(this.label9);
            this.panelControl.Controls.Add(this.comboLanguage);
            this.panelControl.Controls.Add(this.label8);
            this.panelControl.Controls.Add(this.textMaxLevel);
            this.panelControl.Controls.Add(this.textTryNb);
            this.panelControl.Controls.Add(this.label7);
            this.panelControl.Controls.Add(this.label6);
            this.panelControl.Controls.Add(this.textActLevel);
            this.panelControl.Controls.Add(this.label5);
            this.panelControl.Controls.Add(this.textGameName);
            this.panelControl.Controls.Add(this.buttonResolveTable);
            this.panelControl.Controls.Add(this.buttonCheckResolving);
            this.panelControl.Controls.Add(this.buttonSaveGame);
            this.panelControl.Controls.Add(this.comboGameName);
            this.panelControl.Controls.Add(this.textTimer);
            this.panelControl.Controls.Add(this.textGameComment);
            this.panelControl.Controls.Add(this.label4);
            this.panelControl.Controls.Add(this.label3);
            this.panelControl.Controls.Add(this.numericLevel);
            this.panelControl.Controls.Add(this.label2);
            this.panelControl.Controls.Add(this.buttonSaveFile);
            this.panelControl.Controls.Add(this.buttonClearGame);
            this.panelControl.Controls.Add(this.buttonTestGame);
            this.panelControl.Controls.Add(this.buttonLoadFile);
            this.panelControl.Controls.Add(this.buttonFillGame);
            this.panelControl.Controls.Add(this.buttonStartGame);
            this.panelControl.Controls.Add(this.buttonExit);
            this.panelControl.Controls.Add(this.label1);
            this.panelControl.Controls.Add(this.comboGameType);
            this.panelControl.Name = "panelControl";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // comboLanguage
            // 
            this.comboLanguage.FormattingEnabled = true;
            this.comboLanguage.Items.AddRange(new object[] {
            resources.GetString("comboLanguage.Items"),
            resources.GetString("comboLanguage.Items1")});
            resources.ApplyResources(this.comboLanguage, "comboLanguage");
            this.comboLanguage.Name = "comboLanguage";
            this.comboLanguage.SelectedIndexChanged += new System.EventHandler(this.comboLanguage_SelectedIndexChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // textMaxLevel
            // 
            resources.ApplyResources(this.textMaxLevel, "textMaxLevel");
            this.textMaxLevel.Name = "textMaxLevel";
            // 
            // textTryNb
            // 
            resources.ApplyResources(this.textTryNb, "textTryNb");
            this.textTryNb.Name = "textTryNb";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // textActLevel
            // 
            resources.ApplyResources(this.textActLevel, "textActLevel");
            this.textActLevel.Name = "textActLevel";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textGameName
            // 
            resources.ApplyResources(this.textGameName, "textGameName");
            this.textGameName.Name = "textGameName";
            // 
            // buttonResolveTable
            // 
            this.buttonResolveTable.BackColor = System.Drawing.Color.GreenYellow;
            resources.ApplyResources(this.buttonResolveTable, "buttonResolveTable");
            this.buttonResolveTable.Name = "buttonResolveTable";
            this.buttonResolveTable.UseVisualStyleBackColor = false;
            this.buttonResolveTable.Click += new System.EventHandler(this.buttonResolveTable_Click);
            // 
            // buttonCheckResolving
            // 
            this.buttonCheckResolving.BackColor = System.Drawing.Color.MediumTurquoise;
            resources.ApplyResources(this.buttonCheckResolving, "buttonCheckResolving");
            this.buttonCheckResolving.Name = "buttonCheckResolving";
            this.buttonCheckResolving.UseVisualStyleBackColor = false;
            this.buttonCheckResolving.Click += new System.EventHandler(this.buttonCheckResolving_Click);
            // 
            // buttonSaveGame
            // 
            resources.ApplyResources(this.buttonSaveGame, "buttonSaveGame");
            this.buttonSaveGame.Name = "buttonSaveGame";
            this.buttonSaveGame.UseVisualStyleBackColor = true;
            this.buttonSaveGame.Click += new System.EventHandler(this.buttonSaveGame_Click);
            // 
            // comboGameName
            // 
            this.comboGameName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboGameName.FormattingEnabled = true;
            resources.ApplyResources(this.comboGameName, "comboGameName");
            this.comboGameName.Name = "comboGameName";
            this.comboGameName.SelectedIndexChanged += new System.EventHandler(this.comboGameName_SelectedIndexChanged);
            // 
            // textTimer
            // 
            this.textTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.textTimer, "textTimer");
            this.textTimer.Name = "textTimer";
            this.textTimer.ReadOnly = true;
            this.textTimer.TabStop = false;
            // 
            // textGameComment
            // 
            resources.ApplyResources(this.textGameComment, "textGameComment");
            this.textGameComment.Name = "textGameComment";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // numericLevel
            // 
            this.numericLevel.BackColor = System.Drawing.Color.OldLace;
            resources.ApplyResources(this.numericLevel, "numericLevel");
            this.numericLevel.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericLevel.Name = "numericLevel";
            this.numericLevel.ReadOnly = true;
            this.numericLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // buttonSaveFile
            // 
            resources.ApplyResources(this.buttonSaveFile, "buttonSaveFile");
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.UseVisualStyleBackColor = true;
            this.buttonSaveFile.Click += new System.EventHandler(this.buttonSaveFile_Click);
            // 
            // buttonClearGame
            // 
            resources.ApplyResources(this.buttonClearGame, "buttonClearGame");
            this.buttonClearGame.Name = "buttonClearGame";
            this.buttonClearGame.UseVisualStyleBackColor = true;
            this.buttonClearGame.Click += new System.EventHandler(this.buttonClearGame_Click);
            // 
            // buttonTestGame
            // 
            resources.ApplyResources(this.buttonTestGame, "buttonTestGame");
            this.buttonTestGame.Name = "buttonTestGame";
            this.buttonTestGame.UseVisualStyleBackColor = true;
            this.buttonTestGame.Click += new System.EventHandler(this.buttonTestGame_Click);
            // 
            // buttonLoadFile
            // 
            resources.ApplyResources(this.buttonLoadFile, "buttonLoadFile");
            this.buttonLoadFile.Name = "buttonLoadFile";
            this.buttonLoadFile.UseVisualStyleBackColor = true;
            this.buttonLoadFile.Click += new System.EventHandler(this.buttonLoadFile_Click);
            // 
            // buttonFillGame
            // 
            resources.ApplyResources(this.buttonFillGame, "buttonFillGame");
            this.buttonFillGame.Name = "buttonFillGame";
            this.buttonFillGame.UseVisualStyleBackColor = true;
            this.buttonFillGame.Click += new System.EventHandler(this.buttonFillGame_Click);
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.buttonStartGame, "buttonStartGame");
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.UseVisualStyleBackColor = false;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // buttonExit
            // 
            resources.ApplyResources(this.buttonExit, "buttonExit");
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // comboGameType
            // 
            this.comboGameType.FormattingEnabled = true;
            resources.ApplyResources(this.comboGameType, "comboGameType");
            this.comboGameType.Name = "comboGameType";
            this.comboGameType.SelectedIndexChanged += new System.EventHandler(this.comboGameType_SelectedIndexChanged);
            // 
            // SuDokuForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.panelTiles);
            this.Name = "SuDokuForm";
            this.panelTiles.ResumeLayout(false);
            this.panelTiles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureTable)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLevel)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelTiles;
		private System.Windows.Forms.Panel panelControl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboGameType;
		private System.Windows.Forms.TextBox textGameComment;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numericLevel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonSaveFile;
		private System.Windows.Forms.Button buttonClearGame;
		private System.Windows.Forms.Button buttonTestGame;
		private System.Windows.Forms.Button buttonLoadFile;
		private System.Windows.Forms.Button buttonFillGame;
		private System.Windows.Forms.Button buttonStartGame;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.PictureBox pictureTable;
		private System.Windows.Forms.TextBox textTimer;
		private System.Windows.Forms.ComboBox comboGameName;
		private System.Windows.Forms.Button buttonSaveGame;
		private System.Windows.Forms.Button buttonCheckResolving;
		private System.Windows.Forms.Button buttonResolveTable;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textGameName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textActLevel;
		private System.Windows.Forms.TextBox textTryNb;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textMaxLevel;
		private System.Windows.Forms.ComboBox comboLanguage;
		private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
	}
}

