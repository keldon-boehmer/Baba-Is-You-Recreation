using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;

namespace BigBlue.ECS
{
    internal class RulesSystem : EntityUpdateSystem
    {
        private ComponentMapper<Position> _positionMapper;
        private ComponentMapper<Property> _propertyMapper;
        private ComponentMapper<Object> _objectMapper;
        public RulesSystem() 
            : base(Aspect.All(typeof(Position), typeof(Property), typeof(Object)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _positionMapper = mapperService.GetMapper<Position>();
            _propertyMapper = mapperService.GetMapper<Property>();
            _objectMapper = mapperService.GetMapper<Object>();
        }

        public override void Update(GameTime gameTime)
        {
            if (GameStatus.playerMoved)
            {
                // do all the update logic within this conditional, then rules only get parsed when the player moves instead of every update

                // To be efficient, maybe do an initial loop through the active entities, and keep lists
                // of entity ids such that one list contains only the entityIds of "Is" text blocks. That way, when you need to find if an "is" block exists
                // next to an object text block that you've found, you only have to loop through that list instead of every single entity.
                // You could also keep all the object text blocks in a list, so that you loop through those to find the rules.
                // It would probably also be good to keep a List of the entity ids of the Text blocks that are not "is" blocks, which would be useful when parsing for
                // the third block in a rule. Since rules like "Water is You" and "Water is Baba" could both exist, both object and property text blocks would be necessary
                // for the third block entity list.
                // It may also be good to keep a list of the ids of the actual object entities, so that you can apply rules to them by looping through that list,
                // and that way you know you're only changing properties of actual objects, and not any text blocks.
                // I did a similar concept in some of my other systems. Like in the movement system, i did an initial run through
                // of all the active entities observed by the system,
                // and sorted them into lists based on if they were "isYou", "isStop", or "isPush", and thus ignored any entities tracked by this system that do not
                // have those requirements and thus would not be relevant.
                //
                // In this initial run through of active entities, it may also be good to reset all of the properties to false (Except for text blocks, which should always
                // be "isPush" and hedges should (preferrably) always be "isStop")
                //
                // As for the actual entities, you probably only want to require that the entities tracked by this system have "position", "property", and "object" Components.
                // Since the rules system will need to update non-text entities, it shouldn't mandate that entities have a text component. 
                //
                // So that the last note is true, i added a new ObjectType, "Is". Because the "Is" text entity creator did not have an "object" component.
                //
                // Last note (i think) : something to consider is that there is supposed to be a particle/sound effect when a new "is Win" rule is formed. If you can
                // make sure that the system knows when a new rule is formed, and specify exactly where in the code the system discovers that, i should be able to
                // write the code that will generate the particle effect and play the sound.
                //
                // Below is the link to the monogame extended github, which you can consult if you run into any issues/questions about functionality.
                // https://github.com/craftworkgames/MonoGame.Extended/tree/develop/src/cs/MonoGame.Extended.Entities

            }
        }
    }
}
