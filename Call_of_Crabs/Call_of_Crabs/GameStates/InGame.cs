using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Call_of_Crabs.GameStates
{
    class InGame : IGameState
    {
        private Map map = new Map();
        private Rectangle mapRectangle;
        

        List<Character> character = new List<Character>();

        private Camera2D camera;

        private Background background;

        public void Initialize(GraphicsDevice graphics)
        {
            camera = new Camera2D(graphics);
            camera.Position += new Vector2(-70, -150);

            

            Vector2 tileSize = new Vector2();
            foreach(Tile t in map.Tiles)
            {
                if (t == null)
                    continue;
                tileSize = t.Size;
                break;
            }

            Point loc = new Point(0, 0);//map.Tiles[0].Position.ToPoint();
            Point size = new Point((int)(map.XDim * tileSize.X), (int)(map.YDim * tileSize.Y));
            mapRectangle = new Rectangle(loc, size);

            player.Position = new Vector2(1*Tile.DefaultSize.X, 18*Tile.DefaultSize.Y);

        }

        public void LoadContent(ContentManager contentManager)
        {
            background = new Background(contentManager, new string[] { "WaterBackgroundTexture", "FishBackgroundTexture" });
            map.Load(contentManager, "TestMap2");
            character.Add(new Player());
            character.Add(new KritzlerEnemy());
            foreach (Character c in character)
            {
                c.Load(contentManager, "");
            }
            BulletsEverywhere.Load(contentManager);
        }

        public EGameState Update(GameTime time)
        {
            foreach(Character c in character)
            {
                c.Update(time);
            }

            BulletsEverywhere.Update(time, map, character);
            
            foreach(Character c in character)
            {
                c.Collide(map);
            }



            camera.Position = character.ElementAt(0).collision.Center.ToVector2();
            camera.SetVisibilityContainedIn(mapRectangle);

            return EGameState.InGame;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());

            background.Draw(batch,camera);

            map.Draw(batch);
            BulletsEverywhere.Draw(batch);
            foreach(Character c in character)
            {
                c.Draw(batch);
            }

            batch.End();
        }
    }
}
