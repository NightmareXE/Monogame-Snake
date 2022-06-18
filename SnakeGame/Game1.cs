using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnakeGame.Classes;

namespace SnakeGame
{
    public enum ResourceID
    {
        Nothing, //0
        BlueGrid, //1
        SnakeBody, //2
        SnakeHead, //3
    }


    internal class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;

        private Gridmap gridmap = new Gridmap();

        private Snake Snek = new Snake();
        private SpriteFont font;
        private int timer = 0;
        private Food food = new Food();
        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 15d);;


        }
        protected override void Initialize()
        {

            //Sets window resolution to 600x600;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 600;
            
            graphics.ApplyChanges();
            GridElement.Content = Content;

            //Allows GridElement class to load textures

            //Initializing the gridmap
            gridmap.InitializeGrid();
            
            //Creates the snake
            Snek.GenerateSnake(10, new Vector2(10));
            food.SetDefaults();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Arial");

            base.LoadContent();
        }
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        protected override void Update(GameTime gameTime)
        {

            //Updating the elements of the gridmap
            timer++;
            gridmap.UpdateGrid();


            //Updates all the snake segments
            Snek.Update(gameTime);
            if (food.IsColliding(Snek.snakeSegments[0].Hitbox))
            {
                Snek.EatFood();
                food.EatenBySnake();
            }
            gridmap.PostUpdate();

            if (Snek.Dead)
                Exit();

            
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            _spriteBatch.Begin();
            gridmap.Draw(gameTime , _spriteBatch);
            Snek.Draw(gameTime, _spriteBatch, gridmap);
            food.Draw(gameTime, _spriteBatch, gridmap);
            _spriteBatch.DrawString(font, "Elements: " + gridmap.gridElements.Count + " , Count:" + Snek.snakeSegments.Count, Vector2.Zero, Color.Wheat);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public void ColldingWithWalls()
        {
            //Top
            if (Snek.snakeSegments[0].Position.Y > 0)
                Snek.Dead = true;
            if (Snek.snakeSegments[0].Position.X > 0)
                Snek.Dead = true;

        }


    }
}
