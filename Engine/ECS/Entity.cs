using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlue.ECS
{
    internal class Entity
    {
        private List<Component> _components;
        //private List<>
        public Entity()
        {
            _components = new List<Component>();
        }

        public Entity(List<Component> components)
        {
            _components = components;
        }

        public void AddComponent(Component component)
        {
            _components.Add(component);
        }

        public void RemoveComponent(Component component)
        {
            _components.Remove(component);
        }

        public bool Contains(Component component)
        {
            return _components.Contains(component);
        }
    }
}
