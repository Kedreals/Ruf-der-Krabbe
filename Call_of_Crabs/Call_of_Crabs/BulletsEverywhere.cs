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
    public static class BulletsEverywhere
    {
        abstract class ABullet : Character
        {

            public ABullet(Rectangle collisonbox,Rectangle spritebox,int hits) : base(collisonbox, spritebox,hits)
            {
               
            }

            public void Collide(Character c)
            {
                if (c.dead) return;
                Rectangle t;

                Rectangle.Intersect(ref collision, ref c.collision, out t);

                if (t.Width!=0 || t.Height!=0)
                {
                    c.getHit(hitpoints);
                    getHit(hitpoints);
                }
            }

            new public void Collide(Map map)
            {

       
                foreach (Tile tile in map.Tiles)
                {
                    if (tile != null)
                    {
                        Rectangle t;

                        Rectangle.Intersect(ref collision, ref tile.TileRectangle, out t);

                        if (t.Width != 0 || t.Height != 0)
                        {
                            getHit(hitpoints);
                        }


                    }
                }



            }
        }

        class RevolverBullet : ABullet
        {
            static Animation texture=null;

            public Vector2 velocity=new Vector2(500f,0.1f);

            float bulletdrop = 1.001f;

            float distancetravelled = 0;

            public RevolverBullet(bool right):base(new Rectangle(16,11,10,4),new Rectangle(0,0,30,30),1)
            {
                if (!right)
                {
                    velocity.X *= -1;
                    faces = facing.left;
                }
            }

            public override void Draw(SpriteBatch batch)
            {
                if (faces == facing.right) texture.Draw(batch, sprite, Color.White);
                else texture.Draw(batch, sprite, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }

            public override void Load(ContentManager contentManager, string filename)
            {
                if (texture == null) texture = new Animation(contentManager, "Patrone", 1, 3.0f);
            }

            public override void Update(GameTime time)
            {
                
                Vector2 a = velocity * (float)time.ElapsedGameTime.TotalSeconds;
                distancetravelled += a.X;
                velocity.Y = (float)Math.Pow(bulletdrop, (Math.Abs(distancetravelled) / 10));
                Position += a;
            }

            
        }

        class KanoneBullet : ABullet
        {
            static Animation texture=null;

            public Vector2 velocity = new Vector2(200f, 0.1f);

            float bulletdrop = 1.2f;

            float distancetravelled = 0;

            public KanoneBullet(bool right) : base(new Rectangle(16, 11, 10, 4), new Rectangle(0, 0, 30, 30), 3)
            {
                if (!right)
                {
                    velocity.X *= -1;
                    faces = facing.left;
                }
                
            }

            public override void Draw(SpriteBatch batch)
            {
                if (faces == facing.right) texture.Draw(batch, sprite, Color.White);
                else texture.Draw(batch, sprite, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }

            public override void Load(ContentManager contentManager, string filename)
            {
                if (texture == null) texture = new Animation(contentManager, "Kugel", 1, 3.0f);
            }

            public override void Update(GameTime time)
            {
                Vector2 a = velocity * (float)time.ElapsedGameTime.TotalSeconds;
                distancetravelled += a.X;
                velocity.Y = (float)Math.Pow(bulletdrop, (Math.Abs(distancetravelled) / 10));
                Position += a;
            }


        }

        class SeesternBullet : ABullet
        {
            static Animation texture=null;

            public Vector2 velocity = new Vector2(300f, 0f);

           

            public SeesternBullet(bool right) : base(new Rectangle(16, 11, 10, 4), new Rectangle(0, 0, 30, 30), 1)
            {
                if (!right)
                {
                    velocity.X *= -1;
                    faces = facing.left;
                }
            }

            public override void Draw(SpriteBatch batch)
            {
                if (faces == facing.right) texture.Draw(batch, sprite, Color.White);
                else texture.Draw(batch, sprite, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }

            public override void Load(ContentManager contentManager, string filename)
            {
                if (texture == null) texture = new Animation(contentManager, "SeesternSchuss", 1, 3.0f);
            }

            public override void Update(GameTime time)
            {
                Position += velocity * (float)time.ElapsedGameTime.TotalSeconds;
            }


        }

        class MesserBullet : ABullet
        {
            static Animation texture = null;

            public Vector2 velocity = new Vector2(200f, 0.5f);

            float bulletdrop = 10f;

            float distancetravelled = 0;

            public MesserBullet(bool right) : base(new Rectangle(16, 11, 10, 4), new Rectangle(0, 0, 30, 30), 3)
            {
                if (!right)
                {
                    velocity.X *= -1;
                    faces = facing.left;
                }
                Scale(3, 3);
            }

            public override void Draw(SpriteBatch batch)
            {
                if (faces == facing.right) texture.Draw(batch, sprite, Color.White);
                else texture.Draw(batch, sprite, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }

            public override void Load(ContentManager contentManager, string filename)
            {
                if (texture == null) texture = new Animation(contentManager, "Messer", 1, 3.0f);
            }

            public override void Update(GameTime time)
            {
                Vector2 a = velocity * (float)time.ElapsedGameTime.TotalSeconds;
                distancetravelled += a.X;
                velocity.Y = (float)Math.Pow(bulletdrop, (Math.Abs(distancetravelled) / 10));
                Position += a;
            }

        }


        public static void Load(ContentManager contentManager)
        {
            ABullet a;
            a = new RevolverBullet(true);
            a.Load(contentManager, "");
            a = new KanoneBullet(true);
            a.Load(contentManager, "");
            a = new SeesternBullet(true);
            a.Load(contentManager, "");
            a = new MesserBullet(true);
            a.Load(contentManager, "");
        }

        public enum BulletType
        {
            revolver,
            kanone,
            seestern,
            knife
        }

        static ABullet[] bullets=new ABullet[100];
        static uint numberofBullets = 0;

        public static void SpawnBullet(Vector2 position,bool right,BulletType type)
        {
            if (numberofBullets < 100)
            {
                switch (type)
                {
                    case BulletType.revolver:
                        bullets[numberofBullets] = new RevolverBullet(right);
                        bullets[numberofBullets++].position = position;
                        break;

                    case BulletType.kanone:
                        bullets[numberofBullets] = new KanoneBullet(right);
                        bullets[numberofBullets++].position = position;
                        break;

                    case BulletType.seestern:
                        bullets[numberofBullets] = new SeesternBullet(right);
                        bullets[numberofBullets++].position = position;
                        break;
                    case BulletType.knife:
                        bullets[numberofBullets] = new MesserBullet(right);
                        bullets[numberofBullets++].position = position;
                        break;


                }

            }

        }

        static void swap(uint a,uint b)
        {
            ABullet tmp = bullets[a];
            bullets[a] = bullets[b];
            bullets[b] = tmp;
        }

        public static void Update(GameTime time,Map map,List<Character> character)
        {
            for(uint i = 0; i < numberofBullets; ++i)
            {
                ABullet c = bullets[i];

                if(Math.Abs(c.Position.X)>10000) swap(i, --numberofBullets);

                c.Update(time);
                c.Collide(map);

                foreach(Character a in character)
                {
                    c.Collide(a);
                }

                

                if (c.dead)
                {
                    swap(i, --numberofBullets);
                }
            }


        }

        public static void Draw(SpriteBatch batch)
        {
            for (uint i = 0; i < numberofBullets; ++i)
            {
                ABullet c = bullets[i];

                c.Draw(batch);
            }

        }
    }
}
