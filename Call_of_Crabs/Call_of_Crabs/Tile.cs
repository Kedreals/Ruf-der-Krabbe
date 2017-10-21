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
    public enum TileType
    {
        InnerTile,
        SurfaceTile
    }

    public class Tile
    {
        public static Vector2 DefaultSize = new Vector2(20, 20);

        private Texture2D texture;

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                this.TileRectangle.Location = position.ToPoint();
            } }

        private Vector2 size;
        public Vector2 Size { get { return size; } set { size = value;
            TileRectangle.Size = size.ToPoint();
        } }
        public Rectangle TileRectangle;
        public TileType Type;
        public bool IsHostile;

        public float damage;
        public float Damage
        {
            get
            {
                if (IsHostile)
                {
                    return damage;
                }
                else
                {
                    return 0;
                }
            }
            private set { damage = value; }
        }

        public Tile(TileType type, Vector2 position, Vector2 size = default(Vector2), bool isHostile = false, float damage = 0)
        {
            Type = type;
            this.position = position;
            if (size.Length() > 0)
            {
                this.size = size;
            }
            else
            {
                this.size = DefaultSize;
            }
            IsHostile = isHostile;
            Damage = damage;

            TileRectangle = new Rectangle(position.ToPoint(), Size.ToPoint());
        }

        public void Load(ContentManager contentManager, string filename)
        {
            texture = contentManager.Load<Texture2D>("Textures/" + filename);
        }

        public void SetTexture(Texture2D tex)
        {
            texture = tex;
        }

        public void Move(Vector2 vec)
        {
            Position += vec;
            TileRectangle.Location = Position.ToPoint();
        }

        public void Scale(Vector2 vec)
        {
            Size *= vec;
            TileRectangle.Size = Size.ToPoint();
        }

        public void MakeHostile(float damage)
        {
            IsHostile = true;
            Damage = damage;
        }

        public void MakeFriendly()
        {
            IsHostile = false;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, TileRectangle, Color.White);
        }
    }
}
