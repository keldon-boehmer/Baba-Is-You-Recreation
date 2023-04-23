using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace BigBlue
{

    public enum InputAction
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Reset,
        Undo
    }

    public class InputManager
    {
        private static InputManager instance;
        private Dictionary<InputAction, Keys> keyBindings;
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        private const string KeyBindingsFileName = "keybindings.xml";

        public bool moveUp = false;
        public bool moveDown = false;
        public bool moveLeft = false;
        public bool moveRight = false;
        public bool reset = false;
        public bool undo = false;

        private InputManager()
        {
            // Initialize default key bindings
            List<Keys> keyBinds = LoadKeyBindings();

            keyBindings = new Dictionary<InputAction, Keys>()
            {
                { InputAction.MoveUp, keyBinds[0] },
                { InputAction.MoveDown, keyBinds[1] },
                { InputAction.MoveLeft, keyBinds[2] },
                { InputAction.MoveRight, keyBinds[3] },
                { InputAction.Reset, keyBinds[4] },
                { InputAction.Undo, keyBinds[5] }
            };

            SaveKeyBindings();
        }

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputManager();
                }
                return instance;
            }
        }

        public void ResetInputs()
        {
            moveUp = false;
            moveDown = false;
            moveLeft = false;
            moveRight = false;
            reset = false;
            undo = false;
        }

        public void ProcessInput()
        {
            currentKeyboardState = Keyboard.GetState();

            // Execute actions
            if (IsActionTriggered(InputAction.MoveUp))
            {
                moveUp = true;
            }
            else if (IsActionTriggered(InputAction.MoveDown))
            {
                moveDown = true;
            }
            else if (IsActionTriggered(InputAction.MoveLeft))
            {
                moveLeft = true;
            }
            else if (IsActionTriggered(InputAction.MoveRight))
            {
                moveRight = true;
            }
            else if (IsActionTriggered(InputAction.Reset))
            {
                reset = true;
            }
            else if (IsActionTriggered(InputAction.Undo))
            {
                undo = true;
            }

            previousKeyboardState = currentKeyboardState;
        }

        private bool IsActionTriggered(InputAction inputAction)
        {
            Keys key;
            if (keyBindings.TryGetValue(inputAction, out key))
            {
                if (currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key))
                {
                    return true;
                }
            }

            return false;
        }

        public void SetKeyBinding(InputAction inputAction, Keys newKey)
        {
            if (!keyBindings.ContainsValue(newKey))
            {
                keyBindings[inputAction] = newKey;
                SaveKeyBindings();
            }
        }

        public string GetKeyBindingString(InputAction inputAction)
        {
            return keyBindings[inputAction].ToString();
        }

        private List<Keys> LoadKeyBindings()
        {
            if (File.Exists(KeyBindingsFileName))
            {
                // Fixing the bug where the last line of the XML file keys messed up.
                string[] lines = File.ReadAllLines(KeyBindingsFileName);
                lines[lines.Length - 1] = "</ArrayOfKeys>";
                File.WriteAllLines(KeyBindingsFileName, lines);

                XmlSerializer serializer = new XmlSerializer(typeof(List<Keys>));
                using (Stream stream = File.OpenRead(KeyBindingsFileName))
                {
                    return (List<Keys>)serializer.Deserialize(stream);
                }
            }
            return new List<Keys>() { Keys.W, Keys.S, Keys.A, Keys.D, Keys.R, Keys.Z };
        }

        private void SaveKeyBindings()
        {
            List<Keys> keyBinds = new List<Keys>();
            
            foreach (Keys key in keyBindings.Values)
            {
                keyBinds.Add(key);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Keys>));
            using (Stream stream = File.OpenWrite(KeyBindingsFileName))
            {
                serializer.Serialize(stream, keyBinds);
            }
        }
    }
}
