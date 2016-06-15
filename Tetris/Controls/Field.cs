using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Components;

namespace Tetris.Controls
{
    public partial class Field : Panel
    {
        private Timer downTimer = new Timer();


        public int TickTime = 500;

        public Item CurrentItem { get; set; }

        public Color BgColor = Color.White;

        public int SquareSize = 20;

        public Size NetSize
        {
            get
            {
                return new Size(Size.Width / SquareSize, Size.Height / SquareSize );
            }
        }

        public Color?[,] Net { get; set; }

        public int[] DeadLine { get; set; }
        public Field()
        {
            InitializeComponent();
            this.BackColor = BgColor;
            this.Size = new Size(300, 600);
            this.Location = new Point(10, 10);
        }

        public Field(Item initialItem):this()
        {
            CurrentItem = initialItem;
            downTimer.Interval = 500;
            downTimer.Tick += MoveDown;
            downTimer.Start();
        }

        public void Draw(Item item)
        {
            int width = item.Map.GetLength(0);
            int height = item.Map.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (item.Map[i,j] != null)
                    {
                        Net[item.Position.X + i, item.Position.Y + j] = item.Map[i, j];
                    }
                }
            }

            //for test TODO: remove or organize after
            foreach (var pnt in item.BottomBorder.Points())
            {
                Draw(item, pnt);
            }
            //end for test

            Invalidate();
        }

        //Just for tests yet
        public void Draw(Item item, Point point)
        {
            Net[item.Position.X + point.X, item.Position.Y + point.Y] = Color.Black;
        }
        
        public void Clear(Item item)
        {
            int width = item.Map.GetLength(0);
            int height = item.Map.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (item.Map[i, j] != null)
                    {
                        Net[item.Position.X + i, item.Position.Y + j] = BgColor;
                    }
                }
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawNet(e);
        }

        private void DrawNet(PaintEventArgs e)
        {
            
            for (int i = 0; i < Net.GetLength(0); i++)
            {
                for (int j = 0; j < Net.GetLength(1); j++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Net[i, j] ?? BgColor), i * SquareSize, j * SquareSize, SquareSize, SquareSize);
                }
            }
        }

        private void MoveDown(object sender, EventArgs e)
        {
            if (CurrentItem == null)
                return;

            Clear(CurrentItem);
            CurrentItem.Position = new Point(CurrentItem.Position.X,
                                             CurrentItem.Position.Y + 1);
            Draw(CurrentItem);
        }
    }
}
