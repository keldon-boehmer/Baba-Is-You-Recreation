using Baba.ECS;
using Engine.ECS;

namespace Baba.ECS
{
    internal class Action
    {
        private ActionType _type;
        public ActionType Type { get { return _type; } }
        public Action(ActionType type)
        {
            _type = type;
        }

        public Action Clone()
        {
            return new Action(_type); ;
        }
    }
}
