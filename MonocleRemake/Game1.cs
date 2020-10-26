using ECS;
using ECS.Monocle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonocleRemake.Monocle.Services;
using System;

namespace MonocleRemake
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private World world;

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

            World.spriteBatch = _spriteBatch;
            World.content = Content;
            World.GraphicsDevice = GraphicsDevice;

            Player.Register(world);

            world.AddServiceGroup("draw");
            world.AddServiceGroup("update");

            world.AddService(new LifetimeDespawner(), "update");
            world.AddService(new ParticleSpawner(), "update");
            world.AddService(new PlayerController(), "update");
            world.AddService(new PlayerAnimator(), "update");
            world.AddService(new DirectionMover(), "update");
            world.AddService(new Animator(), "update");
            world.AddService(new SpriteRendererService(), "draw");

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
            GraphicsDevice.Clear(Color.ForestGreen);
            // TODO: Add your drawing code here

            world.Run("draw");

            base.Draw(gameTime);
        }


    }
}
