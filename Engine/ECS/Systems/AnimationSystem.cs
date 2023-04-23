using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System.Collections.Generic;

namespace BigBlue.ECS
{
    internal class AnimationSystem : EntityUpdateSystem, IDrawSystem
    {
        private ComponentMapper<Animation> _animationMapper;
        private ComponentMapper<Position> _positionMapper;

        private int _gridWidth;
        private int _gridHeight;
        private int _renderStartX;
        private SpriteBatch _spriteBatch;

        private Rectangle _rect;

        public AnimationSystem(int gridWidth, int gridHeight, int renderStartX, SpriteBatch spriteBatch)
            : base(Aspect.All(typeof(Animation), typeof(Position)))
        {
            _gridWidth = gridWidth;
            _gridHeight = gridHeight;
            _renderStartX = renderStartX;
            _spriteBatch = spriteBatch;
            _rect = new Rectangle(0, 0, _gridWidth, _gridHeight);
        }


        public override void Initialize(IComponentMapperService mapperService)
        {
            _animationMapper = mapperService.GetMapper<Animation>();
            _positionMapper = mapperService.GetMapper<Position>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entityId in ActiveEntities)
            {
                var animation = _animationMapper.Get(entityId);

                animation.ElapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (animation.ElapsedTime > animation.FrameTime)
                {
                    animation.ElapsedTime -= animation.FrameTime;
                    animation.CurrentFrame = (animation.CurrentFrame + 1) % animation.Frames.Length;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle(0, 0, _gridWidth, _gridHeight);
            List<int> pushIds = new List<int>();
            List<int> youIds = new List<int>();

            foreach (var entityId in ActiveEntities)
            {
                if (GetEntity(entityId).Has<Property>())
                {
                    Property property = GetEntity(entityId).Get<Property>();
                    if (property.isYou)
                    {
                        youIds.Add(entityId);
                        continue;
                    }
                    else if (property.isPush)
                    {
                        pushIds.Add(entityId);
                        continue;
                    }
                }

                drawEntity(entityId);
            }

            // render all push and you objects on top of everything else
            foreach(int pushId in pushIds)
            {
                drawEntity(pushId);
            }

            foreach (int youId in youIds)
            {
                drawEntity(youId);
            }
        }

        private void drawEntity(int entityId)
        {
            var animation = _animationMapper.Get(entityId);
            var position = _positionMapper.Get(entityId);

            _rect.X = _renderStartX + _gridWidth * (int)position.Coordinates.X;
            _rect.Y = _gridHeight * (int)position.Coordinates.Y;

            _spriteBatch.Draw(animation.Frames[animation.CurrentFrame], _rect, animation.Color);
        }
    }
}