using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlue.ECS
{
    public static class EntityCreator
    {
        private const string BIGBLUE_FILENAME = "bigblue.png";
        private const string WALL_FILENAME = "water.png";
        private const string FLAG_FILENAME = "flag.png";
        private const string ROCK_FILENAME = "rock.png";
        private const string WATER_FILENAME = "water.png";

        public static void CreateBigBlue(World world, Vector2 position)
        {
            var bigBlue = world.CreateEntity();
            bigBlue.Attach(new Position(position));
            bigBlue.Attach(new Noun(NounType.Rock));
        }
        public static void CreateWall(World world, Vector2 position)
        {
            var wall = world.CreateEntity();
            wall.Attach(new Position(position));
            wall.Attach(new Noun(NounType.Wall));
        }
        public static void CreateFlag(World world, Vector2 position)
        {
            var flag = world.CreateEntity();
            flag.Attach(new Position(position));
            flag.Attach(new Noun(NounType.Flag));
        }
        public static void CreateRock(World world, Vector2 position)
        {
            var rock = world.CreateEntity();
            rock.Attach(new Position(position));
            rock.Attach(new Noun(NounType.Rock));
        }
        public static void CreateWater(World world, Vector2 position)
        {
            var water = world.CreateEntity();
            water.Attach(new Position(position));
            water.Attach(new Noun(NounType.Water));
        }

        public static void CreateNoun(World world, Vector2 position, string text)
        {
            var noun = world.CreateEntity();
            //noun.Attach(animComp);
            noun.Attach(new Position(position));
            noun.Attach(new Noun(NounType.Noun));
        }

        public static void CreateVerb(World world, Vector2 position, string text)
        {
            var verb = world.CreateEntity();
            verb.Attach(new Position(position));
            verb.Attach(new Noun(NounType.Verb));

        }


        //-------------------------------------------------------------
        // This is what I'm thinking of once we have AnimatedComponent
        //-------------------------------------------------------------
        //public static void CreateBigBlue(World world, AnimatedComponent animComp, Vector2 position)
        //{
        //    var bigBlue = world.CreateEntity();
        //    //bigBlue.Attach(animComp);
        //    bigBlue.Attach(new Position(position));
        //    bigBlue.Attach(new Noun(NounType.Rock));
        //}
        //public static void CreateWall(World world, AnimatedComponent animComp, Vector2 position)
        //{
        //    var wall = world.CreateEntity();
        //    //wall.Attach(animComp);
        //    wall.Attach(new Position(position));
        //    wall.Attach(new Noun(NounType.Wall));
        //}
        //public static void CreateFlag(World world, AnimatedComponent animComp, Vector2 position)
        //{
        //    var flag = world.CreateEntity();
        //    //flag.Attach(animComp);
        //    flag.Attach(new Position(position));
        //    flag.Attach(new Noun(NounType.Flag));
        //}
        //public static void CreateRock(World world, AnimatedComponent animComp, Vector2 position)
        //{
        //    var rock = world.CreateEntity();
        //    //rock.Attach(animComp);
        //    rock.Attach(new Position(position));
        //    rock.Attach(new Noun(NounType.Rock));
        //}
        //public static void CreateWater(World world, AnimatedComponent animComp, Vector2 position)
        //{
        //    var water = world.CreateEntity();
        //    //water.Attach(animComp);
        //    water.Attach(new Position(position));
        //    water.Attach(new Noun(NounType.Water));
        //}

        //public static void CreateNoun(World world, AnimatedComponent animComp, Vector2 position, string text)
        //{
        //    var noun = world.CreateEntity();
        //    //noun.Attach(animComp);
        //    noun.Attach(new Position(position));
        //    noun.Attach(new Noun(NounType.Noun));
        //}

        //public static void CreateVerb(World world, AnimatedComponent animComp, Vector2 position, string text)
        //{
        //    var verb = world.CreateEntity();
        //    //verb.Attach(animComp);
        //    verb.Attach(new Position(position));
        //    verb.Attach(new Noun(NounType.Verb));

        //}
    }
}
