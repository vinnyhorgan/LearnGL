using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace LearnGL
{
    class Program
    {
        static void Main()
        {
            const int width = 800;
            const int height = 600;

            var nativeWindowSettings = new NativeWindowSettings
            {
                Size = new Vector2i(width, height),
                Title = "LearnGL by Vinny Horgan",
                Flags = ContextFlags.ForwardCompatible,
                MinimumSize = new Vector2i(width / 2, height / 2),
                Vsync = VSyncMode.On
            };

            using (var window = new Window(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}
