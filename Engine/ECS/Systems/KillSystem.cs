using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                List<int> sinkEntities = new List<int>();
                List<int> youEntities = new List<int>();

                // Find positions of all isKill, isSink, and isYou entities
                foreach (var entityId in ActiveEntities)
                {
                    Property property = _propertyMapper.Get(entityId);

                    if (property.isKill)
                    {
                        killEntities.Add(entityId);
                    }
                    if (property.isSink)
                    {
                        sinkEntities.Add(entityId);
                    }
                    if (property.isYou)
                    {
                        youEntities.Add(entityId);
                    }
                }

                // Apply kill rule to you entities
                foreach (var killId in killEntities)
                {
                    Position killPosition = _positionMapper.Get(killId);
                    foreach (var youId in youEntities)
                    {
                        Position entityPosition = _positionMapper.Get(youId);
                        if (entityPosition.Coordinates == killPosition.Coordinates)
                        {
                            DestroyEntity(youId);
                            _particleRectangle.X = _renderStartX + ((int)entityPosition.Coordinates.X * _gridWidth);
                            _particleRectangle.Y = (int)entityPosition.Coordinates.Y * _gridHeight;
                            Color particleColor = _animationMapper.Get(youId).Color;
                            ParticleSystem.OnDeath(_particleRectangle, 300, 2f, new TimeSpan(0, 0, 0, 0, 500), particleColor);
                        }
                    }
                }

                // Apply sink rule to all non-text entities
                foreach (var sinkId in sinkEntities)
                {
                    Position sinkPosition = _positionMapper.Get(sinkId);
                    foreach (var entityId in ActiveEntities)
                    {
                        Entity entity = GetEntity(entityId);
                        if (entityId != sinkId && !entity.Has<Text>())
                        {
                            Position entityPosition = _positionMapper.Get(entityId);
                            bool entityDestroyed = false;
                            if (entityPosition.Coordinates == sinkPosition.Coordinates)
                            {
                                DestroyEntity(entityId);
                                entityDestroyed = true;
                            }
                            if (entityDestroyed)
                            {
                                _particleRectangle.X = _renderStartX + ((int)entityPosition.Coordinates.X * _gridWidth);
                                _particleRectangle.Y = (int)sinkPosition.Coordinates.Y * _gridHeight;
                                Color particleColor = _animationMapper.Get(entityId).Color;
                                ParticleSystem.OnDeath(_particleRectangle, 300, 2f, new TimeSpan(0, 0, 0, 0, 500), particleColor);
                                DestroyEntity(sinkId);
                            }
                        }
                    }
                }
            }
        }
    }
}
