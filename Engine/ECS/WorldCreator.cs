using BigBlue;
using Engine.GameState;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlue
{
    public static class WorldCreator
    {
        private static Texture2D[] bigBlueSheet;
        private static Texture2D[] flagSheet;
        private static Texture2D[] floorSheet;
        private static Texture2D[] flowersSheet;
        private static Texture2D[] grassSheet;
        private static Texture2D[] hedgeSheet;
        private static Texture2D[] lavaSheet;
        private static Texture2D[] rockSheet;
        private static Texture2D[] wallSheet;
        private static Texture2D[] waterSheet;
        private static Texture2D[] wordBabaSheet;
        private static Texture2D[] wordFlagSheet;
        private static Texture2D[] wordIsSheet;
        private static Texture2D[] wordKillSheet;
        private static Texture2D[] wordLavaSheet;
        private static Texture2D[] wordPushSheet;
        private static Texture2D[] wordRockSheet;
        private static Texture2D[] wordSinkSheet;
        private static Texture2D[] wordStopSheet;
        private static Texture2D[] wordWallSheet;
        private static Texture2D[] wordWaterSheet;
        private static Texture2D[] wordWinSheet;
        private static Texture2D[] wordYouSheet;
        public static World CreateWorld(ContentManager contentManager, Level level)
        {
            InitializeSheets(contentManager);
            // TODO: parse the level file
            // TODO: return the world
            throw new NotImplementedException();
        }

        private static bool sheetsInitialized = false;
        private static void InitializeSheets(ContentManager contentManager)
        {
            if (!sheetsInitialized)
            {
                Texture2D bigBlue = contentManager.Load<Texture2D>("Textures/BigBlue/BigBlue");
                bigBlueSheet = new Texture2D[] { bigBlue };
                flagSheet = createSpriteSheet(contentManager, "Flag", "flag");
                floorSheet = createSpriteSheet(contentManager, "Floor", "floor");
                flowersSheet = createSpriteSheet(contentManager, "Flowers", "flowers");
                grassSheet = createSpriteSheet(contentManager, "Grass", "grass");
                hedgeSheet = createSpriteSheet(contentManager, "Hedge", "hedge");
                lavaSheet = createSpriteSheet(contentManager, "Lava", "lava");
                rockSheet = createSpriteSheet(contentManager, "Rock", "rock");
                wallSheet = createSpriteSheet(contentManager, "Wall", "wall");
                waterSheet = createSpriteSheet(contentManager, "Water", "water");
                wordBabaSheet = createSpriteSheet(contentManager, "Word-Baba", "word-baba");
                wordFlagSheet = createSpriteSheet(contentManager, "Word-Flag", "word-flag");
                wordIsSheet = createSpriteSheet(contentManager, "Word-Is", "word-is");
                wordKillSheet = createSpriteSheet(contentManager, "Word-Kill", "word-kill");
                wordLavaSheet = createSpriteSheet(contentManager, "Word-Lava", "word-lava");
                wordPushSheet = createSpriteSheet(contentManager, "Word-Push", "word-push");
                wordRockSheet = createSpriteSheet(contentManager, "Word-Rock", "word-rock");
                wordSinkSheet = createSpriteSheet(contentManager, "Word-Sink", "word-sink");
                wordStopSheet = createSpriteSheet(contentManager, "Word-Stop", "word-stop");
                wordWallSheet = createSpriteSheet(contentManager, "Word-Wall", "word-wall");
                wordWaterSheet = createSpriteSheet(contentManager, "Word-Water", "word-water");
                wordWinSheet = createSpriteSheet(contentManager, "Word-Win", "word-win");
                wordYouSheet = createSpriteSheet(contentManager, "Word-You", "word-you");
            }
        }
        private static Texture2D[] createSpriteSheet(ContentManager contentManager, string parentFolder, string objectName)
        {
            string name0 = $"Textures/{parentFolder}/{objectName}_0";
            string name1 = $"Textures/{parentFolder}/{objectName}_1";
            string name2 = $"Textures/{parentFolder}/{objectName}_2";

            Texture2D sprite0 = contentManager.Load<Texture2D>(name0);
            Texture2D sprite1 = contentManager.Load<Texture2D>(name1);
            Texture2D sprite2 = contentManager.Load<Texture2D>(name2);

            Texture2D[] spriteSheet = new Texture2D[] { sprite0, sprite1, sprite2 };
            return spriteSheet;
        }
    }
}
