using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;

namespace BigBlue.ECS
{
    internal class RulesSystem : EntityUpdateSystem
    {
        public RulesSystem(AspectBuilder aspectBuilder) : base(aspectBuilder)
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
