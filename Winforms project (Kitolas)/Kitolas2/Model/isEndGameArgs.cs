using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitolas.Model
{
    public class isGameEndArgs : EventArgs
    {
        public bool ifEndGame { get; set; }
        public string? winner { get; set; }
        public int circleCount { get; set; }
    }
}
