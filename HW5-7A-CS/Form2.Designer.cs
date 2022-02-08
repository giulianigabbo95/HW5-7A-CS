namespace MyHomework
{
    partial class Form2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBarChartV = new System.Windows.Forms.Button();
            this.btnBarChartH = new System.Windows.Forms.Button();
            this.btnScatterChart3 = new System.Windows.Forms.Button();
            this.btnScatterChart2 = new System.Windows.Forms.Button();
            this.btnScatterChart1 = new System.Windows.Forms.Button();
            this.btnGenerateSample = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnBarChartV);
            this.panel1.Controls.Add(this.btnBarChartH);
            this.panel1.Controls.Add(this.btnScatterChart3);
            this.panel1.Controls.Add(this.btnScatterChart2);
            this.panel1.Controls.Add(this.btnScatterChart1);
            this.panel1.Controls.Add(this.btnGenerateSample);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(940, 60);
            this.panel1.TabIndex = 7;
            // 
            // btnBarChartV
            // 
            this.btnBarChartV.Location = new System.Drawing.Point(634, 12);
            this.btnBarChartV.Name = "btnBarChartV";
            this.btnBarChartV.Size = new System.Drawing.Size(107, 38);
            this.btnBarChartV.TabIndex = 12;
            this.btnBarChartV.Text = "Bar Chart V";
            this.btnBarChartV.UseVisualStyleBackColor = true;
            this.btnBarChartV.Click += new System.EventHandler(this.btnBarChartH_Click);
            // 
            // btnBarChartH
            // 
            this.btnBarChartH.Location = new System.Drawing.Point(521, 12);
            this.btnBarChartH.Name = "btnBarChartH";
            this.btnBarChartH.Size = new System.Drawing.Size(107, 38);
            this.btnBarChartH.TabIndex = 11;
            this.btnBarChartH.Text = "Bar Chart H";
            this.btnBarChartH.UseVisualStyleBackColor = true;
            this.btnBarChartH.Click += new System.EventHandler(this.btnBarChartV_Click);
            // 
            // btnScatterChart3
            // 
            this.btnScatterChart3.Location = new System.Drawing.Point(350, 12);
            this.btnScatterChart3.Name = "btnScatterChart3";
            this.btnScatterChart3.Size = new System.Drawing.Size(107, 38);
            this.btnScatterChart3.TabIndex = 10;
            this.btnScatterChart3.Text = "Scatter Chart 3";
            this.btnScatterChart3.UseVisualStyleBackColor = true;
            this.btnScatterChart3.Click += new System.EventHandler(this.btnScatterChart3_Click);
            // 
            // btnScatterChart2
            // 
            this.btnScatterChart2.Location = new System.Drawing.Point(237, 12);
            this.btnScatterChart2.Name = "btnScatterChart2";
            this.btnScatterChart2.Size = new System.Drawing.Size(107, 38);
            this.btnScatterChart2.TabIndex = 9;
            this.btnScatterChart2.Text = "Scatter Chart 2";
            this.btnScatterChart2.UseVisualStyleBackColor = true;
            this.btnScatterChart2.Click += new System.EventHandler(this.btnScatterChart2_Click);
            // 
            // btnScatterChart1
            // 
            this.btnScatterChart1.Location = new System.Drawing.Point(124, 12);
            this.btnScatterChart1.Name = "btnScatterChart1";
            this.btnScatterChart1.Size = new System.Drawing.Size(107, 38);
            this.btnScatterChart1.TabIndex = 8;
            this.btnScatterChart1.Text = "Scatter Chart 1";
            this.btnScatterChart1.UseVisualStyleBackColor = true;
            this.btnScatterChart1.Click += new System.EventHandler(this.btnScatterChart1_Click);
            // 
            // btnGenerateSample
            // 
            this.btnGenerateSample.Location = new System.Drawing.Point(11, 12);
            this.btnGenerateSample.Name = "btnGenerateSample";
            this.btnGenerateSample.Size = new System.Drawing.Size(107, 38);
            this.btnGenerateSample.TabIndex = 7;
            this.btnGenerateSample.Text = "Load CSV";
            this.btnGenerateSample.UseVisualStyleBackColor = true;
            this.btnGenerateSample.Click += new System.EventHandler(this.btnGenerateSample_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 60);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(940, 516);
            this.mainPanel.TabIndex = 8;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 576);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.panel1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnScatterChart3;
        private System.Windows.Forms.Button btnScatterChart2;
        private System.Windows.Forms.Button btnScatterChart1;
        private System.Windows.Forms.Button btnGenerateSample;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button btnBarChartH;
        private System.Windows.Forms.Button btnBarChartV;
    }
}