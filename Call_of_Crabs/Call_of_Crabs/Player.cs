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
    public class Player
    {
        private Rectangle basecollision;
        private Rectangle basesprite;
        public Rectangle collision;
        public Rectangle sprite;
        public Texture2D texture;
        public Vector2 offset;

        private Vector2 position;

        public float scale = 1;

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
            }
        }

        public void Scale(float x,float y)
        {
            collision.Width = (int)(basecollision.Width * x);
            collision.Height = (int)(basecollision.Height * y);

            sprite.Width = (int)(basesprite.Width * x);
            sprite.Height = (int)(basesprite.Height * y);
        }

        public Player(Rectangle collisionBox,Rectangle spritearea)
        {
            basecollision = collisionBox;
            basesprite = spritearea;
            offset = (sprite.Location - collision.Location).ToVector2();
            collision = basecollision;
            sprite = basesprite;
            Position = new Vector2(0, 0);
        }



        public void Load(ContentManager contentManager, string filename)
        {
            texture = contentManager.Load<Texture2D>("Textures/" + filename);
        }
        
        public void Update(GameTime time)
        {
            if (Controls.GetKey(Controls.EKey.Up).IsPressed())
                Position += new Vector2(0, -1);
            if (Controls.GetKey(Controls.EKey.Down).IsPressed())
                Position += new Vector2(0, 1f);
            if (Controls.GetKey(Controls.EKey.Left).IsPressed())
                Position += new Vector2(-1f, 0);
            if (Controls.GetKey(Controls.EKey.Right).IsPressed())
                Position += new Vector2(1f, 0);

            if (Controls.GetKey(Controls.EKey.Jump).IsPressed())
            { }

            collision.Location = position.ToPoint();
            sprite.Location = collision.Location + offset.ToPoint();

            //schwerkraft
            Position += new Vector2(0, 0.5f);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, sprite, Color.White);
        }


        public void Collide(Map map)
        {
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

                    collision.Location = position.ToPoint();
                    sprite.Location = collision.Location + offset.ToPoint();
                }
            }


            
        }
    }
}
