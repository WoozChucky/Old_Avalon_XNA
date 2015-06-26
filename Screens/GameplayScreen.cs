using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Avalon
{
    public class GameplayScreen : GameScreen
    {
        Player player;
        Map map;
        SpriteFont font;
        private bool showPlayerInfo;
        public override void LoadContent()
        {
            base.LoadContent();

            XmlManager<Player> playerLoader = new XmlManager<Player>();
            XmlManager<Map> mapLoader = new XmlManager<Map>();

            map = mapLoader.Load("Load/Gameplay/Maps/Map1.xml");
            player = playerLoader.Load("Load/Gameplay/Player.xml");

            //Console.Write(map.NPC[0].NPCType);

            player.LoadContent();
            map.LoadContent();

            font = content.Load<SpriteFont>("Fonts/MorrisRoman");

            showPlayerInfo = false;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
            map.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            player.Update(gameTime);
            map.Update(gameTime, ref player);

            if(InputManager.Instance.KeyPressed(Keys.Z))
            {
                if (showPlayerInfo)
                    showPlayerInfo = false;
                else
                    showPlayerInfo = true;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            map.Draw(spriteBatch, "Underlay");
            player.Draw(spriteBatch);
            map.Draw(spriteBatch, "Overlay");

            if (showPlayerInfo)
            {
                spriteBatch.DrawString(font, "HP: " + player.HP, new Vector2(Global.WIDHT - (Global.WIDHT * 0.3f), 25), Color.Blue, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 1);

                if(player.ArrowCount < 6)
                    spriteBatch.DrawString(font, "Arrows: " + player.ArrowCount, new Vector2(Global.WIDHT - (Global.WIDHT * 0.3f), 40), Color.Red, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 1);
                else
                    spriteBatch.DrawString(font, "Arrows: " + player.ArrowCount, new Vector2(Global.WIDHT - (Global.WIDHT * 0.3f), 40), Color.Blue, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 1);
                spriteBatch.DrawString(font, "Arrows in map: " + player.arrows.Count, new Vector2(Global.WIDHT - (Global.WIDHT * 0.3f), 55), Color.Blue, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 1);

            }
        }
    }
}
