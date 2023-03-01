using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LearnGL
{
    class Camera
    {
        public Vector3 Position = new Vector3(0, 0, 0);
        public float Pitch;
        public float Yaw;
        public float Roll;

        public Camera()
        {

        }

        public void Move(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.W))
            {
                Position.Z -= 0.02f;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                Position.Z += 0.02f;
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                Position.X -= 0.02f;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                Position.X += 0.02f;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Yaw += 1f;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Yaw -= 1f;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Pitch -= 1f;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                Pitch += 1f;
            }
        }

        public Matrix4 GetViewMatrix()
        {
            Matrix4 viewMatrix = Matrix4.Identity;
            viewMatrix = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Pitch));
            viewMatrix *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Yaw));
            viewMatrix *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Roll));
            Vector3 negativeCameraPos = new Vector3(-Position.X, -Position.Y, -Position.Z);
            viewMatrix *= Matrix4.CreateTranslation(negativeCameraPos);

            return viewMatrix;
        }
    }
}
