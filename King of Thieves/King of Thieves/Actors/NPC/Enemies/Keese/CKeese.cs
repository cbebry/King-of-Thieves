using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Keese
{
    class CKeese : CBaseKeese
    {
        public CKeese(int sight, float fov, int foh, params dropRate[] drops)
            : base(0, 0, 150)
        {
            _position.X = _randNum.Next(0,200);
            _position.Y = _randNum.Next(0, 200);
            _home = new Microsoft.Xna.Framework.Vector2(_position.X, position.Y);
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            _imageIndex.Add("keeseIdle", new Graphics.CSprite("keese:Idle", Graphics.CTextures.textures["keese:Idle"]));
            _imageIndex.Add("keeseFly", new Graphics.CSprite("keese:Fly", Graphics.CTextures.textures["keese:Fly"]));
        }
    }
}
