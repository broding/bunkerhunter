using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Flakcore.Display;
using Microsoft.Xna.Framework.Content;
using Flakcore.Utils;
using Flakcore.Physics;

namespace Flakcore
{
    public class Core
    {
        private State CurrentState;
        public List<Camera> Cameras { get; private set; }

        private QuadTree CollisionQuad;
        public CollisionSolver CollisionSolver { get; private set; }

        public Core(Vector2 screenSize, GraphicsDeviceManager graphics, ContentManager content)
        {
            GameManager.initialize(screenSize, graphics, content, this);

            graphics.PreferredBackBufferWidth = (int)screenSize.X;
            graphics.PreferredBackBufferHeight = (int)screenSize.Y;
            graphics.ApplyChanges();

            this.Cameras = new List<Camera>();
            Camera camera = new Camera(0,0,(int)screenSize.X, (int)screenSize.Y);
            this.Cameras.Add(camera);
            GameManager.currentDrawCamera = camera;

            GameManager.worldBounds = new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y);

            setupQuadTree();

        }

        public void Update(GameTime gameTime)
        {
            resetCollisionQuadTree();
                
            this.CurrentState.Update(gameTime);
            this.CollisionSolver.resolveCollisions(gameTime);
            this.CurrentState.PostUpdate(gameTime);

            GameManager.Input.update();

            foreach (Camera camera in Cameras)
                camera.update(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            GameManager.Graphics.GraphicsDevice.Clear(CurrentState.BackgroundColor);

            foreach (Camera camera in Cameras)
            {
                GameManager.currentDrawCamera = camera;
                GameManager.Graphics.GraphicsDevice.Viewport = camera.viewport;
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, null, null);
                this.CurrentState.draw(spriteBatch);
                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, null, null, null, camera.getTransformMatrix());
                drawCollisionQuad(spriteBatch);
                spriteBatch.End();

                spriteBatch.Begin();
                Debug.draw(spriteBatch);
                spriteBatch.DrawString(GameManager.fontDefault, "FPS: " + Math.Round(1 /gameTime.ElapsedGameTime.TotalSeconds), new Vector2(20, 20), Color.Black);
                spriteBatch.End();

            }
        }

        public void switchState(State state)
        {
            this.CurrentState = null;
            this.CurrentState = state;
        }

        private void resetCollisionQuadTree()
        {
            CollisionQuad.clear();

            List<Node> children = CurrentState.getAllChildren(new List<Node>());

            foreach (Node child in children)
            {
                CollisionQuad.insert(child);
            }
        }

        private void drawCollisionQuad(SpriteBatch spriteBatch)
        {
            Texture2D blank = new Texture2D(GameManager.Graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[]{Color.White});

            List<BoundingRectangle> quads = CollisionQuad.getAllQuads(new List<BoundingRectangle>());

            foreach (BoundingRectangle quad in quads)
            {
                // left
                Debug.DrawLine(spriteBatch, blank, 1, Color.White, new Vector2(quad.X, quad.Y), new Vector2(quad.X, quad.Y + quad.Height));
                // right
                Debug.DrawLine(spriteBatch, blank, 1, Color.White, new Vector2(quad.X + quad.Width, quad.Y), new Vector2(quad.X + quad.Width, quad.Y + quad.Height));
                // top
                Debug.DrawLine(spriteBatch, blank, 1, Color.White, new Vector2(quad.X, quad.Y), new Vector2(quad.X + quad.Width, quad.Y));
                // bottom
                Debug.DrawLine(spriteBatch, blank, 1, Color.White, new Vector2(quad.X, quad.Y + quad.Height), new Vector2(quad.X + quad.Width, quad.Y + quad.Height));
            }
        }

        public void setupQuadTree()
        {
            CollisionQuad = new QuadTree(0, new BoundingRectangle(0, 0, GameManager.worldBounds.Width, GameManager.worldBounds.Height));
            CollisionSolver = new CollisionSolver(CollisionQuad);
        }
    }
}
