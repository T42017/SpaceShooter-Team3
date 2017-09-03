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
        public bool IsShooting, HasShot;
        public Vector2 Position, MoveDirection, Velocity, AimDirection;
        Bullet bullet;
        public List<Bullet> BulletsInAir = new List<Bullet>();
        
        public Player(int _moveSpeed, Texture2D _texture)
        {
            MoveSpeed = _moveSpeed;
            Texture = _texture;           
        }

        public void Shoot()
        {
            bullet = new Bullet(300, 1, Texture, Position, AimDirection);
            BulletsInAir.Add(bullet);
        }
    }
}
