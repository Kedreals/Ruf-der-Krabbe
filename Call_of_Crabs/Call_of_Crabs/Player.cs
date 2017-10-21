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
       
        public Animation Revolvertexture;
        public Animation Kanonetexture;
        public Animation Seesterntexture;


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
            Revolvertexture = new Animation(contentManager, "RevolverKrabbeTexture", 3, 3.0f);
            Kanonetexture = new Animation(contentManager, "KanonenKrabbeTexture", 3, 3.0f);
            Seesterntexture = new Animation(contentManager, "SeesternKrabbeTexture", 3, 3.0f);

            SetAnimations(0);
        }
        
        private void SetAnimations(int forwardBackward)
        {
            if(forwardBackward == 0)
            {
                Revolvertexture.StopPlaying = true;
                Kanonetexture.StopPlaying = true;
                Seesterntexture.StopPlaying = true;
            }
            if(forwardBackward > 0)
            {
                Revolvertexture.PlayForward = true;
                Kanonetexture.PlayForward = true;
                Seesterntexture.PlayForward = true;
            }
            if(forwardBackward < 0)
            {
                Revolvertexture.PlayBackward = true;
                Kanonetexture.PlayBackward = true;
                Seesterntexture.PlayBackward = true;
            }
        }

        public override void Update(GameTime time)
        {
            Revolvertexture.Update(time);
            Kanonetexture.Update(time);
            Seesterntexture.Update(time);

            if (Controls.GetKey(Controls.EKey.Up).IsPressed())
                Position += new Vector2(0, -1);
            if (Controls.GetKey(Controls.EKey.Down).IsPressed())
                Position += new Vector2(0, 1f);
            if (Controls.GetKey(Controls.EKey.Left).IsPressed())
            {
                SetAnimations(1);
                Position += new Vector2(-1f, 0);
            }
            else if (Controls.GetKey(Controls.EKey.Right).IsPressed())
            {
                SetAnimations(1);
                Position += new Vector2(1f, 0);
            }
            else
            {
                SetAnimations(0);
            }

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
                    if (faces == facing.right) Revolvertexture.Draw(batch, sprite, Color.White);
                    else Revolvertexture.Draw(batch, sprite, null, Color.White, 0, new Vector2(sprite.Width,0), SpriteEffects.FlipHorizontally, 0);
                    break;

                case weapon.kanone:
                    if (faces == facing.right) Kanonetexture.Draw(batch, sprite, Color.White);
                    else Kanonetexture.Draw(batch, sprite, null, Color.White, 0, new Vector2(sprite.Width, 0), SpriteEffects.FlipHorizontally, 0);
                    break;

                case weapon.seestern:
                    if (faces == facing.right) Seesterntexture.Draw(batch, sprite, Color.White);
                    else Seesterntexture.Draw(batch, sprite, null, Color.White, 0, new Vector2(sprite.Width, 0), SpriteEffects.FlipHorizontally, 0);
                    break;

            }
            
        }


        
    }
}
