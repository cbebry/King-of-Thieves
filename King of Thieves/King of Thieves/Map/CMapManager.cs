using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gears.Cartography;
using Gears.Playable;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Map
{
    class CMapManager
    {
        private static CMap _currentMap;
        public Dictionary<string, CMap> mapPool = null;

        public CMapManager()
        {
            _currentMap = null;
            mapPool = new Dictionary<string, CMap>();
        }

        ~CMapManager()
        {
            clear();
        }

        public void drawMap()
        {
            if (_currentMap != null)
                _currentMap.draw();
        }

        public void updateMap(GameTime gameTime)
        {
            if (_currentMap != null)
                _currentMap.update(gameTime);
        }

        public static Actors.CActor[] queryActorRegistry(Type type, int layer)
        {
            try
            {
                return _currentMap.queryActorRegistry(type, layer);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public void swapMap(string mapName)
        {
            _currentMap = mapPool[mapName];
        }

        public void cacheMaps(bool clearMaps, params string[] maps)
        {
            if (clearMaps)
                clear();

            

            foreach (string file in maps)
                mapPool.Add(file, new CMap(file)); //temporary
        }

        private void clear()
        {
            if (mapPool != null)
                mapPool.Clear();

            mapPool = null;
        }

    }
}
