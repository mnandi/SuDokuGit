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
			this.panelTiles = new System.Windows.Forms.Panel();
			this.pictureTable = new System.Windows.Forms.PictureBox();
			this.panelControl = new System.Windows.Forms.Panel();
			this.textTimer = new System.Windows.Forms.TextBox();
			this.textGameComment = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.numericLevel = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonClear = new System.Windows.Forms.Button();
			this.buttonTest = new System.Windows.Forms.Button();
			this.buttonLoad = new System.Windows.Forms.Button();
			this.buttonFill = new System.Windows.Forms.Button();
			this.buttonGame = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.comboGameType = new System.Windows.Forms.ComboBox();
			this.comboGameName = new System.Windows.Forms.ComboBox();
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
			this.panelControl.Controls.Add(this.comboGameName);
			this.panelControl.Controls.Add(this.textTimer);
			this.panelControl.Controls.Add(this.textGameComment);
			this.panelControl.Controls.Add(this.label4);
			this.panelControl.Controls.Add(this.label3);
			this.panelControl.Controls.Add(this.numericLevel);
			this.panelControl.Controls.Add(this.label2);
			this.panelControl.Controls.Add(this.buttonSave);
			this.panelControl.Controls.Add(this.buttonClear);
			this.panelControl.Controls.Add(this.buttonTest);
			this.panelControl.Controls.Add(this.buttonLoad);
			this.panelControl.Controls.Add(this.buttonFill);
			this.panelControl.Controls.Add(this.buttonGame);
			this.panelControl.Controls.Add(this.buttonExit);
			this.panelControl.Controls.Add(this.label1);
			this.panelControl.Controls.Add(this.comboGameType);
			this.panelControl.Location = new System.Drawing.Point(555, 0);
			this.panelControl.Name = "panelControl";
			this.panelControl.Size = new System.Drawing.Size(300, 555);
			this.panelControl.TabIndex = 1;
			// 
			// textTimer
			// 
			this.textTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.textTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.textTimer.Location = new System.Drawing.Point(142, 12);
			this.textTimer.Name = "textTimer";
			this.textTimer.Size = new System.Drawing.Size(147, 38);
			this.textTimer.TabIndex = 15;
			this.textTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textGameComment
			// 
			this.textGameComment.Location = new System.Drawing.Point(20, 380);
			this.textGameComment.Name = "textGameComment";
			this.textGameComment.Size = new System.Drawing.Size(269, 22);
			this.textGameComment.TabIndex = 14;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(17, 360);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(83, 17);
			this.label4.TabIndex = 12;
			this.label4.Text = "Megjegyzés";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(17, 315);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(108, 17);
			this.label3.TabIndex = 11;
			this.label3.Text = "Játéktábla neve";
			// 
			// numericLevel
			// 
			this.numericLevel.Location = new System.Drawing.Point(142, 92);
			this.numericLevel.Name = "numericLevel";
			this.numericLevel.Size = new System.Drawing.Size(147, 22);
			this.numericLevel.TabIndex = 10;
			this.numericLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 95);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98, 17);
			this.label2.TabIndex = 9;
			this.label2.Text = "Nehézségi fok";
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(143, 408);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(147, 30);
			this.buttonSave.TabIndex = 8;
			this.buttonSave.Text = "Tábla mentése";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonClear
			// 
			this.buttonClear.Location = new System.Drawing.Point(143, 212);
			this.buttonClear.Name = "buttonClear";
			this.buttonClear.Size = new System.Drawing.Size(147, 30);
			this.buttonClear.TabIndex = 7;
			this.buttonClear.Text = "Tábla törlése";
			this.buttonClear.UseVisualStyleBackColor = true;
			// 
			// buttonTest
			// 
			this.buttonTest.Location = new System.Drawing.Point(142, 160);
			this.buttonTest.Name = "buttonTest";
			this.buttonTest.Size = new System.Drawing.Size(147, 30);
			this.buttonTest.TabIndex = 6;
			this.buttonTest.Text = "Tábla tesztelése";
			this.buttonTest.UseVisualStyleBackColor = true;
			this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
			// 
			// buttonLoad
			// 
			this.buttonLoad.Location = new System.Drawing.Point(143, 282);
			this.buttonLoad.Name = "buttonLoad";
			this.buttonLoad.Size = new System.Drawing.Size(147, 30);
			this.buttonLoad.TabIndex = 5;
			this.buttonLoad.Text = "Játék betöltése";
			this.buttonLoad.UseVisualStyleBackColor = true;
			this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
			// 
			// buttonFill
			// 
			this.buttonFill.Location = new System.Drawing.Point(143, 247);
			this.buttonFill.Name = "buttonFill";
			this.buttonFill.Size = new System.Drawing.Size(147, 30);
			this.buttonFill.TabIndex = 4;
			this.buttonFill.Text = "Tábla kitöltése";
			this.buttonFill.UseVisualStyleBackColor = true;
			// 
			// buttonGame
			// 
			this.buttonGame.Location = new System.Drawing.Point(142, 124);
			this.buttonGame.Name = "buttonGame";
			this.buttonGame.Size = new System.Drawing.Size(147, 30);
			this.buttonGame.TabIndex = 3;
			this.buttonGame.Text = "Játék indítása";
			this.buttonGame.UseVisualStyleBackColor = true;
			this.buttonGame.Click += new System.EventHandler(this.buttonGame_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonExit.Location = new System.Drawing.Point(143, 514);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(147, 30);
			this.buttonExit.TabIndex = 2;
			this.buttonExit.Text = "Kilépés";
			this.buttonExit.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Játék típus";
			// 
			// comboGameType
			// 
			this.comboGameType.FormattingEnabled = true;
			this.comboGameType.Location = new System.Drawing.Point(20, 61);
			this.comboGameType.Name = "comboGameType";
			this.comboGameType.Size = new System.Drawing.Size(270, 24);
			this.comboGameType.TabIndex = 0;
			this.comboGameType.SelectedIndexChanged += new System.EventHandler(this.comboGameType_SelectedIndexChanged);
			// 
			// comboGameName
			// 
			this.comboGameName.FormattingEnabled = true;
			this.comboGameName.Location = new System.Drawing.Point(20, 335);
			this.comboGameName.Name = "comboGameName";
			this.comboGameName.Size = new System.Drawing.Size(269, 24);
			this.comboGameName.TabIndex = 16;
			// 
			// SuDokuForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(857, 556);
			this.Controls.Add(this.panelControl);
			this.Controls.Add(this.panelTiles);
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
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonClear;
		private System.Windows.Forms.Button buttonTest;
		private System.Windows.Forms.Button buttonLoad;
		private System.Windows.Forms.Button buttonFill;
		private System.Windows.Forms.Button buttonGame;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.PictureBox pictureTable;
		private System.Windows.Forms.TextBox textTimer;
		private System.Windows.Forms.ComboBox comboGameName;
	}
}

