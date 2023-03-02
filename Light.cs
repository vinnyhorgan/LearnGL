using OpenTK.Mathematics;

namespace LearnGL
{
    class Light
    {
        public Vector3 Position;
        public Vector3 Color;

        public Light(Vector3 position, Vector3 color)
        {
            Position = position;
            Color = color;
        }
    }
}
