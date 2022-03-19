using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperLumic.Abstracts;
using SuperLumic.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SuperLumic.Service
{
    public enum MouseIconStateEnum
    {
        Idle
    }
    public enum MouseButtonStateEnum
    {
        None,
        Clicked,
        Held
    }

    public class MouseService : AbstractUIElement
    {
        public static Tuple<double, double> CurrentMousePosition = new Tuple<double, double>(0, 0);
        public static Tuple<double, double> CurrentMousePositionIncludingPillarBoxes = new Tuple<double, double>(0, 0);
        public static MouseButtonStateEnum M1State = MouseButtonStateEnum.None;
        public static MouseButtonStateEnum M2State = MouseButtonStateEnum.None;
        public static MouseService Instance;
        private List<Tuple<float, MouseIconStateEnum, Guid>> CurrentStateRequests = new List<Tuple<float, MouseIconStateEnum, Guid>>();
        private Texture2D MouseIcon;
        private Dictionary<MouseIconStateEnum, Texture2D> MouseIcons = new Dictionary<MouseIconStateEnum, Texture2D>();
        public static long M1HoldTime = 0;
        public static long M2HoldTime = 0;

        public MouseService() : base(SuperLumic.Instance, 0, 0, 1, 1, SuperLumic.MouseLayer, SuperLumic.MouseLayer)
        {
            Instance = this;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (CurrentMousePositionIncludingPillarBoxes != null && CurrentMousePositionIncludingPillarBoxes.Item1 != -1)
            {
                DrawRawHeightWidth(MouseIcon, CurrentMousePositionIncludingPillarBoxes.Item1, CurrentMousePositionIncludingPillarBoxes.Item2, Game.MouseSize.Item1, Game.MouseSize.Item2);
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

        public void RequestMouseStateRequest(float Layer, MouseIconStateEnum RequestedState, Guid RequestingObjectGUID)
        {
            CurrentStateRequests.Add(new Tuple<float, MouseIconStateEnum, Guid>(Layer, RequestedState, RequestingObjectGUID));
            if (CurrentStateRequests[0].Item1 < Layer)
            {
                ReEvaluateMouseIcon();
            }
        }
        Stopwatch M1StopWatch = new Stopwatch();
        Stopwatch M2StopWatch = new Stopwatch();
        MouseState? M1OnClickMouseState;
        MouseState? M2OnClickMouseState;

        private new Tuple<double, double> GetMousePosition()
        {
            MouseState MousePosition = Mouse.GetState();
            if (MousePosition.Position.X > RealPixelX && MousePosition.Position.X < RealPixelX + RealPixelWidth && MousePosition.Position.Y > RealPixelY && MousePosition.Position.Y < RealPixelHeight - RealPixelY)
            {
                return new Tuple<double, double>(MousePosition.Position.X - RealPixelX + SuperLumic.Instance.PillarBoxXOffset, MousePosition.Position.Y - RealPixelY - SuperLumic.Instance.PillarBoxYOffset);
            }
            return new Tuple<double, double>(-1, -1);// not in the area, return -1,-1
        }
        private Tuple<double, double> GetMousePositionWithPillar()
        {
            MouseState MousePosition = Mouse.GetState();
            if (MousePosition.Position.X > RealPixelX && MousePosition.Position.X < RealPixelX + RealPixelWidth + SuperLumic.Instance.PillarBoxXOffset * 2 && MousePosition.Position.Y > RealPixelY && MousePosition.Position.Y < RealPixelHeight - RealPixelY + SuperLumic.Instance.PillarBoxYOffset * 2)
            {
                return new Tuple<double, double>((MousePosition.Position.X - RealPixelX - SuperLumic.Instance.PillarBoxXOffset) / RealPixelWidth, (MousePosition.Position.Y - RealPixelY - SuperLumic.Instance.PillarBoxYOffset) / RealPixelHeight);
            }
            return new Tuple<double, double>(-1, -1);// not in the area, return -1,-1
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (SuperLumic.Instance.IsActive)
            {
                CurrentMousePosition = GetMousePosition();
                CurrentMousePositionIncludingPillarBoxes = GetMousePositionWithPillar();
                MouseState mousestate = Mouse.GetState();
                switch (mousestate.LeftButton)
                {
                    case ButtonState.Pressed:
                        if (M1OnClickMouseState == null)
                        {
                            M1OnClickMouseState = mousestate;
                            M1StopWatch.Start();
                        }
                        else
                        {
                            if (M1StopWatch.ElapsedMilliseconds > 150)
                            {
                                M1State = MouseButtonStateEnum.Held;
                                M1HoldTime = M1StopWatch.ElapsedMilliseconds;
                            }
                        }
                        break;
                    case ButtonState.Released:
                        if (M1OnClickMouseState != null)
                        {
                            if (M1State == MouseButtonStateEnum.None)
                            {
                                if (M1StopWatch.ElapsedMilliseconds <= 150)
                                {
                                    M1State = MouseButtonStateEnum.Clicked;
                                }
                                else
                                {
                                    M1State = MouseButtonStateEnum.None;
                                }
                            }
                            M1OnClickMouseState = null;
                            M1StopWatch.Reset();
                        }
                        else
                        {
                            M1State = MouseButtonStateEnum.None;
                        }
                        break;
                }
                switch (mousestate.RightButton)
                {
                    case ButtonState.Pressed:
                        if (M2OnClickMouseState == null)
                        {
                            M2OnClickMouseState = mousestate;
                            M2StopWatch.Start();
                        }
                        else
                        {
                            if (M2StopWatch.ElapsedMilliseconds > 150)
                            {
                                M2State = MouseButtonStateEnum.Held;
                                M2HoldTime = M2StopWatch.ElapsedMilliseconds;
                            }
                        }
                        break;
                    case ButtonState.Released:
                        if (M2State == MouseButtonStateEnum.None)
                        {
                            if (M2StopWatch.ElapsedMilliseconds <= 150)
                            {
                                M2State = MouseButtonStateEnum.Clicked;
                            }
                        }
                        else
                        {
                            M2State = MouseButtonStateEnum.None;
                        }
                        M2OnClickMouseState = null;
                        break;
                }
            }
        }

        protected override void LoadContent()
        {
            MouseIcon = TextureManager.Get("MouseIcon.png");
            MouseIcons.Add(MouseIconStateEnum.Idle, TextureManager.Get("MouseIcon.png"));
            base.LoadContent();
        }

        private int ComparerFunction(Tuple<float, MouseIconStateEnum, Guid> x, Tuple<float, MouseIconStateEnum, Guid> y)
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
                    MouseIcons.TryGetValue(MouseIconStateEnum.Idle, out MouseIcon);
                }
            }
            else
            {
                MouseIcons.TryGetValue(MouseIconStateEnum.Idle, out MouseIcon);
            }
        }
    }
}