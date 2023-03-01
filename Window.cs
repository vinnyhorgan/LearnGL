using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using ImGuiNET;

namespace LearnGL
{
    class Window : GameWindow
    {
        ImGuiController controller;

        Loader loader = new Loader();
        Renderer renderer = new Renderer();

        float[] vertices =
        {
            -0.5f, 0.5f, 0f,
            -0.5f, -0.5f, 0f,
            0.5f, -0.5f, 0f,
            0.5f, 0.5f, 0f,
        };

        int[] indices =
        {
            0, 1, 3,
            3, 1, 2
        };

        Model model;
        Shader shader;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            controller = new ImGuiController(ClientSize.X, ClientSize.Y);

            model = loader.LoadModel(vertices, indices);
            shader = new Shader("Assets/shader.vert", "Assets/shader.frag");
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            controller.Update(this, (float)args.Time);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            renderer.Prepare();

            shader.Attach();
            renderer.Render(model);
            shader.Detach();

            ImGui.ShowDemoWindow();

            controller.Render();
            ImGuiController.CheckGLError("End of frame");

            SwapBuffers();
        }

        protected override void OnUnload()
        {
            controller.Dispose();

            loader.Unload();
            shader.Unload();

            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);

            controller.WindowResized(e.Width, e.Height);
        }

        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);

            controller.PressChar((char)e.Unicode);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            controller.MouseScroll(e.Offset);
        }
    }
}
