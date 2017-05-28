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
			this.panelTiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelTiles.Controls.Add(this.pictureTable);
			this.panelTiles.Location = new System.Drawing.Point(0, 0);
			this.panelTiles.Name = "panelTiles";
			this.panelTiles.Size = new System.Drawing.Size(555, 555);
			this.panelTiles.TabIndex = 0;
			// 
			// pictureTable
			// 
			this.pictureTable.BackColor = System.Drawing.SystemColors.Control;
			this.pictureTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureTable.Location = new System.Drawing.Point(0, 0);
			this.pictureTable.Name = "pictureTable";
			this.pictureTable.Size = new System.Drawing.Size(555, 555);
			this.pictureTable.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureTable.TabIndex = 0;
			this.pictureTable.TabStop = false;
			this.pictureTable.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureTable_Paint);
			this.pictureTable.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureTable_MouseClick);
			this.pictureTable.Resize += new System.EventHandler(this.pictureTable_Resize);
			// 
			// panelControl
			// 
			this.panelControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
			this.panelControl.Location = new System.Drawing.Point(555, 0);
			this.panelControl.Name = "panelControl";
			this.panelControl.Size = new System.Drawing.Size(300, 555);
			this.panelControl.TabIndex = 1;
			// 
			// buttonResolveTable
			// 
			this.buttonResolveTable.Location = new System.Drawing.Point(12, 166);
			this.buttonResolveTable.Name = "buttonResolveTable";
			this.buttonResolveTable.Size = new System.Drawing.Size(137, 30);
			this.buttonResolveTable.TabIndex = 18;
			this.buttonResolveTable.Text = "Megoldás";
			this.buttonResolveTable.UseVisualStyleBackColor = true;
			this.buttonResolveTable.Click += new System.EventHandler(this.buttonResolveTable_Click);
			// 
			// buttonCheckResolving
			// 
			this.buttonCheckResolving.Location = new System.Drawing.Point(152, 166);
			this.buttonCheckResolving.Name = "buttonCheckResolving";
			this.buttonCheckResolving.Size = new System.Drawing.Size(137, 30);
			this.buttonCheckResolving.TabIndex = 2;
			this.buttonCheckResolving.Text = "Megoldhatóság";
			this.buttonCheckResolving.UseVisualStyleBackColor = true;
			this.buttonCheckResolving.Click += new System.EventHandler(this.buttonCheckResolving_Click);
			// 
			// buttonSaveGame
			// 
			this.buttonSaveGame.Location = new System.Drawing.Point(154, 275);
			this.buttonSaveGame.Name = "buttonSaveGame";
			this.buttonSaveGame.Size = new System.Drawing.Size(135, 30);
			this.buttonSaveGame.TabIndex = 5;
			this.buttonSaveGame.Text = "Játék mentése";
			this.buttonSaveGame.UseVisualStyleBackColor = true;
			// 
			// comboGameName
			// 
			this.comboGameName.FormattingEnabled = true;
			this.comboGameName.Location = new System.Drawing.Point(12, 335);
			this.comboGameName.Name = "comboGameName";
			this.comboGameName.Size = new System.Drawing.Size(277, 24);
			this.comboGameName.TabIndex = 10;
			this.comboGameName.SelectedIndexChanged += new System.EventHandler(this.comboGameName_SelectedIndexChanged);
			// 
			// textTimer
			// 
			this.textTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.textTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.textTimer.Location = new System.Drawing.Point(154, 12);
			this.textTimer.Name = "textTimer";
			this.textTimer.ReadOnly = true;
			this.textTimer.Size = new System.Drawing.Size(135, 38);
			this.textTimer.TabIndex = 17;
			this.textTimer.TabStop = false;
			this.textTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textGameComment
			// 
			this.textGameComment.Location = new System.Drawing.Point(12, 380);
			this.textGameComment.Name = "textGameComment";
			this.textGameComment.Size = new System.Drawing.Size(277, 22);
			this.textGameComment.TabIndex = 11;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 360);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(83, 17);
			this.label4.TabIndex = 16;
			this.label4.Text = "Megjegyzés";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 315);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(243, 17);
			this.label3.TabIndex = 15;
			this.label3.Text = "Játéktábla neve / Játék kiválasztáasa";
			// 
			// numericLevel
			// 
			this.numericLevel.BackColor = System.Drawing.Color.OldLace;
			this.numericLevel.Location = new System.Drawing.Point(226, 84);
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
			this.numericLevel.Size = new System.Drawing.Size(63, 22);
			this.numericLevel.TabIndex = 7;
			this.numericLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(198, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98, 17);
			this.label2.TabIndex = 14;
			this.label2.Text = "Nehézségi fok";
			// 
			// buttonSaveFile
			// 
			this.buttonSaveFile.Location = new System.Drawing.Point(154, 412);
			this.buttonSaveFile.Name = "buttonSaveFile";
			this.buttonSaveFile.Size = new System.Drawing.Size(136, 30);
			this.buttonSaveFile.TabIndex = 9;
			this.buttonSaveFile.Text = "Lista elmentése";
			this.buttonSaveFile.UseVisualStyleBackColor = true;
			this.buttonSaveFile.Click += new System.EventHandler(this.buttonSaveFile_Click);
			// 
			// buttonClearGame
			// 
			this.buttonClearGame.Location = new System.Drawing.Point(154, 202);
			this.buttonClearGame.Name = "buttonClearGame";
			this.buttonClearGame.Size = new System.Drawing.Size(136, 30);
			this.buttonClearGame.TabIndex = 3;
			this.buttonClearGame.Text = "Tábla törlése";
			this.buttonClearGame.UseVisualStyleBackColor = true;
			this.buttonClearGame.Click += new System.EventHandler(this.buttonClearGame_Click);
			// 
			// buttonTestGame
			// 
			this.buttonTestGame.Location = new System.Drawing.Point(153, 125);
			this.buttonTestGame.Name = "buttonTestGame";
			this.buttonTestGame.Size = new System.Drawing.Size(137, 30);
			this.buttonTestGame.TabIndex = 1;
			this.buttonTestGame.Text = "Tábla tesztelése";
			this.buttonTestGame.UseVisualStyleBackColor = true;
			this.buttonTestGame.Click += new System.EventHandler(this.buttonTestGame_Click);
			// 
			// buttonLoadFile
			// 
			this.buttonLoadFile.Location = new System.Drawing.Point(12, 412);
			this.buttonLoadFile.Name = "buttonLoadFile";
			this.buttonLoadFile.Size = new System.Drawing.Size(136, 30);
			this.buttonLoadFile.TabIndex = 8;
			this.buttonLoadFile.Text = "Betöltés fájlból";
			this.buttonLoadFile.UseVisualStyleBackColor = true;
			this.buttonLoadFile.Click += new System.EventHandler(this.buttonLoadFile_Click);
			// 
			// buttonFillGame
			// 
			this.buttonFillGame.Location = new System.Drawing.Point(154, 239);
			this.buttonFillGame.Name = "buttonFillGame";
			this.buttonFillGame.Size = new System.Drawing.Size(136, 30);
			this.buttonFillGame.TabIndex = 4;
			this.buttonFillGame.Text = "Tábla kitöltése";
			this.buttonFillGame.UseVisualStyleBackColor = true;
			this.buttonFillGame.Click += new System.EventHandler(this.buttonFillGame_Click);
			// 
			// buttonStartGame
			// 
			this.buttonStartGame.Location = new System.Drawing.Point(12, 125);
			this.buttonStartGame.Name = "buttonStartGame";
			this.buttonStartGame.Size = new System.Drawing.Size(135, 30);
			this.buttonStartGame.TabIndex = 0;
			this.buttonStartGame.Text = "Játék indítása";
			this.buttonStartGame.UseVisualStyleBackColor = true;
			this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonExit.Location = new System.Drawing.Point(161, 514);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(129, 30);
			this.buttonExit.TabIndex = 12;
			this.buttonExit.Text = "Kilépés";
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 17);
			this.label1.TabIndex = 13;
			this.label1.Text = "Játék típus";
			// 
			// comboGameType
			// 
			this.comboGameType.FormattingEnabled = true;
			this.comboGameType.Location = new System.Drawing.Point(12, 84);
			this.comboGameType.Name = "comboGameType";
			this.comboGameType.Size = new System.Drawing.Size(208, 24);
			this.comboGameType.TabIndex = 6;
			this.comboGameType.SelectedIndexChanged += new System.EventHandler(this.comboGameType_SelectedIndexChanged);
			// 
			// SuDokuForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(857, 556);
			this.Controls.Add(this.panelControl);
			this.Controls.Add(this.panelTiles);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SuDokuForm";
			this.Text = "SuDoku";
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
	}
}

