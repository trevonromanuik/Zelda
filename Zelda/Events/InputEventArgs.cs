using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zelda.Events
{
    public class InputEventArgs : EventArgs
    {
        public Input Input { get; set; }

        public InputEventArgs(Input input)
        {
            Input = input;
        }
    }
}
