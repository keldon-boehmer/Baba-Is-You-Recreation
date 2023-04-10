namespace BigBlue
{
    internal class Noun
    {
        private NounType _type;
        public NounType Type { get { return _type; } }
        public Noun(NounType type)
        {
            _type = type;
        }
    }
}
