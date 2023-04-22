using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BigBlue
{
    public class ControlsView : GameStateView
    {
        private SpriteFont font;
        private SpriteFont fontSelect;
        private const string CONTROLS_MESSAGE = "Controls";
        private const string WAITING_MESSAGE = "Waiting for key press... ";

        private string control1;
        private string control2;
        private string control3;
        private string control4;
        private string control5;
        private string control6;

        private InputAction currentSelection = InputAction.MoveUp;

        private bool waitingForBind = false;

        // This bool is used for when the user enters this game state.
        // Since the enter key is used to navigate both the main menu and controls menu
        // we want it to be released and pressed again before an action is rebound,
        // which is also triggered by the user pressing enter.
        private bool waitingForEnterRelease = true;

        KeyboardState prevKeyState;
        KeyboardState currentKeyState;

        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);

            prevKeyState = Keyboard.GetState();

            setControlStrings();

        }

        public override void loadContent(ContentManager contentManager)
        {
            font = contentManager.Load<SpriteFont>("Fonts/menu");
            fontSelect = contentManager.Load<SpriteFont>("Fonts/menu-select");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (waitingForBind)
            {
                processRebindInput();
                return GameStateEnum.Controls;
            }
            else if (waitingForEnterRelease)
            {
                processEnterReleaseInput();
                return GameStateEnum.Controls;
            }
            return processMenuingInput();
        }

        private void processRebindInput()
        {
            currentKeyState = Keyboard.GetState();

            if (currentKeyState.IsKeyDown(Keys.Escape))
            {
                waitingForBind = false;
                prevKeyState = currentKeyState;
                return;
            }

            Keys[] pressedKeys = currentKeyState.GetPressedKeys();

            if (pressedKeys.Length > 0)
            {
                Keys rebindKey = pressedKeys[pressedKeys.Length - 1];
                if (prevKeyState.IsKeyUp(rebindKey))
                {
                    InputManager.Instance.SetKeyBinding(currentSelection, rebindKey);
                    waitingForBind = false;
                }
            }
            prevKeyState = currentKeyState;
        }

        private void processEnterReleaseInput()
        {
            KeyboardState tempCurrentState = Keyboard.GetState();
            if (tempCurrentState.IsKeyUp(Keys.Enter))
            {
                currentKeyState = tempCurrentState;
                waitingForEnterRelease = false;
            }
            prevKeyState = currentKeyState;
        }

        private GameStateEnum processMenuingInput()
        {
            currentKeyState = Keyboard.GetState();
            if (currentKeyState.IsKeyDown(Keys.W) && prevKeyState.IsKeyUp(Keys.W))
            {
                if ((int)currentSelection > 0)
                {
                    currentSelection--;
                }
            }
            else if (currentKeyState.IsKeyDown(Keys.S) && prevKeyState.IsKeyUp(Keys.S))
            {
                if ((int)currentSelection < 5)
                {
                    currentSelection++;
                }
            }
            else if (currentKeyState.IsKeyDown(Keys.Escape) && prevKeyState.IsKeyUp(Keys.Escape))
            {
                prevKeyState = currentKeyState;
                currentSelection = 0;
                waitingForEnterRelease = true;
                return GameStateEnum.MainMenu;
            }
            else if (currentKeyState.IsKeyDown(Keys.Enter) && prevKeyState.IsKeyUp(Keys.Enter))
            {
                waitingForBind = true;
            }
            prevKeyState = currentKeyState;
            return GameStateEnum.Controls;
        }

        public override void render(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (waitingForBind)
            {
                renderWaitingMessage();
            }
            else
            {
                renderControlsMenu();
            }
            spriteBatch.End();
        }

        private void renderControlsMenu()
        {
            Vector2 stringSize = font.MeasureString(CONTROLS_MESSAGE);
            spriteBatch.DrawString(font, CONTROLS_MESSAGE,
                new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, graphics.PreferredBackBufferHeight / 10), Color.Yellow);

            float startY = drawKeybind(currentSelection == InputAction.MoveUp ? fontSelect : font, control1, graphics.PreferredBackBufferHeight * 3 / 10, currentSelection == InputAction.MoveUp ? Color.Yellow : Color.Blue);
            startY = drawKeybind(currentSelection == InputAction.MoveDown ? fontSelect : font, control2, startY, currentSelection == InputAction.MoveDown ? Color.Yellow : Color.Blue);
            startY = drawKeybind(currentSelection == InputAction.MoveLeft ? fontSelect : font, control3, startY, currentSelection == InputAction.MoveLeft ? Color.Yellow : Color.Blue);
            startY = drawKeybind(currentSelection == InputAction.MoveRight ? fontSelect : font, control4, startY, currentSelection == InputAction.MoveRight ? Color.Yellow : Color.Blue);
            startY = drawKeybind(currentSelection == InputAction.Reset ? fontSelect : font, control5, startY, currentSelection == InputAction.Reset ? Color.Yellow : Color.Blue);
            drawKeybind(currentSelection == InputAction.Undo ? fontSelect : font, control6, startY, currentSelection == InputAction.Undo ? Color.Yellow : Color.Blue);

        }

        private void renderWaitingMessage()
        {
            Vector2 stringSize = font.MeasureString(WAITING_MESSAGE);
            spriteBatch.DrawString(font, WAITING_MESSAGE,
               new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, graphics.PreferredBackBufferHeight / 2 - stringSize.Y / 2), Color.Yellow);
        }

        private float drawKeybind(SpriteFont font, string text, float y, Color color)
        {
            Vector2 stringSize = font.MeasureString(text);
            spriteBatch.DrawString(
                font,
                text,
                new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y),
                color);

            return y + stringSize.Y;
        }

        private void setControlStrings()
        {
            control1 = "Move Up - " + InputManager.Instance.GetKeyBindingString(InputAction.MoveUp);
            control2 = "Move Down - " + InputManager.Instance.GetKeyBindingString(InputAction.MoveDown);
            control3 = "Move Left - " + InputManager.Instance.GetKeyBindingString(InputAction.MoveLeft);
            control4 = "Move Right - " + InputManager.Instance.GetKeyBindingString(InputAction.MoveRight);
            control5 = "Reset - " + InputManager.Instance.GetKeyBindingString(InputAction.Reset);
            control6 = "Undo - " + InputManager.Instance.GetKeyBindingString(InputAction.Undo);
        }

        public override void update(GameTime gameTime)
        {
            setControlStrings();
        }
    }
}
