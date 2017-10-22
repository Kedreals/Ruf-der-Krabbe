using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        double shotCooldown = 0;

        enum weapon
        {
              revolver,
              kanone,
              seestern
        }

        private weapon currentweapon=weapon.revolver;

        public Player(): base(new Rectangle(25, 25, 50, 50), new Rectangle(0, 0, 200, 100),10)
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

            if (Controls.GetKey(Controls.EKey.Up).HasJustBeenPressed() && jumpcount < 200)
            {
                isJumping = true;
                currjumpduration = 0;
                currjumpheight = 0;
                jumpcount += 1;
            }
            if (Controls.GetKey(Controls.EKey.Down).IsPressed())
            {
                Position += new Vector2(0, 150f) * (float)time.ElapsedGameTime.TotalSeconds;
                isJumping = false;
            }
               
            if (Controls.GetKey(Controls.EKey.Left).IsPressed())
            {
                SetAnimations(1);
                Position += new Vector2(-150f,0) * (float)time.ElapsedGameTime.TotalSeconds;
            }
            else if (Controls.GetKey(Controls.EKey.Right).IsPressed())
            {
                SetAnimations(1);
                Position += new Vector2(150f,0 ) * (float)time.ElapsedGameTime.TotalSeconds;
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

            shotCooldown -= time.ElapsedGameTime.TotalSeconds;
            if (shotCooldown < 0) shotCooldown = -0.1;

            if (Controls.GetKey(Controls.EKey.Shoot).IsPressed())
            {
                if (shotCooldown < 0)
                {
                    
                    Sound.sounds["PewPew2"].Play();
                    switch (currentweapon)
                    {
                        case weapon.revolver:
                            BulletsEverywhere.SpawnBullet(new Vector2((faces == facing.right) ? (collision.X + collision.Width + 30) : collision.X -30, collision.Y), (faces == facing.right), BulletsEverywhere.BulletType.cthullu);
                            BulletsEverywhere.SpawnBullet(new Vector2((faces==facing.right)?(collision.X + collision.Width + 30):collision.X-40, collision.Y + 11), (faces == facing.right), BulletsEverywhere.BulletType.revolver);
                            shotCooldown += 0.5;
                            break;

                        case weapon.kanone:
                            BulletsEverywhere.SpawnBullet(new Vector2((faces == facing.right) ? (collision.X + collision.Width + 30) : collision.X - 40, collision.Y + 13), (faces == facing.right), BulletsEverywhere.BulletType.kanone);
                            shotCooldown += 1;
                            break;

                        case weapon.seestern:
                            BulletsEverywhere.SpawnBullet(new Vector2((faces == facing.right) ? (collision.X + collision.Width + 30) : collision.X - 40, collision.Y + 11), (faces == facing.right), BulletsEverywhere.BulletType.seestern);
                            shotCooldown += 0.6;
                            break;
                    }
                } 
                
            }

            if (isJumping)
            {
                
                Jump(time);
            }
            else
            {
                //schwerkraft
                Position += new Vector2(0, 100f) * (float)time.ElapsedGameTime.TotalSeconds;
            }

        }

        public override void Draw(SpriteBatch batch)
        {
            switch (currentweapon)
            {
                case weapon.revolver:
                    if (faces == facing.right) Revolvertexture.Draw(batch, sprite, Color.White);
                    else Revolvertexture.Draw(batch, sprite, null, Color.White, 0, new Vector2(0,0), SpriteEffects.FlipHorizontally, 0);
                    break;

                case weapon.kanone:
                    if (faces == facing.right) Kanonetexture.Draw(batch, sprite, Color.White);
                    else Kanonetexture.Draw(batch, sprite, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                    break;

                case weapon.seestern:
                    if (faces == facing.right) Seesterntexture.Draw(batch, sprite, Color.White);
                    else Seesterntexture.Draw(batch, sprite, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                    break;

            }
            
        }


        
    }
}
