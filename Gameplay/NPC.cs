using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Xml.Serialization;

namespace Avalon
{

    public class NPC
    {
        public Image Image;
        public Vector2 Velocity;
        public float MoveSpeed;
        public String NPCType;
        public Rectangle FOV;
        public int Aggro;

        private Vector2 directionToPlayer;

        float timer = 5f;
        const float TIMER = 5f;

        public virtual void LoadContent()
        {
            Image.LoadContent();
            FOV = new Rectangle((int)Image.Position.X - (Aggro / 2), (int)Image.Position.Y - (Aggro / 2), Image.SourceRect.Width + (Aggro / 2), Image.SourceRect.Height + (Aggro / 2));
        }

        public virtual void UnloadContent()
        {
            Image.UnloadContent();
        }

        private void SeekPlayer(Player player)
        {
            Image.IsActive = true;

            directionToPlayer = player.Image.Position - Image.Position;
            directionToPlayer.Normalize();

            if (directionToPlayer.X > 0)
            {
                if (directionToPlayer.X > directionToPlayer.Y)
                {
                    directionToPlayer.Y = 0;
                }
                else
                {
                    directionToPlayer.X = 0;
                }
            }
            else if (directionToPlayer.X < 0)
            {
                if (directionToPlayer.X < directionToPlayer.Y)
                {
                    directionToPlayer.Y = 0;
                }
                else
                {
                    directionToPlayer.X = 0;
                }
            }
            else if (directionToPlayer.Y > 0)
            {
                if (directionToPlayer.Y > directionToPlayer.X)
                {
                    directionToPlayer.Y = 0;
                }
                else
                {
                    directionToPlayer.X = 0;
                }
            }
            else if (directionToPlayer.Y < 0)
            {
                if (directionToPlayer.Y < directionToPlayer.X)
                {
                    directionToPlayer.Y = 0;
                }
                else
                {
                    directionToPlayer.X = 0;
                }
            }

            Velocity = directionToPlayer;


            Image.Position += Velocity;

            UpdateFOV();
        }

        private bool PlayerInFOV(Player player)
        {
            Rectangle playerRect = new Rectangle((int)player.Image.Position.X, (int)player.Image.Position.Y, player.Image.SourceRect.Width, player.Image.SourceRect.Height);
            return (FOV.Intersects(playerRect));
        }

        private void Wander(GameTime gameTime)
        {
            Random random = new Random();
            

     
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0)
            {
                int action = random.Next(0, 3);
                Console.WriteLine("Random = " + action.ToString());

                Image.IsActive = true;
                
                switch (action)
                {
                    case 0: //DOWN
                       // if (Velocity.X == 0)
                        //{
                            Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds / 10;
                            Velocity.X = 0;
                            Image.SpriteSheetEffect.CurrentFrame.Y = 0;
                            
                            Console.WriteLine("Moving Down");
                        //}
                        break;
                    case 1: //UP
                       // if (Velocity.X == 0)
                        //{
                            Velocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds / 10;
                            Velocity.X = 0;
                            Image.SpriteSheetEffect.CurrentFrame.Y = 3;
                            Console.WriteLine("Moving Up");
                       // }
                        break;
                    case 2: //RIGHT
                      //  if (Velocity.Y == 0)
                       // {
                            Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds / 10;
                        Velocity.Y = 0;
                            Image.SpriteSheetEffect.CurrentFrame.Y = 2;
                            Console.WriteLine("Moving Right");
                       // }
                        break;
                    case 3: //LEFT
                       // if (Velocity.Y == 0)
                       // {
                            Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds / 10;
                        Velocity.Y = 0;
                        Image.SpriteSheetEffect.CurrentFrame.Y = 1;
                            Console.WriteLine("Moving Left");
                       // }
                        break;
                }
                
                timer = TIMER;   //Reset Timer
            }

            Image.Position += Velocity;

            UpdateFOV();

        }

        private void UpdateFOV()
        {
            FOV = new Rectangle((int)Image.Position.X - (Aggro/2), (int)Image.Position.Y - (Aggro/2), Image.SourceRect.Width + Aggro, Image.SourceRect.Height + Aggro);
        }

        public virtual void Update(GameTime gameTime, ref Player player)
        {
            switch (NPCType)
            {
                case "Aggressive":

                    //if (!PlayerInFOV(player))
                    //    Wander(gameTime);
                    //else
                        SeekPlayer(player);
                    break;

                case "Passive":

                    break;
                default:
                    break;
            }

            Image.Update(gameTime);

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Global.DrawBorder(spriteBatch, FOV, 5, Color.Red);
            Image.Draw(spriteBatch);
        }

    }
}
