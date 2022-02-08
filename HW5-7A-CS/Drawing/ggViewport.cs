using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyHomework
{
    public partial class ggViewport : Panel
    {
        private Panel _container;

        private Point firstPoint;
        PictureBox resizer = new PictureBox();
        private int resizerSize = 5;
        private bool allowResize = false;

        public ggViewport()
        {
            InitializeComponent();
        }

        public ggViewport(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        public ggViewport(Panel container)
        {
            _container = container;
            InitializeComponent();

            resizer.BackColor = Color.Black;
            this.Controls.Add(resizer);

            resizer.Height = resizerSize;
            resizer.Width = resizerSize;
            resizer.Location = new Point(this.Right - resizerSize, this.Bottom - resizerSize);
            resizer.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            resizer.Cursor = Cursors.SizeNWSE;
            resizer.BringToFront();

            resizer.MouseUp += new MouseEventHandler(pictureBox_MouseUp);
            resizer.MouseDown += new MouseEventHandler(pictureBox_MouseDw);
            resizer.MouseMove += new MouseEventHandler(pictureBox_MouseMove);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            firstPoint = Control.MousePosition;
            InitializeComponent();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // Create a temp Point
                Point temp = Control.MousePosition;
                Point res = new Point(firstPoint.X - temp.X, firstPoint.Y - temp.Y);

                var newX = this.Location.X - res.X;
                var newY = this.Location.Y - res.Y;
                if (newX < 0)
                    newX = 0;
                if (newY < 0)
                    newY = 0;
                if (newX + this.Width > _container.Right)
                    newX = _container.Right - this.Width;
                if (newY + this.Height > _container.Height)
                    newY = _container.Height - this.Height;

                // Apply value to object
                this.Location = new Point(newX, newY);

                // Update firstPoint
                firstPoint = temp;
            }
        }

        private void pictureBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            allowResize = false;
        }

        private void pictureBox_MouseDw(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            allowResize = true;
        }

        private void pictureBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (allowResize)
            {
                this.Height = resizer.Top + e.Y;
                this.Width = resizer.Left + e.X;
            }
        }
    }
}
