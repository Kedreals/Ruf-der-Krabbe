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
    enum Level
    {
        FirstLevel
    }

    class EnemyHandler
    {
        private Character[] m_Characters;
        private Vector2[,] m_Paths;
        private float[] m_t;
        private int[] m_forwardBackward;

        public List<Character> List { get { return m_Characters.ToList(); } }

        
        public EnemyHandler(ContentManager content, Level level)
        {
            switch(level)
            {
                case Level.FirstLevel:
                    LoadFirstLevel(content);
                    break;

                default:
                    return;
            }
        }

        private void LoadFirstLevel(ContentManager content)
        {

            m_t = new float[1];
            m_forwardBackward = new int[1];
            m_forwardBackward[0] = 1;
            m_Characters = new Character[1];
            m_Characters[0] = new KritzlerEnemy();
            m_Characters[0].Load(content, "");
            m_Paths = new Vector2[m_Characters.Length, 2];
            m_Paths[0, 0] = new Vector2(100, 100);
            m_Paths[0, 1] = new Vector2(200, 100);
        }

        public void Update(GameTime time)
        {
            for (int i = 0; i < m_Characters.Length; ++i)
            {
                m_t[i] += m_forwardBackward[i] * (float)time.ElapsedGameTime.TotalSeconds * (m_Paths[i, 0] - m_Paths[i, 1]).Length() / 100.0f;
                if (m_t[i] > 1)
                {
                    m_t[i] = 1;
                    m_forwardBackward[i] = -1;
                }
                if(m_t[i] < 0)
                {
                    m_t[i] = 0;
                    m_forwardBackward[i] = 1;
                }
                Vector2 d = ((1 - m_t[i]) * m_Paths[i, 0] + m_t[i] * m_Paths[i, 1])-m_Characters[i].Position;
                d = new Vector2(d.X, 0);
                d.Normalize();
                m_Characters[i].Position += d;

                m_Characters[i].Update(time);
            }
        }

        public void Collide(Map map)
        {
            foreach (Character c in m_Characters)
                c.Collide(map);
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (Character c in m_Characters)
                c.Draw(batch);
        }
    }
}
