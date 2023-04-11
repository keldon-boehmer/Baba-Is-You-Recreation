using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;

namespace BigBlue
{
    public static class EntityCreator
    {
        public static void CreateBigBlue(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var bigBlue = world.CreateEntity();
            bigBlue.Attach(new Position(position));
            bigBlue.Attach(new Noun(NounType.BigBlue));
            bigBlue.Attach(new Animation(spriteSheet, Color.White));
            bigBlue.Attach(new Property());
        }
        public static void CreateWall(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var wall = world.CreateEntity();
            wall.Attach(new Position(position));
            wall.Attach(new Noun(NounType.Wall));
            wall.Attach(new Animation(spriteSheet, Color.Gray));
            wall.Attach(new Property());
        }
        public static void CreateFlag(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var flag = world.CreateEntity();
            flag.Attach(new Position(position));
            flag.Attach(new Noun(NounType.Flag));
            flag.Attach(new Animation(spriteSheet, Color.Yellow));
            flag.Attach(new Property());
        }
        public static void CreateRock(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var rock = world.CreateEntity();
            rock.Attach(new Position(position));
            rock.Attach(new Noun(NounType.Rock));
            rock.Attach(new Animation(spriteSheet, Color.Brown));
            rock.Attach(new Property());
        }
        public static void CreateLava(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var water = world.CreateEntity();
            water.Attach(new Position(position));
            water.Attach(new Noun(NounType.Lava));
            water.Attach(new Animation(spriteSheet, Color.Red));
            water.Attach(new Property());
        }
        public static void CreateWater(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var water = world.CreateEntity();
            water.Attach(new Position(position));
            water.Attach(new Noun(NounType.Water));
            water.Attach(new Animation(spriteSheet, Color.Blue));
            water.Attach(new Property());
        }
        public static void CreateFloor(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var floor = world.CreateEntity();
            floor.Attach(new Position(position));
            floor.Attach(new Noun(NounType.Floor));
            floor.Attach(new Animation(spriteSheet, Color.GhostWhite));
        }
        public static void CreateGrass(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var grass = world.CreateEntity();
            grass.Attach(new Position(position));
            grass.Attach(new Noun(NounType.Grass));
            grass.Attach(new Animation(spriteSheet, Color.Green));
        }
        public static void CreateHedge(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var hedge = world.CreateEntity();
            hedge.Attach(new Position(position));
            hedge.Attach(new Noun(NounType.Hedge));
            hedge.Attach(new Animation(spriteSheet, Color.Green));
        }

        // TODO : Add to these two methods to suit what you need for the rules system. However you want to handle the Text component.
        public static void CreateNoun(World world, Vector2 position, Texture2D[] spriteSheet, Color color)
        {
            var noun = world.CreateEntity();
            noun.Attach(new Position(position));
            noun.Attach(new Noun(NounType.Noun));
            noun.Attach(new Animation(spriteSheet, color));
            noun.Attach(new Property(true));
        }

        public static void CreateVerb(World world, Vector2 position, Texture2D[] spriteSheet, Color color)
        {
            var verb = world.CreateEntity();
            verb.Attach(new Position(position));
            verb.Attach(new Noun(NounType.Verb));
            verb.Attach(new Animation(spriteSheet, color));
            verb.Attach(new Property(true));
        }
    }
}
