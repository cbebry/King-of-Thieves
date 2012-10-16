using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gears.Cloud;
using King_of_Thieves.Input;

namespace King_of_Thieves.Actors.Player
{
    class CPlayer : CActor
    {
        public CPlayer() :
            base("Player", Vector2.Zero, ACTORTYPES.INTERACTABLE)
        {
           

            //resource init

            _initializeResources();
            image = _imageIndex["PlayerWalkDown"];
        }

        protected override void _initializeResources()
        {
            base._initializeResources();
            _imageIndex.Add("PlayerWalkDown", new Graphics.CSprite(Graphics.CTextures.texture("Player:WalkDown")));
            _imageIndex.Add("PlayerWalkLeft", new Graphics.CSprite(Graphics.CTextures.texture("Player:WalkLeft")));
            _imageIndex.Add("PlayerWalkRight", new Graphics.CSprite(Graphics.CTextures.texture("Player:WalkLeft"),null,true));
            _imageIndex.Add("PlayerWalkUp", new Graphics.CSprite(Graphics.CTextures.texture("Player:WalkUp")));
            _imageIndex.Add("PlayerIdleDown", new Graphics.CSprite(Graphics.CTextures.texture("Player:IdleDown")));
            _imageIndex.Add("PlayerIdleUp", new Graphics.CSprite(Graphics.CTextures.texture("Player:IdleUp")));
            _imageIndex.Add("PlayerIdleLeft", new Graphics.CSprite(Graphics.CTextures.texture("Player:IdleLeft")));
            _imageIndex.Add("PlayerIdleRight", new Graphics.CSprite(Graphics.CTextures.texture("Player:IdleLeft"),null,true));
        }

        public override void collide(object sender, object collider)
        {
            throw new NotImplementedException();
        }

        public override void create(object sender)
        {
            throw new NotImplementedException();
        }

        public override void destroy(object sender)
        {
            throw new NotImplementedException();
        }

        public override void draw(object sender)
        {
            throw new NotImplementedException();
        }

        public override void animationEnd(object sender)
        {
            throw new NotImplementedException();
        }

        public override void frame(object sender)
        {
            //throw new NotImplementedException();
        }

        public override void keyDown(object sender)
        {


            if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.A))
            {
                _position.X -= 1;

                if (!_moving)
                {
                    image = _imageIndex["PlayerWalkLeft"];
                    _direction = DIRECTION.LEFT;
                }
                _moving = true;
            }

            if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.D))
            {
                _position.X += 1;

                if (!_moving)
                {
                    image = _imageIndex["PlayerWalkRight"];
                    _direction = DIRECTION.RIGHT;
                }
                _moving = true;
            }


            if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.W))
            {
                _position.Y -= 1;

                if (!_moving)
                {
                    image = _imageIndex["PlayerWalkUp"];
                    _direction = DIRECTION.UP;
                }
                _moving = true;
            }

            if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.S))
            {
                _position.Y += 1;

                if (!_moving)
                {
                    image = _imageIndex["PlayerWalkDown"];
                    _direction = DIRECTION.DOWN;
                }
                _moving = true;
            }
        }

        public override void keyRelease(object sender)
        {
            _moving = false;
            if (!(Master.GetInputManager().GetCurrentInputHandler() as CInput).areKeysPressed)
            {
                
                switch (_direction)
                {
                    case DIRECTION.DOWN:
                        image = _imageIndex["PlayerIdleDown"];
                        break;

                    case DIRECTION.UP:
                        image = _imageIndex["PlayerIdleUp"];
                        break;

                    case DIRECTION.LEFT:
                        image = _imageIndex["PlayerIdleLeft"];
                        break;

                    case DIRECTION.RIGHT:
                        image = _imageIndex["PlayerIdleRight"];
                        break;
                }
            }
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
        }

        public override void drawMe()
        {
            base.drawMe();
        }



        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }
    }
}
