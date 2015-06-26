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
    public class Map
    {
        [XmlElement("Layer")]
        public List<Layer> Layer;
        public Vector2 TileDimensions;
        [XmlElement("NPC")]
        public List<NPC> NPC;

        public Map()
        {
            Layer = new List<Layer>();
            TileDimensions = Vector2.Zero;
            NPC = new List<NPC>();
        }

        public void LoadContent()
        {
            foreach(Layer l in Layer)
            {
                l.LoadContent(TileDimensions);
            }

            foreach (NPC npc in NPC)
            {
                npc.LoadContent();
                Console.WriteLine(npc.NPCType.ToString());
            }
        }

        public void UnloadContent()
        {
            foreach (Layer l in Layer)
            {
                l.UnloadContent();
            }

            foreach (NPC npc in NPC)
            {
                npc.UnloadContent();
            }
        }

        public void Update(GameTime gameTime, ref Player player)
        {
            foreach (Layer l in Layer)
            {
                l.Update(gameTime, ref player, ref NPC);
            }

            foreach (NPC npc in NPC)
            {
                npc.Update(gameTime, ref player);
            }
        }

        public void Draw(SpriteBatch spriteBatch, string drawType)
        {
            foreach (NPC npc in NPC)
                npc.Draw(spriteBatch);

            foreach (Layer l in Layer)
            {
                l.Draw(spriteBatch, drawType);
            }

            
        }

    }
}
