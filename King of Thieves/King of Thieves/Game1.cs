using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using King_of_Thieves.Graphics;
using King_of_Thieves.Actors;

using Gears.Cloud;
using King_of_Thieves.usr.local;
using King_of_Thieves.Input;

namespace King_of_Thieves
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        CSprite testSprite = null;
        CActorTest actorTest;
        Actors.Player.CPlayer player;
        CComponent compTest = new CComponent();
        Actors.Menu.CMenu testMenu;
        CComponent menuComo = new CComponent();

        //Screen Resolution defaults
        private const int ScreenWidth = 320;
        private const int ScreenHeight = 240;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this); 
            //graphics.PreferredBackBufferHeight = ScreenHeight
            //graphics.PreferredBackBufferWidth = ScreenWidth;
            //graphics.SynchronizeWithVerticalRetrace = true;
            //graphics.ApplyChanges();
            
            Content.RootDirectory = @"Content";

            IsFixedTimeStep = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Register our ContentManager
            ContentButler.setGame(this);

            //Setup screen display/graphics device
            ViewportHandler.SetScreen(ScreenWidth, ScreenHeight);
            graphics.PreferredBackBufferWidth = ViewportHandler.GetWidth();
            graphics.PreferredBackBufferHeight = ViewportHandler.GetHeight();
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Graphics.CGraphics.acquireGraphics(ref graphics);

            Master.SetClearColor(Color.CornflowerBlue);

            CTextures.init(Content);

            Master.Push(new Testbed(ref compTest, ref menuComo));

            Master.GetInputManager().AddInputHandler(new CInput());

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Graphics.CGraphics.spriteBatch = spriteBatch;

            // TODO: use this.Content to load your game content here
           // Graphics.CTextureDict.init(Content);
            testSprite = new CSprite(new CTextureAtlas(Content.Load<Texture2D>("test"), 19, 23, 0));
            

            actorTest = new CActorTest("Test");
            player = new Actors.Player.CPlayer();
            compTest.root = player;

            CMasterControl.audioPlayer.soundBank.Add("04_-_Phantom_Ganon", new Sound.CSound(Content.Load<Song>("04_-_Phantom_Ganon"), false, 0));
            CMasterControl.audioPlayer.soundBank.Add("cursor", new Sound.CSound(Content.Load<SoundEffect>("cursor"),true));
            CMasterControl.audioPlayer.soundBank.Add("lttp_heart", new Sound.CSound(Content.Load<SoundEffect>("lttp_heart")));

            testMenu = new Actors.Menu.CMenu("MenuTest", new CSprite(CTextures.texture("menu")), 120, CMasterControl.audioPlayer.soundBank["04_-_Phantom_Ganon"], CMasterControl.audioPlayer.soundBank["cursor"]);
            menuComo.root = testMenu;

            //We really, really need an object database to just do this shit automagically.
            CMasterControl.mapList.Add("TestMap",new Map.MTestMap("TestMap", Map.MAPTYPES.ROOT));
            //CMasterControl.mapList.Add("TestMapPart1", new Map.MTestMap("TestMapPart1", Map.MAPTYPES.CHUNK));
            //Input.CMrMapIO.Save(CMasterControl.mapList["TestMap"].Map, "testmap.xml");
            //CMasterControl.audioPlayer.song = new Sound.CSound(Content.Load<Song>("04_-_Phantom_Ganon"), false, 0);
            //CMasterControl.audioPlayer.addSfx(new Sound.CSound(Content.Load<Song>("04_-_Phantom_Ganon"), false, 0));




        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            CMasterControl.audioPlayer.stop();
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            CMasterControl.gameTime = gameTime;

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }

            Master.Update(gameTime);

            
            //CMasterControl.audioPlayer.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Master.GetClearColor());

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            Master.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
