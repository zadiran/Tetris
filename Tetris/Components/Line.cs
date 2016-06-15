using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Components
{
    class Line : Item
    {

        public Line():base()
        {
            Map = new Color?[1, 4];
            Map[0, 0] = Map[0, 1] = Map[0, 2] = Map[0, 3] = Color.Blue;
        }
    }
}
