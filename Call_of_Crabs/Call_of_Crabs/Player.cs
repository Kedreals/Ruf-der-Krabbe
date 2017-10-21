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
    public class Player : Character
    {
       
        public Texture2D texture;
        


        public Player(Rectangle collisionBox,Rectangle spritearea): base(collisionBox, spritearea)
        {
                
        }



        public override void Load(ContentManager contentManager, string filename)
        {
            texture = contentManager.Load<Texture2D>("Textures/" + filename);
        }
        
        public override void Update(GameTime time)
        {
            if (Controls.GetKey(Controls.EKey.Up).IsPressed())
                Position += new Vector2(0, -1);
            if (Controls.GetKey(Controls.EKey.Down).IsPressed())
                Position += new Vector2(0, 1f);
            if (Controls.GetKey(Controls.EKey.Left).IsPressed())
                Position += new Vector2(-1f, 0);
            if (Controls.GetKey(Controls.EKey.Right).IsPressed())
                Position += new Vector2(1f, 0);

            if (Controls.GetKey(Controls.EKey.Jump).IsPressed())
            { }

     

            //schwerkraft
            Position += new Vector2(0, 0.5f);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, sprite, Color.White);
        }


        
    }
}
