using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call_of_Crabs
{
    class Camera2D
    {
        Vector2 m_pos = new Vector2();
        float m_scale = 1;
        GraphicsDevice m_device;

        Vector2 m_size;

        public Vector2 Position
        {
            get { return m_pos; }
            set { m_pos = value; }
        }

        public float Scale
        {
            get { return m_scale; }
            set
            {
                m_scale = value;
                m_size = new Vector2(m_device.PresentationParameters.BackBufferWidth, m_device.PresentationParameters.BackBufferHeight) / m_scale;
            }
        }

        public Rectangle GetVisibilityRectangle()
        {
            Vector2 upperLeft = new Vector2();
            upperLeft -= m_size / 2.0f;
            upperLeft += m_pos;
            return new Rectangle(upperLeft.ToPoint(), m_size.ToPoint());
        }

        public Camera2D(GraphicsDevice device)
        {
            m_device = device;
        }

        public Matrix GetTransform()
        {
            return Matrix.CreateTranslation(new Vector3(-m_pos.X, -m_pos.Y, 0)) *
                                         Matrix.CreateScale(new Vector3(m_scale, m_scale, 1)) *
                                         Matrix.CreateTranslation(new Vector3(m_device.Viewport.Width * 0.5f, m_device.Viewport.Height * 0.5f, 0));
        }
    }
}
