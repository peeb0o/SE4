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
            this.SuspendLayout();
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(22, 407);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(41, 22);
            this.runButton.TabIndex = 0;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButtonClicked);
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.drawPanel.Location = new System.Drawing.Point(456, 31);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(385, 316);
            this.drawPanel.TabIndex = 1;
            // 
            // singleCommandTextBox
            // 
            this.singleCommandTextBox.Location = new System.Drawing.Point(22, 374);
            this.singleCommandTextBox.Name = "singleCommandTextBox";
            this.singleCommandTextBox.Size = new System.Drawing.Size(385, 20);
            this.singleCommandTextBox.TabIndex = 3;
            this.singleCommandTextBox.TextChanged += new System.EventHandler(this.singleCommandTextBoxRunCommand);
            this.singleCommandTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.singleCommandTextBoxEnterPressed);
            // 
            // multiLineTextBox
            // 
            this.multiLineTextBox.Location = new System.Drawing.Point(22, 31);
            this.multiLineTextBox.Name = "multiLineTextBox";
            this.multiLineTextBox.Size = new System.Drawing.Size(385, 316);
            this.multiLineTextBox.TabIndex = 4;
            this.multiLineTextBox.Text = "";
            this.multiLineTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(22, 13);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 5;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(866, 451);
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
    }
}

