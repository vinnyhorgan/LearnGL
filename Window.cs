using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using ImGuiNET;
using System.Collections.Generic;

namespace LearnGL
{
    class Window : GameWindow
    {
        public static int Width;
        public static int Height;

        ImGuiController controller;

        Loader loader;
        Renderer renderer;

        Dictionary<int, string> shaderAttributes;

        Shader shader;
        Model model;
        Texture texture;
        TexturedModel texturedModel;
        Entity entity;
        Light light;

        Camera camera;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            Width = ClientSize.X;
            Height = ClientSize.Y;

            controller = new ImGuiController(ClientSize.X, ClientSize.Y);

            loader = new Loader();

            shaderAttributes = new Dictionary<int, string>
            {
                { 0, "position" },
                { 1, "textureCoords" },
                { 2, "normal" }
            };

            shader = new Shader("Assets/shader.vert", "Assets/shader.frag", shaderAttributes);

            renderer = new Renderer(shader);

            model = OBJLoader.LoadOBJModel("Assets/stall.obj", loader);
            texture = loader.LoadTexture("Assets/white.png");
            texturedModel = new TexturedModel(model, texture);
            entity = new Entity(texturedModel, new Vector3(0, 0, 0), 0, 0, 0, 1);

            light = new Light(new Vector3(0, 0, 0), new Vector3(1, 1, 1));

            camera = new Camera();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            controller.Update(this, (float)args.Time);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            camera.Move(KeyboardState);

            // entity.IncreasePosition(0, 0, -0.02f);
            entity.IncreaseRotation(0, 1, 0);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            renderer.Prepare();

            shader.SetMatrix("viewMatrix", camera.GetViewMatrix());

            shader.Attach();

            shader.SetVector3("lightPosition", light.Position);
            shader.SetVector3("lightColor", light.Color);

            renderer.Render(entity, shader);
            shader.Detach();

            // ImGui.ShowDemoWindow();

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

            Width = e.Width;
            Height = e.Height;

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
