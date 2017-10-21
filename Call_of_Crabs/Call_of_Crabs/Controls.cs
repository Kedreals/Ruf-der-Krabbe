using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Call_of_Crabs
{
    public static class Controls
    {
        public enum EKey
        {
            None = -1,

            Up,
            Down,
            Right,
            Left,

            Jump,
            Confirm,

            firstweapon,
            secondweapon,
            thirdweapon,

            Count
        }

        private static ControlKey[] keys = new ControlKey[(int)EKey.Count]
            {
                new ControlKey(Keys.W),
                new ControlKey(Keys.S),
                new ControlKey(Keys.D),
                new ControlKey(Keys.A),
                new ControlKey(Keys.Space),
                new ControlKey(Keys.Enter),
                new ControlKey(Keys.D1),
                new ControlKey(Keys.D2),
                new ControlKey(Keys.D3),
            };

        public static ControlKey GetKey(EKey key)
        {
            if (key <= EKey.None || key >= EKey.Count)
                return null;

            return keys[(int)key];
        }

        public static void Update(GameTime gTime)
        {
            for (int i = 0; i < (int)EKey.Count; ++i)
                keys[i].Update(gTime);
        }
    }

    public class ControlKey
    {
        private Keys assignedKey;
        public KeyState PreviousState = KeyState.Up;
        public KeyState CurrentState = KeyState.Up;

        private TimeSpan timeNotPressed = TimeSpan.Zero;
        private TimeSpan timePressed = TimeSpan.Zero;

        public ControlKey(Keys key)
        {
            assignedKey = key;
        }

        public void Update(GameTime ggTime)
        {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState()[assignedKey];

            if (CurrentState == KeyState.Up)
            {
                if (PreviousState == KeyState.Down)
                {
                    timePressed = TimeSpan.Zero;
                }
                timeNotPressed += ggTime.ElapsedGameTime;
            }
            else
            {
                if (PreviousState == KeyState.Up)
                {
                    timeNotPressed = TimeSpan.Zero;
                }
                timePressed += ggTime.ElapsedGameTime;
            }
        }

        public TimeSpan GetTimeReleased()
        {
            return timeNotPressed;
        }

        public TimeSpan GetTimePressed()
        {
            return timePressed;
        }

        public bool HasJustBeenPressed()
        {
            return PreviousState == KeyState.Up && CurrentState == KeyState.Down;
        }

        public bool IsPressed()
        {
            return CurrentState == KeyState.Down;
        }

        public bool HasJustBeenReleased()
        {
            return PreviousState == KeyState.Down && CurrentState == KeyState.Up;
        }

        public bool IsReleased()
        {
            return CurrentState == KeyState.Up;
        }
    }
}
