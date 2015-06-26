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
    public class InputManager
    {
        KeyboardState currentKeyState, previousKeyState;

        private static InputManager instance;

        public static InputManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new InputManager();
                return instance;
            }
        }

        public void Update()
        {
            previousKeyState = currentKeyState;
            if(!ScreenManager.Instance.IsTransitioning)
                currentKeyState = Keyboard.GetState();
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach(Keys key in keys)
            {
                if (currentKeyState.IsKeyDown(key) && previousKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        public Keys GetKeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyDown(key) && previousKeyState.IsKeyUp(key))
                    return key;
            }
            return Keys.None;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyUp(key) && previousKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        



    }
}
