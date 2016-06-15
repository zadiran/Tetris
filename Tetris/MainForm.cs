using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Components;
using Tetris.Controls;

namespace Tetris
{
    public partial class MainForm : Form
    {
        Line ln = new Line();
        int top = 1;
        int left = 3;
        private Field field;

        public MainForm()
        {
            ln.Position = new Point(1, 4);
            field = new Field();
            InitializeComponent();
            this.Controls.Add(field);
            Color?[,] arr = new Color?[field.NetSize.Width, field.NetSize.Height];
      //      arr[left, top] = Color.Red;
          
            field.Net = arr;
            field.Draw(ln);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == Keys.Left)
            {
              //  if (left > 0)
                {
                    field.Clear(ln);
                    ln.Position = new Point(ln.Position.X - 1, ln.Position.Y);
                    field.Draw(ln);
                }
            }
            if (keyData == Keys.Right)
            {
              //  if (left + 1 < field.NetSize.Width)
                {
                    field.Clear(ln);
                    ln.Position = new Point(ln.Position.X + 1, ln.Position.Y);
                    field.Draw(ln);
                }
            }

            if (keyData == Keys.Up)
            {
               // if (top>0)
                {
                    field.Clear(ln);
                    ln.Position = new Point(ln.Position.X, ln.Position.Y -1);
                    field.Draw(ln);
                    //field.Net[left, top] = null;

                    //top--;
                    //field.Net[left, top] = Color.Red;
                    //field.Invalidate();
                }
            }
            if (keyData == Keys.Down)
            {
               // if (top + 1 < field.NetSize.Height)
                {
                    field.Clear(ln);
                    ln.Position = new Point(ln.Position.X, ln.Position.Y + 1);
                    field.Draw(ln);
                }
            }
            if (keyData == Keys.Space)
            {
                field.Clear(ln);
                ln.Rotate();
                field.Draw(ln);

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
