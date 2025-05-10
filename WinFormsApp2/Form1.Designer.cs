namespace WinFormsApp2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            pictureBox1 = new PictureBox();
            openFileDialog1 = new OpenFileDialog();
            button2 = new Button();
            radioHough = new RadioButton();
            radioTemplate = new RadioButton();
            radioContour = new RadioButton();
            radioHybrid = new RadioButton();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(44, 77);
            button1.Name = "button1";
            button1.Size = new Size(111, 29);
            button1.TabIndex = 0;
            button1.Text = "select image";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(245, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(508, 377);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            button2.Location = new Point(27, 287);
            button2.Name = "button2";
            button2.Size = new Size(128, 29);
            button2.TabIndex = 2;
            button2.Text = "Analyes";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // radioHough
            // 
            radioHough.AutoSize = true;
            radioHough.Location = new Point(27, 139);
            radioHough.Name = "radioHough";
            radioHough.Size = new Size(178, 24);
            radioHough.TabIndex = 3;
            radioHough.TabStop = true;
            radioHough.Text = "HoughCircleTransform";
            radioHough.UseVisualStyleBackColor = true;
            // 
            // radioTemplate
            // 
            radioTemplate.AutoSize = true;
            radioTemplate.Location = new Point(27, 169);
            radioTemplate.Name = "radioTemplate";
            radioTemplate.Size = new Size(154, 24);
            radioTemplate.TabIndex = 4;
            radioTemplate.TabStop = true;
            radioTemplate.Text = "TemplateMatching";
            radioTemplate.UseVisualStyleBackColor = true;
            // 
            // radioContour
            // 
            radioContour.AutoSize = true;
            radioContour.Location = new Point(27, 199);
            radioContour.Name = "radioContour";
            radioContour.Size = new Size(148, 24);
            radioContour.TabIndex = 5;
            radioContour.TabStop = true;
            radioContour.Text = "ContourDetection";
            radioContour.UseVisualStyleBackColor = true;
            // 
            // radioHybrid
            // 
            radioHybrid.AutoSize = true;
            radioHybrid.Location = new Point(27, 229);
            radioHybrid.Name = "radioHybrid";
            radioHybrid.Size = new Size(128, 24);
            radioHybrid.TabIndex = 6;
            radioHybrid.TabStop = true;
            radioHybrid.Text = "DetectAnswers";
            radioHybrid.UseVisualStyleBackColor = true;
           
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(808, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(508, 377);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1353, 450);
            Controls.Add(pictureBox2);
            Controls.Add(radioHybrid);
            Controls.Add(radioContour);
            Controls.Add(radioTemplate);
            Controls.Add(radioHough);
            Controls.Add(button2);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private PictureBox pictureBox1;
        private OpenFileDialog openFileDialog1;
        private Button button2;
        private RadioButton radioHough;
        private RadioButton radioTemplate;
        private RadioButton radioContour;
        private RadioButton radioHybrid;
        private PictureBox pictureBox2;
    }
}
