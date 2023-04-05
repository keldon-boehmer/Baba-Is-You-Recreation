using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ECS
{
    internal class Noun : Component
    {
        private NounType _type;
        public NounType Type { get { return _type; } }
        public Noun(NounType type)
        {
            _type = type;
        }
    }
}
