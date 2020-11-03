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

            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;

            _graphics.ToggleFullScreen();

            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;

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

            world.LoadEntities("Entities/Stage1/entityTest.yml");

            Player.Register(world);

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
