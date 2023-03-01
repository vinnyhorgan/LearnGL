using OpenTK.Graphics.OpenGL4;

namespace LearnGL
{
    class Renderer
    {
        public void Prepare()
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        public void Render(Model model)
        {
            GL.BindVertexArray(model.VaoID);
            GL.EnableVertexAttribArray(0);
            GL.DrawElements(BeginMode.Triangles, model.VertexCount, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexAttribArray(0);
            GL.BindVertexArray(0);
        }
    }
}
