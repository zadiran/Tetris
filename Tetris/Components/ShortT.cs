using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Components
{
    public class ShortT : Item
    {
        public ShortT()
        {
            Map = new Color?[2, 3];
            Map[0, 1] = Map[1, 0] = Map[1, 1] = Map[1, 2] = Color.SteelBlue;
        }
    }
}
