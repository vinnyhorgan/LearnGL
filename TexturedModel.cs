namespace LearnGL
{
    class TexturedModel
    {
        public readonly Model Model;
        public readonly Texture Texture;

        public TexturedModel(Model model, Texture texture)
        {
            Model = model;
            Texture = texture;
        }
    }
}
