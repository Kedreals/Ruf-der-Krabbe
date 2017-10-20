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
        public Vector2 Size;
        public Rectangle TileRectangle;
        public TileType Type;
        public bool IsHostile;

        public Tile(TileType type, Vector2 position, Vector2 size = default(Vector2), bool isHostile = false)
        {
            Type = type;
            Position = position;
            if (size.Length() > 0)
            {
                Size = size;
            }
            else
            {
                Size = new Vector2(20, 20);
            }
            IsHostile = isHostile;

            TileRectangle = new Rectangle(position.ToPoint(), Size.ToPoint());
        }

        public void Load(ContentManager contentManager, string filename)
        {
            texture = contentManager.Load<Texture2D>("Textures/" + filename);
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
    }
}
