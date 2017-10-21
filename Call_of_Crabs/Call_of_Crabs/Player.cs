﻿using System;
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

        double shotCooldown = 0;

        enum weapon
        {
              revolver,
              kanone,
              seestern
        }

        private weapon currentweapon=weapon.revolver;

        public Player(): base(new Rectangle(25, 25, 100, 50), new Rectangle(0, 0, 200, 100),3)
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
                Position += new Vector2(0, -150f) * (float)time.ElapsedGameTime.TotalSeconds;
            if (Controls.GetKey(Controls.EKey.Down).IsPressed())
                Position += new Vector2(0, 150f) * (float)time.ElapsedGameTime.TotalSeconds;
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

            if (Controls.GetKey(Controls.EKey.Jump).IsPressed())
            {
                if (shotCooldown < 0)
                {
                    switch (currentweapon)
                    {
                        case weapon.revolver:
                            BulletsEverywhere.SpawnBullet(new Vector2((faces==facing.right)?(collision.X + collision.Width + 2):collision.X-20, collision.Y + 10), (faces == facing.right), BulletsEverywhere.BulletType.revolver);
                            shotCooldown += 0.5;
                            break;

                        case weapon.kanone:
                            BulletsEverywhere.SpawnBullet(new Vector2((faces == facing.right) ? (collision.X + collision.Width + 2) : collision.X - 20, collision.Y + 15), (faces == facing.right), BulletsEverywhere.BulletType.kanone);
                            shotCooldown += 1;
                            break;

                        case weapon.seestern:
                            BulletsEverywhere.SpawnBullet(new Vector2((faces == facing.right) ? (collision.X + collision.Width + 2) : collision.X - 20, collision.Y + 12), (faces == facing.right), BulletsEverywhere.BulletType.seestern);
                            shotCooldown += 0.6;
                            break;
                    }
                } 
                
            }

            //schwerkraft
            Position += new Vector2(0, 10f) * (float)time.ElapsedGameTime.TotalSeconds;

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
