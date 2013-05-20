using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.Keese
{
    enum KEESETYPE
    {
        NORMAL = 0,
        FIRE,
        ICE,
        THUNDER,
        SHADOW
    }

    class CBaseKeese : CBaseEnemy
    {
        private KEESETYPE _type = 0;
        protected Vector2 _home = Vector2.Zero;

        public CBaseKeese(int sight, float fov, int foh, params dropRate[] drops)
            : base(drops)
        {
            //cant see shit, captain
            _fovMagnitude = 0;
            _lineOfSight = 0;

            _hearingRadius = foh;
            image = _imageIndex["keeseIdle"];
            _state = "idle";
            _followRoot = false;
        }

        protected override void _initializeResources()
        {
            base._initializeResources();


        }

        public override void init(string name, Vector2 position, uint compAddress, params string[] additional)
        {
            base.init(name, position, compAddress, additional);
            _home = position;
        }

        protected override void idle()
        {
            base.idle();

            if (!_huntPlayer)
                return;

            //chase the player
            swapImage("keeseFly");
            _state = "chase";
        }

        public override void timer0(object sender, System.Timers.ElapsedEventArgs e)
        {
            base.timer0(sender, e);
            _state = "return";
        }

        public override void timer1(object sender, System.Timers.ElapsedEventArgs e)
        {
            base.timer1(sender, e);

            //pick a random direction to fly in
            if (_state == "flying")
            {
                _direction = (DIRECTION)_randNum.Next(0, 3);
                startTimer1(2);
            }
        }

        protected virtual void goHome()
        {
            moveToPoint((int)_home.X, (int)_home.Y, 1);

            if (MathExt.MathExt.distance(_position, _home) <= 1.1)
            {
                swapImage("keeseIdle");
                _state = "idle";
            }
        }

        protected override void chase()
        {
            moveToPoint((int)Player.CPlayer.glblX, (int)Player.CPlayer.glblY, .75);

            if (MathExt.MathExt.distance(_position, new Microsoft.Xna.Framework.Vector2(Player.CPlayer.glblX, Player.CPlayer.glblX)) > 150)
            {
                _state = "flying";
                startTimer0(8); //return time
                startTimer1(2); //direction change time
            }
        }

        protected virtual void fly()
        {
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    _position.Y += 1f;
                    break;

                case DIRECTION.LEFT:
                    _position.X -= 1;
                    break;

                case DIRECTION.RIGHT:
                    _position.X += 1f;
                    break;

                case DIRECTION.UP:
                    _position.Y -= 1f;
                    break;
            }
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            switch (_state)
            {
                case "idle":
                    idle();
                    break;

                case "flying":
                    fly();
                    break;

                case "chase":
                    chase();
                    break;

                case "return":
                    goHome();
                    break;
            }
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }
    }
}
