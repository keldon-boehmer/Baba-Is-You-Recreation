namespace BigBlue.ECS
{
    internal class Object
    {
        private ObjectType _type;
        public ObjectType Type { get { return _type; } }
        public Object(ObjectType type)
        {
            _type = type;
        }

        public Object Clone()
        {
            return new Object(_type);
        }
        public bool Equals(ObjectType type)
        {
            return _type.Equals(type);
        }
    }
}
