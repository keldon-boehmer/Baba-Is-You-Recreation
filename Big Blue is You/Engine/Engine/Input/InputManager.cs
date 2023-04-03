using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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

        private InputManager()
        {
            // Initialize default key bindings
            List<Keys> keyBinds = LoadKeyBindings();
            SaveKeyBindings();

            keyBindings = new Dictionary<InputAction, Keys>()
            {
                { InputAction.MoveUp, keyBinds[0] },
                { InputAction.MoveDown, keyBinds[1] },
                { InputAction.MoveLeft, keyBinds[2] },
                { InputAction.MoveRight, keyBinds[3] },
                { InputAction.Reset, keyBinds[4] },
                { InputAction.Undo, keyBinds[5] }
            };

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

        public void Update()
        {
            currentKeyboardState = Keyboard.GetState();

            // Execute actions
            if (IsActionTriggered(InputAction.MoveUp))
            {

            }
            else if (IsActionTriggered(InputAction.MoveDown))
            {

            }
            else if (IsActionTriggered(InputAction.MoveLeft))
            {

            }
            else if (IsActionTriggered(InputAction.MoveRight))
            {

            }
            else if (IsActionTriggered(InputAction.Reset))
            {

            }
            else if (IsActionTriggered(InputAction.Undo))
            {

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
