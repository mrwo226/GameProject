// Input manager.
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XnaActionLibrary 
{
    public static class InputManager
    {
        #region Action Enumeration

        // The actions that are possible within the game.
        public enum Action
        {
            Ok,
            Back,
            Pause,
            ExitGame,
            MoveCharacterUp,
            MoveCharacterDown,
            MoveCharacterLeft,
            MoveCharacterRight,
            CursorUp,
            CursorDown,
            TotalActionCount,
        }

        // Readable names of each action.
        private static readonly string[] actionNames = 
        {
            "Ok",
            "Back",
            "Pause",
            "Exit Game",
            "Move Character - Up",
            "Move Character - Down",
            "Move Character - Left",
            "Move Character - Right",
            "Move Cursor - Up",
            "Move Cursor - Down",  
        };

        // Returns the readable name of the given action.
        public static string GetActionName(Action action)
        {
            int index = (int)action;
            if ((index < 0) || (index > actionNames.Length))
                throw new ArgumentException("action");
            return actionNames[index];
        }

        #endregion

        #region Support Types

        // Keyboard controls mapped to a particular action.
        public class ActionMap
        {
            public List<Keys> keyboardKeys = new List<Keys>();
        }


        #endregion

        #region Keyboard Data

        // The state of the keyboard as of the last update.
        private static KeyboardState currentKeyboardState;
        public static KeyboardState CurrentKeyboardState
        {
            get { return currentKeyboardState; }
        }

        // The state of the keyboard as of the previous update.
        private static KeyboardState previousKeyboardState;

        // Check if a key is pressed.
        public static bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public static bool IsKeyTriggered(Keys key)
        {
            return (currentKeyboardState.IsKeyDown(key)) && (!previousKeyboardState.IsKeyDown(key));
        }

        #endregion

        #region Action Mapping

        // The action mappings for the game
        private static ActionMap[] actionMaps;
        public static ActionMap[] ActionMaps
        {
            get { return actionMaps; }
        }

        // Reset the action maps to their default values
        private static void ResetActionMaps()
        {
            actionMaps = new ActionMap[(int)Action.TotalActionCount];

            actionMaps[(int)Action.Ok] = new ActionMap();
            actionMaps[(int)Action.Ok].keyboardKeys.Add(Keys.Enter);

            actionMaps[(int)Action.Back] = new ActionMap();
            actionMaps[(int)Action.Back].keyboardKeys.Add(Keys.Back);

            actionMaps[(int)Action.Pause] = new ActionMap();
            actionMaps[(int)Action.Pause].keyboardKeys.Add(Keys.Escape);

            actionMaps[(int)Action.ExitGame] = new ActionMap();
            actionMaps[(int)Action.ExitGame].keyboardKeys.Add(Keys.Escape);

            actionMaps[(int)Action.MoveCharacterUp] = new ActionMap();
            actionMaps[(int)Action.MoveCharacterUp].keyboardKeys.Add(Keys.Up);

            actionMaps[(int)Action.MoveCharacterDown] = new ActionMap();
            actionMaps[(int)Action.MoveCharacterDown].keyboardKeys.Add(Keys.Down);

            actionMaps[(int)Action.MoveCharacterLeft] = new ActionMap();
            actionMaps[(int)Action.MoveCharacterLeft].keyboardKeys.Add(Keys.Left);

            actionMaps[(int)Action.MoveCharacterRight] = new ActionMap();
            actionMaps[(int)Action.MoveCharacterRight].keyboardKeys.Add(Keys.Right);

            actionMaps[(int)Action.CursorUp] = new ActionMap();
            actionMaps[(int)Action.CursorUp].keyboardKeys.Add(Keys.Up);
            actionMaps[(int)Action.CursorUp].keyboardKeys.Add(Keys.Left);

            actionMaps[(int)Action.CursorDown] = new ActionMap();
            actionMaps[(int)Action.CursorDown].keyboardKeys.Add(Keys.Down);
            actionMaps[(int)Action.CursorDown].keyboardKeys.Add(Keys.Right);
        }

        // Check if an action has been pressed.
        public static bool IsActionPressed(Action action)
        {
            return IsActionMapPressed(actionMaps[(int)action]);
        }

        // Check if an action was performed in the most recent update.
        public static bool IsActionTriggered(Action action)
        {
            return IsActionMapTriggered(actionMaps[(int)action]);
        }

        // Check if an action map has been pressed.  
        private static bool IsActionMapPressed(ActionMap actionMap)
        {
            for (int i = 0; i < actionMap.keyboardKeys.Count; i++)
            {
                if (IsKeyPressed(actionMap.keyboardKeys[i]))
                    return true;
            }
            return false;
        }

        // Check if an action map has been triggered this frame
        private static bool IsActionMapTriggered(ActionMap actionMap)
        {
            for (int i = 0; i < actionMap.keyboardKeys.Count; i++)
            {
                if (IsKeyTriggered(actionMap.keyboardKeys[i]))
                    return true;
            }
            return false;
        }

        #endregion

        #region Initialization

        // Initializes the default control keys for all actions
        public static void Initialize()
        {
            ResetActionMaps();
        }

        #endregion

        #region Updating

        // Updates the keyboard control state.
        public static void Update()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }
        #endregion
    }
}