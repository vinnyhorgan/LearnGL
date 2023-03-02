using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LearnGL
{
    class Camera
    {
        public Vector3 Position = new Vector3(0, 0, 0);
        public float Pitch;
        public float Yaw;
        public float Roll = 0;
        public float MoveSpeed = 0.5f;
        public float LookSpeed = 1.5f;

        public Camera()
        {

        }

        public void Move(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.W))
            {
                Position.Z -= MoveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                Position.Z += MoveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                Position.X -= MoveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                Position.X += MoveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Yaw += LookSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Yaw -= LookSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Pitch -= LookSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                Pitch += LookSpeed;
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
