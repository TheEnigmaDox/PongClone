using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel.DataAnnotations;

namespace EnigmaUtils
{
    internal class TextRenderer
    {
        private SpriteFont m_font;
        private Vector2 m_position;
        private Rectangle m_buttonBounds;

        public Rectangle ButtonBounds
        {
            get { return m_buttonBounds; }
        }

        private Color m_Tint;

        public Color Tint
        {
            get { return m_Tint; }
            set { m_Tint = value; }
        }

        private float alpha = 0.75f;
        private float  alphaChange = 0.6f;

        public float ColorAlpha
        {
            set
            {
                alpha = value;
            }
        }

        public TextRenderer(SpriteFont font, Vector2 position)
        {
            m_font = font;
            m_position = position;
        }

        public void UpdateMe(GameTime gameTime)
        {
            alpha += alphaChange * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (alpha > 0.9f)
            {
                alphaChange *= -1;
            }
            if (alpha < 0.0f)
            {
                alphaChange *= -1;
            }
        }

        public  void DrawString(SpriteBatch sBatch, string textToDraw)
        {
            m_buttonBounds = new Rectangle((int)m_position.X - (int)m_font.MeasureString(textToDraw).X / 2, 
                (int)m_position.Y - (int)m_font.MeasureString(textToDraw).Y / 2, 
                (int)m_font.MeasureString(textToDraw).X, 
                (int)m_font.MeasureString(textToDraw).Y);
            sBatch.DrawString(m_font, textToDraw, m_position - m_font.MeasureString(textToDraw) / 2, m_Tint * alpha);
        }
    }
}
