using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace PongClone
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D testTexture;

        PongBat playerOne;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = GlobalSettings.m_windowSize.X;
            _graphics.PreferredBackBufferHeight = GlobalSettings.m_windowSize.Y;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            testTexture = Content.Load<Texture2D>("Textures/PongBat");

            playerOne = new PongBat(Content.Load<Texture2D>("Textures/PongBat"),
                new Vector2(30, GlobalSettings.m_windowSize.Y / 2 -
                Content.Load<Texture2D>("Textures/PongBat").Height / 2),
                new Vector2(Content.Load<Texture2D>("Textures/PongBat").Width / 2,
                Content.Load<Texture2D>("Textures/PongBat").Height / 2));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            playerOne.DrawMe(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
