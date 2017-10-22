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
        
        Player character;
        private EnemyHandler enemyHandler;

        private Camera2D camera;

        private Background background;

        private Effect effect;
        private RenderTarget2D renderTarget;

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

            renderTarget = new RenderTarget2D(graphics, graphics.PresentationParameters.BackBufferWidth, graphics.PresentationParameters.BackBufferHeight);
        }

        public void LoadContent(ContentManager contentManager)
        {
            background = new Background(contentManager, new string[] { "WaterBackgroundTexture", "FishBackgroundTexture" });
            map.Load(contentManager, "TestMap2");
            Player player = new Player();
            player.Load(contentManager, "");
            //KritzlerEnemy kritzler = new KritzlerEnemy();
            player.Position = new Vector2(700, 550);//new Vector2(1, 18) * Tile.DefaultSize;
            //kritzler.Position = new Vector2(4, 15) * Tile.DefaultSize;
            character = player;
            BulletsEverywhere.Load(contentManager);
            
            enemyHandler = new EnemyHandler(contentManager, Level.FirstLevel, player);
            effect = contentManager.Load<Effect>("Effects/Wobble");
            effect.CurrentTechnique = effect.Techniques[0];
        }

        public EGameState Update(GameTime time)
        {
            if (!character.sprite.Intersects(mapRectangle))
                return EGameState.Lost;

            character.Update(time);

            enemyHandler.Update(time);

            List<Character> c = enemyHandler.List;
            c.Add(character);

            BulletsEverywhere.Update(time, map, c);

            character.Collide(map);

            enemyHandler.Collide(map);

            camera.Position = character.collision.Center.ToVector2();
            camera.SetVisibilityContainedIn(mapRectangle);

            effect.Parameters["Time"].SetValue((float)time.TotalGameTime.TotalSeconds);
            if (character.dead)
                return EGameState.InGame;

            return EGameState.InGame;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.GraphicsDevice.SetRenderTarget(renderTarget);

            batch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());

            background.Draw(batch,camera);

            batch.End();


            batch.GraphicsDevice.SetRenderTarget(null);

            batch.Begin(SpriteSortMode.Deferred, null, null, null, null, effect, null);

            batch.Draw(renderTarget, Vector2.Zero, Color.White);

            batch.End();

            batch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());
            map.Draw(batch);
            BulletsEverywhere.Draw(batch);
            enemyHandler.Draw(batch);
            character.Draw(batch);
            batch.End();

        }
    }
}
