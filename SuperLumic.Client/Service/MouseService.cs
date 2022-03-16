using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperLumic.Abstracts;
using SuperLumic.Content;
using System;
using System.Collections.Generic;

namespace SuperLumic.Service
{
    public enum MouseStateEnum
    {
        Idle
    }

    public class MouseService : AbstractUIElement
    {
        public static MouseService Instance;
        private List<Tuple<float, MouseStateEnum, Guid>> CurrentStateRequests = new List<Tuple<float, MouseStateEnum, Guid>>();
        private Texture2D MouseIcon;
        private Dictionary<MouseStateEnum, Texture2D> MouseIcons = new Dictionary<MouseStateEnum, Texture2D>();
        private Tuple<double, double> MousePosition;

        public MouseService() : base(SuperLumic.Instance, 0, 0, 1, 1, SuperLumic.MouseLayer, SuperLumic.MouseLayer)
        {
            Instance = this;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (MousePosition != null && MousePosition.Item1 != -1)
            {
                DrawRawHeightWidth(MouseIcon, MousePosition.Item1, MousePosition.Item2, Game.MouseSize.Item1, Game.MouseSize.Item2);
            }
        }

        public void ReleaseMouseStateRequest(Guid RequestingObjectGUID)
        {
            int StartNumber = CurrentStateRequests.Count;
            for (int x = 0; x < CurrentStateRequests.Count; x++)
            {
                if (CurrentStateRequests[x].Item3 == RequestingObjectGUID)
                {
                    CurrentStateRequests.RemoveAt(x);
                    x--;
                }
            }
            if (StartNumber != CurrentStateRequests.Count)
            {
                ReEvaluateMouseIcon();
            }
        }

        public void RequestMouseStateRequest(float Layer, MouseStateEnum RequestedState, Guid RequestingObjectGUID)
        {
            CurrentStateRequests.Add(new Tuple<float, MouseStateEnum, Guid>(Layer, RequestedState, RequestingObjectGUID));
            if (CurrentStateRequests[0].Item1 < Layer)
            {
                ReEvaluateMouseIcon();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MousePosition = GetMousePosition();
        }

        protected override void LoadContent()
        {
            MouseIcon = TextureManager.Get("MouseIcon.png");
            MouseIcons.Add(MouseStateEnum.Idle, TextureManager.Get("MouseIcon.png"));
            base.LoadContent();
        }

        private int ComparerFunction(Tuple<float, MouseStateEnum, Guid> x, Tuple<float, MouseStateEnum, Guid> y)
        {
            return x.Item1.CompareTo(y.Item1);
        }

        private void ReEvaluateMouseIcon()
        {
            CurrentStateRequests.Sort(ComparerFunction);
            if (CurrentStateRequests.Count > 0)
            {
                if (!MouseIcons.TryGetValue(CurrentStateRequests[0].Item2, out MouseIcon))
                {
                    MouseIcons.TryGetValue(MouseStateEnum.Idle, out MouseIcon);
                }
            }
            else
            {
                MouseIcons.TryGetValue(MouseStateEnum.Idle, out MouseIcon);
            }
        }
    }
}