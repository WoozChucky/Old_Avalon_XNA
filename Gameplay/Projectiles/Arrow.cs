using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Avalon
{
    public class Arrow
    {
        public Image Image;
        public float MoveSpeed;
        public Keys DirectionKey;

        public Arrow(Keys lastKey, object obj)
        {
            Image = new Image();
            switch (lastKey)
            {
                case Keys.W:
                    Image.Path = "Gameplay/Projectiles/arrow_up";
                    break;
                case Keys.S:
                    Image.Path = "Gameplay/Projectiles/arrow_down";
                    break;
                case Keys.A:
                    Image.Path = "Gameplay/Projectiles/arrow_left";
                    break;
                case Keys.D:
                    Image.Path = "Gameplay/Projectiles/arrow_right";
                    break;
            }
            
            DirectionKey = lastKey;

            if (obj.GetType() == typeof(Player))
            {
                Player p = (Player)obj;
                Image.Position = p.Image.Position;
            }
            MoveSpeed = 250f;
            Image.Scale = new Vector2(1, 2);
            Image.LoadContent();
        }

        public void LoadContent()
        {
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            Image.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
