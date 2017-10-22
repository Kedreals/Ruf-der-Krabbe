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
        private Vector2[] offsets;
        private Point[] points;

        public Background(ContentManager content, string[] layers, Vector2[] offsets)
        {
            m_layers = new Texture2D[layers.Length];
            this.offsets = offsets;
            points = new Point[layers.Length];

            for(int i= 0; i< layers.Length; ++i)
            {
                m_layers[i] = content.Load<Texture2D>("Textures/" + layers[i]);
                points[i] = offsets[i].ToPoint();
            }
        }

        public void Draw(SpriteBatch batch, Camera2D cam)
        {
            batch.Draw(m_layers[0], cam.GetVisibilityRectangle(), Color.White);
            for (int i = 1; i < m_layers.Length; ++i)
            {
                int width = m_layers[i].Width;

                int camTest = cam.GetVisibilityRectangle().X - (points[i].X + width + (int)offsets[i].X);

                while (camTest > 0)
                {
                    points[i].X += width + (int)offsets[i].X;
                    camTest = cam.GetVisibilityRectangle().X - (points[i].X + width + (int)offsets[i].X);
                }

                Rectangle destLeft = new Rectangle(new Point(points[i].X - width - (int)offsets[i].X, 0), m_layers[i].Bounds.Size);
                Rectangle destRight = new Rectangle(new Point(points[i].X + width + (int)offsets[i].X, 0), m_layers[i].Bounds.Size);

                batch.Draw(m_layers[i], new Rectangle(points[i], m_layers[i].Bounds.Size), Color.White);
                batch.Draw(m_layers[i], destLeft, Color.White);
                batch.Draw(m_layers[i], destRight, Color.White);
            }
        }
    }
}
