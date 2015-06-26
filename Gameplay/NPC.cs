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

        float timer = 1.5f;
        const float TIMER = 1.5f;

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
            Vector2 directionToPlayer = player.Image.Position - Image.Position;
            directionToPlayer.Normalize();

            Velocity = directionToPlayer / 2;

            Image.Position += Velocity;

            UpdateFOV();

            //if (Velocity.X == 0)
            //{
            //    if (InputManager.Instance.KeyDown(Keys.Down))
            //    {
            //        Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //        Image.SpriteSheetEffect.CurrentFrame.Y = 0;
            //    }
            //    else if (InputManager.Instance.KeyDown(Keys.Up))
            //    {
            //        Velocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //        Image.SpriteSheetEffect.CurrentFrame.Y = 3;
            //    }
            //    else
            //        Velocity.Y = 0;
            //}

            //if (Velocity.Y == 0)
            //{
            //    if (InputManager.Instance.KeyDown(Keys.Right))
            //    {
            //        Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //        Image.SpriteSheetEffect.CurrentFrame.Y = 2;
            //    }
            //    else if (InputManager.Instance.KeyDown(Keys.Left))
            //    {
            //        Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //        Image.SpriteSheetEffect.CurrentFrame.Y = 1;
            //    }
            //    else
            //        Velocity.X = 0;
            //}

            //if (Velocity.X == 0 && Velocity.Y == 0)
            //    Image.IsActive = false;
        }

        private bool PlayerInFOV(Player player)
        {
            Rectangle playerRect = new Rectangle((int)player.Image.Position.X, (int)player.Image.Position.Y, player.Image.SourceRect.Width, player.Image.SourceRect.Height);
            return (FOV.Intersects(playerRect));
        }

        private void Wander(GameTime gameTime)
        {
            Random random = new Random();
            int action = random.Next(0, 3);

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0)
            {
                Image.IsActive = true;
                
                switch (action)
                {
                    case 0: //DOWN
                       // if (Velocity.X == 0)
                        //{
                            Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds / 10;
                            Image.SpriteSheetEffect.CurrentFrame.Y = 0;
                            //Console.WriteLine("Moving Down");
                        //}
                        break;
                    case 1: //UP
                       // if (Velocity.X == 0)
                        //{
                            Velocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds / 10;
                            Image.SpriteSheetEffect.CurrentFrame.Y = 3;
                            //Console.WriteLine("Moving Up");
                       // }
                        break;
                    case 2: //RIGHT
                      //  if (Velocity.Y == 0)
                       // {
                            Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds / 10;
                            Image.SpriteSheetEffect.CurrentFrame.Y = 2;
                            //Console.WriteLine("Moving Right");
                       // }
                        break;
                    case 3: //LEFT
                       // if (Velocity.Y == 0)
                       // {
                            Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds / 10;
                            Image.SpriteSheetEffect.CurrentFrame.Y = 1;
                            //Console.WriteLine("Moving Left");
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
            Wander(gameTime);
            switch (NPCType)
            {
                case "Aggressive":

                    if (!PlayerInFOV(player))
                    {
                        Wander(gameTime);
                    }
                    else
                    {
                        SeekPlayer(player);
                    }
                        
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
