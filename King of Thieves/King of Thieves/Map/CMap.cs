using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using King_of_Thieves.Actors;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Map
{
    class CMap
    {
        private int _componentCount = 0;
        private CLayer[] _layers = null;
        private static Regex _coordFormat = new Regex("^[0-9]+:[0-9]+$");
        private static Regex _valSplitter = new Regex(":");
        private List<CActor> _actorRegistry = new List<CActor>();
        private Gears.Cartography.Map _internalMap;
        private Graphics.CSprite _tileIndex = null;

        public CMap(string fileName)
        {
            //_internalMap = Gears.Cartography.Map.
            _internalMap = Gears.Cartography.Map.deserialize(fileName);
            _layers = new CLayer[_internalMap.NUM_LAYERS];
            int layerCount = 0;
            _tileIndex = new Graphics.CSprite(_internalMap.TILESET, Graphics.CTextures.textures[_internalMap.TILESET]);

            foreach (Gears.Cartography.layer layer in _internalMap.LAYERS)
            {
                
                uint componentAddresses = 0;
                int componentCount = 0;
                Actors.CComponent[] compList = new CComponent[layer.COMPONENTS == null ? 0 : layer.COMPONENTS.Count()];

                //=======================================================================
                //Tiles
                //=======================================================================
                CTile[] tiles = new CTile[layer.TILES.Count()];
                int tileCounter = 0;
                foreach (Gears.Cartography.tile tile in layer.TILES)
                {
                    if (!_coordFormat.IsMatch(tile.COORDS))
                        throw new FormatException("The coordinate format provided was not valid.\n" + "Tile: " + tile.COORDS);

                    if (!_coordFormat.IsMatch(tile.TILESELECTION))
                        throw new FormatException("The coordinate format provided was not valid.\n" + "Tile: " + tile.TILESELECTION);

                    Vector2 atlasCoords = new Vector2((float)Convert.ToDouble(_valSplitter.Split(tile.TILESELECTION)[0]),
                                                      (float)Convert.ToDouble(_valSplitter.Split(tile.TILESELECTION)[1]));
                    Vector2 mapCoords = new Vector2((float)Convert.ToDouble(_valSplitter.Split(tile.COORDS)[0]),
                                                      (float)Convert.ToDouble(_valSplitter.Split(tile.COORDS)[1]));

                    tiles[tileCounter++] = new CTile(atlasCoords, mapCoords, tile.TILESET);
                }

                if (layer.COMPONENTS != null)
                {
                    //=======================================================================
                    //Components
                    //=======================================================================
                    foreach (Gears.Cartography.component component in layer.COMPONENTS)
                    {
                        CComponent tempComp = new CComponent(componentAddresses);
                        bool root = true;
                        foreach (Gears.Cartography.actors actor in component.ACTORS)
                        {
                            Type actorType = Type.GetType(actor.TYPE);
                            CActor tempActor = (CActor)Activator.CreateInstance(actorType);

                            Vector2 coordinates = Vector2.Zero;

                            if (!_coordFormat.IsMatch(actor.COORDS))
                                throw new FormatException("The coordinate format provided was not valid.\n" + "Actor: " + actor.TYPE.ToString() + " " + actor.NAME);

                            coordinates.X = (float)Convert.ToDouble(_valSplitter.Split(actor.COORDS)[0]);
                            coordinates.Y = (float)Convert.ToDouble(_valSplitter.Split(actor.COORDS)[1]);

                            tempActor.init(actor.NAME, coordinates, componentAddresses, actor.param == null ? null : actor.param.Split(','));
                            tempActor.layer = layerCount;

                            if (root)
                                tempComp.root = tempActor;
                            else
                                tempComp.actors.Add(actor.NAME, tempActor);

                            root = false;
                            _actorRegistry.Add(tempActor);

                        }
                        //register component
                        CMasterControl.commNet.Add((int)componentAddresses++, new List<CActorPacket>());
                        compList[componentCount++] = tempComp;

                    }
                }
                _layers[layerCount++] = new CLayer(layer.NAME, compList, tiles, ref _tileIndex);

            }

        }

        public void draw()
        {
            foreach (CLayer layer in _layers)
            {
                layer.drawLayer();
            }
        }

        public void update(GameTime gameTime)
        {
            foreach (CLayer layer in _layers)
                layer.updateLayer(gameTime);
        }

        public Actors.CActor[] queryActorRegistry(Type type, int layer)
        {
            var query = from actor in _actorRegistry
                        where actor.GetType() == type && actor.layer == layer
                        select actor;

            return query.ToArray();
        }

        private void serializeToXml(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Gears.Cartography.Map));
            TextWriter writer = new StreamWriter(fileName);

            serializer.Serialize(writer, this);
            writer.Close();
        }

       
    }
}
