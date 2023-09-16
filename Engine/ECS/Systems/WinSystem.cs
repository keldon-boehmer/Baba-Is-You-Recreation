using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System.Collections.Generic;

namespace Baba.ECS
{
    internal class WinSystem : EntityUpdateSystem
    {
        // These are the mappers for entities, so we can retrieve their components in O(1) time
        private ComponentMapper<Position> _positionMapper;
        private ComponentMapper<Property> _propertyMapper;

        // Aspect will let the system know that it needs to know about entities with
        // certain components (aspects)
        // Aspect.All lets the system know to only care about entities that have all
        // of the listed components (Entities a system knows about are in the systems "ActiveEntities"
        // Thus, this WinSystem will only be able to access entities that have both Position AND Property
        // components.
        public WinSystem()
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
            // Win condition will never be met unless the player moved
            if (GameStatus.playerMoved)
            {
                // Lists for the position components of all entities with "isYou" property
                // and "isWin" property.
                List<Position> isYouEntities = new List<Position>();
                List<Position> isWinEntities = new List<Position>();

                // Loop through all entities tracked by the WinSystem
                foreach (int entityId in ActiveEntities)
                {
                    // Get the property component of the entity
                    Property property = _propertyMapper.Get(entityId);

                    // If an object is both you and win, the win condition is met
                    if (property.isYou && property.isWin)
                    {
                        GameStatus.playerWon = true;
                        return;
                    }

                    //Otherwise, add their positions to the lists if they are "isYou" or "isWin"
                    if (property.isYou)
                    {
                        isYouEntities.Add(_positionMapper.Get(entityId));
                    }
                    if (property.isWin)
                    {
                        isWinEntities.Add(_positionMapper.Get(entityId));
                    }
                }

                // Loop through the "isYou" and "isWin" lists
                foreach (var youPosition in isYouEntities)
                {
                    Vector2 youCoordinates = youPosition.Coordinates;
                    foreach (var winPosition in isWinEntities)
                    {
                        // If an "isYou" entity matches position with an "isWin" entity, the win condition is met
                        if (youCoordinates == winPosition.Coordinates)
                        {
                            GameStatus.playerWon = true;
                            return;
                        }
                    }
                }
            }
        }
    }
}