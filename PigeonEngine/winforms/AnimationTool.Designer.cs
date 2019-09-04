namespace PigeonEngine.winforms {
	partial class AnimationTool {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
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
			this.animationLabel = new System.Windows.Forms.Label();
			this.spriteLabel = new System.Windows.Forms.Label();
			this.allAnimationsComboBox = new System.Windows.Forms.ComboBox();
			this.allSpritesComboBox = new System.Windows.Forms.ComboBox();
			this.backgroundColorButton = new System.Windows.Forms.Button();
			this.backgroundColorDialog = new System.Windows.Forms.ColorDialog();
			this.previewEnabledCheckBox = new System.Windows.Forms.CheckBox();
			this.previewGroupBox = new System.Windows.Forms.GroupBox();
			this.loopCheckBox = new System.Windows.Forms.CheckBox();
			this.centerPreviewCheckBox = new System.Windows.Forms.CheckBox();
			this.previewGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// animationLabel
			// 
			this.animationLabel.AutoSize = true;
			this.animationLabel.Location = new System.Drawing.Point(12, 80);
			this.animationLabel.Name = "animationLabel";
			this.animationLabel.Size = new System.Drawing.Size(56, 13);
			this.animationLabel.TabIndex = 4;
			this.animationLabel.Text = "Animation:";
			// 
			// spriteLabel
			// 
			this.spriteLabel.AutoSize = true;
			this.spriteLabel.Location = new System.Drawing.Point(12, 34);
			this.spriteLabel.Name = "spriteLabel";
			this.spriteLabel.Size = new System.Drawing.Size(37, 13);
			this.spriteLabel.TabIndex = 3;
			this.spriteLabel.Text = "Sprite:";
			// 
			// allAnimationsComboBox
			// 
			this.allAnimationsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.allAnimationsComboBox.FormattingEnabled = true;
			this.allAnimationsComboBox.Location = new System.Drawing.Point(11, 96);
			this.allAnimationsComboBox.Name = "allAnimationsComboBox";
			this.allAnimationsComboBox.Size = new System.Drawing.Size(177, 21);
			this.allAnimationsComboBox.Sorted = true;
			this.allAnimationsComboBox.TabIndex = 2;
			this.allAnimationsComboBox.SelectedIndexChanged += new System.EventHandler(this.allAnimationsComboBox_SelectedIndexChanged);
			// 
			// allSpritesComboBox
			// 
			this.allSpritesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.allSpritesComboBox.FormattingEnabled = true;
			this.allSpritesComboBox.Location = new System.Drawing.Point(11, 50);
			this.allSpritesComboBox.Name = "allSpritesComboBox";
			this.allSpritesComboBox.Size = new System.Drawing.Size(177, 21);
			this.allSpritesComboBox.Sorted = true;
			this.allSpritesComboBox.TabIndex = 1;
			this.allSpritesComboBox.SelectedIndexChanged += new System.EventHandler(this.AllSpritesComboBox_SelectedIndexChanged);
			// 
			// backgroundColorButton
			// 
			this.backgroundColorButton.Location = new System.Drawing.Point(216, 129);
			this.backgroundColorButton.Name = "backgroundColorButton";
			this.backgroundColorButton.Size = new System.Drawing.Size(102, 23);
			this.backgroundColorButton.TabIndex = 5;
			this.backgroundColorButton.Text = "Background color";
			this.backgroundColorButton.UseVisualStyleBackColor = true;
			this.backgroundColorButton.Click += new System.EventHandler(this.backgroundColorButton_Click);
			// 
			// previewEnabledCheckBox
			// 
			this.previewEnabledCheckBox.AutoSize = true;
			this.previewEnabledCheckBox.Checked = true;
			this.previewEnabledCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.previewEnabledCheckBox.Location = new System.Drawing.Point(6, 19);
			this.previewEnabledCheckBox.Name = "previewEnabledCheckBox";
			this.previewEnabledCheckBox.Size = new System.Drawing.Size(65, 17);
			this.previewEnabledCheckBox.TabIndex = 7;
			this.previewEnabledCheckBox.Text = "Enabled";
			this.previewEnabledCheckBox.UseVisualStyleBackColor = true;
			this.previewEnabledCheckBox.CheckedChanged += new System.EventHandler(this.previewEnabledCheckBox_CheckedChanged);
			// 
			// previewGroupBox
			// 
			this.previewGroupBox.Controls.Add(this.loopCheckBox);
			this.previewGroupBox.Controls.Add(this.centerPreviewCheckBox);
			this.previewGroupBox.Controls.Add(this.previewEnabledCheckBox);
			this.previewGroupBox.Location = new System.Drawing.Point(210, 25);
			this.previewGroupBox.Name = "previewGroupBox";
			this.previewGroupBox.Size = new System.Drawing.Size(118, 98);
			this.previewGroupBox.TabIndex = 8;
			this.previewGroupBox.TabStop = false;
			this.previewGroupBox.Text = "Animation prevew";
			// 
			// loopCheckBox
			// 
			this.loopCheckBox.AutoSize = true;
			this.loopCheckBox.Checked = true;
			this.loopCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.loopCheckBox.Location = new System.Drawing.Point(6, 65);
			this.loopCheckBox.Name = "loopCheckBox";
			this.loopCheckBox.Size = new System.Drawing.Size(50, 17);
			this.loopCheckBox.TabIndex = 9;
			this.loopCheckBox.Text = "Loop";
			this.loopCheckBox.UseVisualStyleBackColor = true;
			this.loopCheckBox.CheckedChanged += new System.EventHandler(this.loopCheckBox_CheckedChanged);
			// 
			// centerPreviewCheckBox
			// 
			this.centerPreviewCheckBox.AutoSize = true;
			this.centerPreviewCheckBox.Checked = true;
			this.centerPreviewCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.centerPreviewCheckBox.Location = new System.Drawing.Point(7, 41);
			this.centerPreviewCheckBox.Name = "centerPreviewCheckBox";
			this.centerPreviewCheckBox.Size = new System.Drawing.Size(86, 17);
			this.centerPreviewCheckBox.TabIndex = 8;
			this.centerPreviewCheckBox.Text = "Place center";
			this.centerPreviewCheckBox.UseVisualStyleBackColor = true;
			this.centerPreviewCheckBox.CheckedChanged += new System.EventHandler(this.centerPreviewCheckBox_CheckedChanged);
			// 
			// AnimationTool
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(338, 168);
			this.Controls.Add(this.previewGroupBox);
			this.Controls.Add(this.backgroundColorButton);
			this.Controls.Add(this.animationLabel);
			this.Controls.Add(this.spriteLabel);
			this.Controls.Add(this.allAnimationsComboBox);
			this.Controls.Add(this.allSpritesComboBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Name = "AnimationTool";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Animator";
			this.Load += new System.EventHandler(this.AnimationTool_Load);
			this.previewGroupBox.ResumeLayout(false);
			this.previewGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox allSpritesComboBox;
		private System.Windows.Forms.Label spriteLabel;
		private System.Windows.Forms.ComboBox allAnimationsComboBox;
		private System.Windows.Forms.Label animationLabel;
		private System.Windows.Forms.Button backgroundColorButton;
		private System.Windows.Forms.ColorDialog backgroundColorDialog;
		private System.Windows.Forms.CheckBox previewEnabledCheckBox;
		private System.Windows.Forms.GroupBox previewGroupBox;
		private System.Windows.Forms.CheckBox centerPreviewCheckBox;
		private System.Windows.Forms.CheckBox loopCheckBox;
	}
}