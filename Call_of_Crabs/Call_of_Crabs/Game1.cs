using Call_of_Crabs.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Call_of_Crabs
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        EGameState currentGameState = EGameState.MainMenu;
        EGameState previouseGameState = EGameState.None;

        IGameState gameState;

        public Game1()
        {
            //dööp
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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
            Sound.Load(Content);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void HandleGameState()
        {
            switch(currentGameState)
            {
                case EGameState.MainMenu:
                    gameState = new MainMenu();
                    break;
                case EGameState.InGame:
                    gameState = new InGame();
                    break;
                case EGameState.Lost:
                    gameState = new Lost();
                    break;
                default:
                    Exit();
                    gameState = null;
                    break;
            }

            if(gameState != null)
            {
                gameState.LoadContent(Content);
                gameState.Initialize(GraphicsDevice);
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                currentGameState = EGameState.None;
            }

            Controls.Update(gameTime);

            if (currentGameState != previouseGameState)
                HandleGameState();

            if (gameState != null)
            {
                previouseGameState = currentGameState;
                currentGameState = gameState.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            
            gameState.Draw(spriteBatch);
            

            base.Draw(gameTime);
        }
    }
}
