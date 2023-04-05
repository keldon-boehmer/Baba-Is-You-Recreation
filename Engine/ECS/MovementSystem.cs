using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ECS
{
    internal class MovementSystem : EntityUpdateSystem
    {
        public MovementSystem(AspectBuilder aspectBuilder) : base(aspectBuilder)
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
