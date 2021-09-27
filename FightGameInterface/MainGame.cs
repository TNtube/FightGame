﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FightGameInterface {
    public class MainGame : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _lineText;

        private Texture2D[] _tankSprites;

        private RenderTarget2D _scaleupTarget;
        const int RENDER_TARGET_WIDTH = 640;
        const int RENDER_TARGET_HEIGHT = 360;
        private float _upScaleAmount;

        public MainGame() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // create 1x1 texture for line drawing
            _lineText = new Texture2D(GraphicsDevice, 1, 1);
            _lineText.SetData(
                new [] { Color.White });// fill the texture with white

            _tankSprites = new[] {
                Content.Load<Texture2D>("assets/tank1"),
                Content.Load<Texture2D>("assets/tank2"),
                Content.Load<Texture2D>("assets/tank3")
            };
            
            _scaleupTarget  = new RenderTarget2D(GraphicsDevice, RENDER_TARGET_WIDTH, RENDER_TARGET_HEIGHT);
            _upScaleAmount = (float)GraphicsDevice.PresentationParameters.BackBufferWidth / RENDER_TARGET_WIDTH;


        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            RenderTarget2D rt = new RenderTarget2D(GraphicsDevice, 200, 200);
            
            GraphicsDevice.SetRenderTarget(_scaleupTarget);
            GraphicsDevice.Clear(Color.SeaGreen);
            _spriteBatch.Begin(
                SpriteSortMode.BackToFront,
                BlendState.AlphaBlend
            );
            
            _spriteBatch.Draw(_tankSprites[0], new Vector2(20, 20), Color.White);
            DrawInterface(Color.White);
            _spriteBatch.End();
            
            
            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(
                SpriteSortMode.Texture,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null);
            _spriteBatch.Draw(_scaleupTarget, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, _upScaleAmount, SpriteEffects.None, 0.0f);
            _spriteBatch.End();
            

            base.Draw(gameTime);
        }

        private void DrawInterface(Color color) {
            const int lineWidth = 3;
            const int offset = 5;
            
            Vector2 topLeft = new (offset, offset);
            Vector2 topRight = new( RENDER_TARGET_WIDTH - offset, offset);
            Vector2 bottomLeft = new (offset, RENDER_TARGET_HEIGHT - offset);
            Vector2 bottomRight = new (RENDER_TARGET_WIDTH - offset, RENDER_TARGET_HEIGHT - offset);

            Utils.Draw.DrawLine(_spriteBatch, _lineText, topLeft, topRight, color, lineWidth);
            Utils.Draw.DrawLine(_spriteBatch, _lineText, topRight, bottomRight, color, lineWidth);
            Utils.Draw.DrawLine(_spriteBatch, _lineText, bottomRight, bottomLeft, color, lineWidth);
            Utils.Draw.DrawLine(_spriteBatch, _lineText, bottomLeft, topLeft, color, lineWidth);
            
            Utils.Draw.DrawLine(_spriteBatch, _lineText, 
                new Vector2(bottomLeft.X, bottomLeft.Y - (bottomLeft - topLeft).Y * 0.3f),
                new Vector2(bottomRight.X, bottomRight.Y - (bottomRight - topRight).Y * 0.3f),
                color, lineWidth
                );
        }
    }
}