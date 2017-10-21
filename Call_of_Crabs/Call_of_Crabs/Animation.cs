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
    public class Animation
    {
        private Texture2D[] m_frames;
        private int frame = 0;
        private float m_fps;
        private float m_frequence => 1.0f / m_fps;
        private float m_frameShownFor = 0.0f;

        private int m_forwardBackward = 1;

        public bool IsPlaying => m_forwardBackward != 0;
        public bool PlayForward { get { return m_forwardBackward > 0; } set { m_forwardBackward = 1; } }
        public bool PlayBackward { get { return m_forwardBackward < 0; } set { m_forwardBackward = -1; } }
        public bool StopPlaying { get { return m_forwardBackward == 0; } set { m_forwardBackward = 0; } }

        public Animation(ContentManager content, string animationName, uint frameCount, float fps)
        {
            m_fps = fps;
            m_frames = new Texture2D[frameCount];
            for (int i = 0; i < frameCount; ++i)
                m_frames[i] = content.Load<Texture2D>("Textures/" + animationName + (i+1));
        }

        public void Update(GameTime gTime)
        {
            if (StopPlaying)
                return;

            m_frameShownFor += (float)gTime.ElapsedGameTime.TotalSeconds;
            if(m_frameShownFor >= m_frequence)
            {
                frame = (frame + m_frames.Length + m_forwardBackward) % m_frames.Length;
                m_frameShownFor = 0;
            }
        }

        public void Draw(SpriteBatch batch, Rectangle destination, Color color)
        {
            batch.Draw(m_frames[frame], destination, color);
        }
    }
}
