using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using King_of_Thieves.Graphics;
using Gears.Cloud;
using King_of_Thieves.Input;
using System.Timers;

namespace King_of_Thieves.Actors
{
    //Actor states
    //idle: Not doing anything
    public enum ACTORTYPES
    {
        MANAGER = 0,
        INTERACTABLE
    }

    public enum DIRECTION
    {
        UP = 0,
        DOWN,
        LEFT,
        RIGHT
    }

    public abstract class CActor
    {
        protected Vector2 _position = Vector2.Zero;
        protected Vector2 _oldPosition = Vector2.Zero;
        public readonly ACTORTYPES ACTORTYPE;
        protected string _name;
        protected List<Type> _collidables;
        protected CAnimation _sprite;
        protected DIRECTION _direction = DIRECTION.UP;
        protected Boolean _moving = false; //used for prioritized movement
        private int _componentAddress = 0;
        protected Dictionary<uint, userEventHandler> _userEvents;
        protected List<uint> _userEventsToFire;
        protected string _state = "Idle";
        public Graphics.CSprite image;
        protected Dictionary<string, Graphics.CSprite> _imageIndex;
        protected Dictionary<string, Sound.CSound> _soundIndex;
        private bool _animationHasEnded = false;
        public List<object> userParams = new List<object>();
        public bool _followRoot = true;
        public int layer;
        //hitboxes will go here as well? What a terrible night for a curse...
        //event handlers will be added here

        public event createHandler onCreate;
        public event destroyHandler onDestroy;
        public event keyDownHandler onKeyDown;
        public event frameHandler onFrame;
        public event drawHandler onDraw;
        public event keyReleaseHandler onKeyRelease;
        public event collideHandler onCollide;
        public event animationEndHandler onAnimationEnd;
        public event timerHandler onTimer0;
        public event timerHandler onTimer1;
        public event timerHandler onTimer2;
        protected Collision.CHitBox _hitBox;

        public virtual void create(object sender) { }
        public virtual void destroy(object sender) { }
        public virtual void keyDown(object sender) { }
        public virtual void keyRelease(object sender) { }
        public virtual void frame(object sender) { }
        public virtual void draw(object sender) { }
        public virtual void collide(object sender, object collider) { }
        public virtual void animationEnd(object sender) { }
        public virtual void timer0(object sender, ElapsedEventArgs e) { if (_timer0 != null) { _timer0.Stop(); _timer0 = null; } }
        public virtual void timer1(object sender, ElapsedEventArgs e) { if (_timer1 != null) { _timer1.Stop(); _timer1 = null; } }

        protected abstract void _addCollidables(); //Use this guy to tell the Actor what kind of actors it can collide with
        protected Random _randNum = new Random();

        private Timer _timer0;
        private Timer _timer1;
        

        public CActor()
            
        {
            onCreate += new createHandler(create);
            onDestroy += new destroyHandler(destroy);
            onKeyDown += new keyDownHandler(keyDown);
            onKeyRelease += new keyReleaseHandler(keyRelease);
            onFrame += new frameHandler(frame);
            onDraw += new drawHandler(draw);
            onAnimationEnd += new animationEndHandler(animationEnd);
            onCollide += new collideHandler(collide);

            _name = name;
            _collidables = new List<Type>();

            try
            {
                _addCollidables();
            }
            catch (NotImplementedException)
            { ;}

            _position = position;

            try
            {
                onCreate(this);
            }
            catch (NotImplementedException)
            { }

            _registerUserEvents();
            _initializeResources();
        }

        ~CActor()
        {
            onCreate -= new createHandler(create);
            onDestroy -= new destroyHandler(destroy);
            onKeyDown -= new keyDownHandler(keyDown);
            onFrame -= new frameHandler(frame);
            onKeyRelease -= new keyReleaseHandler(keyRelease);
            onDraw -= new drawHandler(draw);
        }

        public void startTimer0(double ticks)
        {
            _timer0 = new Timer(ticks * 1000);
            _timer0.Elapsed += new ElapsedEventHandler(timer0);

            _timer0.Enabled = true;
            _timer0.Start();
        }

