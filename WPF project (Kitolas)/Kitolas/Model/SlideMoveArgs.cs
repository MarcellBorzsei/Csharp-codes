using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitolas.Model
{
    public class SlideMoveArgs : EventArgs
    {
        public Key Dir { get; private set; }

        public SlideMoveArgs(string dir)
        {
            switch (dir)
            {
                case "up":
                    Dir = Key.Up;
                    break;
                case "down":
                    Dir = Key.Down;
                    break;
                case "right":
                    Dir = Key.Right;
                    break;
                case "left":
                    Dir = Key.Left;
                    break;


                default:
                    return;
            }
        }
    }
}
