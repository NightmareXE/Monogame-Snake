using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeGame.Classes
{
    public class Entity
    {
        public int Width;

        public int Height;
        public int ID { get; set; }

        public int TextureID = (int)ResourceID.Nothing; //Indicates the texture to be drawn in the Gridmap

        public float velMult = 1f;

        public float scale = 1f;

        public float rotation = 0f;

       

        public Color color = Color.White;

        public SpriteEffects spriteEffects = SpriteEffects.None;
        public Vector2 Position;

        public Vector2 Velocity = Vector2.Zero;

        public Vector2 Center { get { return new Vector2(Position.X + Width / 2, Position.Y + Height / 2); } set { new Vector2(Position.X + Width / 2, Position.Y + Height / 2); } }

        public Rectangle Hitbox;

        

        /// <summary>
        /// Used to initialize the Entity , ran in Initialize() function
        /// </summary>
        public virtual void SetDefaults()
        {

        }

        /// <summary>
        /// Used to update the entity , will be overriden for every unique entity
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
           
                Position += new Vector2(Velocity.Y ,Velocity.X); //Responsible for movement
            




        }

        /// <summary>
        /// Draws entity by putting it in the gridelements to be drawn
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(GameTime gameTime , SpriteBatch spriteBatch , Gridmap gridmap)
        {
            if(TextureID != 0)
            {
                int size = gridmap.size;
                //gridmap.gridElements.Add(new GridElement(TextureID, new Rectangle((int)Position.X * size, (int)Position.Y * size, size, size)));
                gridmap.gridArray[(int)Position.Y, (int)Position.X] = TextureID;

            
            
            
            }
        }
    }
}
