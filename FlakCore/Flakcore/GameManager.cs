using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Flakcore.Display;
using Flakcore.Physics;

namespace Flakcore
{
    public class GameManager
    {
        public static Input input { get; private set; }
        public static GraphicsDeviceManager graphics { get; private set; }
        public static ContentManager content { get; private set; }
        public static Vector2 screenSize { get; private set; }
        private static Core core;
        private static Rectangle _worldBounds;
        public static Rectangle worldBounds 
        { 
            get { return _worldBounds; }
            set { 
                _worldBounds = value;  
                core.setupQuadTree(); 
            } 
        }
        public static Camera currentDrawCamera;

        public static SpriteFont fontDefault;

        public static void initialize(Vector2 screenSize, GraphicsDeviceManager graphics, ContentManager content, Core core)
        {
            GameManager.graphics = graphics;
            GameManager.content = content;
            GameManager.input = new Input();
            GameManager.screenSize = screenSize;
            GameManager.core = core;
            GameManager.worldBounds = Rectangle.Empty;

            //TODO (BR): needs to be done more nice
            fontDefault = content.Load<SpriteFont>("fontDefault");

        }

        /// <summary>
        /// Used to switch between states; old state gets deleted
        /// </summary>
        /// <param name="state"></param>
        public static void switchState(State state)
        {
            core.switchState(state);
        }

        public static void addCamera()
        {
            core.Cameras.Add(new Camera(0, 0, (int)screenSize.X, (int)screenSize.Y));

            recalculateCameras();
        }

        public static Camera getCamera(int index)
        {
            return core.Cameras.ElementAt(index);
        }

        private static void recalculateCameras()
        {
            int cameraWidth = (int)screenSize.X / core.Cameras.Count;

            for (int i = 0; i < core.Cameras.Count; i++)
            {
                core.Cameras.ElementAt(i).resetViewport(cameraWidth * i, 0, cameraWidth, (int)screenSize.Y);
            }
        }

        public int totalCameras()
        {
            return core.Cameras.Count;
        }

        public static void collide(Node node, string collideGroup)
        {
            core.CollisionSolver.addCollision(node, collideGroup, null);
        }

        public static void collide(Node node, string collideGroup, Func<Node, Node, bool> callback)
        {
            core.CollisionSolver.addCollision(node, collideGroup, callback);
        }
    }
}
