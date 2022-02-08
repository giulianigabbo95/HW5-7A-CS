using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomework
{
    public partial class Form2 : Form
    {
        private List<SampleDataStat> sampleStats = new List<SampleDataStat>();
        private List<Point> dataPoints = new List<Point>();
        private List<PointF> dataPointsF = new List<PointF>();
        private ggViewport[] panels = new ggViewport[3];

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ggViewport p0 = new ggViewport(mainPanel);
            p0.Name = "P0";
            p0.BackColor = Color.SteelBlue;
            p0.BorderStyle = BorderStyle.None;
            p0.Width = mainPanel.Width / 2;
            p0.Height = mainPanel.Height / 2;
            p0.Top = 0;
            p0.Left = 0;
            mainPanel.Controls.Add(p0);
            panels[0] = p0;

            ggViewport p1 = new ggViewport(mainPanel);
            p1.Name = "P1";
            p1.BackColor = Color.Salmon;
            p1.BorderStyle = BorderStyle.None;
            p1.Width = mainPanel.Width / 2;
            p1.Height = mainPanel.Height / 2;
            p1.Top = 0;
            p1.Left = p0.Right;
            mainPanel.Controls.Add(p1);
            panels[1] = p1;

            ggViewport p2 = new ggViewport(mainPanel);
            p2.Name = "P2";
            p2.BackColor = Color.Salmon;
            p2.BorderStyle = BorderStyle.None;
            p2.Width = mainPanel.Width / 2;
            p2.Height = mainPanel.Height / 2;
            p2.Top = p0.Bottom;
            p2.Left = 0;
            mainPanel.Controls.Add(p2);
            panels[2] = p2;

            p0.Resize += new EventHandler(viewport0_Resize);
            p1.Resize += new EventHandler(viewport1_Resize);
            p2.Resize += new EventHandler(viewport2_Resize);
        }

        // Events
        private void btnGenerateSample_Click(object sender, EventArgs e)
        {
            LoadSampleData();
            GenerateSampleData();
        }

        private void btnScatterChart1_Click(object sender, EventArgs e)
        {
            if (sampleStats.Count == 0)
                return;

            Graphics g = panels[0].CreateGraphics();
            DrawScatterChart1(g);
        }

        private void btnScatterChart2_Click(object sender, EventArgs e)
        {
            if (sampleStats.Count == 0)
                return;

            Graphics g = panels[0].CreateGraphics();
            DrawScatterChart2(g);
        }

        private void btnScatterChart3_Click(object sender, EventArgs e)
        {
            if (sampleStats.Count == 0)
                return;

            Graphics g = panels[0].CreateGraphics();
            DrawScatterChart3(g);
        }

        private void viewport0_Resize(object sender, EventArgs e)
        {
            Graphics g = panels[0].CreateGraphics();
            DrawScatterChart3(g);
        }

        private void viewport1_Resize(object sender, EventArgs e)
        {
            Graphics g = panels[1].CreateGraphics();
            DrawBarChartV(g);
        }

        private void viewport2_Resize(object sender, EventArgs e)
        {
            Graphics g = panels[2].CreateGraphics();
            DrawBarChartH(g);
        }

        private void btnBarChartH_Click(object sender, EventArgs e)
        {
            if (sampleStats.Count == 0)
                return;

            Graphics g = panels[1].CreateGraphics();
            DrawBarChartV(g);
        }

        private void btnBarChartV_Click(object sender, EventArgs e)
        {
            if (sampleStats.Count == 0)
                return;

            Graphics g = panels[2].CreateGraphics();
            DrawBarChartH(g);
        }

        // Methods
        private void GenerateSampleData()
        {
            dataPoints = new List<Point>();
            foreach (var p in sampleStats)
            {
                dataPoints.Add(new Point(p.Temperatura, p.Valore));
                dataPointsF.Add(new PointF((float)p.TemperaturaF, p.Valore));
            }
        }

        private void LoadSampleData()
        {
            string[] data;
            sampleStats = new List<SampleDataStat>();

            var lines = File.ReadAllLines(@"Dati\DataSample.csv").Skip(1);
            foreach (var line in lines)
            {
                try
                {
                    CultureInfo culture = new CultureInfo("us-US");
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        data = line.Split(',');

                        // Aggiunge un elemento della classe alla collezione
                        sampleStats.Add(new SampleDataStat()
                        {
                            Temperatura = (int)Math.Round(decimal.Parse(data[0].Trim(), culture), 0),
                            TemperaturaF = double.Parse(data[0].Trim(), culture),
                            Valore = int.Parse(data[1].Trim(), culture)
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore.\n\nError message: {ex.Message}\n\n" + $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void DrawScatterChart1(Graphics G)
        {
            var plots = new List<Point>();
            foreach (var item in sampleStats)
            {
                plots.Add(new Point() { X = item.Temperatura, Y = item.Valore });
            }

            var sc = new ggScatterChart(plots, panels[0]);
            sc.DrawChart(G);
            sc.DrawRegressionLine(G);
        }

        private void DrawScatterChart2(Graphics G)
        {
            var plots = new List<Point>();
            foreach (var item in sampleStats)
            {
                plots.Add(new Point() { X = item.Valore, Y = item.Temperatura });
            }
            var sc = new ggScatterChart(plots, panels[0]);
            sc.DrawChart(G);
            sc.DrawRegressionLine(G);
        }

        private void DrawScatterChart3(Graphics G)
        {
            var plots = new List<PointF>();
            foreach (var item in sampleStats)
            {
                plots.Add(new PointF() { X = (float)item.TemperaturaF, Y = item.Valore });
            }

            var sc = new ggScatterChart(plots, panels[0]);
            sc.DrawChart2(G);
            sc.DrawRegressionLine2(G);
        }

        private void DrawBarChartV(Graphics G)
        {
            var plots = new List<PointF>();
            foreach (var item in sampleStats)
            {
                plots.Add(new PointF() { X = (float)item.TemperaturaF, Y = item.Valore });
            }

            var bc = new ggBarChart(plots, panels[1]);
            bc.DrawChartH(G);
        }

        private void DrawBarChartH(Graphics G)
        {
            var plots = new List<PointF>();
            foreach (var item in sampleStats)
            {
                plots.Add(new PointF() { X = item.Valore, Y = (float)item.TemperaturaF });
            }

            var bc = new ggBarChart(plots, panels[2]);
            bc.DrawChartV(G);
        }
    }
}
