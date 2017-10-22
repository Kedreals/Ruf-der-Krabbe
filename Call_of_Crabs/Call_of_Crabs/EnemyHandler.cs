using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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

        private Player m_player;

        
        public EnemyHandler(ContentManager content, Level level, Player player)
        {
            m_player = player;
            switch(level)
            {
                case Level.FirstLevel:
                    LoadFirstLevel(content);
                    break;

                default:
                    return;
            }
            for(int i = 0; i < m_Characters.Count; ++i)
            {
                m_Characters[i].Position = m_Paths[i][0];
            }
        }

        private Path LoadPath(string values)
        {
            values = values.Replace(" ", "");
            string[] v = values.Split(',');

            if(v.Length%2 != 0)
            {
                Console.WriteLine("ERRORRRRRRRRRRRRR!!!!!!");
            }

            Vector2[] va = new Vector2[v.Length/2];

            for (int i = 0; i < va.Length; ++i)
            {
                va[i] = new Vector2(float.Parse(v[2 * i]), float.Parse(v[2 * i + 1]))*Tile.DefaultSize;
            }

            return new Path(va);
        }

        private void LoadFirstLevel(ContentManager content)
        {
            StreamReader streamReader = new StreamReader("EnemyPositionsForLevels/Level1.txt");

            while(!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                if (line.StartsWith("//"))
                    continue;

                if(line.StartsWith("Kritzler"))
                {
                    Character c = new KritzlerEnemy();
                    c.Load(content, "");
                    m_Characters.Add(c);
                    m_t.Add(0);
                    m_forwardBackward.Add(1);
                    m_Paths.Add(LoadPath(line.Split(':')[1]));
                }
            }
        }

        public void Update(GameTime time)
        {
            for (int i = 0; i < m_Characters.Count; ++i)
            {
                if (m_Characters[i].dead)
                    continue;

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

                Vector2 target = m_Paths[i][m_t[i]];

                if((m_player.Position-m_Characters[i].Position).LengthSquared() <= m_Characters[i].ReaktionRadius*m_Characters[i].ReaktionRadius)
                {
                    target = m_player.Position;
                    m_Characters[i].ReactToPlayer(time, target);
                }
                else if (m_Characters[i].Move(target, time))
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
