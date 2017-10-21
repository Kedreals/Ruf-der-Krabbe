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
       
        public Texture2D Revolvertexture;
        public Texture2D Kanonetexture;
        public Texture2D Seesterntexture;


        enum weapon
        {
              revolver,
              kanone,
              seestern
        }

        private weapon currentweapon=weapon.revolver;

        public Player(Rectangle collisionBox,Rectangle spritearea): base(collisionBox, spritearea)
        {
                
        }



        public override void Load(ContentManager contentManager,string filename)
        {
            Revolvertexture = contentManager.Load<Texture2D>("Textures/" + "RevolverKrabbeTexture1");
            Kanonetexture = contentManager.Load<Texture2D>("Textures/" + "KanonenKrabbeTexture1");
            Seesterntexture = contentManager.Load<Texture2D>("Textures/" + "SeesternKrabbeTexture1");
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

            if (Controls.GetKey(Controls.EKey.firstweapon).IsPressed())
                currentweapon = weapon.revolver;
            if (Controls.GetKey(Controls.EKey.secondweapon).IsPressed())
                currentweapon = weapon.kanone;
            if (Controls.GetKey(Controls.EKey.thirdweapon).IsPressed())
                currentweapon = weapon.seestern;



            if (Controls.GetKey(Controls.EKey.Jump).IsPressed())
            { }

     
            
        }

        public override void Draw(SpriteBatch batch)
        {
            switch (currentweapon)
            {
                case weapon.revolver:
                    batch.Draw(Revolvertexture, sprite, Color.White);
                    break;

                case weapon.kanone:
                    batch.Draw(Kanonetexture, sprite, Color.White);
                    break;

                case weapon.seestern:
                    batch.Draw(Seesterntexture, sprite, Color.White);
                    break;

            }
            
        }


        
    }
}
