using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Avalon
{
    public class Layer
    {

        public class TileMap
        {
            [XmlElement("Row")]
            public List<String> Row;

            public TileMap()
            {
                Row = new List<string>();
            }
        }

        [XmlElement("TileMap")]
        public TileMap Tile;
        public Image Image;
        public String SolidTiles, OverlayTiles;

        string state;
        List<Tile> underlayTiles, overlayTiles;

        public Layer()
        {
            Image = new Image();
            underlayTiles = new List<Tile>();
            overlayTiles = new List<Tile>();
            SolidTiles = String.Empty;
            OverlayTiles = String.Empty;
        }

        public void LoadContent(Vector2 tileDimensions)
        {
            Image.LoadContent();
            Vector2 position = -tileDimensions;

            foreach(string row in Tile.Row)
            {
                string[] split = row.Split(']');
                position.X = -tileDimensions.X;
                position.Y += tileDimensions.Y;

                foreach (string s in split)
                {
                    if(s != String.Empty)
                    {
                        position.X += tileDimensions.X;

                        if(!s.Contains("x"))
                        {
                            state = "Passive";
                            Tile tile = new Tile();
                            

                            string str = s.Replace("[", "");
                            int value1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                            int value2 = int.Parse(str.Substring(str.IndexOf(':') + 1));

                            if(SolidTiles.Contains("[" + value1.ToString() + ":" + value2.ToString() + "]"))
                                state = "Solid";

                            tile.LoadContent(position,
                                new Rectangle(value1 * (int)tileDimensions.X, value2 * (int)tileDimensions.Y, 
                                (int)tileDimensions.X, (int)tileDimensions.Y), state);

                            if (OverlayTiles.Contains("[" + value1.ToString() + ":" + value2.ToString() + "]"))
                                overlayTiles.Add(tile);
                            else
                                underlayTiles.Add(tile);
                        }
                    }
                }
            }
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime, ref Player player, ref List<NPC> npc)
        {
            foreach (Tile tile in underlayTiles)
                tile.Update(gameTime, ref player, ref npc);

            foreach (Tile tile in overlayTiles)
                tile.Update(gameTime, ref player, ref npc);
        }

        public void Draw(SpriteBatch spriteBatch, string drawType)
        {
            List<Tile> tiles;

            if (drawType == "Underlay")
                tiles = underlayTiles;
            else
                tiles = overlayTiles;

            foreach (Tile tile in tiles)
            {
                Image.Position = tile.Position;
                Image.SourceRect = tile.SourceRect;
                Image.Draw(spriteBatch);
            }
        }
    }
}
