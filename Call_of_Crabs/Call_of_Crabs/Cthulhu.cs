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
    class Cthulhu : Character
    {
        Animation animation;
        Texture2D death;

        public override bool IsBoss => true;

        public override float ReaktionRadius { get; protected set; } = 20 * Tile.DefaultSize.X;

        public Cthulhu() : base(new Rectangle(0,0,400,400), new Rectangle(0,0,400,400), 50)
        {

        }

        public override bool ReactToPlayer(GameTime time, Vector2 playerPos, Vector2 path)
        {
            if (dead) return false;
            Shoot(playerPos, time);
            return false;
        }

        double shotCooldown = 0.0;
        Random rand = new Random();

        private void Shoot(Vector2 target, GameTime time)
        {
            shotCooldown -= time.ElapsedGameTime.TotalSeconds;
            if (shotCooldown < 0)
            {
                Sound.sounds["Bösesschlitzschlitz"].Play();
                bool right = rand.NextDouble() < 0.5;
                BulletsEverywhere.SpawnBullet(target + new Vector2(right?-200:200,0), right, BulletsEverywhere.BulletType.cthullu);
                shotCooldown = 5;
            }
        }

        public override bool Move(Vector2 target, GameTime time)
        {
            

            return false;
        }

        public override void Draw(SpriteBatch batch)
        {
            if (dead) batch.Draw(death, sprite, Color.White);
            else if (faces == facing.right) animation.Draw(batch, sprite, Color.White);
            else animation.Draw(batch, sprite, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
        }

        public override void Load(ContentManager contentManager, string filename)
        {
            animation = new Animation(contentManager, "Cthulhu", 2, 3);
            death = contentManager.Load<Texture2D>("Textures/DeadKrake");
        }

        public override void Update(GameTime time)
        {
            Position += new Vector2(0, 100) * (float)time.ElapsedGameTime.TotalSeconds;

            animation.Update(time);
        }
    }
}

