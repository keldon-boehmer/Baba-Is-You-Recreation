using Engine.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BigBlue.ECS
{
    internal class RulesSystem : EntityUpdateSystem
    {
        private Rectangle _particleRectangle;
        private int _gridWidth;
        private int _gridHeight;
        private int _renderStartX;

        public RulesSystem(int gridWidth, int gridHeight, int renderStartX) 
            : base(Aspect.All(typeof(Position), typeof(Property)))
        {
            _gridWidth = gridWidth;
            _gridHeight = gridHeight;
            _renderStartX = renderStartX;
            _particleRectangle = new Rectangle(renderStartX, 0, gridWidth, gridHeight);
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        private List<Entity> _nouns = new List<Entity>();
        private List<Entity> _verbs = new List<Entity>();
        private List<Entity> _IsEntities = new List<Entity>();
        private List<Entity> _objectEntities = new List<Entity>(); // Entities that are the actual objects of the game (rocks, flags, walls, etc) Does not include text entities

        private HashSet<ObjectType> _oldYouTypes = new HashSet<ObjectType>();
        private HashSet<ObjectType> _oldWinTypes = new HashSet<ObjectType>();
        private HashSet<ObjectType> _newYouTypes = new HashSet<ObjectType>();
        private HashSet<ObjectType> _newWinTypes = new HashSet<ObjectType>();

        private bool _initialRulesCreated = false;

        public override void Update(GameTime gameTime)
        {
            populateEntityLists();

            // ===========================
            // Now we start handling rules
            // ===========================

            // Step 1: Remove all Property Components from all entities (except IsPush from Text or Hedge entities)
            //      This is to ensure that broken rules do not persist
            ClearProperties();

            // Step 2: Find Rules
            findRules(new Vector2(1, 0));
            findRules(new Vector2(0, 1));

            // Cleanup
            _nouns.Clear();
            _verbs.Clear();
            _IsEntities.Clear();
            _objectEntities.Clear();

            _oldYouTypes = new HashSet<ObjectType>(_newYouTypes);
            _newYouTypes.Clear();

            _oldWinTypes = new HashSet<ObjectType>(_newWinTypes);
            _newWinTypes.Clear();

            _initialRulesCreated = true;
        }

        private void populateEntityLists()
        {
            foreach (int entityID in ActiveEntities)
            {
                Entity entity = GetEntity(entityID);

                if (!entity.Has<Text>())
                {
                    if (entity.Has<Object>())
                    {
                        _objectEntities.Add(entity);
                    }
                    continue;
                }

                if (entity.Has<Object>())
                {
                    if (entity.Get<Object>().Equals(ObjectType.Is))
                    {
                        _IsEntities.Add(entity);
                    }
                    else
                    {
                        _nouns.Add(entity);
                    }
                }
                else if (entity.Has<Action>())
                {
                    _verbs.Add(entity);
                }
            }
        }

        private void ClearProperties()
        {
            foreach (int entityID in ActiveEntities)
            {
                // Clear all Properties from all entities
                Entity entity = GetEntity(entityID);
                entity.Get<Property>().Clear();
                // Add isPush to all Text Entities
                if (entity.Has<Text>())
                {
                    entity.Get<Property>().isPush = true;
                }
                // and isStop to Hedges
                else
                {
                    if (entity.Has<Object>())
                    {
                        if (entity.Get<Object>().Equals(ObjectType.Hedge))
                        {
                            entity.Get<Property>().isStop = true;
                        }
                    }
                }
            }
        }

        private void findRules(Vector2 direction)
        {
            foreach (Entity noun in _nouns)
            {
                Vector2 nounPosition = noun.Get<Position>().Coordinates;
                Vector2 adjacentPosition = new Vector2(nounPosition.X + direction.X, nounPosition.Y + direction.Y);
                Vector2 adjacentPosition2 = new Vector2(adjacentPosition.X + direction.X, adjacentPosition.Y + direction.Y);
                foreach (Entity isTextEntity in _IsEntities)
                {
                    Vector2 isPosition = isTextEntity.Get<Position>().Coordinates;

                    // Check for adjacency in the given direction
                    if (adjacentPosition.Equals(isPosition))
                    {
                        foreach (Entity verb in _verbs)
                        {
                            Vector2 verbPosition = verb.Get<Position>().Coordinates;
                            // Check for adjacency in the given direction
                            if (adjacentPosition2 == verbPosition)
                            {
                                Entity[] rule = new Entity[3] { noun, isTextEntity, verb };
                                applyPropertyRule(rule);
                            }
                        }

                        foreach (Entity noun2 in _nouns)
                        {
                            Vector2 noun2Position = noun2.Get<Position>().Coordinates;
                            // Check for adjacency in the given direction
                            if (isPosition + direction == noun2Position)
                            {
                                Entity[] rule = new Entity[3] { noun, isTextEntity, noun2 };
                                applyConversionRule(rule);
                            }
                        }
                    }
                }
            }
        }

        // Note: Valid Rules are when Text entities align following one of the following formats:
        //              Format 1: [Noun] [Is] [Verb]
        //              Format 2: [Noun1] [Is] [Noun2]
        //    where Nouns are entities with a Text and Object component.
        //    and   Verbs are entities with a Text and Action component.
        // Any and all other orientations of entities will not form a valid rule.
        // case (Format 1): all Objects with the Noun's ObjectType apply the Property associated with the Verb's Action component.
        // case (Format 2): all of Noun1's components are replaced with Noun2's components, except Position

        // This method applies rules of the format [Noun] [Is] [Verb]
        private void applyPropertyRule(Entity[] rule)
        {
            ActionType propertyToApply = rule[2].Get<Action>().Type;
            
            bool createParticleEffects = determineNewYouOrWin(propertyToApply, rule);
            
            // Loop to apply property to entities of Noun's type
            foreach (Entity objectEntity in _objectEntities)
            {
                Object entityType = objectEntity.Get<Object>();
                if (entityType.Equals(rule[0].Get<Object>().Type))
                {
                    objectEntity.Get<Property>().Apply(propertyToApply);
                    if (createParticleEffects && _initialRulesCreated)
                    {
                        Vector2 coordinates = objectEntity.Get<Position>().Coordinates;
                        _particleRectangle.X = _renderStartX + ((int)coordinates.X * _gridWidth);
                        _particleRectangle.Y = (int)coordinates.Y * _gridHeight;
                        ParticleSystem.IsWinOrIsYou(_particleRectangle, 13, 2f, new TimeSpan(0, 0, 0, 0, 1000), Color.Yellow);
                    }
                }
            }
        }

        private bool determineNewYouOrWin(ActionType propertyToApply, Entity[] rule)
        {
            bool newFound = false;

            // Find potential new isWin or isYou rules
            if (propertyToApply == ActionType.Win)
            {
                ObjectType winType = rule[0].Get<Object>().Type;
                _newWinTypes.Add(winType);

                // Test if a new win rule is formed
                if (!_oldWinTypes.Contains(winType))
                {
                    newFound = true;
                    if (_initialRulesCreated)
                    {
                        GameStatus.winConditionChanged = true;
                    }
                }
            }
            else if (propertyToApply == ActionType.You)
            {
                ObjectType youType = rule[0].Get<Object>().Type;
                _newYouTypes.Add(youType);
                // Test if a new win rule is formed
                if (!_oldYouTypes.Contains(youType))
                {
                    newFound = true;
                }
            }

            return newFound;
        }

        // This method applies rules of the format [Noun1] [Is] [Noun2]
        private void applyConversionRule(Entity[] rule)
        {
            // Will be used to clone components to convert entities of Noun1's type to Noun2's type
            Entity objectEqualsNoun2 = null;

            // Initial loop to find an entity of the same object type as Noun 2 refers to
            foreach (Entity objectEntity in _objectEntities)
            {
                Object entityType = objectEntity.Get<Object>();

                if (entityType.Equals(rule[2].Get<Object>().Type))
                {
                    objectEqualsNoun2 = objectEntity;
                    break;
                }
            }

            // If no objects of noun2 type exist, then the rule cannot be applied
            if (objectEqualsNoun2 is null)
            {
                return;
            }

            // Second loop to convert all entities of Noun1's type to Noun2's type/sprite/property
            foreach (Entity objectEntity in _objectEntities)
            {
                Object entityType = objectEntity.Get<Object>();
                Vector2 entityCoords = objectEntity.Get<Position>().Coordinates;
                // Change entities of noun1's type, unless it is the off-screen entity meant for cloning
                if (entityType.Equals(rule[0].Get<Object>().Type) && entityCoords.Y < LevelManager.Instance.GetCurrentLevel().Height)
                {
                    // Attach cloned Object
                    objectEntity.Detach<Object>();
                    objectEntity.Attach(objectEqualsNoun2.Get<Object>().Clone());

                    // Attach cloned Animation
                    objectEntity.Detach<Animation>();
                    objectEntity.Attach(objectEqualsNoun2.Get<Animation>().Clone());

                    // Attach cloned Property
                    objectEntity.Detach<Property>();
                    objectEntity.Attach(objectEqualsNoun2.Get<Property>().Clone());
                }
            }
        }
    }
}