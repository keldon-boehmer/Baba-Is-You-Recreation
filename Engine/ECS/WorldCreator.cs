﻿using Baba.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using System;
using System.Collections.Generic;

namespace Baba
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

            // Add ALL systems to WorldBuilder, build world
            var world = new WorldBuilder()
                .AddSystem(new MovementSystem())
                .AddSystem(new RulesSystem(gridWidth, gridHeight, renderStartX))
                .AddSystem(new KillSystem(gridWidth, gridHeight, renderStartX))
                .AddSystem(new WinSystem())
                .AddSystem(new AnimationSystem(gridWidth, gridHeight, renderStartX, spriteBatch))
                .AddSystem(new CloneSystem(gridWidth, gridHeight, renderStartX, spriteBatch))
                .Build();

            // Create Entities based on the Level's Object Layout
            createLayout(level.ObjectLayout, world);

            //Create Entities based on the Level's Text Layout
            createLayout(level.TextLayout, world);

            // Create extra entities offscreen that can be used for cloning purposes
            createExtraEntities(world, level);
            return world;
        }

        private static void createLayout(char[,] layout, World world)
        {
            for (int i = 0; i < layout.GetLength(0); i++)
            {
                for (int j = 0; j < layout.GetLength(1); j++)
                {
                    if (layout[i, j] == ' ') continue;

                    Vector2 position = new Vector2(j, i);
                    Texture2D[] spriteSheet = spriteSheets[layout[i, j]];
                    createAppropriateEntity(layout[i, j], world, position, spriteSheet);
                }
            }
        }

        private static void createAppropriateEntity(char entityType, World world, Vector2 position, Texture2D[] spriteSheet)
        {
            switch (entityType)
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
                    EntityCreator.CreateBaba(world, position, spriteSheet);
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
                    EntityCreator.CreateBabaText(world, position, spriteSheet);
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

        private static void createExtraEntities(World world, Level level)
        {
            createAppropriateEntity('w', world, new Vector2(0, level.Height + 1), spriteSheets['w']);
            createAppropriateEntity('r', world, new Vector2(0, level.Height + 2), spriteSheets['r']);
            createAppropriateEntity('f', world, new Vector2(0, level.Height + 3), spriteSheets['f']);
            createAppropriateEntity('b', world, new Vector2(0, level.Height + 4), spriteSheets['b']);
            createAppropriateEntity('v', world, new Vector2(0, level.Height + 5), spriteSheets['v']);
            createAppropriateEntity('a', world, new Vector2(0, level.Height + 6), spriteSheets['a']);

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
                Texture2D baba = contentManager.Load<Texture2D>("Textures/Baba/Baba");
                spriteSheets['b'] = new Texture2D[] { baba };
                spriteSheets['l'] = createSpriteSheet(contentManager, "Floor", "floor");
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
