﻿using System;
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
        Random rnd = new Random();
        Item ln = new ShortT();

        private Field field;

        public MainForm()
        {
            ln.Position = new Point(1, 4);
            field = new Field(ln);
            InitializeComponent();
            this.Controls.Add(field);
            
            Color?[,] arr = new Color?[field.NetSize.Width, field.NetSize.Height];
            field.Net = arr;
            field.Draw(ln);

            field.ItemStacked += OnItemStacked;
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
                    ln.Rotate();
                    //ln.Position = new Point(ln.Position.X, ln.Position.Y -1);
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

            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void OnItemStacked(object sender, EventArgs e)
        {
            Item[] figs = { new Line(), new ShortT(), new Square2() };
        
            ln = figs[rnd.Next(3)];
            ln.Position = new Point(1, 4);
            field.CurrentItem =  ln;
            field.ReadyToMoveDown = true;
        }
    }
}
