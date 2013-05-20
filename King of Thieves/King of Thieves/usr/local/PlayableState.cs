using Gears.Navigation;
using King_of_Thieves.Actors;
using Gears.Cloud;
using King_of_Thieves.Input;

namespace King_of_Thieves.usr.local
{
    class PlayableState : MenuReadyGameState
    {
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            CMasterControl.mapManager.drawMap();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            CMasterControl.mapManager.updateMap(gameTime);
        }
    }
}
