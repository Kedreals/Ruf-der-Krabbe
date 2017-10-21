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
    public class Map
    {
        public Tile[] Tiles;
        public int XDim;
        public int YDim;

        public void Load(ContentManager contentManager, string filename)
        {
            Texture2D bitmap = contentManager.Load<Texture2D>("Maps/" + filename);
            Color[] color = new Color[bitmap.Height * bitmap.Width];
            bitmap.GetData(color);

            XDim = bitmap.Width;
            YDim = bitmap.Height;

            Texture2D texSurface = contentManager.Load<Texture2D>("Textures/SurfaceTileTexture");
            Texture2D texInner = contentManager.Load<Texture2D>("Textures/InnerTileTexture");
            //Texture2D texShell = contentManager.Load<Texture2D>("Texture/ShellTiletexture");

            Tiles = new Tile[bitmap.Height * bitmap.Width];

            for (int i = 0; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = color[i*bitmap.Height + j];

                    if (c.ToVector3().Length() == 0)
                    {
                        TileType t = TileType.InnerTile;

                        if (i != 0 && color[(i - 1)*bitmap.Height + j].ToVector3().Length() != 0)
                        {
                            t = TileType.SurfaceTile;
                        }

                        Tiles[i * bitmap.Height + j] = new Tile(t, new Vector2(j * Tile.DefaultSize.X, i*Tile.DefaultSize.Y));
                        switch (t)
                        {
                            case TileType.InnerTile:
                                Tiles[i * bitmap.Height + j].SetTexture(texInner);
                                break;
                            case TileType.SurfaceTile:
                                Tiles[i * bitmap.Height + j].SetTexture(texSurface);
                                break;
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (Tile tile in Tiles)
            {
                if (tile != null)
                {
                    tile.Draw(batch);
                }
            }
        }
    }
}
