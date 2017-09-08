using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MSDF
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        Vector2 screenBorder;
        Vector2 screenCenter;

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            screenCenter = (screenBorder = new Vector2(graphics.PreferredBackBufferWidth,
                                                       graphics.PreferredBackBufferHeight)) / 2;

            base.Initialize();
        }

        Texture2D BG;
        Texture2D[] MSDFTextures;
        Vector2 MSDFTextureSize;

        Effect MSDFshader;

        int _currentTexture;
        int currentTexture
        {
            get { return _currentTexture; }
            set {
                _currentTexture = value;
                if (_currentTexture < 0) _currentTexture = MSDFTextures.Length - 1;
                if (_currentTexture > MSDFTextures.Length - 1) _currentTexture = 0;

                MSDFTextureSize = new Vector2(MSDFTextures[_currentTexture].Width,
                                              MSDFTextures[_currentTexture].Height);
            }
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //@@@@ IMPORTANT : Your MSDF texture must have disable 'ColorKeyEnabled' in Content Pipeline Tool. @@@@
            MSDFTextures = new Texture2D[]
            {
                Content.Load<Texture2D>("MSDFTexture_16xA"),
                Content.Load<Texture2D>("MSDFTexture_128xA"),
                Content.Load<Texture2D>("MSDFTexture_CretaIcon"),
                Content.Load<Texture2D>("test"),
                Content.Load<Texture2D>("U+00242"),
                Content.Load<Texture2D>("U+0024"),
                Content.Load<Texture2D>("MSDFTexture_FontAtlas"),
                Content.Load<Texture2D>("MSDFTexture_FontAtlas2"),
                Content.Load<Texture2D>("MSDFTexture_FontAtlas3")
            };

            BG = Content.Load<Texture2D>("BG");

            MSDFshader = Content.Load<Effect>("MSDFShader");

            currentTexture = 0;

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

        bool isPressed = false;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            KeyboardState state = Keyboard.GetState();

            if (isPressed && state.GetPressedKeys().Length == 0)
                isPressed = false;

            if (!isPressed) {
                if (state.IsKeyDown(Keys.A))
                    currentTexture--;
                else if (state.IsKeyDown(Keys.D))
                    currentTexture++;

                if (state.GetPressedKeys().Length != 0)
                    isPressed = true;
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
            spriteBatch.Begin();

            spriteBatch.Draw(BG, BG.Bounds, Color.White);
            
            spriteBatch.Draw(MSDFTextures[currentTexture],
                             MSDFTextures[currentTexture].Bounds,
                             Color.White);

            spriteBatch.End();

            spriteBatch.Begin(effect:MSDFshader);

            MouseState state = Mouse.GetState();
            Vector2 mousePosition = new Vector2(state.X, state.Y);

            Vector2 scale = (mousePosition - screenCenter) / screenBorder * 10;

            MSDFshader.Parameters["pxRange"].SetValue(scale.Y);                             //best value is 5.
            MSDFshader.Parameters["textureSize"].SetValue(MSDFTextureSize);                 //MSDF shader needs texture size.

                                                                                            //MSDF shader will lerping with this colors.
            MSDFshader.Parameters["bgColor"].SetValue(new Vector4(1, 1, 1, 1));
            MSDFshader.Parameters["fgColor"].SetValue(new Vector4(0, 0.5f, 1, 1));

            scale = MSDFTextureSize * scale.X;

            spriteBatch.Draw(MSDFTextures[currentTexture], 
                             new Rectangle((int)(screenCenter.X - scale.X / 2),
                                           (int)(screenCenter.Y - scale.Y / 2),
                                           (int)scale.X,
                                           (int)scale.Y),
                             Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
