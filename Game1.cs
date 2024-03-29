using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MonoGameEngine
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player _player;
        private Texture2D _background;
        private Rectangle _scenePosition;
        private Camera _camera;
        private Song backgroundMusic;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _player = new Player();
            _player.LoadContent(Content);
            _background = Content.Load<Texture2D>("background2");

            // Carrega a música
            backgroundMusic = Content.Load<Song>("Music");
            MediaPlayer.IsRepeating = true; // Opcional: define se a música será repetida continuamente
            MediaPlayer.Volume = 0.5f; // Opcional: define o volume da música (0.0f a 1.0f)

            // Inicia a reprodução da música
            MediaPlayer.Play(backgroundMusic);
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            Globals.SCREEN_WIDTH = _graphics.PreferredBackBufferWidth;
            Globals.SCREEN_HEIGHT = _graphics.PreferredBackBufferHeight;

            _scenePosition = new Rectangle(
                0,
                Globals.SCREEN_HEIGHT - _background.Height,
                _background.Width, 
                _background.Height);

            _camera = new Camera();
            _player.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Obter o estado atual do teclado
            KeyboardState keyboardState = Keyboard.GetState();

            // Verificar e processar a entrada do jogador
            Vector2 playerOffset = _player.Update((float)gameTime.ElapsedGameTime.TotalSeconds, keyboardState);

            // Atualizar a câmera com o deslocamento do jogador
            _camera.Update(playerOffset, ref _scenePosition);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_background, _scenePosition, Color.White);
            _player.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