        public void startTimer1(double ticks)
        {
            _timer1 = new Timer(ticks * 1000);
            _timer1.Elapsed += new ElapsedEventHandler(timer1);

            _timer1.Enabled = true;
            _timer1.Start();
        }

        //overload this and call the base to process your own parameters
        public virtual void init(string name, Vector2 position, uint compAddress, params string[] additional)
        {
            _name = name;
            _position = position;
            _componentAddress = (int)compAddress;
        }

        public string state
        {
            get
            {
                return _state;
            }
        }

        public void moveToPoint(int x, int y, double speed)
        {
            double distX = 0, distY = 0;

            distX = (int)(x - _position.X);
            distY = (int)(y - _position.Y);

            distX = Math.Sign(distX);
            distY = Math.Sign(distY);

            _position.X += (float)(speed * distX);
            _position.Y += (float)(speed * distY);

        }

        public void swapImage(string imageIndex, bool triggerAnimEnd = true)
        {
            image = _imageIndex[imageIndex];

            if (triggerAnimEnd)
            {
                _animationHasEnded = true;
            }
        }

        public void addFireTrigger(uint userEvent)
        {
            _userEventsToFire.Add(userEvent);
        }

        protected virtual void _registerUserEvents()
        {
            _userEvents = new Dictionary<uint, userEventHandler>();
            _userEventsToFire = new List<uint>();
        }

        public CAnimation spriteIndex
        {
            get
            {
                return _sprite;
            }
            set
            {
                _sprite = value;
            }
        }

        public virtual void update(GameTime gameTime)
        {
            
            //onFrame(this);

            //check collisions
            foreach (Type actor in _collidables)
            {
                //fetch all actors of this type and check them for collisions
                CActor[] collideCheck = Map.CMapManager.queryActorRegistry(actor, layer);
                if (collideCheck == null)
                    continue;
                
                foreach (CActor x in collideCheck)
                {
                    if (_hitBox.checkCollision(x._hitBox))
                    {
                        //trigger collision event
                        onCollide(this, x);
                    }
                }
            }

            if (_animationHasEnded)
                try
                {
                    onAnimationEnd(this);
                }
                catch (NotImplementedException) { ;}

            
            _oldPosition = _position;

            if (image != null)
            {
                image.X = (int)_position.X;
                image.Y = (int)_position.Y;
            }

            if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).areKeysPressed)
                onKeyDown(this);

            if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).areKeysReleased)
                onKeyRelease(this);

            //do timer events
            

            foreach (uint ID in _userEventsToFire)
            {
                _userEvents[ID](this);
            }

            _userEventsToFire.Clear();

            _animationHasEnded = false;
            userParams.Clear();

        }

        public virtual void drawMe()
        {
            try
            {
                onDraw(this);
            }
            catch (NotImplementedException)
            { ;}

            if (image != null)
                _animationHasEnded = image.draw((int)_position.X, (int)_position.Y);
 
        }

        protected virtual void _initializeResources()
        {
            //add sprites to image index by overloading this function.
            //also add resources to the texture cache here.
            _imageIndex = new Dictionary<string, CSprite>();
            _soundIndex = new Dictionary<string, Sound.CSound>();
        }

        private void _closeResources()
        {
            _imageIndex.Clear();
            _imageIndex = null;

            
        }

        public Vector2 position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public Vector2 oldPosition
        {
            get
            {
                return _oldPosition;
            }
            
        }

        public Vector2 distanceFromLastFrame
        {
            get
            {
                return (position - oldPosition);
            }
        }

        public virtual void remove()
        {
            onDestroy(this);
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        private void _checkCollisions()
        {
            //This shit is WEIRD.
            //fetch my hitboxes
            //List<BoundingBox> myBoxes = CMasterControl.hitboxes[this.GetType()][_name];
            
           
        }

        //this will go up to the component and trigger the specified user event in the specified actor
        //what this does is create a "packet" that will float around in some higher level scope for the component to pick up
        protected void _triggerUserEvent(int eventNum, string actorName, params string[] param)
        {
            CMasterControl.commNet[_componentAddress].Add(new CActorPacket(eventNum, actorName, param));
        }
    }
}
