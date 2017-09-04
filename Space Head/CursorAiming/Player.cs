using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    internal class Player : UnitWithGun
    {
       
        


        public Player(int _moveSpeed, Texture2D _texture, Game game) : base(game)
        {
            MoveSpeed = _moveSpeed;
            Texture = _texture;
        }


        public override void UpdateMovement(GameTime gameTime)
        {
            MoveDirection = new Vector2(0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                MoveDirection.X += -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                MoveDirection.X += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                MoveDirection.Y += -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                MoveDirection.Y += 1;
            }
 
            if (MoveDirection.X != 0 && MoveDirection.Y != 0) MoveDirection.Normalize();

            Velocity = MoveDirection * (MoveSpeed * gameTime.ElapsedGameTime.Milliseconds / 1000);
            Position += Velocity;
        }
        
    }
}