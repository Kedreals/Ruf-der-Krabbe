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
        Camera2D camera;
        Texture2D test;

        public void Initialize(GraphicsDevice graphics)
        {
            camera = new Camera2D(graphics);
            camera.Position += new Vector2(-70, -150);
        }

        public void LoadContent(ContentManager contentManager)
        {
            test = contentManager.Load<Texture2D>("Textures/Kitzler");
            map.Load(contentManager, "TestMap");
            player.Load(contentManager, "RevolverKrabbeTexture1");
        }

        public EGameState Update(GameTime time)
        {
            player.Update(time);

            player.Collide(map);

            camera.Position = player.sprite.Center.ToVector2();

            return EGameState.InGame;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());

            map.Draw(batch);
            batch.Draw(test, Vector2.Zero);
            player.Draw(batch);

            batch.End();
        }
    }
}
