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
    public class Player
    {
        public Image Image;
        public Vector2 Velocity;
        public float MoveSpeed;
        public int HP;

        [XmlIgnore]
        public List<Arrow> arrows;
        public int ArrowCount;

        public Player()
        {
            Velocity = Vector2.Zero;
            arrows = new List<Arrow>();
            ArrowCount = 20;
        }

        public void LoadContent()
        {
            Image.LoadContent();
            foreach (Arrow arrow in arrows)
                arrow.LoadContent();
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
            foreach (Arrow arrow in arrows)
                arrow.UnloadContent();
        }

        private void Movement(GameTime gameTime)
        {
            Image.IsActive = true;
            if (Velocity.X == 0)
            {
                if (InputManager.Instance.KeyDown(Keys.Down))
                {
                    Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.SpriteSheetEffect.CurrentFrame.Y = 0;
                }
                else if (InputManager.Instance.KeyDown(Keys.Up))
                {
                    Velocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.SpriteSheetEffect.CurrentFrame.Y = 3;
                }
                else
                    Velocity.Y = 0;
            }

            if (Velocity.Y == 0)
            {
                if (InputManager.Instance.KeyDown(Keys.Right))
                {
                    Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.SpriteSheetEffect.CurrentFrame.Y = 2;
                }
                else if (InputManager.Instance.KeyDown(Keys.Left))
                {
                    Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.SpriteSheetEffect.CurrentFrame.Y = 1;
                }
                else
                    Velocity.X = 0;
            }

            if (Velocity.X == 0 && Velocity.Y == 0)
                Image.IsActive = false;
        }

        public void Update(GameTime gameTime)
        {

            Movement(gameTime);

            Image.Update(gameTime);
            Image.Position += Velocity;

            Keys[] keyss = new Keys[4];
            keyss[0] = Keys.W;
            keyss[1] = Keys.S;
            keyss[2] = Keys.A;
            keyss[3] = Keys.D;

            if (InputManager.Instance.KeyPressed(keyss))
                CreateArrow(keyss);
            ShootArrow(gameTime);

            foreach (Arrow arrow in arrows)
                arrow.Update(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
            foreach (Arrow arrow in arrows)
                arrow.Draw(spriteBatch);
        }

        private void CreateArrow(Keys[] keyss)
        {
            if(ArrowCount > 0)
            {
                Arrow arrow = new Arrow(InputManager.Instance.GetKeyPressed(keyss), this);
                arrows.Add(arrow);
                ArrowCount--;
            }
        }

        private void ShootArrow(GameTime gameTime)
        {
            foreach (Arrow a in arrows)
            {
                switch(a.DirectionKey)
                {
                    case Keys.W:
                        a.Image.Position.Y -= a.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                    case Keys.S:
                        a.Image.Position.Y += a.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                    case Keys.A:
                        a.Image.Position.X -= a.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                    case Keys.D:
                        a.Image.Position.X += a.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                }
            }

            for(int i = 0; i < arrows.Count; i++)
            {
                if (arrows[i].Image.Position.X <= 0 || arrows[i].Image.Position.X >= Global.WIDHT || arrows[i].Image.Position.Y <= 0 || arrows[i].Image.Position.Y >= Global.HEIGHT)
                    arrows.RemoveAt(i);
            }
        }
    }
}
