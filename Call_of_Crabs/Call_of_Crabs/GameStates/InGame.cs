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
        public void Initialize(GraphicsDevice graphics)
        {
        }

        public void LoadContent(ContentManager contentManager)
        {
        }

        public EGameState Update(GameTime time)
        {

            return EGameState.InGame;
        }

        public void Draw(SpriteBatch batch)
        {
            
        }
    }
}
