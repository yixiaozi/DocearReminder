namespace DocearReminder
{
    partial class ChatGPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatGPT));
            this.question = new System.Windows.Forms.TextBox();
            this.ask_button = new System.Windows.Forms.Button();
            this.results_richtext = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // question
            // 
            this.question.Location = new System.Drawing.Point(25, 12);
            this.question.Name = "question";
            this.question.Size = new System.Drawing.Size(664, 21);
            this.question.TabIndex = 0;
            // 
            // ask_button
            // 
            this.ask_button.Location = new System.Drawing.Point(713, 12);
            this.ask_button.Name = "ask_button";
            this.ask_button.Size = new System.Drawing.Size(75, 23);
            this.ask_button.TabIndex = 1;
            this.ask_button.Text = "提交";
            this.ask_button.UseVisualStyleBackColor = true;
            this.ask_button.Click += new System.EventHandler(this.ask_button_ClickAsync);
            // 
            // results_richtext
            // 
            this.results_richtext.Location = new System.Drawing.Point(25, 49);
            this.results_richtext.Name = "results_richtext";
            this.results_richtext.Size = new System.Drawing.Size(763, 389);
            this.results_richtext.TabIndex = 2;
            this.results_richtext.Text = "";
            // 
            // ChatGPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.results_richtext);
            this.Controls.Add(this.ask_button);
            this.Controls.Add(this.question);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChatGPT";
            this.Text = "ChatGPT";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox question;
        private System.Windows.Forms.Button ask_button;
        private System.Windows.Forms.RichTextBox results_richtext;
    }
}