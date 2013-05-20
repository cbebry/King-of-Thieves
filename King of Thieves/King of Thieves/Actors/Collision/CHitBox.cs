using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Collision
{
    public class CHitBox : CActor
    {
        private Vector2 _halfSize;
        private Vector2 _center;

        public CHitBox(float width, float height)
        {
            _halfSize = new Vector2(width * .5f, height * .5f);
            _center = new Vector2(_halfSize.X, _halfSize.Y);
        }

        public override void update(GameTime gameTime)
        {
            _center.X = _position.X + _halfSize.X;
            _center.Y = _position.Y + _halfSize.Y;
            base.update(gameTime);
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }

        public bool checkCollision(CHitBox sender)
        {
            
            float distance = 0;
            float length = 0;
            CHitBox otherBox = sender;

            distance = Math.Abs((_position.X + _center.X) - (otherBox.position.X + otherBox._center.X));
            length = _halfSize.X + otherBox._halfSize.X;

            if (distance < length)
            {
                distance = Math.Abs((_position.Y + _center.Y) - (otherBox.position.Y + otherBox._center.Y));
                length = _halfSize.Y + otherBox._halfSize.Y;

                return distance < length;
            }

            return false;
        }

        public override void draw(object sender)
        {
            return;
        }

        public float halfWidth
        {
            get
            {
                return _halfSize.X;
            }
        }

        public float halfHeight
        {
            get
            {
                return _halfSize.Y;
            }
        }

        public Vector2 center
        {
            get
            {
                return _center;
            }
        }
    }
}
