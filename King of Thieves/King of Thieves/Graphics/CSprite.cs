using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Graphics
{ 
    class CSprite : CRenderable
    {
        private Texture2D _shaderTex = null; 
        private Rectangle _size;
        protected string _name = "";
        protected Vector2 _position = Vector2.Zero;
        private CTextureAtlas _imageAtlas;
        private int _frameTracker = 0;
        private int frameX = 0, frameY = 0;
        private bool _flipH = false;
        private bool _flipV = false;
        

        public CSprite(CTextureAtlas atlas, Effect shader = null, bool flipH = false, bool flipV = false, params VertexPositionColor[] vertices)
            : base(shader, vertices)
        {
            _imageAtlas = atlas;
            _size = new Rectangle(0,0, atlas.FrameWidth, atlas.FrameHeight);
            _name = _imageAtlas.sourceImage.Name;
            _flipH = flipH;
            _flipV = flipV;
            CMasterControl.drawList.AddLast(this);
        }

        public override void draw(int x, int y)
        {
            if (_imageAtlas == null)
                throw new FormatException("Unable to draw sprite " + _name + ", may be the target of an animation.");
            if (isOffscreen != false)
                renderOffScreen();

            _frameTracker += _imageAtlas.FrameRate;

            if (_frameTracker >= 60)
            {
                _frameTracker = 0;
                frameX++;

                if (frameX >= _imageAtlas.tileXCount)
                {
                    frameX = 0;
                    frameY++;

                    if (frameY >= _imageAtlas.tileYCount)
                        frameY = 0;
                }
                
            }
            _size = _imageAtlas.getTile(frameX, frameY);
            _position.X = x; _position.Y = y;
            //CGraphics.spriteBatch.Draw(_imageAtlas.sourceImage, _position, _size, Color.White);

            if (!(_flipV || _flipH))
                CGraphics.spriteBatch.Draw(_imageAtlas.sourceImage, _position, _size, Color.White);
            else if (_flipV)
                CGraphics.spriteBatch.Draw(_imageAtlas.sourceImage, _position, _size, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.FlipVertically, 0);
            else if(_flipH)
                CGraphics.spriteBatch.Draw(_imageAtlas.sourceImage, _position, _size, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 0);
            base.draw(x,y);
        }

        public int X
        {
            get
            {
                return (int)_position.X;
            }
            set
            {
                _position.X = value;
            }
        }

        public int Y
        {
            get
            {
                return (int)_position.Y;
            }
            set
            {
                _position.Y = value;
            }
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
