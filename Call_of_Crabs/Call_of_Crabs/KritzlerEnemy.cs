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
        



        public KritzlerEnemy(): base(new Rectangle(15, 15, 70, 70), new Rectangle(0, 0, 100, 100),5)
        {

        }



        public override void Load(ContentManager contentManager, string filename)
        {
            texture = contentManager.Load<Texture2D>("Textures/" + "Kitzler");
        }

        public override void Update(GameTime time)
        {

            //schwerkraft
            Position += new Vector2(0, 1000f) * (float)time.ElapsedGameTime.TotalSeconds;

        }

        public override void Draw(SpriteBatch batch)
        {          
             batch.Draw(texture, sprite, Color.White);
        }


    }
}
