using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace LearnGL
{
    class Loader
    {
        private List<int> vaos = new List<int>();
        private List<int> vbos = new List<int>();
        private List<int> textures = new List<int>();

        public Model LoadModel(float[] positions, int[] indices)
        {
            int vaoID = CreateVAO();
            StoreData(0, positions);
            BindIndicesBuffer(indices);
            UnbindVAO();

            return new Model(vaoID, indices.Length);
        }

        public int LoadTexture(string path)
        {
            Texture texture = Texture.LoadTexture(path);
            textures.Add(texture.ID);

            return texture.ID;
        }

        public void Unload()
        {
            foreach (int vao in vaos)
            {
                GL.DeleteVertexArray(vao);
            }

            foreach (int vbo in vbos)
            {
                GL.DeleteBuffer(vbo);
            }

            foreach (int texture in textures)
            {
                GL.DeleteTexture(texture);
            }
        }

        private int CreateVAO()
        {
            int vaoID = GL.GenVertexArray();
            vaos.Add(vaoID);
            GL.BindVertexArray(vaoID);

            return vaoID;
        }

        private void UnbindVAO()
        {
            GL.BindVertexArray(0);
        }

        private void StoreData(int attribute, float[] data)
        {
            int vboID = GL.GenBuffer();
            vbos.Add(vboID);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StreamDraw);
            GL.VertexAttribPointer(attribute, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void BindIndicesBuffer(int[] indices)
        {
            int vboID = GL.GenBuffer();
            vbos.Add(vboID);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StreamDraw);
        }
    }
}
