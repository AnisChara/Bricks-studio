﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Action
    {
        public string name;
        public string function;
        public string description;

        public Action(string name, string function, string description)
        {
            this.name = name;
            this.function = function;
            this.description = description;
        }
    }
}
