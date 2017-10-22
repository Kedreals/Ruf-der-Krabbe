using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call_of_Crabs
{
    class Kraken : Character
    {
        Animation animation;
        //Texture2D death;

        float verticalSpeed = 100;
        float horizontalSpeed = 100;

        public override float ReaktionRadius { get; protected set; } = 4 * Tile.DefaultSize.X;

        public Kraken() : base(new Rectangle(14,31,69,38), new Rectangle(0,0,100,100), 2)
        {

        }

        public override bool ReactToPlayer(GameTime time, Vector2 playerPos, Vector2 path)
        {
            Vector2 d = playerPos - Position;

            float verticalDot = Vector2.Dot(d, new Vector2(0, 1));


            if (Math.Abs(verticalDot) < 10)
                Shoot(playerPos, time);

            return Move(path, time);
        }

        double shotCooldown = 0.0;

        private void Shoot(Vector2 target, GameTime time)
        {
            shotCooldown -= time.ElapsedGameTime.TotalSeconds;
            if (shotCooldown < 0)
            {
                BulletsEverywhere.SpawnBullet(new Vector2((faces == facing.right) ? (collision.X + collision.Width + 10) : collision.X - 50, collision.Y + 10), (faces == facing.right), BulletsEverywhere.BulletType.regenbogen);
                shotCooldown = 0.5;
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

            Position += d * (float)time.ElapsedGameTime.TotalSeconds;

            return res;
        }

        public override void Draw(SpriteBatch batch)
        {
            if (dead) animation.Draw(batch, sprite, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipVertically, 0);
            else if (faces == facing.left) animation.Draw(batch, sprite, Color.White);
            else animation.Draw(batch, sprite, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
        }

        public override void Load(ContentManager contentManager, string filename)
        {
            animation = new Animation(contentManager, "RegenbogenfischTexture", 7, 3);
            //death = contentManager.Load<Texture2D>("Textures/DeadQualle");
        }

        public override void Update(GameTime time)
        {
            if (dead)
                Position -= new Vector2(0, 100) * (float)time.ElapsedGameTime.TotalSeconds;
            else
                animation.Update(time);
        }
    }
}
}
