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
    public abstract class Character
    {

        private Rectangle basecollision;
        private Rectangle basesprite;
        public Rectangle collision;
        public Rectangle sprite;
        public Vector2 offsetRight;
        public Vector2 offsetLeft;

        public enum facing
        {
               left,
               right
        }

        public facing faces = facing.right;

        private Vector2 position;

        public float scale = 1;

        private Vector2 lastpos;

        private float threshold = 0;

        public Vector2 Position
        {
            get { return position; }
            set
            {
                lastpos = position;
                position = value;
                collision.Location = position.ToPoint();
                threshold += (lastpos.X - position.X);
                if (threshold >= 2) { faces = facing.left; threshold -= 2; }
                else if (threshold <= -2) { faces = facing.right; threshold += 2; }
                if(faces==facing.right)sprite.Location = collision.Location + offsetRight.ToPoint();
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

        public Character(Rectangle collisionBox, Rectangle spritearea)
        {
            basecollision = collisionBox;
            basesprite = spritearea;
            offsetRight = (basesprite.Location - basecollision.Location).ToVector2();
            offsetLeft = new Vector2(basesprite.Width - (basecollision.X + basecollision.Width), offsetRight.Y);
            collision = basecollision;
            sprite = basesprite;
            Position = new Vector2(0, 0);
        }


        public abstract void Load(ContentManager contentManager, string filename);


        public abstract void Update(GameTime time);

    
        public abstract void Draw(SpriteBatch batch);



        public void Collide(Map map)
        {

            //schwerkraft
            Position += new Vector2(0, 0.5f);


            foreach (Tile tile in map.Tiles)
            {
                if (tile != null)
                {
                    Rectangle t;

                    Rectangle.Intersect(ref collision, ref tile.TileRectangle, out t);

                    if (t.Width < t.Height)
                    {
                        Position -= new Vector2(t.Width, 0);
                    }
                    else
                    {
                        Position -= new Vector2(0, t.Height);
                    }


                }
            }



        }

    }   
}