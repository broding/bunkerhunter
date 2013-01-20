using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Display;
using Microsoft.Xna.Framework;
using Flakcore.Utils;

namespace Bunker_Hunter.UI
{
    class DamageIndicator : Node
    {
        private static Pool<DamageText> Pool;
        private static Layer UILayer;

        public static void Initialize(Layer uiLayer)
        {
            DamageIndicator.UILayer = uiLayer;
            DamageIndicator.Pool = new Pool<DamageText>(50, false, DamageIndicator.IsTextActive, DamageIndicator.SetupText); 
        }

        private static bool IsTextActive(DamageText text)
        {
            return !text.Active;
        }

        private static DamageText SetupText()
        {
            DamageText text = new DamageText();
            text.Deactivate();

            DamageIndicator.UILayer.AddChild(text);

            return text;
        }

        public static void Show(Node node, int damage)
        {
            DamageText text = DamageIndicator.Pool.New();
            text.Position = node.WorldPosition;
            text.Activate();
        }

        public static void Show(Node node, int damage, Color color)
        {
        }
    }
}
