using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EnigmaUtils;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;

namespace PongClone
{
    enum GameState
    {
        Title,
        Settings,
        Game,
        GameOver
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D testTexture;

        PongBat playerOne;

        RenderTarget2D titleCanvas;
        RenderTarget2D settingsCanvas;
        RenderTarget2D playCanvas;
        RenderTarget2D gameOverCanvas;

        KeyboardState kbState, oldKbState;
        MouseState mouseState;
        Point adjustedMousePos;
        Rectangle mouseRect;

        TextRenderer titleText;
        TextRenderer settingsButtonFont;

        GameState gameState = GameState.Title;
        GameSettings settings;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

#if DEBUG
        public static Texture2D debugPixel;
#endif

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            settings = new GameSettings(_graphics);

            settings.WindowSizes.Add(new Point(800, 800));
            settings.WindowSizes.Add(new Point(1024, 1024));
            settings.WindowSizes.Add(new Point(1920, 1080));

            settings.SetScreenRes(0);


            _graphics.SynchronizeWithVerticalRetrace = true;
            IsFixedTimeStep = true;

            titleCanvas = new RenderTarget2D(_graphics.GraphicsDevice, 1920, 1080);
            settingsCanvas = new RenderTarget2D(_graphics.GraphicsDevice, 1920, 1080);
            playCanvas = new RenderTarget2D(_graphics.GraphicsDevice, 1920, 1080);
            gameOverCanvas = new RenderTarget2D(_graphics.GraphicsDevice, 1920, 1080);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

#if DEBUG
            debugPixel = Content.Load<Texture2D>("Textures/Pixel");
#endif

            // TODO: use this.Content to load your game content here

            titleText = new TextRenderer(Content.Load<SpriteFont>("Fonts/TestFont"),
                new Vector2(1920 / 2,
                1080 / 2));

            settingsButtonFont = new TextRenderer(Content.Load<SpriteFont>("Fonts/SettingsButtonFont"),
                new Vector2(1920 / 2,
                1080 / 3 * 2));

            testTexture = Content.Load<Texture2D>("Textures/ScreenTest");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            settings.UpdateMe();

            kbState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            adjustedMousePos = MousePos();
            mouseRect = new Rectangle(adjustedMousePos.X - 4, adjustedMousePos.Y - 4, 8, 8);

            switch (gameState)
            {
                case GameState.Title:
                    UpdateTitle(gameTime);
                    break;
                case GameState.Settings:
                    UpdateSettings();
                    break;
                case GameState.Game:
                    UpdateGame();
                    break;
                case GameState.GameOver:
                    UpdateGameOver();
                    break;
            }

            if (kbState.IsKeyDown(Keys.Left) && oldKbState.IsKeyUp(Keys.Left))
            {
                settings.SetScreenRes(settings.CurrentRes - 1);
            }
            else if (kbState.IsKeyDown(Keys.Right) && oldKbState.IsKeyUp(Keys.Right))
            {
                settings.SetScreenRes(settings.CurrentRes + 1);
            }

            if (kbState.IsKeyDown(Keys.Down) && oldKbState.IsKeyUp(Keys.Down))
            {
                Debug.WriteLine("Down has been pressed!");
                settings.SetFullScreen(false);
            }
            if (kbState.IsKeyDown(Keys.Up) && oldKbState.IsKeyUp(Keys.Up))
            {
                Debug.WriteLine("Up has been pressed!");
                settings.SetFullScreen(true);
            }

            Debug.WriteLine(mouseState.Position);

            oldKbState = kbState;

            base.Update(gameTime);
        }

        private void UpdateTitle(GameTime gt)
        {
            titleText.UpdateMe(gt);

            if (titleText.ButtonBounds.Intersects(mouseRect))
            {
                titleText.Tint = Color.Red;
            }
            else
            {
                titleText.Tint = Color.White;
            }

            Debug.WriteLine(settingsButtonFont.ButtonBounds);
        }

        private void UpdateSettings()
        {

        }

        private void UpdateGame()
        {

        }

        private void UpdateGameOver()
        {

        }

        private Point MousePos()
        {
            //Get the current window size
            int windowWidth = settings.WindowSizes[settings.CurrentRes].X;
            int windowHeight = settings.WindowSizes[settings.CurrentRes].Y;

            //Target Render size
            int renderWidth = 1920;
            int renderHeight = 1080;

            //Calculate scaling
            float scaleX = (float)renderWidth / windowWidth;
            float scaleY = (float)renderHeight / windowHeight;

            //Scale the mouse position
            int correctedMouseX = (int)(mouseState.X * scaleX);
            int correctedMouseY = (int)(mouseState.Y * scaleY);

            return new Point(correctedMouseX, correctedMouseY);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            switch (gameState)
            {
                case GameState.Title:
                    DrawTitle();
                    break;
                case GameState.Settings:
                    DrawSettings();
                    break;
                case GameState.Game:
                    DrawGame();
                    break;
                case GameState.GameOver:
                    DrawGameOver();
                    break;
            }

            base.Draw(gameTime);
        }

        private void DrawTitle()
        {
            GraphicsDevice.SetRenderTarget(titleCanvas);

            _spriteBatch.Begin();

#if DEBUG
            _spriteBatch.Draw(debugPixel, titleText.ButtonBounds, Color.White * 0.5f);
            _spriteBatch.Draw(debugPixel, mouseRect, Color.White * 0.5f);
#endif

            titleText.DrawString(_spriteBatch, "Pong Clone");
            settingsButtonFont.DrawString(_spriteBatch, "Settings");
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin();
            _spriteBatch.Draw(titleCanvas, GraphicsDevice.Viewport.Bounds, null, Color.White);
            _spriteBatch.End();

        }

        private void DrawSettings()
        {
            GraphicsDevice.SetRenderTarget(settingsCanvas);

            _spriteBatch.Begin();
            
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin();
            
            _spriteBatch.End();
        }

        private void DrawGame()
        {
            GraphicsDevice.SetRenderTarget(playCanvas);

            _spriteBatch.Begin();

            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin();

            _spriteBatch.End();
        }

        private void DrawGameOver()
        {
            GraphicsDevice.SetRenderTarget(gameOverCanvas);

            _spriteBatch.Begin();

            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin();

            _spriteBatch.End();
        }
    }
}
