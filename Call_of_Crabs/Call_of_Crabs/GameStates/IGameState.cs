using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Call_of_Crabs.GameStates
{
    enum EGameState
    {
        None = -1,

        MainMenu,
        //Options,
        InGame,
        //Credits,


        Count
    }
    
    interface IGameState
    {
        void LoadContent(ContentManager contentManager);
        void Initialize(GraphicsDevice device);
        EGameState Update(GameTime time);
        void Draw(SpriteBatch batch);
    }
}
