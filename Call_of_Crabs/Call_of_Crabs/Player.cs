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

        public float scale = 1;

        public Vector2 Position
        {
            get { return new Vector2(collision.X, collision.Y); }
            set
            {
                collision.Location = value.ToPoint();
                sprite.Location = collision.Location + offset.ToPoint();
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

        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, sprite, Color.White);
        }

    }
}
