using OpenTK.Mathematics;

namespace LearnGL
{
    class Utils
    {
        public static Matrix4 createTransformationMatrix(Vector3 translation, float rx, float ry, float rz, float scale)
        {
            Matrix4 matrix = Matrix4.Identity;
            matrix *= Matrix4.CreateTranslation(translation);
            matrix *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rx));
            matrix *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(ry));
            matrix *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rz));
            matrix *= Matrix4.CreateScale(scale);

            return matrix;
        }
    }
}
