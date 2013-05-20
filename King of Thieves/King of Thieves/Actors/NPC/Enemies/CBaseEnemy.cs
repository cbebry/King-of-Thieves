using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using King_of_Thieves.Graphics;
using Gears.Cloud;
using King_of_Thieves.Input;
namespace King_of_Thieves.Actors.NPC.Enemies
{
    //has all the things that an enemy NPC will have
    //ex: item drops
    public struct dropRate
    {
        public object item;
        public float rate;
    }

    public abstract class CBaseEnemy : CActor
    {
        protected Dictionary<object,float> _itemDrop; //leave this as object until we have classes for items ready
        protected int _lineOfSight;
        protected int _fovMagnitude;
        protected float _visionRange; //this is an angle
        protected float _visionSlope;
        protected int _hearingRadius; //how far away they can hear you from
        protected bool _huntPlayer = false;

        //protected abstract void _addCollidables();
        public CBaseEnemy(params dropRate[] drops) 
            :  base()
        {
            foreach (dropRate x in drops)
                _itemDrop.Add(x.item, x.rate);

            //calculate field of view
            _fovMagnitude = (int)Math.Cos(_visionRange * (Math.PI / 180.0));
            _visionSlope = (int)Math.Tan(_visionRange * (Math.PI/180.0));
        }

        protected override void _initializeResources()
        {
            base._initializeResources();
        }

        //just chill there
        protected virtual void idle()
        {
            _huntPlayer = hunt();
        }

        //look for the player while idling
        private bool hunt()
        {
            //check if the player is within the line of sight
            //switch (_direction)
            //{
            //    case DIRECTION.UP:
            //        if (Actors.Player.CPlayer.glblY <= _position.Y && Actors.Player.CPlayer.glblY >= (_position.Y - _lineOfSight))
            //        {

            //        }
            //        break;
            //}
            
            //check hearing field
            if (MathExt.MathExt.distance(_position, new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY)) <= _hearingRadius)
                return true;

            return false;

        }

        //chase the player
        protected virtual void chase()
        {

        }

        public override void destroy(object sender)
        {
            _dropItem();

            base.destroy(sender);
        }

        private void _dropItem()
        {
            object itemToDrop = null;
            Random roller = new Random();
            double sum = 0;

            foreach (KeyValuePair<object, float> x in _itemDrop)
            {
                sum += roller.NextDouble();

                if (sum >= x.Value)
                {
                    itemToDrop = x.Key;
                    break;
                }
            }
        }

        protected bool _checkLineofSight(float x, float y)
        {
            //return _visionSlope * (x - _position.X) + (y - _position.Y);

            switch (_direction)
            {
                case DIRECTION.UP:
                    //return 
                    break;
            }


            return false;
        }
    }
}
