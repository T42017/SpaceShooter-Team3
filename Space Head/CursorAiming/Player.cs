using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    class Player
    {
        public int MoveSpeed;
        public Texture2D Texture;
        static int Health = 3;

        public Vector2 Position, Direction, Velocity;
        
        public Player(int _moveSpeed, Texture2D _texture)
        {
            MoveSpeed = _moveSpeed;
            Texture = _texture;
        }
    }
}
