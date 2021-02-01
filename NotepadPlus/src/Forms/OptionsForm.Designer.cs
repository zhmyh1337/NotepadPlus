
namespace NotepadPlus
{
    partial class OptionsForm
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
            System.Windows.Forms.Label autosaveLabel;
            System.Windows.Forms.Label autologgingLabel;
            this._listbox = new System.Windows.Forms.ListBox();
            this._autosavePanel = new System.Windows.Forms.Panel();
            this._autosaveRadioButton5 = new System.Windows.Forms.RadioButton();
            this._autosaveRadioButton4 = new System.Windows.Forms.RadioButton();
            this._autosaveRadioButton3 = new System.Windows.Forms.RadioButton();
            this._autosaveRadioButton2 = new System.Windows.Forms.RadioButton();
            this._autosaveRadioButton1 = new System.Windows.Forms.RadioButton();
            this._autologgingPanel = new System.Windows.Forms.Panel();
            this._autologgingRadioButton5 = new System.Windows.Forms.RadioButton();
            this._autologgingRadioButton4 = new System.Windows.Forms.RadioButton();
            this._autologgingRadioButton3 = new System.Windows.Forms.RadioButton();
            this._autologgingRadioButton2 = new System.Windows.Forms.RadioButton();
            this._autologgingRadioButton1 = new System.Windows.Forms.RadioButton();
            autosaveLabel = new System.Windows.Forms.Label();
            autologgingLabel = new System.Windows.Forms.Label();
            this._autosavePanel.SuspendLayout();
            this._autologgingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // autosaveLabel
            // 
            autosaveLabel.AutoSize = true;
            autosaveLabel.Location = new System.Drawing.Point(10, 0);
            autosaveLabel.Name = "autosaveLabel";
            autosaveLabel.Size = new System.Drawing.Size(70, 20);
            autosaveLabel.TabIndex = 2;
            autosaveLabel.Text = "Autosave";
            // 
            // autologgingLabel
            // 
            autologgingLabel.AutoSize = true;
            autologgingLabel.Location = new System.Drawing.Point(10, 0);
            autologgingLabel.Name = "autologgingLabel";
            autologgingLabel.Size = new System.Drawing.Size(93, 20);
            autologgingLabel.TabIndex = 2;
            autologgingLabel.Text = "Autologging";
            // 
            // _listbox
            // 
            this._listbox.FormattingEnabled = true;
            this._listbox.ItemHeight = 20;
            this._listbox.Items.AddRange(new object[] {
            "Autosave",
            "Autologging",
            "Syntax coloring (C#)",
            "Compilation"});
            this._listbox.Location = new System.Drawing.Point(12, 12);
            this._listbox.Name = "_listbox";
            this._listbox.Size = new System.Drawing.Size(151, 244);
            this._listbox.TabIndex = 0;
            this._listbox.SelectedIndexChanged += new System.EventHandler(this.OnListboxSelectedIndexChanged);
            // 
            // _autosavePanel
            // 
            this._autosavePanel.Controls.Add(this._autosaveRadioButton5);
            this._autosavePanel.Controls.Add(this._autosaveRadioButton4);
            this._autosavePanel.Controls.Add(this._autosaveRadioButton3);
            this._autosavePanel.Controls.Add(autosaveLabel);
            this._autosavePanel.Controls.Add(this._autosaveRadioButton2);
            this._autosavePanel.Controls.Add(this._autosaveRadioButton1);
            this._autosavePanel.Location = new System.Drawing.Point(169, 12);
            this._autosavePanel.Name = "_autosavePanel";
            this._autosavePanel.Size = new System.Drawing.Size(373, 244);
            this._autosavePanel.TabIndex = 1;
            // 
            // _autosaveRadioButton5
            // 
            this._autosaveRadioButton5.AutoSize = true;
            this._autosaveRadioButton5.Location = new System.Drawing.Point(10, 143);
            this._autosaveRadioButton5.Name = "_autosaveRadioButton5";
            this._autosaveRadioButton5.Size = new System.Drawing.Size(141, 24);
            this._autosaveRadioButton5.TabIndex = 5;
            this._autosaveRadioButton5.Text = "Every 30 minutes";
            this._autosaveRadioButton5.UseVisualStyleBackColor = true;
            // 
            // _autosaveRadioButton4
            // 
            this._autosaveRadioButton4.AutoSize = true;
            this._autosaveRadioButton4.Location = new System.Drawing.Point(10, 113);
            this._autosaveRadioButton4.Name = "_autosaveRadioButton4";
            this._autosaveRadioButton4.Size = new System.Drawing.Size(141, 24);
            this._autosaveRadioButton4.TabIndex = 4;
            this._autosaveRadioButton4.Text = "Every 10 minutes";
            this._autosaveRadioButton4.UseVisualStyleBackColor = true;
            // 
            // _autosaveRadioButton3
            // 
            this._autosaveRadioButton3.AutoSize = true;
            this._autosaveRadioButton3.Location = new System.Drawing.Point(10, 83);
            this._autosaveRadioButton3.Name = "_autosaveRadioButton3";
            this._autosaveRadioButton3.Size = new System.Drawing.Size(133, 24);
            this._autosaveRadioButton3.TabIndex = 3;
            this._autosaveRadioButton3.Text = "Every 5 minutes";
            this._autosaveRadioButton3.UseVisualStyleBackColor = true;
            // 
            // _autosaveRadioButton2
            // 
            this._autosaveRadioButton2.AutoSize = true;
            this._autosaveRadioButton2.Location = new System.Drawing.Point(10, 53);
            this._autosaveRadioButton2.Name = "_autosaveRadioButton2";
            this._autosaveRadioButton2.Size = new System.Drawing.Size(115, 24);
            this._autosaveRadioButton2.TabIndex = 1;
            this._autosaveRadioButton2.Text = "Every minute";
            this._autosaveRadioButton2.UseVisualStyleBackColor = true;
            // 
            // _autosaveRadioButton1
            // 
            this._autosaveRadioButton1.AutoSize = true;
            this._autosaveRadioButton1.Checked = true;
            this._autosaveRadioButton1.Location = new System.Drawing.Point(10, 23);
            this._autosaveRadioButton1.Name = "_autosaveRadioButton1";
            this._autosaveRadioButton1.Size = new System.Drawing.Size(113, 24);
            this._autosaveRadioButton1.TabIndex = 0;
            this._autosaveRadioButton1.TabStop = true;
            this._autosaveRadioButton1.Text = "No autosave";
            this._autosaveRadioButton1.UseVisualStyleBackColor = true;
            // 
            // _autologgingPanel
            // 
            this._autologgingPanel.BackColor = System.Drawing.SystemColors.Control;
            this._autologgingPanel.Controls.Add(this._autologgingRadioButton5);
            this._autologgingPanel.Controls.Add(this._autologgingRadioButton4);
            this._autologgingPanel.Controls.Add(this._autologgingRadioButton3);
            this._autologgingPanel.Controls.Add(autologgingLabel);
            this._autologgingPanel.Controls.Add(this._autologgingRadioButton2);
            this._autologgingPanel.Controls.Add(this._autologgingRadioButton1);
            this._autologgingPanel.Location = new System.Drawing.Point(169, 12);
            this._autologgingPanel.Name = "_autologgingPanel";
            this._autologgingPanel.Size = new System.Drawing.Size(373, 244);
            this._autologgingPanel.TabIndex = 6;
            // 
            // _autologgingRadioButton5
            // 
            this._autologgingRadioButton5.AutoSize = true;
            this._autologgingRadioButton5.Location = new System.Drawing.Point(10, 143);
            this._autologgingRadioButton5.Name = "_autologgingRadioButton5";
            this._autologgingRadioButton5.Size = new System.Drawing.Size(141, 24);
            this._autologgingRadioButton5.TabIndex = 5;
            this._autologgingRadioButton5.Text = "Every 30 minutes";
            this._autologgingRadioButton5.UseVisualStyleBackColor = true;
            // 
            // _autologgingRadioButton4
            // 
            this._autologgingRadioButton4.AutoSize = true;
            this._autologgingRadioButton4.Location = new System.Drawing.Point(10, 113);
            this._autologgingRadioButton4.Name = "_autologgingRadioButton4";
            this._autologgingRadioButton4.Size = new System.Drawing.Size(141, 24);
            this._autologgingRadioButton4.TabIndex = 4;
            this._autologgingRadioButton4.Text = "Every 10 minutes";
            this._autologgingRadioButton4.UseVisualStyleBackColor = true;
            // 
            // _autologgingRadioButton3
            // 
            this._autologgingRadioButton3.AutoSize = true;
            this._autologgingRadioButton3.Location = new System.Drawing.Point(10, 83);
            this._autologgingRadioButton3.Name = "_autologgingRadioButton3";
            this._autologgingRadioButton3.Size = new System.Drawing.Size(133, 24);
            this._autologgingRadioButton3.TabIndex = 3;
            this._autologgingRadioButton3.Text = "Every 5 minutes";
            this._autologgingRadioButton3.UseVisualStyleBackColor = true;
            // 
            // _autologgingRadioButton2
            // 
            this._autologgingRadioButton2.AutoSize = true;
            this._autologgingRadioButton2.Location = new System.Drawing.Point(10, 53);
            this._autologgingRadioButton2.Name = "_autologgingRadioButton2";
            this._autologgingRadioButton2.Size = new System.Drawing.Size(115, 24);
            this._autologgingRadioButton2.TabIndex = 1;
            this._autologgingRadioButton2.Text = "Every minute";
            this._autologgingRadioButton2.UseVisualStyleBackColor = true;
            // 
            // _autologgingRadioButton1
            // 
            this._autologgingRadioButton1.AutoSize = true;
            this._autologgingRadioButton1.Checked = true;
            this._autologgingRadioButton1.Location = new System.Drawing.Point(10, 23);
            this._autologgingRadioButton1.Name = "_autologgingRadioButton1";
            this._autologgingRadioButton1.Size = new System.Drawing.Size(136, 24);
            this._autologgingRadioButton1.TabIndex = 0;
            this._autologgingRadioButton1.TabStop = true;
            this._autologgingRadioButton1.Text = "No autologging";
            this._autologgingRadioButton1.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 267);
            this.Controls.Add(this._listbox);
            this.Controls.Add(this._autosavePanel);
            this.Controls.Add(this._autologgingPanel);
            this.Name = "OptionsForm";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.OnOptionsFormLoad);
            this._autosavePanel.ResumeLayout(false);
            this._autosavePanel.PerformLayout();
            this._autologgingPanel.ResumeLayout(false);
            this._autologgingPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox _listbox;
        private System.Windows.Forms.Panel _autosavePanel;
        private System.Windows.Forms.RadioButton _autosaveRadioButton4;
        private System.Windows.Forms.RadioButton _autosaveRadioButton3;
        private System.Windows.Forms.RadioButton _autosaveRadioButton2;
        private System.Windows.Forms.RadioButton _autosaveRadioButton1;
        private System.Windows.Forms.RadioButton _autosaveRadioButton5;
        private System.Windows.Forms.Panel _autologgingPanel;
        private System.Windows.Forms.RadioButton _autologgingRadioButton5;
        private System.Windows.Forms.RadioButton _autologgingRadioButton4;
        private System.Windows.Forms.RadioButton _autologgingRadioButton3;
        private System.Windows.Forms.RadioButton _autologgingRadioButton2;
        private System.Windows.Forms.RadioButton _autologgingRadioButton1;
    }
}