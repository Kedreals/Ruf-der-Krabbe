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
        public static ControlKey Up = new ControlKey(Keys.W);
        public static ControlKey Down = new ControlKey(Keys.S);
        public static ControlKey Right = new ControlKey(Keys.D);
        public static ControlKey Left = new ControlKey(Keys.A);

        public static ControlKey Jump = new ControlKey(Keys.Space);

        public static ControlKey Confirm = new ControlKey(Keys.Enter);
        
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
