﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Components.Enums;
using Tetris.Components.Service;

namespace Tetris.Components
{
    public abstract class Item
    {
        public Border BottomBorder { get; set; }

        public Point Position { get; set; }
        public Color?[,] Map { get; protected set; }

        public Item()
        {
            BottomBorder = new Border(this, Direction.Bottom);
        }

        // TODO: rewrite to event
        public virtual void Rotate(RotateDirection rd = RotateDirection.Right)
        { //TODO: Improve rotation. Working not enough good
            int height= Map.GetLength(1);
            int width = Map.GetLength(0);
            var newmap = new Color?[height, width];
            if (rd == RotateDirection.Right) //TODO: Left rotation
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        newmap[j, i] = Map[i, j];
                    } 
                }
                Map = newmap;
            }

            BottomBorder.Refresh();
        }
    }
}
