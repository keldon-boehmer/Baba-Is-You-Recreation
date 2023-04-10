using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using System;
using System.Collections.Generic;

namespace BigBlue
{
    public static class WorldCreator
    {
        private static Dictionary<string, Texture2D[]> spriteSheets;

        public static World CreateWorld(Level level, int screenWidth, int screenHeight, SpriteBatch spriteBatch)
        {
            // TODO: add ALL systems to worldBuilder, build world
            /*var world = new WorldBuilder()
                .AddSystem(new AnimationSystem(screenWidth / level.Width, screenHeight / level.Height, spriteBatch))
                .Build();*/
            
            // TODO: create entities based on level's layout
            // TODO: return the world
            throw new NotImplementedException();
        }

        #region Sprite Sheet Loading
        private static bool sheetsInitialized = false;
        public static void InitializeSheets(ContentManager contentManager)
        {
            if (!sheetsInitialized)
            {
                spriteSheets = new Dictionary<string, Texture2D[]>();
                Texture2D bigBlue = contentManager.Load<Texture2D>("Textures/BigBlue/BigBlue");
                spriteSheets["bigBlueSheet"] = new Texture2D[] { bigBlue };
                spriteSheets["flagSheet"] = createSpriteSheet(contentManager, "Flag", "flag");
                spriteSheets["floorSheet"] = createSpriteSheet(contentManager, "Floor", "floor");
                spriteSheets["flowersSheet"] = createSpriteSheet(contentManager, "Flowers", "flowers");
                spriteSheets["grassSheet"] = createSpriteSheet(contentManager, "Grass", "grass");
                spriteSheets["hedgeSheet"] = createSpriteSheet(contentManager, "Hedge", "hedge");
                spriteSheets["lavaSheet"] = createSpriteSheet(contentManager, "Lava", "lava");
                spriteSheets["rockSheet"] = createSpriteSheet(contentManager, "Rock", "rock");
                spriteSheets["wallSheet"] = createSpriteSheet(contentManager, "Wall", "wall");
                spriteSheets["waterSheet"] = createSpriteSheet(contentManager, "Water", "water");
                spriteSheets["wordBabaSheet"] = createSpriteSheet(contentManager, "Word-Baba", "word-baba");
                spriteSheets["wordFlagSheet"] = createSpriteSheet(contentManager, "Word-Flag", "word-flag");
                spriteSheets["wordIsSheet"] = createSpriteSheet(contentManager, "Word-Is", "word-is");
                spriteSheets["wordKillSheet"] = createSpriteSheet(contentManager, "Word-Kill", "word-kill");
                spriteSheets["wordLavaSheet"] = createSpriteSheet(contentManager, "Word-Lava", "word-lava");
                spriteSheets["wordPushSheet"] = createSpriteSheet(contentManager, "Word-Push", "word-push");
                spriteSheets["wordRockSheet"] = createSpriteSheet(contentManager, "Word-Rock", "word-rock");
                spriteSheets["wordSinkSheet"] = createSpriteSheet(contentManager, "Word-Sink", "word-sink");
                spriteSheets["wordStopSheet"] = createSpriteSheet(contentManager, "Word-Stop", "word-stop");
                spriteSheets["wordWallSheet"] = createSpriteSheet(contentManager, "Word-Wall", "word-wall");
                spriteSheets["wordWaterSheet"] = createSpriteSheet(contentManager, "Word-Water", "word-water");
                spriteSheets["wordWinSheet"] = createSpriteSheet(contentManager, "Word-Win", "word-win");
                spriteSheets["wordYouSheet"] = createSpriteSheet(contentManager, "Word-You", "word-you");
                sheetsInitialized = true;
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
        #endregion
    }
}
