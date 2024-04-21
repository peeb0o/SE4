using System.Drawing;
using System.Windows.Forms;

namespace SE4
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
            this.runButton = new System.Windows.Forms.Button();
            this.drawPanel = new System.Windows.Forms.Panel();
            this.singleCommandTextBox = new System.Windows.Forms.TextBox();
            this.multiLineTextBox = new System.Windows.Forms.RichTextBox();
            this.Save = new System.Windows.Forms.Button();
            this.Load = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // runButton
            // 
            this.runButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.runButton.Location = new System.Drawing.Point(12, 403);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(63, 22);
            this.runButton.TabIndex = 0;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButtonClicked);
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.drawPanel.Location = new System.Drawing.Point(253, 31);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(613, 331);
            this.drawPanel.TabIndex = 1;
            // 
            // singleCommandTextBox
            // 
            this.singleCommandTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.singleCommandTextBox.Location = new System.Drawing.Point(12, 377);
            this.singleCommandTextBox.Name = "singleCommandTextBox";
            this.singleCommandTextBox.Size = new System.Drawing.Size(385, 20);
            this.singleCommandTextBox.TabIndex = 3;
            this.singleCommandTextBox.TextChanged += new System.EventHandler(this.singleCommandTextBoxRunCommand);
            this.singleCommandTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.singleCommandTextBoxEnterPressed);
            // 
            // multiLineTextBox
            // 
            this.multiLineTextBox.Location = new System.Drawing.Point(12, 31);
            this.multiLineTextBox.Name = "multiLineTextBox";
            this.multiLineTextBox.Size = new System.Drawing.Size(223, 331);
            this.multiLineTextBox.TabIndex = 4;
            this.multiLineTextBox.Text = "";
            this.multiLineTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(710, 413);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 5;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.saveButtonClicked);
            // 
            // Load
            // 
            this.Load.Location = new System.Drawing.Point(791, 413);
            this.Load.Name = "Load";
            this.Load.Size = new System.Drawing.Size(75, 23);
            this.Load.TabIndex = 6;
            this.Load.Text = "Load";
            this.Load.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(874, 448);
            this.Controls.Add(this.Load);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.multiLineTextBox);
            this.Controls.Add(this.singleCommandTextBox);
            this.Controls.Add(this.drawPanel);
            this.Controls.Add(this.runButton);
            this.Name = "Form1";
            this.Text = "Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button runButton;
        private Panel drawPanel;
        private TextBox singleCommandTextBox;
        private RichTextBox multiLineTextBox;
        private Button Save;
        private Button Load;
    }
}

