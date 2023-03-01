using ObjLoader.Loader.Loaders;
using System;
using System.IO;

namespace LearnGL
{
    class OBJLoader
    {
        public static Model LoadOBJModel(string path)
        {
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();

            FileStream fileStream = new FileStream(path, FileMode.Open);
            LoadResult result = objLoader.Load(fileStream);
            fileStream.Close();

            Console.WriteLine(result.Vertices[0].X);

            return new Model(0, 0);
        }
    }
}
