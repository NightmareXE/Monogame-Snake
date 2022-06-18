using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeGame.Classes
{
    public class Food : Entity
    {
        public override void SetDefaults()
        {
            Width = Height = 12;
            TextureID = 3;
            Position.X = 20;
            Position.Y = 20;
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

        }

        public bool IsColliding(Rectangle snakeHitbox)
        {
            if (snakeHitbox.Contains(this.Hitbox))
                return true;
            else return false;
        }
        public void EatenBySnake()
        {

            Position = new Vector2(new Random().Next(0, 50), new Random().Next(0, 50));
            Hitbox.X = (int)Position.X;
            Hitbox.Y = (int)Position.Y;
        }
       
    }
}
