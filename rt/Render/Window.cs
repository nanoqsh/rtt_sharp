using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using RT.Engine;
using System;

namespace RT.Render
{
    class Window
    {
        private readonly GameWindow gameWindow;
        private readonly Controller controller;
        private Frame? frame;

        public Window(int width, int height, string title)
        {
            gameWindow = new GameWindow(width, height, GraphicsMode.Default, title, GameWindowFlags.Default)
            {
                VSync = VSyncMode.On,
                CursorVisible = false
            };

            gameWindow.Load += OnLoad;
            gameWindow.UpdateFrame += OnUpdateFrame;
            gameWindow.RenderFrame += OnRenderFrame;
            gameWindow.KeyDown += OnKeyDown;
            gameWindow.KeyUp += OnKeyUp;
            gameWindow.FocusedChanged += OnFocusedChanged;
            gameWindow.Resize += OnResize;

            controller = new Controller();
        }

        public void Run(double updateRate)
        {
            gameWindow.Run(updateRate);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            CheckOpenGLError();
            frame = new Frame();
        }

        private void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            if (gameWindow.Focused)
                controller.MouseUpdate();

            Core.Unit.Map.Update((float)e.Time, controller);
        }

        private void OnRenderFrame(object sender, FrameEventArgs e)
        {
            frame!.Draw();
            gameWindow.SwapBuffers();
        }

        private void OnKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    gameWindow.Exit();
                    break;

                case Key.F11:
                    gameWindow.WindowState = gameWindow.WindowState == WindowState.Fullscreen
                        ? WindowState.Normal
                        : WindowState.Fullscreen;
                    break;
            }

            controller.DownKey(e.Key);
            Core.Unit.Map.DownKey(e.Key, controller);
        }

        private void OnKeyUp(object sender, KeyboardKeyEventArgs e)
        {
            controller.UpKey(e.Key);
        }

        private void OnFocusedChanged(object sender, EventArgs e)
        {
            controller.MouseUpdate();
        }

        private void OnResize(object sender, EventArgs e)
        {
            frame!.Resize(gameWindow.ClientRectangle);
        }

        private static void CheckOpenGLError()
        {
            ErrorCode errCode = GL.GetError();

            if (errCode != ErrorCode.NoError)
                throw new Exception($"OpenGl error: {errCode}");
        }
    }
}
