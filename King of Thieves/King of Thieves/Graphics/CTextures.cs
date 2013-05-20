using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace King_of_Thieves.Graphics
{
    static class CTextures
    {
        public readonly static Dictionary<string, CTextureAtlas> textures = new Dictionary<string, CTextureAtlas>();
        public readonly static Dictionary<string, Texture2D> rawTextures = new Dictionary<string, Texture2D>();
        private static ContentManager _content;
        private static RenderTarget2D _tileMapGen = null;
        private static SpriteBatch _tileBatch = null;

        public static void init(ContentManager content)
        {
            _content = content;

            //Core textures should go here.  Things that are ALWAYS used (or small enough to not hog up memory)
            //other textures should only be loaded when they're needed and should be removed from memory asap.

            _prepareTextures();

            _tileBatch = new SpriteBatch(CGraphics.GPU);

            textures.Add("test", new CTextureAtlas(_content.Load<Texture2D>("test"), "test", 19, 23, 0));
            textures.Add("mcDungeon2", new CTextureAtlas(_content.Load<Texture2D>("mcDungeon2"), 16, 16, 0, 1));
            textures.Add("menu", new CTextureAtlas(_content.Load<Texture2D>("menu"), 288, 192, 0,0));
            textures.Add("Player:WalkDown", new CTextureAtlas("Player", 32, 32, 1, "0:1", "9:1", 15));
            textures.Add("Player:WalkLeft", new CTextureAtlas("Player", 32, 32, 1, "11:0", "15:1", 15));
            textures.Add("Player:WalkUp", new CTextureAtlas("Player", 32, 32, 1, "17:0", "21:1", 15));
            textures.Add("Player:IdleDown", new CTextureAtlas("Player", 32, 32, 1, "0:0", "0:0", 0));
            textures.Add("Player:IdleUp", new CTextureAtlas("Player", 32, 32, 1, "2:0", "2:0", 0));
            textures.Add("Player:IdleLeft", new CTextureAtlas("Player", 32, 32, 1, "1:0", "1:0", 0));
            textures.Add("Player:SwingUp", new CTextureAtlas("Player", 32, 32, 1, "23:2", "33:2", 55));
            textures.Add("Player:SwingDown", new CTextureAtlas("Player", 32, 32, 1, "23:0", "33:0", 55));
            textures.Add("Player:SwingLeft", new CTextureAtlas("Player", 32, 32, 1, "23:1", "32:1", 55));
            textures.Add("GerudoSword:SwingDown", new CTextureAtlas("Swords", 64, 64, 1, "0:0", "7:0", 55));
            textures.Add("GerudoSword:SwingUp", new CTextureAtlas("Swords", 64, 64, 1, "0:1", "7:1", 55));
            textures.Add("GerudoSword:SwingRight", new CTextureAtlas("Swords", 64, 64, 1, "0:2", "7:2", 55));
            textures.Add("chuChu:Wobble", new CTextureAtlas("chuchuGreen", 32, 32, 1, "0:0", "15:0", 12));
            textures.Add("chuChu:Idle", new CTextureAtlas("chuchuGreen", 32, 32, 1, "0:1", "7:1", 5));
            textures.Add("chuChu:PopUp", new CTextureAtlas("chuchuGreen", 32, 32, 1, "8:1", "10:1", 5));
            textures.Add("chuChu:Hop", new CTextureAtlas("chuchuGreen", 32, 32, 1, "0:2", "6:2", 7));
            textures.Add("chuChu:PopDown", new CTextureAtlas("chuchuGreen", 32, 32, 1, "7:2", "9:2", 5));
            textures.Add("keese:Idle", new CTextureAtlas("keese", 32, 32, 1, "0:0", "0:0"));
            textures.Add("keese:Fly", new CTextureAtlas("keese", 32, 32, 1, "1:0", "5:0",15));

            //tilesets
            textures.Add("tileset:test", new CTextureAtlas("test", 16, 16, 0, "0:0", "15:15",0,false,false,true));
        }

        private static void _prepareTextures()
        {
            rawTextures.Add("Player", _content.Load<Texture2D>("Player"));
            rawTextures.Add("Swords", _content.Load<Texture2D>("Swords"));
            rawTextures.Add("chuchuGreen", _content.Load<Texture2D>("chuchuGreen"));
            rawTextures.Add("keese", _content.Load<Texture2D>("keese"));
            rawTextures.Add("test", _content.Load<Texture2D>("tiletest"));
        }

        public static void cleanUp(string nameSpace = "")
        {
            if (nameSpace == "")
            {
                textures.Clear();
                return;
            }

            var resourcesToRemove = (from pair in textures
                                     where pair.Key.Contains(nameSpace)
                                     select pair.Key).ToArray();

            foreach (string key in resourcesToRemove)
                textures.Remove(key);

            _tileBatch.Dispose();
            _tileBatch = null;
        }

        public static Texture2D generateLayerImage(Map.CLayer layerToRender, Map.CTile[] tileStrip)
        {
            _tileMapGen = new RenderTarget2D(CGraphics.GPU, layerToRender.width, layerToRender.height);

            CGraphics.GPU.SetRenderTarget(_tileMapGen);
            _tileBatch.Begin();

            foreach (Map.CTile tile in tileStrip)
                _tileBatch.Draw(textures[tile.tileSet].sourceImage, tile.tileCoords, textures[tile.tileSet].getTile((int)tile.atlasCoords.X, (int)tile.atlasCoords.Y), Color.White);

            _tileBatch.End();
            CGraphics.GPU.SetRenderTarget(null);

            return (_tileMapGen);
        }


    }
}
