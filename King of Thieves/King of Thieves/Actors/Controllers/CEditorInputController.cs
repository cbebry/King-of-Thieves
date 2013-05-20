using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Controllers
{
    class CEditorInputController : CActor
    {
        public bool _shutDown = false;

        public override void keyDown(object sender)
        {
            if ((Gears.Cloud.Master.GetInputManager().GetCurrentInputHandler() as Input.CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.Back))
            {
                _shutDown = true;
            }

        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }
    }
}
