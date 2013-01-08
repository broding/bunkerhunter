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
        private Vector2 Position;
        private float rotation;
        private float zoom;

        private Matrix transformMatrix;

        public Node followNode { get; set; }

        public Camera(int x, int y, int width, int height)
        {
            Position = Vector2.Zero;
            zoom = 1f;
            rotation = 0;
            viewport = new Viewport(x, y, width, height);
        }

        public void resetViewport(int x, int y, int width, int height)
        {
            viewport = new Viewport(x, y, width, height);
        }

        public Matrix GetTransformMatrix()
        {
            transformMatrix =
               Matrix.CreateTranslation(new Vector3((int)-Position.X, (int)-Position.Y, 0)) *
               Matrix.CreateRotationZ(rotation) *
               Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
               Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f,
                   viewport.Height * 0.5f, 0));

            return transformMatrix;
        }

        public Vector2 TransformPosition(Vector2 position)
        {
            return position - new Vector2(this.Position.X - GameManager.ScreenSize.X / 2, this.Position.Y - GameManager.ScreenSize.Y / 2); ;
        }

        public void update(GameTime gameTime)
        {
            if (followNode != null)
            {
                Position.Y = followNode.Position.Y + followNode.Height / 2;
                Position.X = followNode.Position.X + followNode.Width / 2;
            }

            Position.X = Math.Max((GameManager.ScreenSize.X - GameManager.LevelBorderSize.X) / 2, Position.X);
            Position.Y = Math.Max((GameManager.ScreenSize.Y - GameManager.LevelBorderSize.Y) / 2, Position.Y);

            //this.position.X = (float)Math.Round(this.position.X);
            //this.position.Y = (float)Math.Round(this.position.Y);
        }
    }
}
