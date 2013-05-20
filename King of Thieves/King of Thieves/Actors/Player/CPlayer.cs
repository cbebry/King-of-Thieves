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
        private bool _swordReleased = true;
        private static Vector2 _readableCoords = new Vector2();

        public CPlayer() :
            base()
        {

            _name = "Player";
            _position = Vector2.Zero;
            //resource init

            
            image = _imageIndex["PlayerWalkDown"];
        }

        protected override void _initializeResources()
        {
            base._initializeResources();
            _imageIndex.Add("PlayerWalkDown", new Graphics.CSprite("Player:WalkDown", Graphics.CTextures.textures["Player:WalkDown"]));
            _imageIndex.Add("PlayerWalkLeft", new Graphics.CSprite("Player:WalkLeft", Graphics.CTextures.textures["Player:WalkLeft"]));
            _imageIndex.Add("PlayerWalkRight", new Graphics.CSprite("Player:WalkLeft", Graphics.CTextures.textures["Player:WalkLeft"],null,true));
            _imageIndex.Add("PlayerWalkUp", new Graphics.CSprite("Player:WalkUp", Graphics.CTextures.textures["Player:WalkUp"]));

            _imageIndex.Add("PlayerIdleDown", new Graphics.CSprite("Player:IdleDown", Graphics.CTextures.textures["Player:IdleDown"]));
            _imageIndex.Add("PlayerIdleUp", new Graphics.CSprite("Player:IdleUp", Graphics.CTextures.textures["Player:IdleUp"]));
            _imageIndex.Add("PlayerIdleLeft", new Graphics.CSprite("Player:IdleLeft", Graphics.CTextures.textures["Player:IdleLeft"]));
            _imageIndex.Add("PlayerIdleRight", new Graphics.CSprite("Player:IdleLeft", Graphics.CTextures.textures["Player:IdleLeft"],null,true));

            _imageIndex.Add("PlayerSwingUp", new Graphics.CSprite("Player:SwingUp", Graphics.CTextures.textures["Player:SwingUp"]));
            _imageIndex.Add("PlayerSwingDown", new Graphics.CSprite("Player:SwingDown", Graphics.CTextures.textures["Player:SwingDown"]));
            _imageIndex.Add("PlayerSwingRight", new Graphics.CSprite("Player:SwingLeft", Graphics.CTextures.textures["Player:SwingLeft"],null, true));
            _imageIndex.Add("PlayerSwingLeft", new Graphics.CSprite("Player:SwingLeft", Graphics.CTextures.textures["Player:SwingLeft"]));


        }

        public override void collide(object sender, object collider)
        {
            //throw new NotImplementedException();
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
            switch (_state)
            {
                case "Swinging":
                      _state = "Idle";
                    break;
            }

            
        }

        public override void frame(object sender)
        {
            //throw new NotImplementedException();
        }

        public override void keyDown(object sender)
        {
            if (_state == "Idle" || _state == "Moving")
            {

                if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.End))
                    Master.Pop();

                if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.A))
                {
                    _position.X -= 1;
                    image = _imageIndex["PlayerWalkLeft"];
                    _direction = DIRECTION.LEFT;
                    _state = "Moving";
                }

                if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.D))
                {
                    _position.X += 1;
                    image = _imageIndex["PlayerWalkRight"];
                    _direction = DIRECTION.RIGHT;
                    _state = "Moving";
                }

                if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.W))
                {
                    _position.Y -= 1;
                    image = _imageIndex["PlayerWalkUp"];
                    _direction = DIRECTION.UP;
                    _state = "Moving";
                }

                if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.S))
                {
                    _position.Y += 1;
                    image = _imageIndex["PlayerWalkDown"];
                    _direction = DIRECTION.DOWN;
                    _state = "Moving";
                }

                if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    _state = "Swinging";
                    _swordReleased = false;
                }
            }
        }

        public override void keyRelease(object sender)
        {
            if (!(Master.GetInputManager().GetCurrentInputHandler() as CInput).areKeysPressed)
            {
                if (_state == "Moving")
                    _state = "Idle";
            }
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            switch (_state)
            {
                case "Swinging":
                    if (!_swordReleased)
                    {
                        _swordReleased = true;
                        Vector2 swordPos = Vector2.Zero;
                        Random random = new Random();
                        int attackSound = random.Next(0, 3);

                        Sound.CSound[] temp = new Sound.CSound[4];

                        temp[0] = CMasterControl.audioPlayer.soundBank["Player:Attack1"];
                        temp[1] = CMasterControl.audioPlayer.soundBank["Player:Attack2"];
                        temp[2] = CMasterControl.audioPlayer.soundBank["Player:Attack3"];
                        temp[3] = CMasterControl.audioPlayer.soundBank["Player:Attack4"];

                        CMasterControl.audioPlayer.addSfx(temp[attackSound]);
                        CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Player:SwordSlash"]);

                        switch (_direction)
                        {
                            case DIRECTION.UP:
                                swapImage("PlayerSwingUp");
                                swordPos.X = _position.X - 13;
                                swordPos.Y = _position.Y - 13;
                                break;

                            case DIRECTION.LEFT:
                                swapImage("PlayerSwingLeft");
                                swordPos.X = _position.X - 18;
                                swordPos.Y = _position.Y - 10;
                                break;

                            case DIRECTION.RIGHT:
                                swapImage("PlayerSwingRight");
                                swordPos.X = _position.X - 12;
                                swordPos.Y = _position.Y - 10;
                                break;

                            case DIRECTION.DOWN:
                                swapImage("PlayerSwingDown");
                                swordPos.X = _position.X - 17;
                                swordPos.Y = _position.Y - 13;
                                break;
                        }

                        _triggerUserEvent(0, "sword", _direction.ToString(), swordPos.X.ToString(), swordPos.Y.ToString());
                    }

                    break;
                case "Idle":
                    switch (_direction)
                    {
                        case DIRECTION.DOWN:
                            swapImage("PlayerIdleDown", false);
                            break;

                        case DIRECTION.UP:
                            swapImage("PlayerIdleUp", false);
                            break;

                        case DIRECTION.LEFT:
                            swapImage("PlayerIdleLeft", false);
                            break;

                        case DIRECTION.RIGHT:
                            swapImage("PlayerIdleRight", false);
                            break;
                    }

                    break;
            }
            _readableCoords = _position;
        }

        public override void drawMe()
        {
            base.drawMe();
        }

        public static float glblX
        {
            get
            {
                return _readableCoords.X;
            }
        }

        public static float glblY
        {
            get
            {
                return _readableCoords.Y;
            }
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Actors.NPC.Enemies.Keese.CKeese));
        }
    }
}
