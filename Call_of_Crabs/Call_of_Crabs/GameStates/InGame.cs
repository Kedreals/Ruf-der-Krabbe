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
        private Player player = new Player(new Rectangle(25, 25, 100, 50), new Rectangle(0, 0, 200, 100));

        private Rectangle mapRectangle;

        private KritzlerEnemy kritzler = new KritzlerEnemy(new Rectangle(15, 15, 70, 70), new Rectangle(0, 0, 100, 100));

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
            kritzler.Position += new Vector2(70, 150);

            player.Position = new Vector2(1*Tile.DefaultSize.X, 18*Tile.DefaultSize.Y);

        }

        public void LoadContent(ContentManager contentManager)
        {
            background = new Background(contentManager, new string[] { "WaterBackgroundTexture", "FishBackgroundTexture" });
            map.Load(contentManager, "TestMap2");
            player.Load(contentManager,"");
            kritzler.Load(contentManager, "");
        }

        public EGameState Update(GameTime time)
        {
            player.Update(time);
            kritzler.Update(time);


            player.Collide(map);
            kritzler.Collide(map);



            camera.Position = player.collision.Center.ToVector2();
            camera.SetVisibilityContainedIn(mapRectangle);

            return EGameState.InGame;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());

            background.Draw(batch,camera);

            map.Draw(batch);
            kritzler.Draw(batch);
            player.Draw(batch);

            batch.End();
        }
    }
}
