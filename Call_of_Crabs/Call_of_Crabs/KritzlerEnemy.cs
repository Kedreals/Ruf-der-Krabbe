using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Call_of_Crabs
{
    public class KritzlerEnemy: Character
    {
        public Texture2D texture;
        



        public KritzlerEnemy(Rectangle collisionBox, Rectangle spritearea): base(collisionBox, spritearea)
        {

        }



        public override void Load(ContentManager contentManager, string filename)
        {
            texture = contentManager.Load<Texture2D>("Textures/" + "Kitzler");
        }

        public override void Update(GameTime time)
        {
           


        }

        public override void Draw(SpriteBatch batch)
        {          
             batch.Draw(texture, sprite, Color.White);
        }


    }
}
