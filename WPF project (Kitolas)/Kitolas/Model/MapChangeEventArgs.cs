using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitolas.Model
{
    public class MapChangeEventArgs
    {
        public Game Game { get; private set; }

        public MapChangeEventArgs(Game game)
        {
            Game = game;
        }
    }
}
