using BigBlue.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using System;
using System.Collections.Generic;

namespace BigBlue
{
    public static class WorldCreator
    {
        private static Dictionary<char, Texture2D[]> spriteSheets = new Dictionary<char, Texture2D[]>();

        public static World CreateWorld(Level level, int screenWidth, int screenHeight, SpriteBatch spriteBatch)
        {
            int gridHeight = screenHeight / level.Height;
            int gridWidth = gridHeight;
            int gameplayWidth = gridWidth * level.Width;
            int renderStartX = (screenWidth - gameplayWidth) / 2;

            if (gameplayWidth > screenWidth)
            {
                gridWidth = screenWidth / level.Width;
                renderStartX = 0;
            }

            // TODO: add ALL systems to WorldBuilder, build world
            var world = new WorldBuilder()
                //.AddSystem(new MovementSystem())
                //.AddSystem(new RulesSystem())
                .AddSystem(new KillSystem(gridWidth, gridHeight, renderStartX))
                .AddSystem(new WinSystem())
                .AddSystem(new AnimationSystem(gridWidth, gridHeight, renderStartX, spriteBatch))
                .AddSystem(new CloneSystem(gridWidth, gridHeight, renderStartX, spriteBatch))
                .Build();

            // TODO: Create Entities based on the Level's Object Layout
            for (int i = 0; i < level.ObjectLayout.GetLength(0); i++)
            {
                for (int j = 0; j < level.ObjectLayout.GetLength(1); j++)
                {
                    if (level.ObjectLayout[i, j] == ' ') continue;

                    Vector2 position = new Vector2(i, j);
                    Texture2D[] spriteSheet = spriteSheets[level.ObjectLayout[i, j]];
                    switch (level.ObjectLayout[i, j])
                    {
                        case 'w':
                            EntityCreator.CreateWall(world, position, spriteSheet);
                            break;
                        case 'r':
                            EntityCreator.CreateRock(world, position, spriteSheet);
                            break;
                        case 'f':
                            EntityCreator.CreateFlag(world, position, spriteSheet);
                            break;
                        case 'b':
                            EntityCreator.CreateBigBlue(world, position, spriteSheet);
                            break;
                        case 'l':
                            EntityCreator.CreateFloor(world, position, spriteSheet);
                            break;
                        case 'g':
                            EntityCreator.CreateGrass(world, position, spriteSheet);
                            break;
                        case 'a':
                            EntityCreator.CreateWater(world, position, spriteSheet);
                            break;
                        case 'v':
                            EntityCreator.CreateLava(world, position, spriteSheet);
                            break;
                        case 'h':
                            EntityCreator.CreateHedge(world, position, spriteSheet);
                            break;
                        default:
                            throw new Exception();
                    }
                }
            }

            // TODO: Create Entities based on the Level's Text Layout
            for (int i = 0; i < level.TextLayout.GetLength(0); i++)
            {
                for (int j = 0; j < level.TextLayout.GetLength(1); j++)
                {
                    if (level.ObjectLayout[i, j] == ' ') continue;

                    Vector2 position = new Vector2(i, j);
                    Texture2D[] spriteSheet = spriteSheets[level.ObjectLayout[i, j]];
                    switch (level.TextLayout[i, j])
                    {
                        case 'W':
                            EntityCreator.CreateWallText(world, position, spriteSheet);
                            break;
                        case 'R':
                            EntityCreator.CreateRockText(world, position, spriteSheet);
                            break;
                        case 'F':
                            EntityCreator.CreateFlagText(world, position, spriteSheet);
                            break;
                        case 'B':
                            EntityCreator.CreateBigBlueText(world, position, spriteSheet);
                            break;
                        case 'I':
                            EntityCreator.CreateIsText(world, position, spriteSheet);
                            break;
                        case 'S':
                            EntityCreator.CreateStopText(world, position, spriteSheet);
                            break;
                        case 'P':
                            EntityCreator.CreatePushText(world, position, spriteSheet);
                            break;
                        case 'V':
                            EntityCreator.CreateLavaText(world, position, spriteSheet);
                            break;
                        case 'A':
                            EntityCreator.CreateWaterText(world, position, spriteSheet);
                            break;
                        case 'Y':
                            EntityCreator.CreateYouText(world, position, spriteSheet);
                            break;
                        case 'X':
                            EntityCreator.CreateWinText(world, position, spriteSheet);
                            break;
                        case 'N':
                            EntityCreator.CreateSinkText(world, position, spriteSheet);
                            break;
                        case 'K':
                            EntityCreator.CreateKillText(world, position, spriteSheet);
                            break;
                        default:
                            throw new Exception();
                    }
                }
            }

            return world;
        }

        #region Sprite Sheet Loading
        private static bool sheetsInitialized = false;
        public static void InitializeSheets(ContentManager contentManager)
        {
            if (!sheetsInitialized)
            {
                sheetsInitialized = true;
                // Creating sprite sheets for Object Entities (lowercase char)
                spriteSheets['w'] = createSpriteSheet(contentManager, "Wall", "wall");
                spriteSheets['r'] = createSpriteSheet(contentManager, "Rock", "rock");
                spriteSheets['f'] = createSpriteSheet(contentManager, "Flag", "flag");
                Texture2D bigBlue = contentManager.Load<Texture2D>("Textures/BigBlue/BigBlue");
                spriteSheets['b'] = new Texture2D[] { bigBlue };
                spriteSheets['l'] = createSpriteSheet(contentManager, "Floor", "floor");
                //charToSpriteSheet['f'] = createSpriteSheet(contentManager, "Flowers", "flowers");
                spriteSheets['g'] = createSpriteSheet(contentManager, "Grass", "grass");
                spriteSheets['a'] = createSpriteSheet(contentManager, "Water", "water");
                spriteSheets['v'] = createSpriteSheet(contentManager, "Lava", "lava");
                spriteSheets['h'] = createSpriteSheet(contentManager, "Hedge", "hedge");

                // Creating sprite sheets for Text Entities (uppercase char)
                spriteSheets['W'] = createSpriteSheet(contentManager, "Word-Wall", "word-wall");
                spriteSheets['R'] = createSpriteSheet(contentManager, "Word-Rock", "word-rock");
                spriteSheets['F'] = createSpriteSheet(contentManager, "Word-Flag", "word-flag");
                spriteSheets['B'] = createSpriteSheet(contentManager, "Word-Baba", "word-baba");
                spriteSheets['I'] = createSpriteSheet(contentManager, "Word-Is", "word-is");
                spriteSheets['S'] = createSpriteSheet(contentManager, "Word-Stop", "word-stop");
                spriteSheets['P'] = createSpriteSheet(contentManager, "Word-Push", "word-push");
                spriteSheets['V'] = createSpriteSheet(contentManager, "Word-Lava", "word-lava");
                spriteSheets['A'] = createSpriteSheet(contentManager, "Word-Water", "word-water");
                spriteSheets['Y'] = createSpriteSheet(contentManager, "Word-You", "word-you");
                spriteSheets['X'] = createSpriteSheet(contentManager, "Word-Win", "word-win");
                spriteSheets['N'] = createSpriteSheet(contentManager, "Word-Sink", "word-sink");
                spriteSheets['K'] = createSpriteSheet(contentManager, "Word-Kill", "word-kill");
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
