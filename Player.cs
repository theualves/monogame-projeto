using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player
{
    private Texture2D[] _images;
    private int _index;
    private Rectangle _position;
    private const float SPEED = 500;
    private const int IMAGE_WIDTH = 80;
    private const int IMAGE_HEIGHT = 80;
    private Timer _timer;
    private Rectangle _movementBounds;
    private const float JUMP_SPEED = 500; // Velocidade do pulo
    private const float GRAVITY = 1500;
    private bool isJumping = false; // Flag para indicar se o jogador está pulando
    private float jumpVelocity = 0; // Velocidade vertical do pulo
    private bool isAKeyPressed = false; // Flag para indicar se a tecla A está pressionada

    public void LoadContent(ContentManager content)
    {
        _images = new Texture2D[6]
        {
            content.Load<Texture2D>("sprite-persona/sprite1"), content.Load<Texture2D>("sprite-persona/sprite2"),
            content.Load<Texture2D>("sprite-persona/sprite3"), content.Load<Texture2D>("sprite-persona/sprite4"),
            content.Load<Texture2D>("sprite-persona/sprite5"), content.Load<Texture2D>("sprite-persona/sprite6")
        };
    }

    public void Initialize()
    {
        _index = 0;
        _position = new Rectangle
        (
            0, Globals.SCREEN_HEIGHT - IMAGE_HEIGHT - 36, // Inicializa o jogador no canto esquerdo inferior da tela
            IMAGE_WIDTH, IMAGE_HEIGHT
        );
        _timer = new Timer();
        _timer.Start(IncrementIndex, 0.075f, true);
        _movementBounds = new Rectangle
        (
            0, 0, // Limites de movimento não foram definidos
            (int)(Globals.SCREEN_WIDTH * 0.8f), Globals.SCREEN_HEIGHT
        );
    }

    public Vector2 Update(float deltaTime, KeyboardState keyboardState)
    {
        Vector2 direction = Vector2.Zero;

        if (keyboardState.IsKeyDown(Keys.A))
        {
            direction.X = -1.0f;
            isAKeyPressed = true;
        }
        else if (keyboardState.IsKeyDown(Keys.D))
        {
            direction.X = 1.0f;
            isAKeyPressed = false;
        }

        // Define o índice da imagem com base na direção do movimento
        if (direction != Vector2.Zero)
        {
            _index = MathHelper.Clamp(_index, 0, 4); // Limita o índice até a 5ª imagem
            _timer.Update(deltaTime); // Atualiza a animação
        }
        else
        {
            // Se não houver movimento, exibe a 6ª imagem
            _index = 5;
        }

        // Adicionando o pulo
        if (keyboardState.IsKeyDown(Keys.Space) && !isJumping)
        {
            isJumping = true;
            jumpVelocity = -JUMP_SPEED;
        }

        // Aplicando a gravidade ao pulo
        if (isJumping)
        {
            jumpVelocity += GRAVITY * deltaTime;
            _position.Y += (int)(jumpVelocity * deltaTime);

            // Verificando se o jogador atingiu o chão
            if (_position.Y >= Globals.SCREEN_HEIGHT - IMAGE_HEIGHT - 36)
            {
                _position.Y = Globals.SCREEN_HEIGHT - IMAGE_HEIGHT - 36;
                isJumping = false;
            }
        }

        // Movimento lateral e animação
        Vector2 offset = Vector2.Zero;
        if (direction != Vector2.Zero)
        {
            direction.Normalize();
            offset = direction * SPEED * deltaTime;

            Rectangle newPosition = _position;
            newPosition.X += (int)offset.X;
            newPosition.Y += (int)offset.Y;

            if (newPosition.X >= _movementBounds.Left && newPosition.Right <= _movementBounds.Right)
            {
                _position.X = newPosition.X;

                // Atualizar o índice apenas quando uma tecla de movimento for pressionada
                _timer.Update(deltaTime);
            }
            if (newPosition.Y >= _movementBounds.Top && newPosition.Bottom <= _movementBounds.Bottom)
            {
                _position.Y = newPosition.Y;
            }
        }

        return offset;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects spriteEffect = SpriteEffects.None; // Por padrão, não há efeito de sprite

        if (isAKeyPressed) // Se a tecla A estiver pressionada
        {
            spriteEffect = SpriteEffects.FlipHorizontally; // Inverte o sprite horizontalmente
        }

        spriteBatch.Draw(_images[_index], _position, null, Color.White, 0f, Vector2.Zero, spriteEffect, 0f);
    }

    private void IncrementIndex()
    {
        _index++;
        if (_index > 4)
        {
            _index = 0;
        }
    }
}
