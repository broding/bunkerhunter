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
        public static float Gravity { get; set; }
        public static Input Input { get; private set; }
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static ContentManager Content { get; private set; }
        public static Vector2 ScreenSize { get; private set; }
        public static Vector2 LevelBorderSize { get; set; }

        public static FontController FontController { get; private set; }

        public static Layer BulletLayer;
        public static Layer UILayer;

        public static int UpdateCalls;

        private static Core Core;
        private static Rectangle _worldBounds;
        public static Rectangle WorldBounds 
        { 
            get { return _worldBounds; }
            set { 
                _worldBounds = value;  
                Core.setupQuadTree(); 
            } 
        }
        public static Camera currentDrawCamera;

        public static SpriteFont fontDefault;

        public static void initialize(Vector2 screenSize, GraphicsDeviceManager graphics, ContentManager content, Core core)
        {
            GameManager.Gravity = 14;
            GameManager.Graphics = graphics;
            GameManager.Content = content;
            GameManager.Input = new Input();
            GameManager.ScreenSize = screenSize;
            GameManager.Core = core;
            GameManager.WorldBounds = Rectangle.Empty;
            GameManager.FontController = new FontController();

            //TODO (BR): needs to be done more nice
            fontDefault = content.Load<SpriteFont>("fontDefault");

        }

        /// <summary>
        /// Used to switch between states; old state gets deleted
        /// </summary>
        /// <param name="state"></param>
        public static void SwitchState(Type state)
        {
            GameManager.SwitchState(state, StateTransition.IMMEDIATELY, StateTransition.IMMEDIATELY);
        }

        public static void SwitchState(Type state, StateTransition startTransition, StateTransition endTransition)
        {
            Core.SwitchState(state, startTransition, endTransition);
        }

        public static void addCamera()
        {
            Core.Cameras.Add(new Camera(0, 0, (int)ScreenSize.X, (int)ScreenSize.Y));

            recalculateCameras();
        }

        public static Camera getCamera(int index)
        {
            return Core.Cameras.ElementAt(index);
        }

        private static void recalculateCameras()
        {
            int cameraWidth = (int)ScreenSize.X / Core.Cameras.Count;

            for (int i = 0; i < Core.Cameras.Count; i++)
            {
                Core.Cameras.ElementAt(i).resetViewport(cameraWidth * i, 0, cameraWidth, (int)ScreenSize.Y);
            }
        }

        public int totalCameras()
        {
            return Core.Cameras.Count;
        }

        public static void collide(Node node, string collideGroup)
        {
            Core.CollisionSolver.addCollision(node, collideGroup, null, null);
        }

        public static void collide(Node node, string collideGroup, Action<Node, Node> callback, Func<Node, Node, bool> checker)
        {
            Core.CollisionSolver.addCollision(node, collideGroup, callback, checker);
        }

        public static void collide(Node node, string collideGroup, Action<Node, Node> callback)
        {
            Core.CollisionSolver.addCollision(node, collideGroup, callback, null);
        }

    }
}
