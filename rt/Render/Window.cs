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
        private readonly MousePosition mousePosition;
        private Frame? frame;
        private readonly Camera camera;

        public Window(int width, int height, string title, int pixelSize = 1)
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
            gameWindow.FocusedChanged += OnFocusedChanged;
            gameWindow.Resize += OnResize;

            mousePosition = new MousePosition();

            camera = new Camera(new Vector3(-3, 0, 0));
        }

        public void MakeFrame()
        {
            frame = new Frame(camera);
        }

        public void Run(double updateRate)
        {
            gameWindow.Run(updateRate);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            CheckOpenGLError();
        }

        private void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            if (gameWindow.Focused)
            {
                // const float sensitivity = 0.3f;
                // Vector2 delta = mousePosition.Update();
                // camera.Rotate(delta * sensitivity);
            }

            frame!.Update(e.Time);
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
        }

        private void OnFocusedChanged(object sender, EventArgs e)
        {
            mousePosition.Update();
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
