namespace PigeonEngine.winforms {
	partial class PigeonUi {
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
			this.components = new System.ComponentModel.Container();
			this.objectTree = new System.Windows.Forms.TreeView();
			this.localPositionYText = new System.Windows.Forms.TextBox();
			this.localPositionXText = new System.Windows.Forms.TextBox();
			this.worldPositionYText = new System.Windows.Forms.TextBox();
			this.localPositionLabel = new System.Windows.Forms.Label();
			this.worldPositionXText = new System.Windows.Forms.TextBox();
			this.worldPositionLabel = new System.Windows.Forms.Label();
			this.objectNameText = new System.Windows.Forms.TextBox();
			this.objectNameLabel = new System.Windows.Forms.Label();
			this.componentCountLabel = new System.Windows.Forms.Label();
			this.componentsList = new System.Windows.Forms.ListBox();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.consoleTabPage = new System.Windows.Forms.TabPage();
			this.consoleLogText = new System.Windows.Forms.RichTextBox();
			this.consoleCommandCommit = new System.Windows.Forms.Button();
			this.consoleCommandText = new System.Windows.Forms.TextBox();
			this.objectsTabPage = new System.Windows.Forms.TabPage();
			this.objectDetailsGroupBox = new System.Windows.Forms.GroupBox();
			this.objectFlipGroupBox = new System.Windows.Forms.GroupBox();
			this.objectFlipXCheckBox = new System.Windows.Forms.CheckBox();
			this.objectFlipYCheckBox = new System.Windows.Forms.CheckBox();
			this.objectDebugGroupBox = new System.Windows.Forms.GroupBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.drawPositionCheckbox = new System.Windows.Forms.CheckBox();
			this.objectDetailsRefreshButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.objectDrawEnabledCheckbox = new System.Windows.Forms.CheckBox();
			this.objectUpdateEnabledCheckbox = new System.Windows.Forms.CheckBox();
			this.objectLayerGroupBox = new System.Windows.Forms.GroupBox();
			this.objectLayerVarianceText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.objectParentLayerText = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.objectDrawLayerText = new System.Windows.Forms.TextBox();
			this.objectDrawLayerLabel = new System.Windows.Forms.Label();
			this.objectLayerText = new System.Windows.Forms.TextBox();
			this.objectInheritLayerCheckBox = new System.Windows.Forms.CheckBox();
			this.objectLayerLabel = new System.Windows.Forms.Label();
			this.objectVarianceCheckBox = new System.Windows.Forms.CheckBox();
			this.objectPositionGroupBox = new System.Windows.Forms.GroupBox();
			this.componentDetailsGroupBox = new System.Windows.Forms.GroupBox();
			this.componentDetailsButton = new System.Windows.Forms.Button();
			this.componentCountText = new System.Windows.Forms.TextBox();
			this.objectChildrenCountText = new System.Windows.Forms.TextBox();
			this.objectChildrenCountLabel = new System.Windows.Forms.Label();
			this.objectListGroupBox = new System.Windows.Forms.GroupBox();
			this.objectListRefresh = new System.Windows.Forms.Button();
			this.utilitiesTabPage = new System.Windows.Forms.TabPage();
			this.utilitiesResolutionSetButton = new System.Windows.Forms.Button();
			this.utilitiesResolutionYTextfield = new System.Windows.Forms.TextBox();
			this.utilitiesResolutionXTextfield = new System.Windows.Forms.TextBox();
			this.utilitiesResolutionLabel = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.utilitiesScaleLabel = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.utilitiesTickButton = new System.Windows.Forms.Button();
			this.utilitiesPauseButton = new System.Windows.Forms.Button();
			this.toolTipper = new System.Windows.Forms.ToolTip(this.components);
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.animatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hitboxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miscToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.takeScreenshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openSaveDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sandboxWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl.SuspendLayout();
			this.consoleTabPage.SuspendLayout();
			this.objectsTabPage.SuspendLayout();
			this.objectDetailsGroupBox.SuspendLayout();
			this.objectFlipGroupBox.SuspendLayout();
			this.objectDebugGroupBox.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.objectLayerGroupBox.SuspendLayout();
			this.objectPositionGroupBox.SuspendLayout();
			this.componentDetailsGroupBox.SuspendLayout();
			this.objectListGroupBox.SuspendLayout();
			this.utilitiesTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// objectTree
			// 
			this.objectTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.objectTree.Location = new System.Drawing.Point(6, 19);
			this.objectTree.Name = "objectTree";
			this.objectTree.Size = new System.Drawing.Size(157, 339);
			this.objectTree.TabIndex = 0;
			this.objectTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.objectTree_AfterSelect);
			// 
			// localPositionYText
			// 
			this.localPositionYText.Location = new System.Drawing.Point(67, 19);
			this.localPositionYText.Name = "localPositionYText";
			this.localPositionYText.ReadOnly = true;
			this.localPositionYText.Size = new System.Drawing.Size(37, 20);
			this.localPositionYText.TabIndex = 11;
			this.localPositionYText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// localPositionXText
			// 
			this.localPositionXText.Location = new System.Drawing.Point(25, 19);
			this.localPositionXText.Name = "localPositionXText";
			this.localPositionXText.ReadOnly = true;
			this.localPositionXText.Size = new System.Drawing.Size(37, 20);
			this.localPositionXText.TabIndex = 10;
			this.localPositionXText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// worldPositionYText
			// 
			this.worldPositionYText.Location = new System.Drawing.Point(67, 49);
			this.worldPositionYText.Name = "worldPositionYText";
			this.worldPositionYText.ReadOnly = true;
			this.worldPositionYText.Size = new System.Drawing.Size(37, 20);
			this.worldPositionYText.TabIndex = 9;
			this.worldPositionYText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// localPositionLabel
			// 
			this.localPositionLabel.AutoSize = true;
			this.localPositionLabel.Location = new System.Drawing.Point(3, 22);
			this.localPositionLabel.Name = "localPositionLabel";
			this.localPositionLabel.Size = new System.Drawing.Size(16, 13);
			this.localPositionLabel.TabIndex = 7;
			this.localPositionLabel.Text = "L:";
			// 
			// worldPositionXText
			// 
			this.worldPositionXText.Location = new System.Drawing.Point(25, 49);
			this.worldPositionXText.Name = "worldPositionXText";
			this.worldPositionXText.ReadOnly = true;
			this.worldPositionXText.Size = new System.Drawing.Size(37, 20);
			this.worldPositionXText.TabIndex = 6;
			this.worldPositionXText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// worldPositionLabel
			// 
			this.worldPositionLabel.AutoSize = true;
			this.worldPositionLabel.Location = new System.Drawing.Point(3, 53);
			this.worldPositionLabel.Name = "worldPositionLabel";
			this.worldPositionLabel.Size = new System.Drawing.Size(18, 13);
			this.worldPositionLabel.TabIndex = 5;
			this.worldPositionLabel.Text = "G:";
			// 
			// objectNameText
			// 
			this.objectNameText.Location = new System.Drawing.Point(47, 19);
			this.objectNameText.Name = "objectNameText";
			this.objectNameText.ReadOnly = true;
			this.objectNameText.Size = new System.Drawing.Size(232, 20);
			this.objectNameText.TabIndex = 4;
			this.objectNameText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// objectNameLabel
			// 
			this.objectNameLabel.AutoSize = true;
			this.objectNameLabel.Location = new System.Drawing.Point(7, 22);
			this.objectNameLabel.Name = "objectNameLabel";
			this.objectNameLabel.Size = new System.Drawing.Size(38, 13);
			this.objectNameLabel.TabIndex = 3;
			this.objectNameLabel.Text = "Name:";
			// 
			// componentCountLabel
			// 
			this.componentCountLabel.AutoSize = true;
			this.componentCountLabel.Location = new System.Drawing.Point(33, 22);
			this.componentCountLabel.Name = "componentCountLabel";
			this.componentCountLabel.Size = new System.Drawing.Size(38, 13);
			this.componentCountLabel.TabIndex = 2;
			this.componentCountLabel.Text = "Count:";
			// 
			// componentsList
			// 
			this.componentsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.componentsList.FormattingEnabled = true;
			this.componentsList.Location = new System.Drawing.Point(6, 45);
			this.componentsList.Name = "componentsList";
			this.componentsList.ScrollAlwaysVisible = true;
			this.componentsList.Size = new System.Drawing.Size(134, 199);
			this.componentsList.TabIndex = 0;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.consoleTabPage);
			this.tabControl.Controls.Add(this.objectsTabPage);
			this.tabControl.Controls.Add(this.utilitiesTabPage);
			this.tabControl.Location = new System.Drawing.Point(12, 27);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(639, 440);
			this.tabControl.TabIndex = 2;
			// 
			// consoleTabPage
			// 
			this.consoleTabPage.BackColor = System.Drawing.Color.Transparent;
			this.consoleTabPage.Controls.Add(this.consoleLogText);
			this.consoleTabPage.Controls.Add(this.consoleCommandCommit);
			this.consoleTabPage.Controls.Add(this.consoleCommandText);
			this.consoleTabPage.Location = new System.Drawing.Point(4, 22);
			this.consoleTabPage.Name = "consoleTabPage";
			this.consoleTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.consoleTabPage.Size = new System.Drawing.Size(631, 414);
			this.consoleTabPage.TabIndex = 2;
			this.consoleTabPage.Text = "Console";
			// 
			// consoleLogText
			// 
			this.consoleLogText.BackColor = System.Drawing.SystemColors.WindowText;
			this.consoleLogText.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.consoleLogText.ForeColor = System.Drawing.SystemColors.Window;
			this.consoleLogText.Location = new System.Drawing.Point(7, 7);
			this.consoleLogText.Name = "consoleLogText";
			this.consoleLogText.Size = new System.Drawing.Size(618, 373);
			this.consoleLogText.TabIndex = 3;
			this.consoleLogText.Text = "";
			// 
			// consoleCommandCommit
			// 
			this.consoleCommandCommit.Location = new System.Drawing.Point(581, 386);
			this.consoleCommandCommit.Name = "consoleCommandCommit";
			this.consoleCommandCommit.Size = new System.Drawing.Size(44, 23);
			this.consoleCommandCommit.TabIndex = 2;
			this.consoleCommandCommit.Text = "Send";
			this.consoleCommandCommit.UseVisualStyleBackColor = true;
			this.consoleCommandCommit.Click += new System.EventHandler(this.consoleCommandCommit_Click);
			// 
			// consoleCommandText
			// 
			this.consoleCommandText.BackColor = System.Drawing.SystemColors.WindowText;
			this.consoleCommandText.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.consoleCommandText.ForeColor = System.Drawing.Color.LimeGreen;
			this.consoleCommandText.Location = new System.Drawing.Point(7, 388);
			this.consoleCommandText.Name = "consoleCommandText";
			this.consoleCommandText.Size = new System.Drawing.Size(568, 18);
			this.consoleCommandText.TabIndex = 1;
			this.consoleCommandText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.consoleCommandText_KeyPress);
			// 
			// objectsTabPage
			// 
			this.objectsTabPage.Controls.Add(this.objectDetailsGroupBox);
			this.objectsTabPage.Controls.Add(this.objectListGroupBox);
			this.objectsTabPage.Location = new System.Drawing.Point(4, 22);
			this.objectsTabPage.Name = "objectsTabPage";
			this.objectsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.objectsTabPage.Size = new System.Drawing.Size(631, 414);
			this.objectsTabPage.TabIndex = 0;
			this.objectsTabPage.Text = "Objects";
			this.objectsTabPage.UseVisualStyleBackColor = true;
			// 
			// objectDetailsGroupBox
			// 
			this.objectDetailsGroupBox.Controls.Add(this.objectFlipGroupBox);
			this.objectDetailsGroupBox.Controls.Add(this.objectDebugGroupBox);
			this.objectDetailsGroupBox.Controls.Add(this.objectDetailsRefreshButton);
			this.objectDetailsGroupBox.Controls.Add(this.groupBox1);
			this.objectDetailsGroupBox.Controls.Add(this.objectLayerGroupBox);
			this.objectDetailsGroupBox.Controls.Add(this.objectPositionGroupBox);
			this.objectDetailsGroupBox.Controls.Add(this.componentDetailsGroupBox);
			this.objectDetailsGroupBox.Controls.Add(this.objectNameLabel);
			this.objectDetailsGroupBox.Controls.Add(this.objectChildrenCountText);
			this.objectDetailsGroupBox.Controls.Add(this.objectChildrenCountLabel);
			this.objectDetailsGroupBox.Controls.Add(this.objectNameText);
			this.objectDetailsGroupBox.Enabled = false;
			this.objectDetailsGroupBox.Location = new System.Drawing.Point(182, 7);
			this.objectDetailsGroupBox.Name = "objectDetailsGroupBox";
			this.objectDetailsGroupBox.Size = new System.Drawing.Size(443, 396);
			this.objectDetailsGroupBox.TabIndex = 3;
			this.objectDetailsGroupBox.TabStop = false;
			this.objectDetailsGroupBox.Text = "Object Details";
			// 
			// objectFlipGroupBox
			// 
			this.objectFlipGroupBox.Controls.Add(this.objectFlipXCheckBox);
			this.objectFlipGroupBox.Controls.Add(this.objectFlipYCheckBox);
			this.objectFlipGroupBox.Location = new System.Drawing.Point(128, 249);
			this.objectFlipGroupBox.Name = "objectFlipGroupBox";
			this.objectFlipGroupBox.Size = new System.Drawing.Size(151, 108);
			this.objectFlipGroupBox.TabIndex = 24;
			this.objectFlipGroupBox.TabStop = false;
			this.objectFlipGroupBox.Text = "Flipping";
			// 
			// objectFlipXCheckBox
			// 
			this.objectFlipXCheckBox.AutoSize = true;
			this.objectFlipXCheckBox.Location = new System.Drawing.Point(6, 19);
			this.objectFlipXCheckBox.Name = "objectFlipXCheckBox";
			this.objectFlipXCheckBox.Size = new System.Drawing.Size(52, 17);
			this.objectFlipXCheckBox.TabIndex = 21;
			this.objectFlipXCheckBox.Text = "Flip X";
			this.objectFlipXCheckBox.UseVisualStyleBackColor = true;
			this.objectFlipXCheckBox.CheckedChanged += new System.EventHandler(this.objectFlipXCheckBox_CheckedChanged);
			// 
			// objectFlipYCheckBox
			// 
			this.objectFlipYCheckBox.AutoSize = true;
			this.objectFlipYCheckBox.Location = new System.Drawing.Point(6, 42);
			this.objectFlipYCheckBox.Name = "objectFlipYCheckBox";
			this.objectFlipYCheckBox.Size = new System.Drawing.Size(52, 17);
			this.objectFlipYCheckBox.TabIndex = 20;
			this.objectFlipYCheckBox.Text = "Flip Y";
			this.objectFlipYCheckBox.UseVisualStyleBackColor = true;
			this.objectFlipYCheckBox.CheckedChanged += new System.EventHandler(this.objectFlipYCheckBox_CheckedChanged);
			// 
			// objectDebugGroupBox
			// 
			this.objectDebugGroupBox.Controls.Add(this.checkBox2);
			this.objectDebugGroupBox.Controls.Add(this.checkBox1);
			this.objectDebugGroupBox.Controls.Add(this.drawPositionCheckbox);
			this.objectDebugGroupBox.Location = new System.Drawing.Point(7, 249);
			this.objectDebugGroupBox.Name = "objectDebugGroupBox";
			this.objectDebugGroupBox.Size = new System.Drawing.Size(114, 108);
			this.objectDebugGroupBox.TabIndex = 23;
			this.objectDebugGroupBox.TabStop = false;
			this.objectDebugGroupBox.Text = "Debug";
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Enabled = false;
			this.checkBox2.Location = new System.Drawing.Point(6, 65);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(94, 17);
			this.checkBox2.TabIndex = 18;
			this.checkBox2.Text = "Show Y Sorter";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Enabled = false;
			this.checkBox1.Location = new System.Drawing.Point(6, 42);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(97, 17);
			this.checkBox1.TabIndex = 17;
			this.checkBox1.Text = "Show Hitboxes";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// drawPositionCheckbox
			// 
			this.drawPositionCheckbox.AutoSize = true;
			this.drawPositionCheckbox.Location = new System.Drawing.Point(6, 19);
			this.drawPositionCheckbox.Name = "drawPositionCheckbox";
			this.drawPositionCheckbox.Size = new System.Drawing.Size(93, 17);
			this.drawPositionCheckbox.TabIndex = 16;
			this.drawPositionCheckbox.Text = "Show Position";
			this.drawPositionCheckbox.UseVisualStyleBackColor = true;
			this.drawPositionCheckbox.CheckedChanged += new System.EventHandler(this.drawPositionCheckbox_CheckedChanged);
			// 
			// objectDetailsRefreshButton
			// 
			this.objectDetailsRefreshButton.BackgroundImage = global::PigeonEngine.Properties.Resources.refresh;
			this.objectDetailsRefreshButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.objectDetailsRefreshButton.Location = new System.Drawing.Point(194, 362);
			this.objectDetailsRefreshButton.Name = "objectDetailsRefreshButton";
			this.objectDetailsRefreshButton.Size = new System.Drawing.Size(24, 24);
			this.objectDetailsRefreshButton.TabIndex = 22;
			this.toolTipper.SetToolTip(this.objectDetailsRefreshButton, "Refresh object details");
			this.objectDetailsRefreshButton.UseVisualStyleBackColor = true;
			this.objectDetailsRefreshButton.Click += new System.EventHandler(this.objectDetailsRefreshButton_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.objectDrawEnabledCheckbox);
			this.groupBox1.Controls.Add(this.objectUpdateEnabledCheckbox);
			this.groupBox1.Location = new System.Drawing.Point(10, 71);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(111, 78);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Control";
			// 
			// objectDrawEnabledCheckbox
			// 
			this.objectDrawEnabledCheckbox.AutoSize = true;
			this.objectDrawEnabledCheckbox.Location = new System.Drawing.Point(6, 45);
			this.objectDrawEnabledCheckbox.Name = "objectDrawEnabledCheckbox";
			this.objectDrawEnabledCheckbox.Size = new System.Drawing.Size(51, 17);
			this.objectDrawEnabledCheckbox.TabIndex = 1;
			this.objectDrawEnabledCheckbox.Text = "Draw";
			this.objectDrawEnabledCheckbox.UseVisualStyleBackColor = true;
			this.objectDrawEnabledCheckbox.CheckedChanged += new System.EventHandler(this.objectDrawEnabledCheckbox_CheckedChanged);
			// 
			// objectUpdateEnabledCheckbox
			// 
			this.objectUpdateEnabledCheckbox.AutoSize = true;
			this.objectUpdateEnabledCheckbox.Location = new System.Drawing.Point(6, 22);
			this.objectUpdateEnabledCheckbox.Name = "objectUpdateEnabledCheckbox";
			this.objectUpdateEnabledCheckbox.Size = new System.Drawing.Size(61, 17);
			this.objectUpdateEnabledCheckbox.TabIndex = 0;
			this.objectUpdateEnabledCheckbox.Text = "Update";
			this.objectUpdateEnabledCheckbox.UseVisualStyleBackColor = true;
			this.objectUpdateEnabledCheckbox.CheckedChanged += new System.EventHandler(this.objectUpdateEnabledCheckbox_CheckedChanged);
			// 
			// objectLayerGroupBox
			// 
			this.objectLayerGroupBox.Controls.Add(this.objectLayerVarianceText);
			this.objectLayerGroupBox.Controls.Add(this.label2);
			this.objectLayerGroupBox.Controls.Add(this.objectParentLayerText);
			this.objectLayerGroupBox.Controls.Add(this.label1);
			this.objectLayerGroupBox.Controls.Add(this.objectDrawLayerText);
			this.objectLayerGroupBox.Controls.Add(this.objectDrawLayerLabel);
			this.objectLayerGroupBox.Controls.Add(this.objectLayerText);
			this.objectLayerGroupBox.Controls.Add(this.objectInheritLayerCheckBox);
			this.objectLayerGroupBox.Controls.Add(this.objectLayerLabel);
			this.objectLayerGroupBox.Controls.Add(this.objectVarianceCheckBox);
			this.objectLayerGroupBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.objectLayerGroupBox.Location = new System.Drawing.Point(127, 71);
			this.objectLayerGroupBox.Name = "objectLayerGroupBox";
			this.objectLayerGroupBox.Size = new System.Drawing.Size(152, 171);
			this.objectLayerGroupBox.TabIndex = 18;
			this.objectLayerGroupBox.TabStop = false;
			this.objectLayerGroupBox.Text = "Layer";
			// 
			// objectLayerVarianceText
			// 
			this.objectLayerVarianceText.Location = new System.Drawing.Point(58, 117);
			this.objectLayerVarianceText.Name = "objectLayerVarianceText";
			this.objectLayerVarianceText.ReadOnly = true;
			this.objectLayerVarianceText.Size = new System.Drawing.Size(88, 20);
			this.objectLayerVarianceText.TabIndex = 29;
			this.objectLayerVarianceText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(28, 120);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 13);
			this.label2.TabIndex = 28;
			this.label2.Text = "Var.:";
			// 
			// objectParentLayerText
			// 
			this.objectParentLayerText.Location = new System.Drawing.Point(58, 91);
			this.objectParentLayerText.Name = "objectParentLayerText";
			this.objectParentLayerText.ReadOnly = true;
			this.objectParentLayerText.Size = new System.Drawing.Size(88, 20);
			this.objectParentLayerText.TabIndex = 27;
			this.objectParentLayerText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(16, 94);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 26;
			this.label1.Text = "Parent:";
			// 
			// objectDrawLayerText
			// 
			this.objectDrawLayerText.Location = new System.Drawing.Point(58, 143);
			this.objectDrawLayerText.Name = "objectDrawLayerText";
			this.objectDrawLayerText.ReadOnly = true;
			this.objectDrawLayerText.Size = new System.Drawing.Size(88, 20);
			this.objectDrawLayerText.TabIndex = 25;
			this.objectDrawLayerText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// objectDrawLayerLabel
			// 
			this.objectDrawLayerLabel.AutoSize = true;
			this.objectDrawLayerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.objectDrawLayerLabel.Location = new System.Drawing.Point(25, 146);
			this.objectDrawLayerLabel.Name = "objectDrawLayerLabel";
			this.objectDrawLayerLabel.Size = new System.Drawing.Size(32, 13);
			this.objectDrawLayerLabel.TabIndex = 24;
			this.objectDrawLayerLabel.Text = "Final:";
			// 
			// objectLayerText
			// 
			this.objectLayerText.Location = new System.Drawing.Point(58, 65);
			this.objectLayerText.Name = "objectLayerText";
			this.objectLayerText.ReadOnly = true;
			this.objectLayerText.Size = new System.Drawing.Size(88, 20);
			this.objectLayerText.TabIndex = 23;
			this.objectLayerText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// objectInheritLayerCheckBox
			// 
			this.objectInheritLayerCheckBox.AutoSize = true;
			this.objectInheritLayerCheckBox.Location = new System.Drawing.Point(58, 19);
			this.objectInheritLayerCheckBox.Name = "objectInheritLayerCheckBox";
			this.objectInheritLayerCheckBox.Size = new System.Drawing.Size(79, 17);
			this.objectInheritLayerCheckBox.TabIndex = 1;
			this.objectInheritLayerCheckBox.Text = "Inheritance";
			this.objectInheritLayerCheckBox.UseVisualStyleBackColor = true;
			this.objectInheritLayerCheckBox.CheckedChanged += new System.EventHandler(this.objectInheritLayerCheckBox_CheckedChanged);
			// 
			// objectLayerLabel
			// 
			this.objectLayerLabel.AutoSize = true;
			this.objectLayerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.objectLayerLabel.Location = new System.Drawing.Point(12, 68);
			this.objectLayerLabel.Name = "objectLayerLabel";
			this.objectLayerLabel.Size = new System.Drawing.Size(45, 13);
			this.objectLayerLabel.TabIndex = 22;
			this.objectLayerLabel.Text = "Internal:";
			// 
			// objectVarianceCheckBox
			// 
			this.objectVarianceCheckBox.AutoSize = true;
			this.objectVarianceCheckBox.Location = new System.Drawing.Point(58, 42);
			this.objectVarianceCheckBox.Name = "objectVarianceCheckBox";
			this.objectVarianceCheckBox.Size = new System.Drawing.Size(68, 17);
			this.objectVarianceCheckBox.TabIndex = 0;
			this.objectVarianceCheckBox.Text = "Variance";
			this.objectVarianceCheckBox.UseVisualStyleBackColor = true;
			this.objectVarianceCheckBox.CheckedChanged += new System.EventHandler(this.objectVarianceCheckBox_CheckedChanged);
			// 
			// objectPositionGroupBox
			// 
			this.objectPositionGroupBox.Controls.Add(this.worldPositionLabel);
			this.objectPositionGroupBox.Controls.Add(this.localPositionXText);
			this.objectPositionGroupBox.Controls.Add(this.worldPositionYText);
			this.objectPositionGroupBox.Controls.Add(this.localPositionYText);
			this.objectPositionGroupBox.Controls.Add(this.worldPositionXText);
			this.objectPositionGroupBox.Controls.Add(this.localPositionLabel);
			this.objectPositionGroupBox.Location = new System.Drawing.Point(6, 155);
			this.objectPositionGroupBox.Name = "objectPositionGroupBox";
			this.objectPositionGroupBox.Size = new System.Drawing.Size(115, 87);
			this.objectPositionGroupBox.TabIndex = 17;
			this.objectPositionGroupBox.TabStop = false;
			this.objectPositionGroupBox.Text = "Position";
			// 
			// componentDetailsGroupBox
			// 
			this.componentDetailsGroupBox.Controls.Add(this.componentDetailsButton);
			this.componentDetailsGroupBox.Controls.Add(this.componentsList);
			this.componentDetailsGroupBox.Controls.Add(this.componentCountLabel);
			this.componentDetailsGroupBox.Controls.Add(this.componentCountText);
			this.componentDetailsGroupBox.Location = new System.Drawing.Point(285, 71);
			this.componentDetailsGroupBox.Name = "componentDetailsGroupBox";
			this.componentDetailsGroupBox.Size = new System.Drawing.Size(146, 286);
			this.componentDetailsGroupBox.TabIndex = 15;
			this.componentDetailsGroupBox.TabStop = false;
			this.componentDetailsGroupBox.Text = "Components";
			// 
			// componentDetailsButton
			// 
			this.componentDetailsButton.Enabled = false;
			this.componentDetailsButton.Location = new System.Drawing.Point(71, 250);
			this.componentDetailsButton.Name = "componentDetailsButton";
			this.componentDetailsButton.Size = new System.Drawing.Size(69, 23);
			this.componentDetailsButton.TabIndex = 15;
			this.componentDetailsButton.Text = "Details";
			this.componentDetailsButton.UseVisualStyleBackColor = true;
			// 
			// componentCountText
			// 
			this.componentCountText.Location = new System.Drawing.Point(77, 19);
			this.componentCountText.Name = "componentCountText";
			this.componentCountText.ReadOnly = true;
			this.componentCountText.Size = new System.Drawing.Size(34, 20);
			this.componentCountText.TabIndex = 14;
			this.componentCountText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// objectChildrenCountText
			// 
			this.objectChildrenCountText.Location = new System.Drawing.Point(362, 18);
			this.objectChildrenCountText.Name = "objectChildrenCountText";
			this.objectChildrenCountText.ReadOnly = true;
			this.objectChildrenCountText.Size = new System.Drawing.Size(34, 20);
			this.objectChildrenCountText.TabIndex = 13;
			this.objectChildrenCountText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// objectChildrenCountLabel
			// 
			this.objectChildrenCountLabel.AutoSize = true;
			this.objectChildrenCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.objectChildrenCountLabel.Location = new System.Drawing.Point(308, 21);
			this.objectChildrenCountLabel.Name = "objectChildrenCountLabel";
			this.objectChildrenCountLabel.Size = new System.Drawing.Size(48, 13);
			this.objectChildrenCountLabel.TabIndex = 12;
			this.objectChildrenCountLabel.Text = "Children:";
			// 
			// objectListGroupBox
			// 
			this.objectListGroupBox.BackColor = System.Drawing.Color.Transparent;
			this.objectListGroupBox.Controls.Add(this.objectListRefresh);
			this.objectListGroupBox.Controls.Add(this.objectTree);
			this.objectListGroupBox.Location = new System.Drawing.Point(6, 6);
			this.objectListGroupBox.Name = "objectListGroupBox";
			this.objectListGroupBox.Size = new System.Drawing.Size(169, 397);
			this.objectListGroupBox.TabIndex = 2;
			this.objectListGroupBox.TabStop = false;
			this.objectListGroupBox.Text = "All Objects";
			// 
			// objectListRefresh
			// 
			this.objectListRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.objectListRefresh.BackColor = System.Drawing.Color.Transparent;
			this.objectListRefresh.BackgroundImage = global::PigeonEngine.Properties.Resources.refresh;
			this.objectListRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.objectListRefresh.Location = new System.Drawing.Point(69, 363);
			this.objectListRefresh.Name = "objectListRefresh";
			this.objectListRefresh.Size = new System.Drawing.Size(24, 24);
			this.objectListRefresh.TabIndex = 2;
			this.toolTipper.SetToolTip(this.objectListRefresh, "Refresh object list");
			this.objectListRefresh.UseVisualStyleBackColor = false;
			this.objectListRefresh.Click += new System.EventHandler(this.objectListRefresh_Click);
			// 
			// utilitiesTabPage
			// 
			this.utilitiesTabPage.Controls.Add(this.utilitiesResolutionSetButton);
			this.utilitiesTabPage.Controls.Add(this.utilitiesResolutionYTextfield);
			this.utilitiesTabPage.Controls.Add(this.utilitiesResolutionXTextfield);
			this.utilitiesTabPage.Controls.Add(this.utilitiesResolutionLabel);
			this.utilitiesTabPage.Controls.Add(this.numericUpDown1);
			this.utilitiesTabPage.Controls.Add(this.utilitiesScaleLabel);
			this.utilitiesTabPage.Location = new System.Drawing.Point(4, 22);
			this.utilitiesTabPage.Name = "utilitiesTabPage";
			this.utilitiesTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.utilitiesTabPage.Size = new System.Drawing.Size(631, 414);
			this.utilitiesTabPage.TabIndex = 1;
			this.utilitiesTabPage.Text = "Utilities";
			this.utilitiesTabPage.UseVisualStyleBackColor = true;
			// 
			// utilitiesResolutionSetButton
			// 
			this.utilitiesResolutionSetButton.Location = new System.Drawing.Point(165, 38);
			this.utilitiesResolutionSetButton.Name = "utilitiesResolutionSetButton";
			this.utilitiesResolutionSetButton.Size = new System.Drawing.Size(34, 23);
			this.utilitiesResolutionSetButton.TabIndex = 11;
			this.utilitiesResolutionSetButton.Text = "Set";
			this.utilitiesResolutionSetButton.UseVisualStyleBackColor = true;
			this.utilitiesResolutionSetButton.Click += new System.EventHandler(this.utilitiesResolutionSetButton_Click);
			// 
			// utilitiesResolutionYTextfield
			// 
			this.utilitiesResolutionYTextfield.Location = new System.Drawing.Point(120, 40);
			this.utilitiesResolutionYTextfield.Name = "utilitiesResolutionYTextfield";
			this.utilitiesResolutionYTextfield.Size = new System.Drawing.Size(39, 20);
			this.utilitiesResolutionYTextfield.TabIndex = 10;
			this.utilitiesResolutionYTextfield.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.utilitiesResolutionTextFields_KeyPress);
			// 
			// utilitiesResolutionXTextfield
			// 
			this.utilitiesResolutionXTextfield.Location = new System.Drawing.Point(75, 40);
			this.utilitiesResolutionXTextfield.Name = "utilitiesResolutionXTextfield";
			this.utilitiesResolutionXTextfield.Size = new System.Drawing.Size(39, 20);
			this.utilitiesResolutionXTextfield.TabIndex = 9;
			this.utilitiesResolutionXTextfield.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.utilitiesResolutionTextFields_KeyPress);
			// 
			// utilitiesResolutionLabel
			// 
			this.utilitiesResolutionLabel.AutoSize = true;
			this.utilitiesResolutionLabel.Location = new System.Drawing.Point(9, 43);
			this.utilitiesResolutionLabel.Name = "utilitiesResolutionLabel";
			this.utilitiesResolutionLabel.Size = new System.Drawing.Size(60, 13);
			this.utilitiesResolutionLabel.TabIndex = 8;
			this.utilitiesResolutionLabel.Text = "Resolution:";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(75, 11);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(37, 20);
			this.numericUpDown1.TabIndex = 7;
			this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// utilitiesScaleLabel
			// 
			this.utilitiesScaleLabel.AutoSize = true;
			this.utilitiesScaleLabel.Location = new System.Drawing.Point(9, 13);
			this.utilitiesScaleLabel.Name = "utilitiesScaleLabel";
			this.utilitiesScaleLabel.Size = new System.Drawing.Size(37, 13);
			this.utilitiesScaleLabel.TabIndex = 0;
			this.utilitiesScaleLabel.Text = "Scale:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.utilitiesTickButton);
			this.groupBox2.Controls.Add(this.utilitiesPauseButton);
			this.groupBox2.Location = new System.Drawing.Point(269, 473);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(98, 50);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Frame Control";
			// 
			// utilitiesTickButton
			// 
			this.utilitiesTickButton.BackgroundImage = global::PigeonEngine.Properties.Resources.tick;
			this.utilitiesTickButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.utilitiesTickButton.Location = new System.Drawing.Point(52, 19);
			this.utilitiesTickButton.Name = "utilitiesTickButton";
			this.utilitiesTickButton.Size = new System.Drawing.Size(24, 24);
			this.utilitiesTickButton.TabIndex = 1;
			this.toolTipper.SetToolTip(this.utilitiesTickButton, "Tick (one frame)");
			this.utilitiesTickButton.UseVisualStyleBackColor = true;
			this.utilitiesTickButton.Click += new System.EventHandler(this.tickButton_Click);
			// 
			// utilitiesPauseButton
			// 
			this.utilitiesPauseButton.BackgroundImage = global::PigeonEngine.Properties.Resources.pause;
			this.utilitiesPauseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.utilitiesPauseButton.Location = new System.Drawing.Point(22, 19);
			this.utilitiesPauseButton.Name = "utilitiesPauseButton";
			this.utilitiesPauseButton.Size = new System.Drawing.Size(24, 24);
			this.utilitiesPauseButton.TabIndex = 0;
			this.toolTipper.SetToolTip(this.utilitiesPauseButton, "Pause");
			this.utilitiesPauseButton.UseVisualStyleBackColor = true;
			this.utilitiesPauseButton.Click += new System.EventHandler(this.pauseButton_Click);
			// 
			// toolTipper
			// 
			this.toolTipper.Tag = "abc";
			// 
			// menuStrip1
			// 
			this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem,
            this.miscToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.menuStrip1.Size = new System.Drawing.Size(663, 24);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.animatorToolStripMenuItem,
            this.hitboxesToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
			this.toolsToolStripMenuItem.Text = "Design tools";
			// 
			// animatorToolStripMenuItem
			// 
			this.animatorToolStripMenuItem.Name = "animatorToolStripMenuItem";
			this.animatorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.animatorToolStripMenuItem.Text = "Animator";
			this.animatorToolStripMenuItem.Click += new System.EventHandler(this.animatorToolStripMenuItem_Click);
			// 
			// hitboxesToolStripMenuItem
			// 
			this.hitboxesToolStripMenuItem.Enabled = false;
			this.hitboxesToolStripMenuItem.Name = "hitboxesToolStripMenuItem";
			this.hitboxesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.hitboxesToolStripMenuItem.Text = "Hitboxes";
			// 
			// miscToolStripMenuItem
			// 
			this.miscToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.takeScreenshotToolStripMenuItem,
            this.openSaveDirectoryToolStripMenuItem,
            this.sandboxWorldToolStripMenuItem});
			this.miscToolStripMenuItem.Name = "miscToolStripMenuItem";
			this.miscToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
			this.miscToolStripMenuItem.Text = "Commands";
			// 
			// takeScreenshotToolStripMenuItem
			// 
			this.takeScreenshotToolStripMenuItem.Name = "takeScreenshotToolStripMenuItem";
			this.takeScreenshotToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.takeScreenshotToolStripMenuItem.Text = "Screenshot";
			this.takeScreenshotToolStripMenuItem.Click += new System.EventHandler(this.takeScreenshotToolStripMenuItem_Click);
			// 
			// openSaveDirectoryToolStripMenuItem
			// 
			this.openSaveDirectoryToolStripMenuItem.Name = "openSaveDirectoryToolStripMenuItem";
			this.openSaveDirectoryToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.openSaveDirectoryToolStripMenuItem.Text = "Open save dir";
			this.openSaveDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openSaveDirectoryToolStripMenuItem_Click);
			// 
			// sandboxWorldToolStripMenuItem
			// 
			this.sandboxWorldToolStripMenuItem.Name = "sandboxWorldToolStripMenuItem";
			this.sandboxWorldToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.sandboxWorldToolStripMenuItem.Text = "Sandbox world";
			this.sandboxWorldToolStripMenuItem.Click += new System.EventHandler(this.sandboxWorldToolStripMenuItem_Click);
			// 
			// PigeonUi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(663, 556);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.Name = "PigeonUi";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Pigeon";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PigeonUi_FormClosed);
			this.Load += new System.EventHandler(this.PigeonUi_Load);
			this.tabControl.ResumeLayout(false);
			this.consoleTabPage.ResumeLayout(false);
			this.consoleTabPage.PerformLayout();
			this.objectsTabPage.ResumeLayout(false);
			this.objectDetailsGroupBox.ResumeLayout(false);
			this.objectDetailsGroupBox.PerformLayout();
			this.objectFlipGroupBox.ResumeLayout(false);
			this.objectFlipGroupBox.PerformLayout();
			this.objectDebugGroupBox.ResumeLayout(false);
			this.objectDebugGroupBox.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.objectLayerGroupBox.ResumeLayout(false);
			this.objectLayerGroupBox.PerformLayout();
			this.objectPositionGroupBox.ResumeLayout(false);
			this.objectPositionGroupBox.PerformLayout();
			this.componentDetailsGroupBox.ResumeLayout(false);
			this.componentDetailsGroupBox.PerformLayout();
			this.objectListGroupBox.ResumeLayout(false);
			this.utilitiesTabPage.ResumeLayout(false);
			this.utilitiesTabPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView objectTree;
		private System.Windows.Forms.ListBox componentsList;
		private System.Windows.Forms.Label componentCountLabel;
		private System.Windows.Forms.Button objectListRefresh;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage objectsTabPage;
		private System.Windows.Forms.TabPage utilitiesTabPage;
		private System.Windows.Forms.TextBox objectNameText;
		private System.Windows.Forms.Label objectNameLabel;
		private System.Windows.Forms.Label localPositionLabel;
		private System.Windows.Forms.TextBox worldPositionXText;
		private System.Windows.Forms.Label worldPositionLabel;
		private System.Windows.Forms.TextBox localPositionYText;
		private System.Windows.Forms.TextBox localPositionXText;
		private System.Windows.Forms.TextBox worldPositionYText;
		private System.Windows.Forms.TextBox componentCountText;
		private System.Windows.Forms.TextBox objectChildrenCountText;
		private System.Windows.Forms.Label objectChildrenCountLabel;
		private System.Windows.Forms.GroupBox objectDetailsGroupBox;
		private System.Windows.Forms.GroupBox componentDetailsGroupBox;
		private System.Windows.Forms.GroupBox objectListGroupBox;
		private System.Windows.Forms.TabPage consoleTabPage;
		private System.Windows.Forms.GroupBox objectLayerGroupBox;
		private System.Windows.Forms.GroupBox objectPositionGroupBox;
		private System.Windows.Forms.CheckBox drawPositionCheckbox;
		private System.Windows.Forms.Button componentDetailsButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox objectDrawEnabledCheckbox;
		private System.Windows.Forms.CheckBox objectUpdateEnabledCheckbox;
		private System.Windows.Forms.CheckBox objectVarianceCheckBox;
		private System.Windows.Forms.CheckBox objectFlipXCheckBox;
		private System.Windows.Forms.CheckBox objectFlipYCheckBox;
		private System.Windows.Forms.CheckBox objectInheritLayerCheckBox;
		private System.Windows.Forms.TextBox objectLayerText;
		private System.Windows.Forms.Label objectLayerLabel;
		private System.Windows.Forms.TextBox objectDrawLayerText;
		private System.Windows.Forms.Label objectDrawLayerLabel;
		private System.Windows.Forms.TextBox consoleCommandText;
		private System.Windows.Forms.TextBox objectLayerVarianceText;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox objectParentLayerText;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RichTextBox consoleLogText;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button utilitiesTickButton;
		private System.Windows.Forms.Button utilitiesPauseButton;
		private System.Windows.Forms.ToolTip toolTipper;
		private System.Windows.Forms.Button consoleCommandCommit;
		private System.Windows.Forms.Button objectDetailsRefreshButton;
		private System.Windows.Forms.GroupBox objectFlipGroupBox;
		private System.Windows.Forms.GroupBox objectDebugGroupBox;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label utilitiesScaleLabel;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem animatorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hitboxesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miscToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem takeScreenshotToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openSaveDirectoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sandboxWorldToolStripMenuItem;
		private System.Windows.Forms.Button utilitiesResolutionSetButton;
		private System.Windows.Forms.TextBox utilitiesResolutionYTextfield;
		private System.Windows.Forms.TextBox utilitiesResolutionXTextfield;
		private System.Windows.Forms.Label utilitiesResolutionLabel;
	}
}