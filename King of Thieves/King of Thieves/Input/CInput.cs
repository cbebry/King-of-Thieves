﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Input
{
    static class CInput
    {
        //git sanity test, don't mind me. -Steve
        private static GamePadState _padStateCurrent;
        private static KeyboardState _keyStateCurrent;
        private static MouseState _mouseStateCurrent;

        private static GamePadState _padStatePrevious;
        private static KeyboardState _keyStatePrevious;
        private static MouseState _mouseStatePrevious;
        public static keyEventArgs keyEvents = new keyEventArgs();
        private static List<Keys> _temp = new List<Keys>();
        
        public static bool getInputDown(Buttons button)
        {
            if (!_padStateCurrent.IsConnected)
                return false;

            return _padStateCurrent.IsButtonDown(button);
        }

        public static bool getInputDown(Keys key)
        {
            if (_padStateCurrent.IsConnected)
                return false;

            return _keyStateCurrent.IsKeyDown(key);
        }

        public static bool getInputRelease(Buttons button)
        {
            if (!_padStateCurrent.IsConnected)
                return false;

            return (_padStatePrevious.IsButtonDown(button) && _padStateCurrent.IsButtonUp(button));
        }

        public static bool getInputRelease(Keys key)
        {
            if (_padStateCurrent.IsConnected)
                return false;

            return (_keyStatePrevious.IsKeyDown(key) && _keyStateCurrent.IsKeyUp(key));

            //MouseState x; x.
        }

        public static bool getInputRelease(){return true;}
        public static bool getInputDown() { return true; } //not quite sure how to handle the mouse buttons yet.  Doesn't seem as simple as the other types.

        public static int mouseX
        {
            get
            {
                return _mouseStateCurrent.X;
            }
        }

        public static int mouseY
        {
            get
            {
                return _mouseStateCurrent.Y;
            }
        }

        public static int scrollWheel
        {
            get
            {
                return _mouseStateCurrent.ScrollWheelValue;
            }
        }

        public static bool areKeysPressed
        {
            get
            {
                return (_keyStateCurrent.GetPressedKeys().Count() > 0);
            }
        }

        public static Keys[] keysPressed
        {
            get
            {
                return _keyStateCurrent.GetPressedKeys();
            }
        }

        public static Keys[] keysReleased
        {
            get
            {
                return keyEvents.releasedKeys;
            }
        }

        public static Keys[] keysOld
        {
            get
            {
                return keyEvents.oldKeys;
            }
        }

        public static bool areKeysReleased
        {
            get
            {
                return (keyEvents.releasedKeys.Count() > 0);
            }
        }

        //should be called once per frame
        public static void update()
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
