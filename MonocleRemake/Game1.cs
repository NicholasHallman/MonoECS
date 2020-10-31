using ECS;
using ECS.Monocle;
using ECS.Monocle.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonocleRemake.Monocle.Entities;
using MonocleRemake.Monocle.Services;
using MonocleRemake.Monocle.Services.UI;
using System;
using System.Diagnostics;

namespace MonocleRemake
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private World world;
        private Entity fpsDisplay;
        private FrameCounter fc;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.ToggleFullScreen();

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;

            _graphics.ToggleFullScreen();

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;

            //_graphics.ToggleFullScreen();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            world = new World();

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            fc = new FrameCounter();

            World.spriteBatch = _spriteBatch;
            World.content = Content;
            World.GraphicsDevice = GraphicsDevice;

            Entity pauseButton = new Entity();
            Entity playButton = new Entity();
            Action<Entity> TogglePauseButtonOnClick = (pauseButton) => { };
            Entity cherryPetals = CherryPetalEmitter.Register(world);

            Action<Entity> TogglePlayButtonOnClick = (playButton) =>
            {
                world.RemoveEntity(playButton);
                cherryPetals = CherryPetalEmitter.Register(world);
                pauseButton = ToggleButton.Register(world,
                    Content.Load<Texture2D>("play-toggle1"),
                    Content.Load<Texture2D>("play-toggle2"),
                    new Vector2(1920 / 2, 32),
                    new Vector2(16, 16),
                    TogglePauseButtonOnClick
                );
            };

            TogglePauseButtonOnClick = (pauseButton) =>
            {
                world.RemoveEntity(pauseButton);
                world.RemoveEntity(cherryPetals);
                playButton = ToggleButton.Register(world,
                    Content.Load<Texture2D>("play-toggle3"),
                    Content.Load<Texture2D>("play-toggle4"),
                    new Vector2(1920 / 2, 32),
                    new Vector2(16, 16),
                    TogglePlayButtonOnClick
                );
            };

            

            pauseButton = ToggleButton.Register(world, 
                Content.Load<Texture2D>("play-toggle1"), 
                Content.Load<Texture2D>("play-toggle2"),
                new Vector2(1920 / 2, 32), 
                new Vector2(16, 16),
                TogglePauseButtonOnClick
            );

            

            Player.Register(world);
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    Bush.Register(world, new Vector2(1280 / 2 + (64 * i), 720 / 2 + (64 * j)));

                }
            }

            fpsDisplay = world.CreateEntity()
                .AddComponent<Label>()
                .AddComponent<Transform>();

            fpsDisplay.GetComponent<Label>().color = Color.White;
            fpsDisplay.GetComponent<Label>().text = "Hello World";
            fpsDisplay.GetComponent<Label>().spriteFont = Content.Load<SpriteFont>("Font");
            fpsDisplay.GetComponent<Transform>().position = new Vector2(100, 100);

            //

            world.AddServiceGroup("draw");
            world.AddServiceGroup("update");

            world.AddService(new ClickDetect(), "update");
            world.AddService(new LifetimeDespawner(), "update");
            world.AddService(new ParticleSpawner(), "update");
            world.AddService(new PlayerController(), "update");
            world.AddService(new PlayerAnimator(), "update");
            world.AddService(new DirectionMover(), "update");
            world.AddService(new SpreadFire(), "update");
            world.AddService(new SpriteSpinner(), "update");
            world.AddService(new Animator(), "update");
            world.AddService(new SpriteRendererService(), "draw");
            world.AddService(new FontRenderer(), "draw");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            world.Run("update");


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            fc.Update(deltaTime);

            fpsDisplay.GetComponent<Label>().text = string.Format("FPS: {0}", fc.AverageFramesPerSecond);

            world.Run("draw");



            base.Draw(gameTime);
        }


    }
}
