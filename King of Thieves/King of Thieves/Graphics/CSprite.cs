﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Graphics
{
    class CSprite : CRenderable
    {

        private Texture2D _sprite = null;
        private CTileSheet _sheet = null;
        private Rectangle _size;

        public CSprite(Texture2D sprite, Effect shader = null, params VertexPositionColor[] vertices)
            : base(shader, vertices)
        {
            _sprite = sprite;
            _size = new Rectangle(0, 0, _sprite.Width, _sprite.Height);
        }

        public CSprite(CTileSheet spriteSheet, Effect shader = null, params VertexPositionColor[] vertices)
            :base(shader, vertices)
        {
            _sheet = spriteSheet;
            _size = new Rectangle(0, 0, _sprite.Width, _sprite.Height);
        }

        public override void draw()
        {
            CGraphics.spriteBatch.Draw(_sprite, _size, Color.White);
            base.draw();
        }

        public int width
        {
            get
            {
                return _size.Width;
            }
        }

        public int height
        {
            get
            {
                return _size.Height;
            }
        }

        
    }
}
