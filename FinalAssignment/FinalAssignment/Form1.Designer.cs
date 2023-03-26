namespace FinalAssignment
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dispayStack = new System.Windows.Forms.Button();
            this.displayArrList = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(38, 68);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(219, 226);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // dispayStack
            // 
            this.dispayStack.Location = new System.Drawing.Point(38, 308);
            this.dispayStack.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dispayStack.Name = "dispayStack";
            this.dispayStack.Size = new System.Drawing.Size(90, 35);
            this.dispayStack.TabIndex = 1;
            this.dispayStack.Text = "Display stack";
            this.dispayStack.UseVisualStyleBackColor = true;
            this.dispayStack.Click += new System.EventHandler(this.dispayStack_Click);
            // 
            // displayArrList
            // 
            this.displayArrList.Location = new System.Drawing.Point(166, 308);
            this.displayArrList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.displayArrList.Name = "displayArrList";
            this.displayArrList.Size = new System.Drawing.Size(90, 35);
            this.displayArrList.TabIndex = 2;
            this.displayArrList.Text = "Display arraylist";
            this.displayArrList.UseVisualStyleBackColor = true;
            this.displayArrList.Click += new System.EventHandler(this.displayArrList_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Time Elapsed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.displayArrList);
            this.Controls.Add(this.dispayStack);
            this.Controls.Add(this.richTextBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button dispayStack;
        private System.Windows.Forms.Button displayArrList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

