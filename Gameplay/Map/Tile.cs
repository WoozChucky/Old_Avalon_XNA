using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Avalon
{
    public class Tile
    {

        Vector2 position;
        Rectangle sourceRect;
        string state;

        public Rectangle SourceRect
        {
            get { return sourceRect;  }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public void LoadContent(Vector2 position, Rectangle sourcRect, string state)
        {
            this.position = position;
            this.sourceRect = sourcRect;
            this.state = state;
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime, ref Player player, ref List<NPC> npcList)
        {
            Rectangle tileRect = new Rectangle((int)position.X, (int)position.Y, sourceRect.Width, sourceRect.Height);
            Rectangle playerRect = new Rectangle((int)player.Image.Position.X, (int)player.Image.Position.Y, player.Image.SourceRect.Width, player.Image.SourceRect.Height);
            Rectangle npcRect;

            if (state == "Solid")
            {
                if(playerRect.Intersects(tileRect))
                {
                    if (player.Velocity.X < 0)
                        player.Image.Position.X = tileRect.Right;
                    else if (player.Velocity.X > 0)
                        player.Image.Position.X = tileRect.Left - player.Image.SourceRect.Width;
                    else if (player.Velocity.Y < 0)
                        player.Image.Position.Y = tileRect.Bottom;
                    else
                        player.Image.Position.Y = tileRect.Top - player.Image.SourceRect.Height;

                    player.Velocity = Vector2.Zero;
                }

                foreach (NPC npc in npcList)
                {
                    npcRect = new Rectangle((int)npc.Image.Position.X, (int)npc.Image.Position.Y, npc.Image.SourceRect.Width, npc.Image.SourceRect.Height);

                    if (npcRect.Intersects(tileRect))
                    {
                        if (npc.Velocity.X < 0)
                            npc.Image.Position.X = tileRect.Right;
                        else if (npc.Velocity.X > 0)
                            npc.Image.Position.X = tileRect.Left - npc.Image.SourceRect.Width;
                        else if (npc.Velocity.Y < 0)
                            npc.Image.Position.Y = tileRect.Bottom;
                        else if (npc.Velocity.Y > 0)
                            npc.Image.Position.Y = tileRect.Top - npc.Image.SourceRect.Height;

                        npc.Velocity = Vector2.Zero;
                    }
                }

                
                return;
            }
        }
    }
}
