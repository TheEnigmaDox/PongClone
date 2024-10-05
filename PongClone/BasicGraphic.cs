using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongClone
{
    internal class StaticGraphic
    {
        private Texture2D m_texture;
        private Vector2 m_position;
        private Vector2 m_origin;
        

        public StaticGraphic(Texture2D txr, Vector2 pos, Vector2 origin) 
        {
            m_texture = txr;
            m_position = pos;
            m_origin = origin;
        }

        public virtual void DrawMe(SpriteBatch sBatch)
        {
            sBatch.Draw(m_texture,
                m_position,
                Color.White);
        }
    }
}
