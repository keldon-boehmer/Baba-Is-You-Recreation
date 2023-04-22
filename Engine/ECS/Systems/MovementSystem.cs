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
    internal class MovementSystem : EntityUpdateSystem
    {

        private ComponentMapper<Position> _positionMapper;
        private ComponentMapper<Property> _propertyMapper;

        private List<int> _youEntityIds = new List<int>();
        private List<int> _pushEntityIds = new List<int>();
        private List<int> _stopEntityIds = new List<int>();
        private HashSet<int> _movedEntityIds = new HashSet<int>();

        public MovementSystem()
            : base(Aspect.All(typeof(Position), typeof(Property)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _positionMapper = mapperService.GetMapper<Position>();
            _propertyMapper = mapperService.GetMapper<Property>();
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 moveDirection = getMoveDirection();
            Vector2 noDirection = new Vector2(0, 0);

            if (moveDirection == noDirection)
            {
                return;
            }

            getNeededEntities();

            foreach(var youId in _youEntityIds)
            {
                addNewMovedEntities(youId, moveDirection);
            }

            moveEntities(moveDirection);
            clearLists();
        }

        private Vector2 getMoveDirection()
        {
            Vector2 moveDirection = new Vector2(0, 0);

            if (InputManager.Instance.moveUp)
            {
                moveDirection.Y = -1;
            }
            else if (InputManager.Instance.moveDown)
            {
                moveDirection.Y = 1;
            }
            else if (InputManager.Instance.moveLeft)
            {
                moveDirection.X = -1;
            }
            else if (InputManager.Instance.moveRight)
            {
                moveDirection.X = 1;
            }


            return moveDirection;
        }

        private void getNeededEntities()
        {
            foreach (var entityId in ActiveEntities)
            {
                Property property = _propertyMapper.Get(entityId);

                if (property.isYou)
                {
                    _youEntityIds.Add(entityId);
                }
                if (property.isPush)
                {
                    _pushEntityIds.Add(entityId);
                }
                if (property.isStop)
                {
                    _stopEntityIds.Add(entityId);
                }
            }
        }

        private void addNewMovedEntities(int youId, Vector2 moveDirection)
        {
            HashSet<int> entitiesAffected = new HashSet<int>();
            entitiesAffected.Add(youId);
            bool moveLineOfEntities = true;
            Vector2 currPosition = _positionMapper.Get(youId).Coordinates;
            bool pushEntityFound = true;

            while (moveLineOfEntities && pushEntityFound)
            {
                pushEntityFound = false;
                currPosition += moveDirection;

                if (outOfBounds(currPosition))
                {
                    entitiesAffected.Clear();
                    moveLineOfEntities = false;
                }
                else
                {
                    foreach (int pushId in _pushEntityIds)
                    {
                        Vector2 pushPosition = _positionMapper.Get(pushId).Coordinates;
                        if (pushPosition == currPosition)
                        {
                            entitiesAffected.Add(pushId);
                            pushEntityFound = true;
                        }
                    }

                    foreach (int stopId in _stopEntityIds)
                    {
                        Vector2 stopPosition = _positionMapper.Get(stopId).Coordinates;
                        if (stopPosition == currPosition)
                        {
                            entitiesAffected.Clear();
                            moveLineOfEntities = false;
                        }
                    }
                }
            }

            _movedEntityIds.UnionWith(entitiesAffected);
        }

        private bool outOfBounds(Vector2 currPosition)
        {
            Level currentLevel = LevelManager.Instance.GetCurrentLevel();
            if (currPosition.X < 0
                || currPosition.Y < 0)
            {
                return true;
            }
            if (currPosition.X >= currentLevel.Width
                || currPosition.Y >= currentLevel.Height)
            {
                return true;
            }
            return false;
        }

        private void moveEntities(Vector2 moveDirection)
        {
            if (_movedEntityIds.Count > 0)
            {
                GameStatus.playerMoved = true;
                foreach (int moveId in _movedEntityIds)
                {
                    _positionMapper.Get(moveId).Coordinates += moveDirection;
                }
            }
        }

        private void clearLists()
        {
            _youEntityIds.Clear();
            _pushEntityIds.Clear();
            _stopEntityIds.Clear();
            _movedEntityIds.Clear();
        }
    }
}
