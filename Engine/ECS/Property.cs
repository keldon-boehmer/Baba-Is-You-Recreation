using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlue.ECS
{
    internal class Property: Component
    {
        private PropertyType _type;
        public PropertyType Type { get { return _type; } }
        public Property(PropertyType type)
        {
            _type = type;
        }
    }
}
