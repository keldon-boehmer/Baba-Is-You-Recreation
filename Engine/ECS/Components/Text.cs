using BigBlue.ECS;
using Engine.ECS;

namespace BigBlue.ECS
{
    // TODO : Implement Clone method that returns a deep copy of this component. For use with undo mechanic.
    internal class Text
    {
        private TextType _type;
        public TextType Type { get { return _type; } }
        public Text(TextType type)
        {
            _type = type;
        }

        public Text Clone()
        {
            return new Text(_type); ;
        }
    }
}
