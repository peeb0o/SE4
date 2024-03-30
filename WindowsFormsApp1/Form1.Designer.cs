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
            this.runButton = new Button();
            this.drawPanel = new Panel();
            this.singleCommandTextBox = new TextBox();
            this.multiLineTextBox = new RichTextBox();
            this.SuspendLayout();
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(22, 407);
            this.runButton.Name = "button1";
            this.runButton.Size = new System.Drawing.Size(41, 22);
            this.runButton.TabIndex = 0;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.drawPanel.Location = new System.Drawing.Point(456, 31);
            this.drawPanel.Name = "panel1";
            this.drawPanel.Size = new System.Drawing.Size(385, 316);
            this.drawPanel.TabIndex = 1;
            // 
            // singleCommandTextBox
            // 
            this.singleCommandTextBox.Location = new System.Drawing.Point(22, 374);
            this.singleCommandTextBox.Name = "textBox1";
            this.singleCommandTextBox.Size = new System.Drawing.Size(385, 20);
            this.singleCommandTextBox.TabIndex = 3;
            // 
            // multiLineTextBox
            // 
            this.multiLineTextBox.Location = new System.Drawing.Point(22, 31);
            this.multiLineTextBox.Name = "richTextBox1";
            this.multiLineTextBox.Size = new System.Drawing.Size(385, 316);
            this.multiLineTextBox.TabIndex = 4;
            this.multiLineTextBox.Text = "";
            this.multiLineTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(866, 451);
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
    }
}

