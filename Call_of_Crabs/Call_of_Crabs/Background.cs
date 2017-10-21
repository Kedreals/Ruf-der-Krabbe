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
    class Background
    {
        Texture2D[] m_layers;

        public Background(ContentManager content, string[] layers)
        {
            m_layers = new Texture2D[layers.Length];

            for(int i= 0; i< layers.Length; ++i)
            {
                m_layers[i] = content.Load<Texture2D>("Textures/" + layers[i]);
            }
        }

        public void Draw(SpriteBatch batch, Camera2D cam)
        {
            batch.Draw(m_layers[0], cam.GetVisibilityRectangle(), Color.White);
            for(int i = 1; i < m_layers.Length; ++i)
            {
                Rectangle dest = m_layers[i].Bounds;
                Rectangle test = Rectangle.Intersect(dest, cam.GetVisibilityRectangle());

                while(test.Height > 0 && test.Width <= 0)
                {
                    dest.Offset(new Point(dest.Width, 0));
                    test = Rectangle.Intersect(dest, cam.GetVisibilityRectangle());
                }

                Rectangle destLeft = new Rectangle(dest.Location - new Point(dest.Width, 0), dest.Size);
                Rectangle destRight = new Rectangle(dest.Location + new Point(dest.Width, 0), dest.Size);

                batch.Draw(m_layers[i], dest, Color.White);
                batch.Draw(m_layers[i], destLeft, Color.White);
                batch.Draw(m_layers[i], destRight, Color.White);
            }
        }
    }
}
