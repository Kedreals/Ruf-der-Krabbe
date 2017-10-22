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
        public Texture2D death;
        public Animation animation;
        
        float horizontalSpeed = 1.0f;
        public override float ReaktionRadius { get; protected set; }




        public KritzlerEnemy(): base(new Rectangle(15, 15, 70, 70), new Rectangle(0, 0, 100, 100),5)
        {
            ReaktionRadius = 4 * Tile.DefaultSize.X;
        }

        public override bool Move(Vector2 target, GameTime time)
        {
            Vector2 d = target - Position;

            float verticalDot = Vector2.Dot(d, new Vector2(0, 1));
            float horizontalDot = Vector2.Dot(d, new Vector2(1, 0));

            bool res = /*Math.Abs(verticalDot) <= verticalSpeed &&*/ Math.Abs(horizontalDot) <= horizontalSpeed;

            d.Y = 0;
            if(verticalDot < 0 && jumpcount < 2 && !isJumping)
            {
                isJumping = true;
                currjumpduration = 0;
                jumpcount++;
                currjumpheight = 0;
            }

            if (isJumping)
                Jump(time);
            

            if (horizontalDot > horizontalSpeed)
                d.X = horizontalSpeed;
            if (horizontalDot < horizontalSpeed)
                d.X = -horizontalSpeed;

            Position += d;

            return res;
        }

        public override void ReactToPlayer(GameTime time, Vector2 playerPos)
        {
            Move(playerPos, time);
            Shoot(playerPos, time);
        }

        double shotCooldown = 0.0;

        private void Shoot(Vector2 target, GameTime time)
        {
            shotCooldown -= time.ElapsedGameTime.TotalSeconds;
            if (shotCooldown < 0)
            {
                BulletsEverywhere.SpawnBullet(new Vector2((faces == facing.right) ? (collision.X + collision.Width + 2) : collision.X - 20, collision.Y + 10), (faces == facing.right), BulletsEverywhere.BulletType.revolver);
                shotCooldown += 0.5;
            }
        }

        public override void Load(ContentManager contentManager, string filename)
        {
            death = contentManager.Load<Texture2D>("Textures/" + "DeadKritzler");
            animation = new Animation(contentManager, "Kitzler", 3, 3);
        }

        public override void Update(GameTime time)
        {
            if (!isJumping)
                Position += new Vector2(0, 100f) * (float)time.ElapsedGameTime.TotalSeconds;

        }

        public override void Draw(SpriteBatch batch)
        {
            if (dead) batch.Draw(death, sprite, Color.White);
            else if (faces == facing.right) animation.Draw(batch, sprite, Color.White);
            else animation.Draw(batch, sprite, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
        }


    }
}
