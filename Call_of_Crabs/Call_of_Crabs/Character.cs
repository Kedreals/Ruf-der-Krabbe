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
    public abstract class Character
    {
        public int hitpoints;

        public bool dead = false;

        private Rectangle basecollision;
        private Rectangle basesprite;
        public Rectangle collision;
        public Rectangle sprite;
        public Vector2 offsetRight;
        public Vector2 offsetLeft;

        public float Speed;

        public enum facing
        {
               left,
               right
        }

        public facing faces = facing.right;

        public Vector2 position;

        public float scale = 1;

        private Vector2 lastpos;

        private float threshold = 0;

        protected float jumpheight = 70f;
        protected float currjumpduration = 0f;
        protected float currjumpheight = 0;
        protected float jumpspeed = 300;
        protected int jumpcount = 0;
        protected bool isJumping = false;
        protected float jumpduration = 0.5f;

        public virtual float ReaktionRadius { get; protected set; } = 0;

        public virtual void Die()
        {
            dead = true;
        }

        public virtual void ReactToPlayer(GameTime time, Vector2 playerPos)
        {

        }

        public void Jump(GameTime time)
        {
            currjumpduration += (float)time.ElapsedGameTime.TotalSeconds;
            float dy = 2 * (jumpheight / (jumpduration * jumpduration)) * (currjumpduration - jumpduration);

            Position += new Vector2(0, dy)*(float)time.ElapsedGameTime.TotalSeconds;

            currjumpheight -= dy*(float)time.ElapsedGameTime.TotalSeconds;

            if (dy > 0)
                isJumping = false;
        }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                lastpos = position;
                position = value;
                collision.Location = position.ToPoint();
                threshold += (lastpos.X - position.X);
                if (threshold >= 2) { faces = facing.left; threshold = 2; }
                else if (threshold <= -2) { faces = facing.right; threshold = -2; }
                if (faces == facing.right) sprite.Location = collision.Location + offsetRight.ToPoint();
                else sprite.Location = collision.Location + offsetLeft.ToPoint();
            }
        }


        public void Scale(float x, float y)
        {
            collision.Width = (int)(basecollision.Width * x);
            collision.Height = (int)(basecollision.Height * y);

            sprite.Width = (int)(basesprite.Width * x);
            sprite.Height = (int)(basesprite.Height * y);
        }

        public Character(Rectangle collisionBox, Rectangle spritearea,int hits)
        {
            basecollision = collisionBox;
            basesprite = spritearea;
            offsetRight = (basesprite.Location - basecollision.Location).ToVector2();

            offsetRight = new Vector2(basesprite.X - basecollision.X, basesprite.Y - basecollision.Y);
            offsetLeft = new Vector2((- basesprite.Width+basecollision.Width+basecollision.X),offsetRight.Y);

            collision = basecollision;
            sprite = basesprite;
            position = new Vector2(0, 0);
            hitpoints = hits;
        }

        public abstract void Load(ContentManager contentManager, string filename);


        public abstract void Update(GameTime time);

    
        public abstract void Draw(SpriteBatch batch);

        /// <summary>
        /// moves this character in the direction of the target.
        /// Returns bool if the target is reached in the next tick
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual bool Move(Vector2 target, GameTime time)
        {
            return false;
        }

        public void getHit(int damage)
        {
            hitpoints -= damage;
            if (hitpoints <= 0) dead = true;
        }

        public void Collide(Map map)
        {
            foreach (Tile tile in map.Tiles)
            {
                if (tile != null && tile.Type!=TileType.DecorationTile)
                {
                    Rectangle t;

                    Rectangle.Intersect(ref collision, ref tile.TileRectangle, out t);

                    if(t.Height == 0 && t.Width == 0)
                        continue;

                    if (t.Width < t.Height)
                    {
                        int sign = Math.Sign(Position.X - tile.Position.X);
                        Position = Position + sign * new Vector2(t.Width, 0);
                        
                    }
                    else
                    {
                        int sign = Math.Sign(Position.Y - tile.Position.Y);
                        if (sign < 0)
                        {
                            jumpcount = 0;
                        }
                        Position = Position + sign * new Vector2(0, t.Height);
                        isJumping = false;
                        
                    }


                }
            }



        }

    }   
}
