using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeGame.Classes
{

    public class GridElement
    {
        public GridElement(int textureID , Rectangle rect , int[,] parentGrid)
        {
            TextureID = textureID;
            GridRect = rect;
            if(TextureID != 0)
            {
                texture = Content.Load<Texture2D>("Texture_" + TextureID);

            }
            this.parentGrid = parentGrid;

        }

        public int TextureID;

        private int[,] parentGrid;

        public Rectangle GridRect;

        public static ContentManager Content;

        public Texture2D texture;


        public void UpdateElement()
        {
            TextureID = parentGrid[GridRect.X/12, GridRect.Y/12];
            if(TextureID != 0)
                texture = Content.Load<Texture2D>("Texture_" + TextureID);


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(TextureID != 0)
            {
                spriteBatch.Draw(texture, GridRect, Color.White);

            }
        }
    }
    public class Gridmap
    {
        public int mapWidth = 50;

        public int mapHeight = 50;

        public int size = 12;

       

        public int TextureID = 0;
        
        public int[,] gridArray;

        public List<GridElement> gridElements = new List<GridElement>();

        //Sets all elements of array to 0
        public void InitializeGrid()
        {
            gridArray = new int[mapWidth, mapHeight];
            for(int x = 0;x < mapWidth; x++)
            {
                for(int y = 0;y < mapHeight; y++)
                {
                    

                    gridArray[y , x] = 0;
                    int num = gridArray[y, x];
                    
                    gridElements.Add(new GridElement(num, new Rectangle(x * size, y * size, size, size), gridArray));

                    


                }
            }
        }
        
        public void UpdateGrid()
        {
            foreach(GridElement gridElement in gridElements)
            {
                gridElement.UpdateElement();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(GridElement element in gridElements)
            {
                element.Draw(spriteBatch);
                
            }
        }
        public void PostUpdate()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    gridArray[y, x] = 0;
                }
            }
        }
        
    }
}
