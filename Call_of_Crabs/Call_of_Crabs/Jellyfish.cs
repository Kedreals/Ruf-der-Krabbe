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
    class Jellyfish : Character
    {
        Animation animation;
        Texture2D death;
        public override float ReaktionRadius { get; protected set; } = 6*Tile.DefaultSize.Y;
        float verticalSpeed = 10;
        float horizontalSpeed = 1;

        public Jellyfish() : base(new Rectangle(28,15,44,31), new Rectangle(0,0,100,100), 1)
        {

        }

        public override bool ReactToPlayer(GameTime time, Vector2 playerPos, Vector2 path)
        {
            if (dead) return false;
            Shoot(playerPos, time);
            return Move(path, time);
        }

        double shotCooldown = 0.0;

        private void Shoot(Vector2 target, GameTime time)
        {
            shotCooldown -= time.ElapsedGameTime.TotalSeconds;
            if (shotCooldown < 0 )
            {
                Sound.sounds["Blob"].Play();
                BulletsEverywhere.SpawnBullet(sprite.Center.ToVector2()+new Vector2(0,verticalSpeed), true, BulletsEverywhere.BulletType.seifenblase);
                shotCooldown += 0.5;
            }
            else if(shotCooldown < 0)
            {
                shotCooldown = 0;
            }
            
        }

        public override bool Move(Vector2 target, GameTime time)
        {
            Vector2 d = target - Position;

            float verticalDot = Vector2.Dot(d, new Vector2(0, 1));
            float horizontalDot = Vector2.Dot(d, new Vector2(1, 0));

            bool res = Math.Abs(verticalDot) <= verticalSpeed && Math.Abs(horizontalDot) <= horizontalSpeed;

            if (verticalDot > verticalSpeed)
                d.Y = horizontalSpeed;
            if (verticalDot < -verticalSpeed)
                d.Y = -verticalSpeed;

            if (horizontalDot > horizontalSpeed)
                d.X = horizontalSpeed;
            if (horizontalDot < horizontalSpeed)
                d.X = -horizontalSpeed;

            Position += d;

            return res;
        }

        public override void Draw(SpriteBatch batch)
        {
            if(dead) batch.Draw(death, sprite, Color.White);
            else if (faces == facing.right) animation.Draw(batch, sprite, Color.White);
            else animation.Draw(batch, sprite, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
        }

        public override void Load(ContentManager contentManager, string filename)
        {
            animation = new Animation(contentManager, "QualleTexture", 3, 3);
            death = contentManager.Load<Texture2D>("Textures/DeadQualle");
        }

        public override void Update(GameTime time)
        {
            if (dead)
                Position += new Vector2(0, 100) * (float)time.ElapsedGameTime.TotalSeconds;
            animation.Update(time);
        }
    }
}
