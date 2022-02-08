using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyHomework
{
    public class ggBarChart
    {
        private List<PointF> _dataPoints;
        private Panel _container;

        private Pen penBlack = new Pen(Color.Black);
        private Pen penGray = new Pen(Color.LightGray);
        private Pen penBlue = new Pen(Color.Blue, 1);

        private int X;
        private int Y;
        private int W;
        private int H;

        private int distanceX;
        private int distanceY;
        private int stepX;
        private int stepY;
        private int offsetX;
        private int offsetY;

        private int markerSize = 3;

        private List<PointF> Y_Items = new List<PointF>();
        private List<PointF> X_Items = new List<PointF>();

        private List<Point> Y_Markers = new List<Point>();
        private List<Point> X_Markers = new List<Point>();

        private int Y_NbItems;
        private int X_NbItems;

        private int BarWidth;
        private int BarHeight;

        public ggBarChart(List<PointF> dataPoints, Panel container)
        {
            if (dataPoints.Count == 0) return;

            _dataPoints = dataPoints;
            _container = container;

            var orderedX = _dataPoints.OrderBy(p => p.X).ToList();
            var orderedY = _dataPoints.OrderBy(p => p.Y).ToList();

            Y_Items = new List<PointF>();
            Y_Items.Add(new PointF() { X = 0, Y = 0 });
            Y_Items = Y_Items.Union(orderedY.AsEnumerable()).ToList();

            X_Items = new List<PointF>();
            X_Items.Add(new PointF() { X = 0, Y = 0 });
            X_Items = X_Items.Union(orderedX.AsEnumerable()).ToList();

            offsetX = _container.Left;
            offsetY = _container.Top;

            X = 30;
            Y = 20;
            W = _container.Width - 60;
            H = _container.Height - 40;

            int i = 0;
            var maxY = Y_Items.Max(f => f.Y);
            while (maxY / Math.Pow(10, i) > 1)
                i++;
            stepY = (int)Math.Pow(10, i - 1) / 2;

            i = 0;
            var maxX = X_Items.Max(f => f.X);
            while (maxX / Math.Pow(10, i) > 1)
                i++;
            stepX = (int)Math.Pow(10, i - 1) / 2;

            Y_NbItems = (int)Math.Truncate(maxY / stepY) + 1;
            X_NbItems = (int)Math.Truncate(maxX / stepX) + 1;

            distanceY = H / Y_NbItems;
            distanceX = W / X_NbItems;

            double minDiff;

            minDiff = double.MaxValue;
            for (int p = 1; p < orderedX.Count; p++)
            {
                var diff = orderedX[p].X - orderedX[p - 1].X;
                if (diff < minDiff)
                    minDiff = diff;
            }
            BarWidth = (int)Math.Truncate(minDiff / ((double)stepX / (double)distanceX));

            minDiff = double.MaxValue;
            for (int p = 1; p < orderedY.Count; p++)
            {
                var diff = orderedY[p].Y - orderedY[p - 1].Y;
                if (diff < minDiff)
                    minDiff = diff;
            }
            BarHeight = (int)Math.Truncate(minDiff / ((double)stepY / (double)distanceY));
        }

        public void DrawChartV(Graphics G)
        {
            if (G == null || _dataPoints.Count == 0) return;

            G.Clear(_container.BackColor);

            DrawMarkers(G);

            var data = (from f in _dataPoints
                        group f by new { f.Y } into grp
                        where grp.Count() > 0
                        select new
                        {
                            Y = grp.Key.Y,
                            X = grp.Sum(p => p.X)
                        }).ToList();

            SolidBrush color = new SolidBrush(Color.FromArgb(200, 0, 0));

            var nbItems = _dataPoints.GroupBy(p => p.X).Select(x => x.FirstOrDefault()).ToList().Count();

            var divisoreY = (double)stepY / (double)distanceY;
            var divisoreX = (double)stepX / (double)distanceX;

            int i = 0;
            foreach (var item in Y_Items.OrderBy(f => f.Y))
            {
                var posY = FindIndex(Y_Markers, item.Y);
                var posX = FindIndex(X_Markers, item.X);

                var y = Y_Markers[posY].Y - (item.Y - Y_Markers[posY].X) / divisoreY - BarHeight/2;
                var w = X_Markers[posX].Y + (item.X - X_Markers[posX].X)/ divisoreX - X;
                var x = X;
                var h = BarHeight;

                G.FillRectangle(color, (float)x, (float)y, (float)w, (float)h);
                G.DrawRectangle(Pens.Black, (float)x, (float)y, (float)w, (float)h);
                i++;
                //G.DrawString(item.Y.ToString(), new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new PointF((float)x, (float)y));
                //G.DrawString(item.X.ToString(), new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new PointF((float)x, (float)y+20));
            }
        }

        public void DrawChartH(Graphics G)
        {
            if (G == null || _dataPoints.Count == 0) return;

            G.Clear(_container.BackColor);

            DrawMarkers(G);

            var data = (from f in _dataPoints
                        group f by new { f.Y } into grp
                        where grp.Count() > 0
                        select new
                        {
                            Y = grp.Key.Y,
                            X = grp.Sum(p => p.X)
                        }).ToList();

            SolidBrush color = new SolidBrush(Color.FromArgb(200, 0, 0));

            var nbItems = _dataPoints.GroupBy(p => p.X).Select(x => x.FirstOrDefault()).ToList().Count();

            var divisoreY = (double)stepY / (double)distanceY;
            var divisoreX = (double)stepX / (double)distanceX;

            int i = 0;
            foreach (var item in Y_Items.OrderBy(f => f.Y))
            {
                var posY = FindIndex(Y_Markers, item.Y);
                var posX = FindIndex(X_Markers, item.X);

                var y = Y_Markers[posY].Y - (item.Y - Y_Markers[posY].X) / divisoreY; 
                var w = BarWidth;
                var x = X_Markers[posX].Y + (item.X - X_Markers[posX].X) / divisoreX - BarWidth/2;
                var h = (Y_Markers[0].Y - y);

                G.FillRectangle(color, (float)x, (float)y, (float)w, (float)h);
                G.DrawRectangle(Pens.Black, (float)x, (float)y, (float)w, (float)h);
                i++;
                //G.DrawString(item.Y.ToString(), new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new PointF((float)x, (float)y));
                //G.DrawString(item.X.ToString(), new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new PointF((float)x, (float)y+20));
            }
        }

        public void DrawMarkers(Graphics G)
        {
            if (G == null || _dataPoints.Count == 0) return;

            G.Clear(_container.BackColor);
            SolidBrush backcolor = new SolidBrush(Color.White);
            SolidBrush plotcolor = new SolidBrush(Color.Orange);

            G.FillRectangle(backcolor, new Rectangle(X, Y, W, H));
            G.DrawLine(penBlack, new Point(X, Y), new Point(X, Y + H));
            G.DrawLine(penBlack, new Point(X, Y + H), new Point(X + W, Y + H));

            Point p;
            int coordX;
            int coordY;

            // Markers Y
            for (int i = 0; i <= Y_NbItems; i++) 
            {
                coordY = Y + H - i * distanceY;

                if (i > 0)
                    G.DrawLine(penGray, new Point(X + markerSize, coordY), new Point(X - markerSize + W, coordY));
                G.DrawLine(penBlack, new Point(X - markerSize, coordY), new Point(X + markerSize, coordY));
                // Labels
                p = new Point(X - markerSize - 25, coordY - markerSize * 2);
                var label = i * stepY;
                G.DrawString(label.ToString(), new Font("Courier New", 7, FontStyle.Regular), new SolidBrush(Color.Black), p);

                Y_Markers.Add(new Point(i * stepY, coordY));
            }

            // Markers X
            for (int i = 0; i <= X_NbItems; i++)
            {
                coordX = X + i * distanceX;

                if (i > 0)
                    G.DrawLine(penGray, new Point(coordX, Y + markerSize), new Point(coordX, Y + H - markerSize));
                G.DrawLine(penBlack, new Point(coordX, Y + H - markerSize), new Point(coordX, Y + H + markerSize));
                // Labels
                p = new Point(coordX, Y + H + markerSize * 2);
                var label = i * stepX;
                G.DrawString(label.ToString(), new Font("Courier New", 7, FontStyle.Regular), new SolidBrush(Color.Black), p);

                X_Markers.Add(new Point(i * stepX, coordX));
            }
        }

        private int FindIndex(List<Point> points, float pointToFind)
        {
            int x = -1;

            //points = points.OrderBy(x => x.X).ToList();
            bool found = false;
            int i = 0;
            while (i < points.Count && !found)
            {
                if (pointToFind >= points[i].X && pointToFind < points[i + 1].X)
                {
                    x = i;
                    found = true;
                }
                i++;
            }

            return x;
        }
    }
}
