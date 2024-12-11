using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PongClone
{
    class GameSettings
    {
        private bool m_fullScreenActive;

        public bool FullScreen
        {
            get { return m_fullScreenActive; }
            set { m_fullScreenActive = value; }
        }

        private int m_currentRes = 0;

        public int CurrentRes
        {
            get { return m_currentRes; }
            set { m_currentRes = value; }
        }

        private List<Point> m_windowSizes = new List<Point>();

        public List<Point> WindowSizes
        {
            get { return m_windowSizes; }
            set { m_windowSizes = value;  }
        }

        GraphicsDeviceManager m_graphics;

        public GameSettings(GraphicsDeviceManager graphics) 
        {
            m_currentRes = 0;

            m_graphics = graphics;
            m_fullScreenActive = m_graphics.IsFullScreen;
        }

        public void UpdateMe()
        {
            if (m_fullScreenActive)
            {
                m_graphics.IsFullScreen = true;
            }
            else if (!m_fullScreenActive)
            {
                m_graphics.IsFullScreen = false;
            }
        }

        public void SetScreenRes(int index)
        {
            m_currentRes = index;

            if(m_currentRes >= m_windowSizes.Count)
            {
                m_currentRes = 0;
            }
            else if(m_currentRes < 0)
            {
                m_currentRes = m_windowSizes.Count - 1;
            }

            m_graphics.PreferredBackBufferWidth = m_windowSizes[m_currentRes].X;
            m_graphics.PreferredBackBufferHeight = m_windowSizes[m_currentRes].Y;
            m_graphics.ApplyChanges();
        }

        public void SetFullScreen(bool active)
        {
            m_fullScreenActive = active;
            m_graphics.IsFullScreen = m_fullScreenActive;
            m_graphics.ApplyChanges();
        }
    }
}
