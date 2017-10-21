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

    class Path
    {
        Vector2[] controllPoints;

        float[] weights;

        Dictionary<KeyValuePair<int, int>, int> pascal = new Dictionary<KeyValuePair<int, int>, int>();

        private int CalculatePascal(int row, int col)
        {
            if (row < 2)
            {
                pascal.Add(new KeyValuePair<int, int>(row, col), 1);
                return 1;
            }
            if (col%row == 0)
            {
                pascal.Add(new KeyValuePair<int, int>(row, col), 1);
                return 1;
            }
            return GetPascal(row - 1, col) + GetPascal(row - 1, col - 1);
            
        }

        private int GetPascal(int row, int col)
        {
            if (col > row)
                return 0;
            if(pascal.ContainsKey(new KeyValuePair<int, int>(row, col)))
            {
                return pascal[new KeyValuePair<int, int>(row, col)];
            }

            return CalculatePascal(row, col);
        }

        public Path(Vector2[] points)
        {
            controllPoints = new Vector2[points.Length];
            weights = new float[controllPoints.Length];
            for(int i = 0; i < points.Length; ++i)
            {
                controllPoints[i] = new Vector2(points[i].X, points[i].Y);
                weights[i] = GetPascal(points.Length - 1, i);
            }
        }

        
        
        public Path(Vector2[] points, float[] w) : this(points)
        {
            
            for (int i = 0; i < points.Length; ++i)
            {
                weights[i] *= w[i];
            }
        }

        public Vector2 Get(float t)
        {
            return this[t];
        }

        public Vector2 this[float t]
        {
            get
            {
                Vector2 res = new Vector2();

                float a = 1 - t;

                for (int i = 0; i < controllPoints.Length; ++i)
                {
                    res += (float)(Math.Pow(a, controllPoints.Length - 1 - i) * Math.Pow(t, i)) * weights[i] * controllPoints[i];
                }

                return res;
            }
        }
    }

    class EnemyHandler
    {
        private List<Character> m_Characters = new List<Character>();
        private List<Path> m_Paths = new List<Path>();
        private List<float> m_t = new List<float>();
        private List<int> m_forwardBackward = new List<int>();

        public List<Character> List { get { return new List<Character>(m_Characters); } }

        
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
            m_forwardBackward.Add(1);
            m_t.Add(0);
            m_Characters.Add(new KritzlerEnemy());
            m_Characters[0].Load(content, "");
            m_Characters[0].Position = new Vector2(700, 450);
            m_Paths.Add(new Path(new Vector2[] { new Vector2(700, 450), new Vector2(900, 450), new Vector2(900,250) }, new float[] { 1, 1.0f, 1.0f }));
        }

        public void Update(GameTime time)
        {
            for (int i = 0; i < m_Characters.Count; ++i)
            {


                if (m_t[i] > 1)
                {
                    m_t[i] = 1;
                    m_forwardBackward[i] = -1;
                }
                if (m_t[i] < 0)
                {
                    m_t[i] = 0;
                    m_forwardBackward[i] = 1;
                }

                if (m_Characters[i].Move(m_Paths[i][m_t[i]], time))
                    m_t[i] += m_forwardBackward[i] * 0.1f;

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
