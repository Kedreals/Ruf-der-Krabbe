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

        public Vector2 Position
        {
            get { return m_pos; }
            set { m_pos = value; }
        }

        public float Scale
        {
            get { return m_scale; }
            set { m_scale = value; }
        }

        public Camera2D(GraphicsDevice device)
        {
            m_device = device;
        }

        public Matrix GetTransform()
        {
            return Matrix.CreateTranslation(new Vector3(m_pos.X, m_pos.Y, 0)) *
                                         Matrix.CreateScale(new Vector3(m_scale, m_scale, 1)) *
                                         Matrix.CreateTranslation(new Vector3(m_device.Viewport.Width * 0.5f, m_device.Viewport.Height * 0.5f, 0));
        }
    }
}
