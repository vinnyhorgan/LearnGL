namespace LearnGL
{
    class Model
    {
        public readonly int VaoID;
        public readonly int VertexCount;

        public Model(int vaoID, int vertexCount)
        {
            VaoID = vaoID;
            VertexCount = vertexCount;
        }
    }
}
