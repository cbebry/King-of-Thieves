using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Map
{
    class ComponentManager : Gears.Playable.UnitManager
    {

        private Actors.CActor[] _actorRegistry;
        private int _actorsInRegistry = 0;

        public ComponentManager(ComponentFactory[] factories)
        {
            Register(factories);

            //for (int i = 0; i < factories.Count(); i++)
            //    for (int j = 0; j < factories[i].
        }

        private void registerActors(Actors.CComponent component)
        {
            
        }
        
    }
}
