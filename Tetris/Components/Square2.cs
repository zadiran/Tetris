using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Components.Enums;

namespace Tetris.Components
{
    public class Square2 : Item
    {
        public Square2():base()
        {
            Map = new Color?[2, 2];
            Map[0, 0] = Map[1, 0] = Map[0, 1] = Map[1, 1] = Color.Purple;
        }

        public new void Rotate(RotateDirection rd = RotateDirection.Right) { }
    }
}
