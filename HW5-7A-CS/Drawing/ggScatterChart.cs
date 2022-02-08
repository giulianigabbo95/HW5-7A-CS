using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyHomework
{
    public class ggScatterChart
    {
        private List<Point> _dataPoints;
        private List<PointF> _dataPointsF;
        private Panel _container;

        private Pen penBlack = new Pen(Color.Black);
        private Pen penGray = new Pen(Color.LightGray);
        private Pen penBlue = new Pen(Color.Blue, 1);

        private int X;
        private int Y;
        private int W;
        private int H;

        private int stepX;
        private int stepY;
        private int offsetX;
        private int offsetY;

        private int markerSize = 3;
        private int plotSize = 8;

        private List<Point> Y_Items = new List<Point>();
        private List<Point> X_Items = new List<Point>();
        private List<PointF> Y_ItemsF = new List<PointF>();
        private List<PointF> X_ItemsF = new List<PointF>();


        public ggScatterChart(List<Point> dataPoints, Panel container)
        {
            _dataPoints = dataPoints;
            _container = container;

            var orderedX = _dataPoints.OrderBy(p => p.X).ToList();
            var orderedY = _dataPoints.OrderBy(p => p.Y).ToList();

            Y_Items = new List<Point>();
            Y_Items.Add(new Point() { X = 0, Y = 0 });
            Y_Items = Y_Items.Union(orderedY.AsEnumerable()).ToList();

            X_Items = new List<Point>();
            X_Items.Add(new Point() { X = 0, Y = 0 });
            X_Items = X_Items.Union(orderedX.AsEnumerable()).ToList();

            offsetX = _container.Left;
            offsetY = _container.Top;

            X = 30;
            Y = 20;
            W = _container.Width - 60;
            H = _container.Height- 40;

            stepY = H / (Y_Items.Count);
            stepX = W / (X_Items.Count);
        }
        public ggScatterChart(List<PointF> dataPoints, Panel container)
        {
            _dataPointsF = dataPoints;
            _container = container;

            var orderedX = _dataPointsF.OrderBy(p => p.X).ToList();
            var orderedY = _dataPointsF.OrderBy(p => p.Y).ToList();

            Y_ItemsF = new List<PointF>();
            Y_ItemsF.Add(new PointF() { X = 0, Y = 0 });
            Y_ItemsF = Y_ItemsF.Union(orderedY.AsEnumerable()).ToList();

            X_ItemsF = new List<PointF>();
            X_ItemsF.Add(new PointF() { X = 0, Y = 0 });
            X_ItemsF = X_ItemsF.Union(orderedX.AsEnumerable()).ToList();

            offsetX = _container.Left;
            offsetY = _container.Top;

            X = 30;
            Y = 20;
            W = _container.Width - 60;
            H = _container.Height - 40;

            stepY = H / (Y_ItemsF.Count);
            stepX = W / (X_ItemsF.Count);
        }

        public void DrawChart(Graphics G)
        {
            if (G == null || _dataPoints.Count == 0) return;

            G.Clear(_container.BackColor);
            SolidBrush backcolor = new SolidBrush(Color.White);
            SolidBrush plotcolor = new SolidBrush(Color.Orange);

            G.FillRectangle(backcolor, new Rectangle(X, Y, W, H));
            G.DrawLine(penBlack, new Point(X, Y), new Point(X, Y + H));
            G.DrawLine(penBlack, new Point(X , Y + H), new Point(X + W, Y + H));

            Point p;
            int coordX;
            int coordY;

            // Markers Y
            for (int i = 0; i < Y_Items.Count; i += 1)
            {
                coordY = Y + H - i * stepY;

                if (i > 0)
                    G.DrawLine(penGray, new Point(X + markerSize, coordY), new Point(X - markerSize + W, coordY));
                G.DrawLine(penBlack, new Point(X - markerSize, coordY), new Point(X + markerSize, coordY));
                // Labels
                p = new Point(X - markerSize - 20, coordY - markerSize * 2);
                var label = Y_Items[i].Y;      // Math.Truncate((decimal)Y_Items[i].Valore / 100) * 100;
                G.DrawString(label.ToString(), new Font("Courier New", 7, FontStyle.Regular), new SolidBrush(Color.Black), p);
            }
            // Markers X
            for (int i = 0; i < X_Items.Count; i += 1)
            {
                coordX = X + i * stepX;

                if (i > 0)
                    G.DrawLine(penGray, new Point(coordX, Y + markerSize), new Point(coordX, Y + H - markerSize));
                G.DrawLine(penBlack, new Point(coordX, Y + H - markerSize), new Point(coordX, Y + H + markerSize));
                // Labels
                p = new Point(coordX, Y + H + markerSize * 2);
                var label = X_Items[i].X;    
                G.DrawString(label.ToString(), new Font("Courier New", 7, FontStyle.Regular), new SolidBrush(Color.Black), p);
            }

            // plot Valori
            foreach (Point item in _dataPoints)
            {
                if (item.X > 0)
                {
                    var vx = Array.IndexOf(X_Items.ToArray(), X_Items.Where(f => f.X == item.X).FirstOrDefault());
                    var vy = Array.IndexOf(Y_Items.ToArray(), Y_Items.Where(f => f.Y == item.Y).FirstOrDefault());

                    p = new Point(X + vx * stepX, Y + (Y_Items.Count - vy) * stepY - offsetY);

                    var plot = new Point(p.X, p.Y + offsetY);
                    G.FillEllipse(plotcolor, plot.X, plot.Y, plotSize, plotSize);
                }
            }

        }
        public void DrawChart2(Graphics G)
        {
            if (G == null || _dataPointsF.Count == 0) return;

            G.Clear(_container.BackColor);
            SolidBrush backcolor = new SolidBrush(Color.White);
            SolidBrush plotcolor = new SolidBrush(Color.Orange);

            G.FillRectangle(backcolor, new Rectangle(X, Y, W, H));
            G.DrawLine(penBlack, new Point(X, Y), new Point(X, Y + H));
            G.DrawLine(penBlack, new Point(X, Y + H), new Point(X + W, Y + H));

            PointF p;
            int coordX;
            int coordY;

            // Markers Y
            for (int i = 0; i < Y_ItemsF.Count; i += 1)
            {
                coordY = Y + H - i * stepY;

                if (i > 0)
                    G.DrawLine(penGray, new PointF(X + markerSize, coordY), new PointF(X - markerSize + W, coordY));
                G.DrawLine(penBlack, new PointF(X - markerSize, coordY), new PointF(X + markerSize, coordY));
                // Labels
                p = new PointF(X - markerSize - 20, coordY - markerSize * 2);
                var label = Y_ItemsF[i].Y;      // Math.Truncate((decimal)Y_Items[i].Valore / 100) * 100;
                G.DrawString(label.ToString(), new Font("Courier New", 7, FontStyle.Regular), new SolidBrush(Color.Black), p);
            }
            // Markers X
            for (int i = 0; i < X_ItemsF.Count; i += 1)
            {
                coordX = X + i * stepX;

                if (i > 0)
                    G.DrawLine(penGray, new PointF(coordX, Y + markerSize), new PointF(coordX, Y + H - markerSize));
                G.DrawLine(penBlack, new PointF(coordX, Y + H - markerSize), new PointF(coordX, Y + H + markerSize));
                // Labels
                p = new Point(coordX, Y + H + markerSize * 2);
                var label = X_ItemsF[i].X;
                G.DrawString(label.ToString(), new Font("Courier New", 7, FontStyle.Regular), new SolidBrush(Color.Black), p);
            }

            // plot Valori
            foreach (PointF item in _dataPointsF)
            {
                var vx = Array.IndexOf(X_ItemsF.ToArray(), X_ItemsF.Where(f => f.X == item.X).FirstOrDefault());
                var vy = Array.IndexOf(Y_ItemsF.ToArray(), Y_ItemsF.Where(f => f.Y == item.Y).FirstOrDefault());

                p = new PointF(X + vx * stepX, Y + (Y_ItemsF.Count - vy) * stepY - offsetY);

                var plot = new PointF(p.X, p.Y + offsetY);
                G.FillEllipse(plotcolor, plot.X, plot.Y, plotSize, plotSize);
            }

        }

        public void DrawRegressionLine(Graphics G)
        {
            if (_dataPoints.Count == 0)
                return;

            //calculate the slope of the trendline
            //calculate the intercept of the trendline
            //get the minimum x value in the scatterplot
            //get the maximum x value in the scatterplot
            //calculate the minimum y value by using the slope, intercept and minimum x value
            //calculate the maximum y value by using the slope, intercept and maximum x value

            double LRSlope;
            double LRIntercept;
            List<Point> pointsOnLinearLine = new List<Point>();

            CalculateLinearRegression(_dataPoints, out LRSlope, out LRIntercept);
            foreach (Point onePoint in _dataPoints)
            {
                pointsOnLinearLine.Add(new Point(onePoint.X, (int)Math.Truncate(LRSlope * onePoint.X + LRIntercept)));
            }

            var orderdList = pointsOnLinearLine.OrderBy(f => f.X).ToList();
            var fp = orderdList.First();
            var lp = orderdList.Last();

            // find the section in the chart where the point lies
            var vx1 = Array.IndexOf(X_Items.ToArray(), X_Items.Where(p => p.X == fp.X).FirstOrDefault());
            var vy1 = Array.IndexOf(Y_Items.ToArray(), Y_Items.Where(p => p.Y >= fp.Y).FirstOrDefault());
            var p1 = new Point(X + vx1 * stepX, Y + (Y_Items.Count - vy1) * stepY);

            // Adjust point to find the exact point in the chart
            p1 = GetExactPointPosition(p1, fp, vx1, vy1);

            // find the section in the chart where the point lies
            var vx2 = Array.IndexOf(X_Items.ToArray(), X_Items.Where(p => p.X == lp.X).FirstOrDefault());
            var vy2 = Array.IndexOf(Y_Items.ToArray(), Y_Items.Where(p => p.Y >= lp.Y).FirstOrDefault());
            var p2 = new Point(X + vx2 * stepX, Y + (Y_Items.Count - vy2) * stepY);

            // Adjust point to find the exact point in the chart
            p2 = GetExactPointPosition(p2, lp, vx2, vy2);

            // Draw line from p1 to p2
            G.DrawLine(penBlue, p1, p2);
        }
        public void DrawRegressionLine2(Graphics G)
        {
            if (_dataPointsF.Count == 0)
                return;

            //calculate the slope of the trendline
            //calculate the intercept of the trendline
            //get the minimum x value in the scatterplot
            //get the maximum x value in the scatterplot
            //calculate the minimum y value by using the slope, intercept and minimum x value
            //calculate the maximum y value by using the slope, intercept and maximum x value

            double LRSlope;
            double LRIntercept;
            List<PointF> pointsOnLinearLine = new List<PointF>();

            CalculateLinearRegression(_dataPointsF, out LRSlope, out LRIntercept);
            foreach (PointF onePoint in _dataPointsF)
            {
                pointsOnLinearLine.Add(new PointF(onePoint.X, (int)Math.Truncate(LRSlope * onePoint.X + LRIntercept)));
            }

            var orderdList = pointsOnLinearLine.OrderBy(f => f.X).ToList();
            var fp = orderdList.First();
            var lp = orderdList.Last();

            // find the section in the chart where the point lies
            var vx1 = Array.IndexOf(X_ItemsF.ToArray(), X_ItemsF.Where(p => p.X == fp.X).FirstOrDefault());
            var vy1 = Array.IndexOf(Y_ItemsF.ToArray(), Y_ItemsF.Where(p => p.Y >= fp.Y).FirstOrDefault());
            var p1 = new PointF(X + vx1 * stepX, Y + (Y_ItemsF.Count - vy1) * stepY);

            // Adjust point to find the exact point in the chart
            p1 = GetExactPointPosition(p1, fp, vx1, vy1);

            // find the section in the chart where the point lies
            var vx2 = Array.IndexOf(X_ItemsF.ToArray(), X_ItemsF.Where(p => p.X == lp.X).FirstOrDefault());
            var vy2 = Array.IndexOf(Y_ItemsF.ToArray(), Y_ItemsF.Where(p => p.Y >= lp.Y).FirstOrDefault());
            var p2 = new PointF(X + vx2 * stepX, Y + (Y_ItemsF.Count - vy2) * stepY);

            // Adjust point to find the exact point in the chart
            p2 = GetExactPointPosition(p2, lp, vx2, vy2);

            // Draw line from p1 to p2
            G.DrawLine(penBlue, p1, p2);
        }

        public Point GetExactPointPosition(Point p,  Point linearp, int x, int y)
        {
            // Try to find the exact pixel for y depending on the design chart area (calculates approximated proportion between points)
            int previous;
            int diff1;
            int diff2;
            double fraction;
            int diff3;

            var vy = Array.IndexOf(Y_Items.ToArray(), Y_Items.Where(f => f.X == linearp.X).FirstOrDefault());

            previous = Y + (Y_Items.Count - vy - 2) * stepY - offsetY;
            diff1 = p.Y - previous;
            diff2 = Y_Items[y].Y - Y_Items[y - 1].Y;
            fraction = (double)diff1 / (double)diff2;
            diff3 = Y_Items[y].Y - linearp.Y;

            var returnP = new Point(X + x * stepX, p.Y + (int)Math.Round(diff3 * fraction, 0));
            return returnP;
        }
        public PointF GetExactPointPosition(PointF p, PointF linearp, int x, int y)
        {
            // Try to find the exact pixel for y depending on the design chart area (calculates approximated proportion between points)
            int previous;
            float diff1;
            float diff2;
            float fraction;
            float diff3;

            var vy = Array.IndexOf(Y_ItemsF.ToArray(), Y_ItemsF.Where(f => f.X == linearp.X).FirstOrDefault());

            previous = Y + (Y_ItemsF.Count - vy - 2) * stepY - offsetY;
            diff1 = p.Y - previous;
            diff2 = Y_ItemsF[y].Y - Y_ItemsF[y - 1].Y;
            fraction = diff1 / diff2;
            diff3 = Y_ItemsF[y].Y - linearp.Y;

            var returnP = new PointF(X + x * stepX, p.Y + (float)Math.Round(diff3 * fraction, 0));
            return returnP;
        }

        // Calculate the linear regression coefficient
        private void CalculateLinearRegression(List<Point> AListOfPoints, out double Slope, out double Intercept)
        {
            // Calculate the caracteristics of this distribution: look up the regression theory
            // Find the values S1, Sx, Sy, Sxx, and Sxy.
            var S1 = AListOfPoints.Count();
            var Sx = 0.0;
            var Sy = 0.0;
            var Sxx = 0.0;
            var Sxy = 0.0;

            foreach (PointF onePoint in AListOfPoints)
            {
                Sx += onePoint.X;
                Sy += onePoint.Y;
                Sxx += onePoint.X * onePoint.X;
                Sxy += onePoint.X * onePoint.Y;
            }

            // Calculate the Slope and Intercept
            Slope = (Sxy * S1 - Sx * Sy) / (Sxx * S1 - Sx * Sx);
            Intercept = (Sxy * Sx - Sy * Sxx) / (Sx * Sx - S1 * Sxx);
        }
        private void CalculateLinearRegression(List<PointF> AListOfPoints, out double Slope, out double Intercept)
        {
            // Calculate the caracteristics of this distribution: look up the regression theory
            // Find the values S1, Sx, Sy, Sxx, and Sxy.
            var S1 = AListOfPoints.Count();
            var Sx = 0.0;
            var Sy = 0.0;
            var Sxx = 0.0;
            var Sxy = 0.0;

            foreach (PointF onePoint in AListOfPoints)
            {
                var x = Math.Round(onePoint.X, 0); //onePoint.X
                Sx += x;
                Sy += onePoint.Y;
                Sxx += x * x;
                Sxy += x * onePoint.Y;
            }

            // Calculate the Slope and Intercept
            Slope = (Sxy * S1 - Sx * Sy) / (Sxx * S1 - Sx * Sx);
            Intercept = (Sxy * Sx - Sy * Sxx) / (Sx * Sx - S1 * Sxx);
        }
    }
}
