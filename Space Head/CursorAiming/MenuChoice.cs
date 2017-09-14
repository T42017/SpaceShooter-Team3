﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace CursorAiming
{
    class MenuChoice
    {
        public float X { get; set; }
        public float Y { get; set; }

        public string Text { get; set; }
        public bool Selected { get; set; }

        public Action ClickAction { get; set; }
        public Rectangle HitBox { get; set; }
    }
}