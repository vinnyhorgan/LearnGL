using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace LearnGL
{
    class Renderer
    {
        private static readonly float FOV = 70;
        private static readonly float NEAR_PLANE = 0.1f;
        private static readonly float FAR_PLANE = 1000;

        private Matrix4 projectionMatrix;

        public Renderer(Shader shader)
        {
            createProjectionMatrix();

            shader.SetMatrix("projectionMatrix", projectionMatrix);
        }

        public void Prepare()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
        }

        public void Render(Entity entity, Shader shader)
        {
            TexturedModel texturedModel = entity.TexturedModel;
            Model model = texturedModel.Model;

            GL.BindVertexArray(model.VaoID);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            Matrix4 transformationMatrix = Utils.createTransformationMatrix(entity.Position, entity.RotX, entity.RotY, entity.RotZ, entity.Scale);

            shader.SetMatrix("transformationMatrix", transformationMatrix);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texturedModel.Texture.ID);
            GL.DrawElements(BeginMode.Triangles, model.VertexCount, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.BindVertexArray(0);
        }

        private void createProjectionMatrix()
        {
            float aspectRatio = (float)Window.Width / (float)Window.Height;
            float y_scale = (float)((1f / Math.Tan(MathHelper.DegreesToRadians(FOV / 2f))) * aspectRatio);
            float x_scale = y_scale / aspectRatio;
            float frustum_length = FAR_PLANE - NEAR_PLANE;

            projectionMatrix = new Matrix4(
                x_scale, 0, 0, 0,
                0, y_scale, 0, 0,
                0, 0, -((FAR_PLANE + NEAR_PLANE) / frustum_length), -1,
                0, 0, -((2 * NEAR_PLANE * FAR_PLANE) / frustum_length), 0
            );
        }
    }
}
