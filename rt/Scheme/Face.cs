namespace RT.Scheme
{
    class Face
    {
        public readonly uint[,]? Data;
        public readonly string? Contact;
        public readonly uint? Layer;

        public Face(uint[,]? data, string? contact, uint? layer)
        {
            Data = data;
            Contact = contact;
            Layer = layer;
        }
    }
}
