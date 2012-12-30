using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Flakcore.Utils;

namespace Flakcore.Display
{
    public class Camera
    {
        public Viewport viewport { get; private set; }
        private Vector2 position;
        private float rotation;
        private float zoom;

        private Matrix transformMatrix;

        public Node followNode { get; set; }

        public Camera(int x, int y, int width, int height)
        {
            position = Vector2.Zero;
            zoom = 1f;
            rotation = 0;
            viewport = new Viewport(x, y, width, height);
        }

        public void resetViewport(int x, int y, int width, int height)
        {
            viewport = new Viewport(x, y, width, height);
        }

        public Matrix getTransformMatrix()
        {
            transformMatrix =
               Matrix.CreateTranslation(new Vector3((int)-position.X, (int)-position.Y, 0)) *
               Matrix.CreateRotationZ(rotation) *
               Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
               Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f,
                   viewport.Height * 0.5f, 0));

            return transformMatrix;
        }

        public void update(GameTime gameTime)
        {
            if (followNode != null)
            {
                position.Y = followNode.Position.Y + followNode.Height / 2;
                position.X = followNode.Position.X + followNode.Width / 2;
            }

            position.X = Math.Max(GameManager.ScreenSize.X / 2, position.X);
            position.Y = Math.Max(GameManager.ScreenSize.Y / 2, position.Y);
        }
    }
}
