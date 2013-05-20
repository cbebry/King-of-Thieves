using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace King_of_Thieves.Actors
{
    //component is a series of actors.  Bongo Bongo for example would be 3 actors: 2 hands and a main body, main body being the root.
    //this is really a wrapper around CActor to group related CActors together.
    public class CComponent : Gears.Playable.Unit
    {
        //if the root moves, all children follow it.  Actors otherwise are free to move freely of each other.
        //actors can also rotate around the root.
        public CActor root;
        public Dictionary<string, CActor> actors;
        private uint _address;

        protected override string TextureFileLocation
        {
            get { return ""; }
        }

        public CComponent(uint address = 0)
        {
            actors = new Dictionary<string, CActor>();
            _address = address;
        }

        private void passMessage(ref CActor actor, uint eventID, params string[] param)
        {
            actor.addFireTrigger(eventID);
            actor.userParams.AddRange(param);
        }

        public override void Update(GameTime gameTime)
        {
            root.update(gameTime);

            foreach (KeyValuePair<string, CActor> kvp in actors)
            {
                //first get messages from the commNet
                if (CMasterControl.commNet[(int)_address].Count() > 0)
                {
                    CActorPacket[] packetData = new CActorPacket[CMasterControl.commNet[(int)_address].Count()];
                    CMasterControl.commNet[(int)_address].CopyTo(packetData);

                    var group = from packets in packetData
                                where kvp.Key == packets.actor
                                select packets;

                    foreach (var result in group)
                    {
                        
                        //pass the message to the actor
                        CActor temp = kvp.Value;
                        passMessage(ref temp, (uint)result.userEventID, result.getParams());
                        CMasterControl.commNet[(int)_address].Remove(result);
                        
                    }
          
                }

                //update position relative to the root
                if (kvp.Value._followRoot)
                    kvp.Value.position += root.distanceFromLastFrame;

                //update
                kvp.Value.update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch) //spritebatch not used
        {
            root.drawMe();
            foreach (KeyValuePair<string, CActor> kvp in actors)
            {
                kvp.Value.drawMe();
            }
        }

        public void destroyActors()
        {
            root.remove();
            foreach (KeyValuePair<string, CActor> kvp in actors)
            {
                kvp.Value.remove();
            }
        }


    }
}
