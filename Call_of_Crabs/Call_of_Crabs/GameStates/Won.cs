using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Call_of_Crabs.GameStates
{
    class Won : IGameState
    {
        Texture2D background;

        public void Draw(SpriteBatch batch)
        {
            batch.Begin();
            batch.Draw(background, batch.GraphicsDevice.Viewport.Bounds, Color.White);
            batch.End();
        }

        public void Initialize(GraphicsDevice device)
        {
            
        }

        public void LoadContent(ContentManager contentManager)
        {
            background = contentManager.Load<Texture2D>("Textures/Win");
        }

        public EGameState Update(GameTime time)
        {
            if (Controls.GetKey(Controls.EKey.Confirm).HasJustBeenPressed())
                return EGameState.MainMenu;

            return EGameState.Won;
        }
    }
}
