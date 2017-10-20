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
        Camera2D camera;
        Texture2D test;

        public void Initialize(GraphicsDevice graphics)
        {
            camera = new Camera2D(graphics);
        }

        public void LoadContent(ContentManager contentManager)
        {
            test = contentManager.Load<Texture2D>("Textures/Kitzler");
        }

        public EGameState Update(GameTime time)
        {
            if (Controls.GetKey(Controls.EKey.Up).IsPressed())
                camera.Position += new Vector2(0, 1);
            if (Controls.GetKey(Controls.EKey.Down).IsPressed())
                camera.Position += new Vector2(0, -1f);
            if (Controls.GetKey(Controls.EKey.Left).IsPressed())
                camera.Position += new Vector2(1f,0);
            if (Controls.GetKey(Controls.EKey.Right).IsPressed())
                camera.Position += new Vector2(-1f,0);

            if (Controls.GetKey(Controls.EKey.Jump).IsPressed())
                camera.Scale *= 0.99f;

            return EGameState.InGame;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());

            batch.Draw(test, Vector2.Zero);

            batch.End();
        }
    }
}
