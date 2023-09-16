using Engine.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;

namespace Baba.ECS
{
    public static class EntityCreator
    {
        public static void CreateWall(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var wall = world.CreateEntity();
            wall.Attach(new Position(position));
            wall.Attach(new Object(ObjectType.Wall));
            wall.Attach(new Animation(spriteSheet, Color.Gray));
            wall.Attach(new Property());
        }
        public static void CreateWallText(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var wallText = world.CreateEntity();
            wallText.Attach(new Position(position));
            wallText.Attach(new Object(ObjectType.Wall));
            wallText.Attach(new Text(TextType.Wall));
            wallText.Attach(new Animation(spriteSheet, Color.Gray));
            wallText.Attach(new Property(isPush: true));
        }
        public static void CreateRock(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var rock = world.CreateEntity();
            rock.Attach(new Position(position));
            rock.Attach(new Object(ObjectType.Rock));
            rock.Attach(new Animation(spriteSheet, Color.Brown));
            rock.Attach(new Property());
        }
        public static void CreateRockText(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var rockText = world.CreateEntity();
            rockText.Attach(new Position(position));
            rockText.Attach(new Object(ObjectType.Rock));
            rockText.Attach(new Text(TextType.Rock));
            rockText.Attach(new Animation(spriteSheet, Color.Brown));
            rockText.Attach(new Property(isPush: true));
        }
        public static void CreateFlag(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var flag = world.CreateEntity();
            flag.Attach(new Position(position));
            flag.Attach(new Object(ObjectType.Flag));
            flag.Attach(new Animation(spriteSheet, Color.Yellow));
            flag.Attach(new Property());
        }
        public static void CreateFlagText(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var flagText = world.CreateEntity();
            flagText.Attach(new Position(position));
            flagText.Attach(new Object(ObjectType.Flag));
            flagText.Attach(new Text(TextType.Flag));
            flagText.Attach(new Animation(spriteSheet, Color.Yellow));
            flagText.Attach(new Property());
        }
        public static void CreateBigBlue(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var bigBlue = world.CreateEntity();
            bigBlue.Attach(new Position(position));
            bigBlue.Attach(new Object(ObjectType.BigBlue));
            bigBlue.Attach(new Animation(spriteSheet, Color.White));
            bigBlue.Attach(new Property(isYou: true));
        }
        public static void CreateBigBlueText(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var bigBlueText = world.CreateEntity();
            bigBlueText.Attach(new Position(position));
            bigBlueText.Attach(new Object(ObjectType.BigBlue));
            bigBlueText.Attach(new Text(TextType.BigBlue));
            bigBlueText.Attach(new Animation(spriteSheet, Color.White));
            bigBlueText.Attach(new Property(isPush: true));
        }
        public static void CreateFloor(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var floor = world.CreateEntity();
            floor.Attach(new Position(position));
            floor.Attach(new Object(ObjectType.Floor));
            floor.Attach(new Animation(spriteSheet, Color.SandyBrown));
        }
        public static void CreateGrass(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var grass = world.CreateEntity();
            grass.Attach(new Position(position));
            grass.Attach(new Object(ObjectType.Grass));
            grass.Attach(new Animation(spriteSheet, Color.Green));
        }
        public static void CreateWater(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var water = world.CreateEntity();
            water.Attach(new Position(position));
            water.Attach(new Object(ObjectType.Water));
            water.Attach(new Animation(spriteSheet, Color.Blue));
            water.Attach(new Property());
        }
        public static void CreateWaterText(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var waterText = world.CreateEntity();
            waterText.Attach(new Position(position));
            waterText.Attach(new Object(ObjectType.Water));
            waterText.Attach(new Text(TextType.Water));
            waterText.Attach(new Animation(spriteSheet, Color.Blue));
            waterText.Attach(new Property(isPush: true));
        }
        public static void CreateLava(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var lava = world.CreateEntity();
            lava.Attach(new Position(position));
            lava.Attach(new Object(ObjectType.Lava));
            lava.Attach(new Animation(spriteSheet, Color.Red));
            lava.Attach(new Property());
        }
        public static void CreateLavaText(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var lavaText = world.CreateEntity();
            lavaText.Attach(new Position(position));
            lavaText.Attach(new Object(ObjectType.Lava));
            lavaText.Attach(new Text(TextType.Lava));
            lavaText.Attach(new Animation(spriteSheet, Color.Red));
            lavaText.Attach(new Property(isPush: true));
        }
        public static void CreateHedge(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var hedge = world.CreateEntity();
            hedge.Attach(new Position(position));
            hedge.Attach(new Object(ObjectType.Hedge));
            hedge.Attach(new Animation(spriteSheet, Color.DarkGreen));
            hedge.Attach(new Property(isStop: true));
        }

        public static void CreateIsText(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var isText = world.CreateEntity();
            isText.Attach(new Position(position));
            isText.Attach(new Object(ObjectType.Is));
            isText.Attach(new Text(TextType.Is));
            isText.Attach(new Animation(spriteSheet, Color.White));
            isText.Attach(new Property(isPush: true));
        }

        public static void CreateStopText(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var stopText = world.CreateEntity();
            stopText.Attach(new Position(position));
            stopText.Attach(new Text(TextType.Stop));
            stopText.Attach(new Action(ActionType.Stop));
            stopText.Attach(new Animation(spriteSheet, Color.Green));
            stopText.Attach(new Property(isPush: true));
        }
        public static void CreatePushText(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var pushText = world.CreateEntity();
            pushText.Attach(new Position(position));
            pushText.Attach(new Text(TextType.Push));
            pushText.Attach(new Action(ActionType.Push));
            pushText.Attach(new Animation(spriteSheet, Color.Brown));
            pushText.Attach(new Property(isPush: true));
        }
        public static void CreateYouText(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var youText = world.CreateEntity();
            youText.Attach(new Position(position));
            youText.Attach(new Text(TextType.You));
            youText.Attach(new Action(ActionType.You));
            youText.Attach(new Animation(spriteSheet, Color.Purple));
            youText.Attach(new Property(isPush: true));
        }
        public static void CreateWinText(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var winText = world.CreateEntity();
            winText.Attach(new Position(position));
            winText.Attach(new Text(TextType.Win));
            winText.Attach(new Action(ActionType.Win));
            winText.Attach(new Animation(spriteSheet, Color.Yellow));
            winText.Attach(new Property(isPush: true));
        }
        public static void CreateSinkText(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var sinkText = world.CreateEntity();
            sinkText.Attach(new Position(position));
            sinkText.Attach(new Text(TextType.Sink));
            sinkText.Attach(new Action(ActionType.Sink));
            sinkText.Attach(new Animation(spriteSheet, Color.Blue));
            sinkText.Attach(new Property(isPush: true));
        }
        public static void CreateKillText(World world, Vector2 position, Texture2D[] spriteSheet)
        {
            var killText = world.CreateEntity();
            killText.Attach(new Position(position));
            killText.Attach(new Text(TextType.Kill));
            killText.Attach(new Action(ActionType.Kill));
            killText.Attach(new Animation(spriteSheet, Color.Red));
            killText.Attach(new Property(isPush: true));
        }
    }
}
