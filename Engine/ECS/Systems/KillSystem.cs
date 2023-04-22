using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlue.ECS
{
    internal class KillSystem : EntityUpdateSystem
    {
        private ComponentMapper<Position> _positionMapper;
        private ComponentMapper<Property> _propertyMapper;
        private ComponentMapper<Animation> _animationMapper;

        private Rectangle _particleRectangle;
        private int _gridWidth;
        private int _gridHeight;
        private int _renderStartX;

        public KillSystem(int gridWidth, int gridHeight, int renderStartX) : base(Aspect.All(typeof(Position), typeof(Property), typeof(Animation)))
        {
            _gridWidth = gridWidth;
            _gridHeight = gridHeight;
            _renderStartX = renderStartX;
            _particleRectangle = new Rectangle(renderStartX, 0, gridWidth, gridHeight);
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _positionMapper = mapperService.GetMapper<Position>();
            _propertyMapper = mapperService.GetMapper<Property>();
            _animationMapper = mapperService.GetMapper<Animation>();
        }

        public override void Update(GameTime gameTime)
        {
            // Entities will never need to be destroyed unless the player moved
            if (GameStatus.playerMoved)
            {
                List<int> killEntities = new List<int>();

                // Find positions of all isKill entities
                foreach (var entityId in ActiveEntities)
                {
                    if (_propertyMapper.Get(entityId).isKill)
                    {
                        killEntities.Add(entityId);
                    }
                }

                foreach (var killEntityId in killEntities)
                {
                    Position killPosition = _positionMapper.Get(killEntityId);
                    foreach (var entityId in ActiveEntities)
                    {
                        if (entityId != killEntityId)
                        {
                            Position entityPosition = _positionMapper.Get(entityId);
                            if (entityPosition.Coordinates == killPosition.Coordinates)
                            {
                                DestroyEntity(entityId);
                                _particleRectangle.X = _renderStartX + ((int)entityPosition.Coordinates.X * _gridWidth);
                                _particleRectangle.Y = (int)killPosition.Coordinates.Y * _gridHeight;
                                Color particleColor = _animationMapper.Get(entityId).Color;
                                ParticleSystem.OnDeath(_particleRectangle, 300, 2f, new TimeSpan(0, 0, 0, 0, 500), particleColor);
                            }
                        }
                    }
                }
            }
        }
    }
}
