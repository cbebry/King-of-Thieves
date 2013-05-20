using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Map
{
    class CLayer
    {
        private ComponentManager _components;
        public readonly string NAME;
        private int _width, _height; //PIXELS!!!
        private Graphics.CSprite _image; //key refers to the image depth
        private CTile[] _tiles; //raw tile data

        public CLayer(string name, Actors.CComponent[] components, CTile[] tiles, ref Graphics.CSprite image)
        {
            NAME = name;
            _tiles = tiles;
            _image = image;
            _components = new ComponentManager(new ComponentFactory[]{ new ComponentFactory(components) } );
            //_image = Graphics.CTextures.generateLayerImage(this, tiles);
        }

        ~CLayer()
        {
             _image = null;
        }

        public void updateLayer(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //components
            _components.Update(gameTime);
            
        }

        public void drawLayer()
        {
            foreach (CTile tile in _tiles)
                _image.draw((int)tile.tileCoords.X, (int)tile.tileCoords.Y, (int)tile.atlasCoords.X, (int)tile.atlasCoords.Y, 16, 16);

            _components.Draw(Graphics.CGraphics.spriteBatch);


        }

        public int width
        {
            get
            {
                return _width;
            }
        }

        public int height
        {
            get
            {
                return _height;
            }
        }

        public void addImage(Graphics.CSprite image)
        {
            _image = image;
        }
    }
}
