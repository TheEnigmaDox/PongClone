using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongClone
{
    internal class PongBat : StaticGraphic
    {
        Texture2D m_texture;
        Vector2 m_position;
        Vector2 m_origin;

        public PongBat(Texture2D txr, Vector2 pos, Vector2 origin) : base(txr, pos, origin)
        {
            m_texture = txr;
            m_position = pos;
            m_origin = origin;
        }

        public void Update()
        {

        }
    }
}
