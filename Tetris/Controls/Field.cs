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
        public event EventHandler ItemStacked;

        private Timer downTimer = new Timer();

        // TODO: Move all settings to detached class
        public int TickTime = 500;

        public Item CurrentItem { get; set; }

        public Color BgColor = Color.White;

        public int SquareSize = 20;

        public bool ReadyToMoveDown = true;

        public Size NetSize
        {
            get
            {
                return new Size(Size.Width / SquareSize, Size.Height / SquareSize );
            }
        }

        public Color?[,] Net { get; set; }

        public List<Point> DeadLine { get; set; }

        public List<Point> LeftBorder { get; set; }

        public List<Point> RightBorder { get; set; }

        public Field()
        {
            InitializeComponent();
            DoubleBuffered = true;
            this.BackColor = BgColor;
            this.Size = new Size(300, 600);
            this.Location = new Point(10, 10);

            LeftBorder = Enumerable.Range(0, NetSize.Height).Select(x => new Point(0, x)).ToList();
            RightBorder = Enumerable.Range(0, NetSize.Height).Select(x => new Point(NetSize.Width-1, x)).ToList();
            DeadLine = Enumerable.Range(0, NetSize.Width).Select(x => new Point(x, NetSize.Height-1)).ToList();
            ItemStacked += OnItemStacked;
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
            //foreach (var pnt in item.BottomBorder.Points())
            //{
            //    Draw(item, pnt);
            //}
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

        public void UpdateLimits()
        {
            DeadLine.AddRange(CurrentItem.TopBorder.ToAbsolute(CurrentItem.Position).Select(x => new Point(x.X, x.Y - 1)));
            DeadLine = DeadLine.Except(CurrentItem.BottomBorder.ToAbsolute(CurrentItem.Position)).ToList();

            LeftBorder.AddRange(CurrentItem.RightBorder.ToAbsolute(CurrentItem.Position).Select(x => new Point(x.X + 1, x.Y)));
            LeftBorder = LeftBorder.Where(x => x.X < NetSize.Width).Distinct().ToList();
            
            RightBorder.AddRange(CurrentItem.LeftBorder.ToAbsolute(CurrentItem.Position).Select(x => new Point(x.X - 1, x.Y)));
            RightBorder = RightBorder.Where(x => x.X >=0).Distinct().ToList();
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

            if (!ReadyToMoveDown)
                return;

            if (DeadLine.Intersect(CurrentItem.BottomBorder.ToAbsolute(CurrentItem.Position)).Any())
            {
                UpdateLimits();
                if (ItemStacked != null)
                    ItemStacked(this, new EventArgs());
            }
            else
            {
                Clear(CurrentItem);
                CurrentItem.Position = new Point(CurrentItem.Position.X,
                                                 CurrentItem.Position.Y + 1);
                Draw(CurrentItem);
            }
        }

        private void OnItemStacked(object sender, EventArgs e)
        {
            ReadyToMoveDown = false;
          //  CurrentItem = null;
        }
    }
}
