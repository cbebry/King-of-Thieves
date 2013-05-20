using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.usr.local
{
    class EditorMode : Gears.Navigation.MenuReadyGameState
    {
        private Forms.Map_Editor.EditorComponents _componentEditor = new Forms.Map_Editor.EditorComponents();
        private Forms.Map_Editor.EditorTiles _tileEditor = new Forms.Map_Editor.EditorTiles();
        private Actors.CComponent _controlManager = new Actors.CComponent();

        public EditorMode()
        {
            _componentEditor.Visible = true;
            _tileEditor.Visible = true;

            _controlManager.root = new Actors.Controllers.CEditorInputController();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _controlManager.Update(gameTime);

            if (((Actors.Controllers.CEditorInputController)_controlManager.root)._shutDown)
            {
                _componentEditor.Visible = false;
                _tileEditor.Visible = false;
                Gears.Cloud.Master.Pop();
            }
            
        }
    }
}
