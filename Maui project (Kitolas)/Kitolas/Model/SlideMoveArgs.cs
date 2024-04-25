using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitolas.Model
{
    public class SlideMoveArgs : EventArgs
    {
        public Keys Dir { get; private set; }

        public SlideMoveArgs(string dir)
        {
            switch (dir)
            {
                case "up":
                    Dir = Keys.Up;
                    break;
                case "down":
                    Dir = Keys.Down;
                    break;
                case "right":
                    Dir = Keys.Right;
                    break;
                case "left":
                    Dir = Keys.Left;
                    break;


                default:
                    return;
            }
        }
    }
}
