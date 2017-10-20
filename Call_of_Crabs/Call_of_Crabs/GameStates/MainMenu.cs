using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Call_of_Crabs.GameStates
{
    class MainMenu : IGameState
    {
        private enum Button
        {
            None = -1,

            StartGame,
            Quit,

            Count
        }


        SpriteFont font;
        Color color = Color.Black;

        Rectangle[] Buttons;
        string[] ButtonTexts;
        Vector2[] TextOffsets;
        Random rand;

        Texture2D ButtonBackground;

        int selected = 0;

        public void Initialize(GraphicsDevice graphics)
        {
            rand = new Random();
            Buttons = new Rectangle[(int)Button.Count];
            ButtonTexts = new string[(int)Button.Count];
            TextOffsets = new Vector2[(int)Button.Count];

            Vector2 size = new Vector2(0, 0);

            for(int i = 0; i < (int)Button.Count; ++i)
            {
                ButtonTexts[i] = ((Button)i).ToString();
                Vector2 mesure = font.MeasureString(ButtonTexts[i]);
                if (mesure.LengthSquared() > size.LengthSquared())
                    size = mesure;
            }

            for(int i = 0; i < (int)Button.Count; ++i)
            {
                Vector2 pos = new Vector2((float)rand.NextDouble() * (float)(graphics.PresentationParameters.BackBufferWidth - size.X),
                                          (float)rand.NextDouble() * (float)(graphics.PresentationParameters.BackBufferHeight - size.Y));

                Buttons[i] = new Rectangle(pos.ToPoint(), size.ToPoint());
                Vector2 mesure = font.MeasureString(ButtonTexts[i]);

                TextOffsets[i] = 0.5f * size - 0.5f * mesure;
            }

            ButtonBackground = new Texture2D(graphics, (int)1, (int)1);
            ButtonBackground.SetData(new Color[]{ Color.DarkOrange});
        }

        public void LoadContent(ContentManager contentManager)
        {
            font = contentManager.Load<SpriteFont>("SpriteFonts/Arial");
        }

        float t = 0;

        public EGameState Update(GameTime time)
        {
            t += (float)time.ElapsedGameTime.TotalSeconds;

            float cost = (float)Math.Cos(t)/2.0f + 0.5f;
            color = Color.Lerp(Color.Lerp(Color.Red, Color.Blue, cost), Color.Lerp(Color.Blue, Color.Green, cost), cost);

            KeyboardState state = Keyboard.GetState();

            if(state.IsKeyDown(Keys.Down))
            {
                selected += 1;
                selected = selected % (int)Button.Count;
            }
            else if(state.IsKeyDown(Keys.Up))
            {
                selected += (int)Button.Count - 1;
                selected = selected % (int)Button.Count;
            }

            return EGameState.MainMenu;
        }

        public void Draw(SpriteBatch batch)
        {

            for (int i = 0; i < (int)Button.Count; ++i)
            {
                if (i == selected)
                    continue;

                batch.Draw(ButtonBackground, Buttons[i], Color.White);
                batch.DrawString(font, ButtonTexts[i], Buttons[i].Location.ToVector2() + TextOffsets[i], Color.Black);
            }

            batch.Draw(ButtonBackground, Buttons[selected], Color.White);
            batch.DrawString(font, ButtonTexts[selected], Buttons[selected].Location.ToVector2() + TextOffsets[selected], color);
        }
    }
}
