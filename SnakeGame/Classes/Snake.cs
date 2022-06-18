using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SnakeGame.Classes
{
    public class SnakeSegment : Entity
    {
        public SnakeSegment( float spawnPosX , float spawnPosY , int textureID = (int)ResourceID.SnakeBody)
        {
            TextureID = textureID;
            this.Position.X = spawnPosY; //Inverted because pf 2D array
            this.Position.Y = spawnPosX;
            Width = Height = 12;
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }
        
        public override void Update(GameTime gameTime)
        {
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            base.Update(gameTime);
        }
    }
    public class Snake : Entity
    {
        public bool Dead;
        public int SnakeLength = 2; //0 INDEXED
        

        public List<Entity> snakeSegments = new List<Entity>();

        public void GenerateSnake(int length, Vector2 headPos)
        {
            for (int i = 0; i < length; i++)
            {
                int TextureID = i == 0 ? (int)ResourceID.SnakeHead : (int)ResourceID.SnakeBody;
                snakeSegments.Add(new SnakeSegment((int)headPos.X + i, (int)headPos.Y , TextureID));
            }
        }
        public override void Update(GameTime gameTime)
        {
            //Segments will only follow if head is moving
            if (snakeSegments[0].Velocity != Vector2.Zero)
            {
                for (int i = snakeSegments.Count - 1; i > 0; i--)
                {

                    snakeSegments[i].Position.X = snakeSegments[i - 1].Position.X;
                    snakeSegments[i].Position.Y = snakeSegments[i - 1].Position.Y;

                }
            }
            

            var kstate = Keyboard.GetState();

            bool Up = kstate.IsKeyDown(Keys.W);
            bool Down = kstate.IsKeyDown(Keys.S);
            bool Left = kstate.IsKeyDown(Keys.A);
            bool Right = kstate.IsKeyDown(Keys.D);

            
            if (Up && snakeSegments[0].Velocity.Y != 1)
            {
                snakeSegments[0].Velocity.Y = -1;
                snakeSegments[0].Velocity.X = 0;


            }
            else if (Down && snakeSegments[0].Velocity.Y != -1)
            {
                snakeSegments[0].Velocity.Y = 1;
                snakeSegments[0].Velocity.X = 0;
            }
            else if (Left && snakeSegments[0].Velocity.X != 1)
            {
                snakeSegments[0].Velocity.X = -1;
                snakeSegments[0].Velocity.Y = 0;
            }
            else if (Right && snakeSegments[0].Velocity.X != -1)
            {
                snakeSegments[0].Velocity.X = 1;
                snakeSegments[0].Velocity.Y = 0;
            }

            foreach (SnakeSegment segment in snakeSegments)
                segment.Update(gameTime);

            CollisionWithBody();


        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Gridmap gridmap)
        {
            foreach(Entity snakeSegment in snakeSegments)
            {
                snakeSegment.Draw(gameTime , spriteBatch , gridmap);
            }
            base.Draw(gameTime, spriteBatch, gridmap);

        }
        public void CollisionWithBody()
        {
            foreach (Entity snakeSegment in snakeSegments)
            {
                if (snakeSegments[0].Hitbox.Contains(snakeSegment.Hitbox) && snakeSegment != snakeSegments[0] && snakeSegment != snakeSegments[1])
                {
                    Dead = true;
                }
                

            }
        }
        public void EatFood()
        {
            Vector2 lastSegmentPos = snakeSegments[snakeSegments.Count - 1].Position;
            snakeSegments.Add(new SnakeSegment(lastSegmentPos.Y, lastSegmentPos.X));
        }
        

    }
}
