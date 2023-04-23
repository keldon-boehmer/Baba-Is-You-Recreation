using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BigBlue.ECS
{
    internal class RulesSystem : EntityUpdateSystem
    {
        private ComponentMapper<Position> _positionMapper;
        private ComponentMapper<Property> _propertyMapper;
        private ComponentMapper<Object> _objectMapper;
        private ComponentMapper<Action> _actionMapper;
        private ComponentMapper<Text> _textMapper;

        public RulesSystem() 
            : base(Aspect.All(typeof(Position), typeof(Property), typeof(Object)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _positionMapper = mapperService.GetMapper<Position>();
            _propertyMapper = mapperService.GetMapper<Property>();
            _objectMapper = mapperService.GetMapper<Object>();
            _actionMapper = mapperService.GetMapper<Action>();
            _textMapper = mapperService.GetMapper<Text>();
        }


        private Dictionary<int, Position> _prevTextPositions = new Dictionary<int, Position>();  // (key, value) = TextEntity's (id, Position)
        private List<Entity> _nouns = new List<Entity>();
        private List<Entity> _verbs = new List<Entity>();
        private List<Entity> _IsEntities = new List<Entity>();  // 100% grammatically correct plural of "is"...

        public override void Update(GameTime gameTime)
        {
            if (!GameStatus.playerMoved) return;

            // Updating Text Positions
            Dictionary<int, Position> textPositions = new Dictionary<int, Position>();
            bool diffFound = false;
            foreach (int entityID in ActiveEntities)
            {
                Entity textEntity = GetEntity(entityID);
                if (!textEntity.Has<Text>()) continue;
                textPositions.Add(entityID, textEntity.Get<Position>().Clone());
                if (!_prevTextPositions.ContainsKey(entityID)) { continue; }
                if (!textPositions[entityID].Equals(_prevTextPositions[entityID]))
                    diffFound = true;
                if (textEntity.Get<Object>().Equals(ObjectType.Is))
                    _IsEntities.Add(textEntity);
                else if (textEntity.Has<Object>())
                    _nouns.Add(textEntity);
                else if (textEntity.Has<Action>())
                    _verbs.Add(textEntity);
            }
            _prevTextPositions = textPositions;
            // if the position of text entities did not change, rules could not have changed
            if (!diffFound)
                return;
            // ===========================
            // Now we start handling rules
            // ===========================

            // Step 1: Remove all Property Components from all entities (except IsPush from Text or Hedge entities)
            //      This is to ensure that broken rules do not persist
            ClearProperties();

            // Step 2: Find Rules
            foreach (Position TextPosition in textPositions.Values)
            {

            }
            foreach (Entity Is in _IsEntities)
            {
                
            }

            // Step 3: Apply Rules


            // Cleanup, Cleanup, everybody everywhere
            _nouns.Clear();
            _verbs.Clear();
            _IsEntities.Clear();

            

            /*
            // do all the update logic within this conditional, then rules only get parsed when the player moves instead of every update

            // To be efficient, maybe do an initial loop through the active entities, and keep lists
            //  of entity ids such that one list contains only the entityIds of "Is" text blocks. That way, when you need to find
            //  if an "is" block exists next to an object text block that you've found, you only have to loop through that list
            //  instead of every single entity.
            // You could also keep all the object text blocks in a list, so that you loop through those to find the rules.
            // It would probably also be good to keep a List of the entity ids of the Text blocks that are not "is" blocks,
            //  which would be useful when parsing for the third block in a rule. Since rules like "Water is You" and "Water is Baba"
            //  could both exist, both object and property text blocks would be necessary for the third block entity list.
            // It may also be good to keep a list of the ids of the actual object entities, so that you can apply rules to them by
            //  looping through that list, and that way you know you're only changing properties of actual objects, and not any text blocks.
            // I did a similar concept in some of my other systems. Like in the movement system, i did an initial run through
            //  of all the active entities observed by the system, and sorted them into lists based on if they were
            //  "isYou", "isStop", or "isPush", and thus ignored any entities tracked by this system that do not have those
            //  requirements and thus would not be relevant.
            //
            // In this initial run through of active entities, it may also be good to reset all of the properties to false
            //  (Except for text blocks, which should always be "isPush" and hedges should (preferrably) always be "isStop")
            //
            // As for the actual entities, you probably only want to require that the entities tracked by this system have
            // "position", "property", and "object" Components. Since the rules system will need to update non-text entities,
            // it shouldn't mandate that entities have a text component. 
            //
            // So that the last note is true, i added a new ObjectType, "Is". Because the "Is" text entity creator did not have an
            //  "object" component.
            //
            // Last note (i think) : something to consider is that there is supposed to be a particle/sound effect when a new
            //  "is Win" rule is formed. If you can make sure that the system knows when a new rule is formed, and specify
            //  exactly where in the code the system discovers that, i should be able to write the code that will generate
            //  the particle effect and play the sound.
            //
            // Here is the link to the monogame extended github: https://github.com/craftworkgames/MonoGame.Extended/tree/develop/src/cs/MonoGame.Extended.Entities
            */
        }

        private void ClearProperties()
        {
            foreach (int entityID in ActiveEntities)
            {
                // Clear all Properties from all entities
                Entity entity = GetEntity(entityID);
                entity.Get<Property>().Clear();
                // Add isPush to all Text Entities or Hedge Objects
                if (entity.Has<Text>() || entity.Get<Object>().Equals(ObjectType.Hedge))
                    entity.Get<Property>().isPush = true;
            }
        }

        // Note: Valid Rules are when Text entities align following one of the following formats:
        //              Format 1: [Noun] [Is] [Verb]
        //              Format 2: [Noun1] [Is] [Noun2]
        //    where Nouns are entities with a Text and Object component.
        //    and   Verbs are entities with a Text and Action component.
        // Any and all other orientations of entities will not form a valid rule.
        // case (Format 1): all Objects with the Noun's ObjectType gain the Property component associated with the Verb's Action component.
        // case (Format 2): all of Noun1's components are replaced with Noun2's components, except Position
        private void ApplyRule(Entity[] rule)
        {
            // Format 1: [Noun] [Is] [Verb]
            if (_nouns.Contains(rule[0]) && _verbs.Contains(rule[2]))
            {
                // TODO: find all Objects with the Noun's ObjectType and attach whatever Property component is associated with the Verb's Action component.
            }
            // Format 2: [Noun1] [Is] [Noun2]
            else if (_nouns.Contains(rule[0]) && _nouns.Contains(rule[2]))
            {
                // TODO: Replace all of Noun1's components with Noun2's components (except Position)
            }
            else
            {
                Debug.WriteLine("Found an invalid rule");
            }
        }
    }
}