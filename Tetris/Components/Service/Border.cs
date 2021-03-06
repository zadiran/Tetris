﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Components.Enums;

namespace Tetris.Components.Service
{
    public class Border
    {
        // Item, border for
        private Item _item;
        
        // Side of item
        public Direction Direction { get; private set; }

        private Lazy<List<Point>> _lazyPoints;

        private List<Point> points { get { return _lazyPoints.Value; } }

        public Border(Item item, Direction direction)
        {
            Direction = direction;

            _item = item;
            _lazyPoints = new Lazy<List<Point>>(getBorder);
        }

        public void Refresh()
        {
            _lazyPoints = new Lazy<List<Point>>(getBorder);
        }

        private List<Point> getBorder()
        {
            //TODO: add calculation for other borders
            var _points = new List<Point>();
            switch (Direction)
            {
                case Direction.Top:
                    _points = getTopBorder(_item);
                    break;
                case Direction.Left:
                    _points = getLeftBorder(_item);
                    break;
                case Direction.Bottom:
                    _points = getBottomBorder(_item);
                    break;
                case Direction.Rigth:
                    _points = getRightBorder(_item);
                    break;
                default:
                    break;
            }

            return _points;
        }

        private List<Point> getLeftBorder(Item item)
        {
            var _points = new List<Point>();

            int width = item.Map.GetLength(0);
            int height = item.Map.GetLength(1);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (item.Map[j, i] != null)
                    {
                        _points.Add(new Point(j, i));
                        break;
                    }
                }
            }

            return _points;
        }

        private List<Point> getRightBorder(Item item)
        {

            var _points = new List<Point>();

            int width = item.Map.GetLength(0);
            int height = item.Map.GetLength(1);

            for (int i = 0; i < height; i++)
            {
                for (int j = width - 1; j >= 0; j--)
                {
                    if (item.Map[j, i] != null)
                    {
                        _points.Add(new Point(j, i));
                        break;
                    }
                }
            }

            return _points;
        }

        private List<Point> getTopBorder(Item item)
        {
            var _points = new List<Point>();

            int width = item.Map.GetLength(0);
            int height = item.Map.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height ; j++)
                {
                    if (item.Map[i, j] != null)
                    {
                        _points.Add(new Point(i, j));
                        break;
                    }
                }
            }

            return _points;
        }

        private List<Point> getBottomBorder(Item item)
        {
            var _points = new List<Point>();

            int width = item.Map.GetLength(0);
            int height = item.Map.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = height-1; j >= 0 ; j--)
                {
                    if (item.Map[i,j] != null)
                    {
                        _points.Add(new Point(i, j));
                        break;
                    }
                }
            }
 
            return _points;
        }

        public List<Point> ToAbsolute(Point point)
        {
            return points.Select(x => new Point(x.X + point.X, x.Y + point.Y)).ToList();
        }

        // just for test
        public List<Point> Points()
        {
            return points;
        }
    }
}
