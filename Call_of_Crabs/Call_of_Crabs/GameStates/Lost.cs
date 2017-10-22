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
    class Lost : IGameState
    {
        Animation m_Animation;

        public void Draw(SpriteBatch batch)
        {
            batch.Begin();
            m_Animation.Draw(batch, batch.GraphicsDevice.Viewport.Bounds, Color.White);
            batch.End();
        }

        public void Initialize(GraphicsDevice device)
        {
            
        }

        public void LoadContent(ContentManager contentManager)
        {
            m_Animation = new Animation(contentManager, "Deadscreen", 1, 1);
        }

        public EGameState Update(GameTime time)
        {
            if (Controls.GetKey(Controls.EKey.Confirm).HasJustBeenPressed())
                return EGameState.MainMenu;
            return EGameState.Lost;
        }
    }
}
