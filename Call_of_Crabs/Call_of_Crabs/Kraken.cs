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
        Texture2D death;

        float verticalSpeed = 100;
        float horizontalSpeed = 100;

        public override float ReaktionRadius { get; protected set; } = 4 * Tile.DefaultSize.X;

        public Kraken() : base(new Rectangle(34,0,28,58), new Rectangle(0,0,150,100), 2)
        {

        }

        public override bool ReactToPlayer(GameTime time, Vector2 playerPos, Vector2 path)
        {
            if (dead) return false;
            Shoot(playerPos, time);
            Move(playerPos, time);
            return false;
        }

        double shotCooldown = 0.0;

        private void Shoot(Vector2 target, GameTime time)
        {
            shotCooldown -= time.ElapsedGameTime.TotalSeconds;
            if (shotCooldown < 0)
            {
                BulletsEverywhere.SpawnBullet(new Vector2((faces == facing.right) ? (collision.X + collision.Width + 20) : collision.X - 50, collision.Y + 35), (faces == facing.right), BulletsEverywhere.BulletType.kanone);
                shotCooldown = 1.5;
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
            if (dead) batch.Draw(death, sprite, Color.White);
            else if (faces == facing.right) animation.Draw(batch, sprite, Color.White);
            else animation.Draw(batch, sprite, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
        }

        public override void Load(ContentManager contentManager, string filename)
        {
            animation = new Animation(contentManager, "GegnerKrakenTexture", 3, 3);
            death = contentManager.Load<Texture2D>("Textures/DeadKrake");
        }

        public override void Update(GameTime time)
        {
            if (dead)
                Position += new Vector2(0, 100) * (float)time.ElapsedGameTime.TotalSeconds;

            animation.Update(time);
        }
    }

}
