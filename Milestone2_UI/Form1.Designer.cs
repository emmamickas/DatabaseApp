
namespace Milestone1_Dellwo
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Checkin_Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Checkin_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Checkin_Chart)).BeginInit();
            this.SuspendLayout();
            // 
            // Checkin_Chart
            // 
            chartArea3.Name = "ChartArea1";
            this.Checkin_Chart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.Checkin_Chart.Legends.Add(legend3);
            this.Checkin_Chart.Location = new System.Drawing.Point(25, 12);
            this.Checkin_Chart.Name = "Checkin_Chart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.Checkin_Chart.Series.Add(series3);
            this.Checkin_Chart.Size = new System.Drawing.Size(590, 426);
            this.Checkin_Chart.TabIndex = 0;
            this.Checkin_Chart.Text = "Number of Checkins Per Month";
            // 
            // Checkin_Button
            // 
            this.Checkin_Button.Location = new System.Drawing.Point(641, 27);
            this.Checkin_Button.Name = "Checkin_Button";
            this.Checkin_Button.Size = new System.Drawing.Size(126, 107);
            this.Checkin_Button.TabIndex = 1;
            this.Checkin_Button.Text = "Check In";
            this.Checkin_Button.UseVisualStyleBackColor = true;
            this.Checkin_Button.Click += new System.EventHandler(this.Checkin_Button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Checkin_Button);
            this.Controls.Add(this.Checkin_Chart);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Checkin_Chart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart Checkin_Chart;
        private System.Windows.Forms.Button Checkin_Button;
    }
}