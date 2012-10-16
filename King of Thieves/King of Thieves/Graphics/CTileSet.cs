using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Graphics
{
    class CTileSet
    {
        private CTextureAtlas _atlas; //pass the FULL texture atlas here


        public CTileSet(ref CTextureAtlas atlas, int width, int height)
        {
            _atlas = atlas;


        }
    }
}
