using OpenTK.Mathematics;

namespace LearnGL
{
    class Entity
    {
        public readonly TexturedModel TexturedModel;
        public Vector3 Position;
        public float RotX, RotY, RotZ;
        public float Scale;

        public Entity(TexturedModel texturedModel, Vector3 position, float rotX, float rotY, float rotZ, float scale)
        {
            TexturedModel = texturedModel;
            Position = position;
            RotX = rotX;
            RotY = rotY;
            RotZ = rotZ;
            Scale = scale;
        }

        public void IncreasePosition(float dx, float dy, float dz)
        {
            Position.X += dx;
            Position.Y += dy;
            Position.Z += dz;
        }

        public void IncreaseRotation(float dx, float dy, float dz)
        {
            RotX += dx;
            RotY += dy;
            RotZ += dz;
        }
    }
}
