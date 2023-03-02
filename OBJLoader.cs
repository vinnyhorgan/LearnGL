using ObjLoader.Loader.Loaders;
using System;
using System.Collections.Generic;
using System.IO;

namespace LearnGL
{
    class OBJLoader
    {
        public static Model LoadOBJModel(string path, Loader loader)
        {
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();

            FileStream fileStream = new FileStream(path, FileMode.Open);
            LoadResult result = objLoader.Load(fileStream);
            fileStream.Close();

            List<float> vertices = new List<float>();

            foreach (var vertex in result.Vertices)
            {
                vertices.Add(vertex.X);
                vertices.Add(vertex.Y);
                vertices.Add(vertex.Z);
            }

            List<float> textureCoords = new List<float>();

            foreach (var texture in result.Textures)
            {
                textureCoords.Add(texture.X);
                textureCoords.Add(1 - texture.Y);
            }

            List<int> indices = new List<int>();

            foreach (var group in result.Groups)
            {
                foreach (var face in group.Faces)
                {
                    indices.Add(face[0].VertexIndex - 1);
                    indices.Add(face[1].VertexIndex - 1);
                    indices.Add(face[2].VertexIndex - 1);
                }
            }

            List<float> normals = new List<float>();

            foreach (var normal in result.Normals)
            {
                normals.Add(normal.X);
                normals.Add(normal.Y);
                normals.Add(normal.Z);
            }

            Model model = loader.LoadModel(vertices.ToArray(), textureCoords.ToArray(), normals.ToArray(), indices.ToArray());

            return model;
        }
    }
}
