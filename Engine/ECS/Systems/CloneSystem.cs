using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlue.ECS
{
    internal class CloneSystem : EntityUpdateSystem
    {
        private int _gridWidth;
        private int _gridHeight;
        private int _renderStartX;
        private SpriteBatch _spriteBatch;

        public CloneSystem(int gridWidth, int gridHeight, int renderStartX, SpriteBatch spriteBatch)
            : base(Aspect.All(typeof(Position)))
        {
            _gridWidth = gridWidth;
            _gridHeight = gridHeight;
            _renderStartX = renderStartX;
            _spriteBatch = spriteBatch;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (GameStatus.playerMoved || WorldClone.undone)
            {
                var cloneWorld = new WorldBuilder()
                    .AddSystem(new MovementSystem())
                    .AddSystem(new RulesSystem())
                    .AddSystem(new KillSystem(_gridWidth, _gridHeight, _renderStartX))
                    .AddSystem(new WinSystem())
                    .AddSystem(new AnimationSystem(_gridWidth, _gridHeight, _renderStartX, _spriteBatch))
                    .AddSystem(new CloneSystem(_gridWidth, _gridHeight, _renderStartX, _spriteBatch))
                    .Build();

                foreach (var entityId in ActiveEntities)
                {
                    var cloneEntity = cloneWorld.CreateEntity();
                    var originalEntity = GetEntity(entityId);

                    Position positionClone = originalEntity.Get<Position>().Clone();
                    cloneEntity.Attach(positionClone);

                    if (GetEntity(entityId).Has<Animation>())
                    {
                        Animation animationClone = originalEntity.Get<Animation>().Clone();
                        cloneEntity.Attach(animationClone);
                    }

                    if (GetEntity(entityId).Has<Object>())
                    {
                        Object objectClone = originalEntity.Get<Object>().Clone();
                        cloneEntity.Attach(objectClone);
                    }

                    if (GetEntity(entityId).Has<Property>())
                    {
                        Property propertyClone = originalEntity.Get<Property>().Clone();
                        cloneEntity.Attach(propertyClone);
                    }

                    if (GetEntity(entityId).Has<Text>())
                    {
                        Text textClone = originalEntity.Get<Text>().Clone();
                        cloneEntity.Attach(textClone);
                    }
                }

                WorldClone.cloneWorld = cloneWorld;

            }
        }
    }
}
