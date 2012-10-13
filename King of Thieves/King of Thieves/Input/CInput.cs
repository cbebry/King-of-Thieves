using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Gears.Cloud;
using Gears.Cloud.Input;

namespace King_of_Thieves.Input
{
    class CInput : InputHandler
    {
        private GamePadState _padStateCurrent;
        private KeyboardState _keyStateCurrent;
        private MouseState _mouseStateCurrent;

        private GamePadState _padStatePrevious;
        private KeyboardState _keyStatePrevious;
        private MouseState _mouseStatePrevious;
        public keyEventArgs keyEvents = new keyEventArgs();
        private List<Keys> _temp = new List<Keys>();
        
        public bool getInputDown(Buttons button)
        {
            if (!_padStateCurrent.IsConnected)
                return false;

            return _padStateCurrent.IsButtonDown(button);
        }

        public bool getInputDown(Keys key)
        {
            if (_padStateCurrent.IsConnected)
                return false;

            return _keyStateCurrent.IsKeyDown(key);
        }

        public bool getInputRelease(Buttons button)
        {
            if (!_padStateCurrent.IsConnected)
                return false;

            return (_padStatePrevious.IsButtonDown(button) && _padStateCurrent.IsButtonUp(button));
        }

        public bool getInputRelease(Keys key)
        {
            if (_padStateCurrent.IsConnected)
                return false;

            return (_keyStatePrevious.IsKeyDown(key) && _keyStateCurrent.IsKeyUp(key));

            //MouseState x; x.
        }

        public bool getInputRelease(){return true;}
        public bool getInputDown() { return true; } //not quite sure how to handle the mouse buttons yet.  Doesn't seem as simple as the other types.

        public int mouseX
        {
            get
            {
                return _mouseStateCurrent.X;
            }
        }

        public int mouseY
        {
            get
            {
                return _mouseStateCurrent.Y;
            }
        }

        public int scrollWheel
        {
            get
            {
                return _mouseStateCurrent.ScrollWheelValue;
            }
        }

        public bool areKeysPressed
        {
            get
            {
                return (_keyStateCurrent.GetPressedKeys().Count() > 0);
            }
        }

        public Keys[] keysPressed
        {
            get
            {
                return _keyStateCurrent.GetPressedKeys();
            }
        }

        public Keys[] keysReleased
        {
            get
            {
                return keyEvents.releasedKeys;
            }
        }

        public Keys[] keysOld
        {
            get
            {
                return keyEvents.oldKeys;
            }
        }

        public bool areKeysReleased
        {
            get
            {
                return (keyEvents.releasedKeys.Count() > 0);
            }
        }


        public override void Update(GameTime gameTime)
        {
            _temp.Clear();

            if (keyEvents.oldKeys != null)
                foreach (Keys key in keyEvents.oldKeys)
                    if (!keyEvents.keys.Contains(key))
                        _temp.Add(key);

            keyEvents.releasedKeys = _temp.ToArray();

            _padStatePrevious = _padStateCurrent;
            _keyStatePrevious = _keyStateCurrent;
            _mouseStatePrevious = _mouseStateCurrent;


            _padStateCurrent = GamePad.GetState(PlayerIndex.One);
            _keyStateCurrent = Keyboard.GetState();
            _mouseStateCurrent = Mouse.GetState();

            //if (areKeysPressed)
            keyEvents.oldKeys = keyEvents.keys;
            keyEvents.keys = keysPressed;

            //base.Update(gameTime);
        }
    }

    class keyEventArgs : EventArgs
    {
        public Keys[] keys;
        public Keys[] tappedKeys;
        public Keys[] oldKeys;
        public Keys[] releasedKeys;
    }



}
