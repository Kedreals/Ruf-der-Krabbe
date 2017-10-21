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

            Random r = new Random(786545);

            Texture2D texSurface = contentManager.Load<Texture2D>("Textures/SurfaceTileTexture");
            Texture2D texInner = contentManager.Load<Texture2D>("Textures/InnerTileTexture");
            Texture2D texShell = contentManager.Load<Texture2D>("Textures/ShellTileTexture");
            Texture2D texStone = contentManager.Load<Texture2D>("Textures/StoneTexture");
            Texture2D texGrass = contentManager.Load<Texture2D>("Textures/SeagrassTexture");
            Texture2D texCoral = contentManager.Load<Texture2D>("Textures/CoralTexture");

            Tiles = new Tile[bitmap.Height * bitmap.Width];

            for (int i = 0; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = color[i*bitmap.Width + j];

                    if (c.ToVector3().Length() == 0)
                    {
                        TileType t = TileType.SurfaceTile;

                        if (i == 0 || color[(i - 1)*bitmap.Width + j] != Color.White)
                        {
                            t = TileType.InnerTile;
                        }

                        Tiles[i * bitmap.Width + j] = new Tile(t, new Vector2(j * Tile.DefaultSize.X, i*Tile.DefaultSize.Y));
                        switch (t)
                        {
                            case TileType.InnerTile:
                                Tiles[i * bitmap.Width + j].SetTexture(texInner);
                                break;
                            case TileType.SurfaceTile:
                                Tiles[i * bitmap.Width + j].SetTexture(texSurface);
                                if (r.Next(10) <= 2)
                                {
                                    Tiles[(i - 1) * bitmap.Width + j] = new Tile(TileType.DecorationTile, new Vector2(j * Tile.DefaultSize.X, (i - 1) * Tile.DefaultSize.Y));
                                    switch (r.Next(3))
                                    {
                                        case 0:
                                            Tiles[(i - 1) * bitmap.Width + j].SetTexture(texCoral);
                                            break;
                                        case 1:
                                            Tiles[(i - 1) * bitmap.Width + j].SetTexture(texGrass);
                                            break;
                                        case 2:
                                            Tiles[(i - 1) * bitmap.Width + j].SetTexture(texStone);
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                    else if (c == Color.Red)
                    {
                        Tiles[i * bitmap.Width + j] = new Tile(TileType.ShellTile, new Vector2(j * Tile.DefaultSize.X, i * Tile.DefaultSize.Y), isHostile: true, damage: 10);
                        Tiles[i * bitmap.Width + j].SetTexture(texShell);
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
