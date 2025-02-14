namespace Library
{
    partial class FeedbackForm
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
            this.listViewFeedback = new System.Windows.Forms.ListView();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewFeedback
            // 
            this.listViewFeedback.HideSelection = false;
            this.listViewFeedback.Location = new System.Drawing.Point(104, 23);
            this.listViewFeedback.Name = "listViewFeedback";
            this.listViewFeedback.Size = new System.Drawing.Size(536, 268);
            this.listViewFeedback.TabIndex = 0;
            this.listViewFeedback.UseCompatibleStateImageBehavior = false;
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(540, 323);
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(100, 20);
            this.txtResponse.TabIndex = 1;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(104, 323);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(100, 20);
            this.txtMessage.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(540, 349);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 25);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnRespond_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(104, 349);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 25);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // FeedbackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.listViewFeedback);
            this.Name = "FeedbackForm";
            this.Text = "FeedbackForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewFeedback;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}