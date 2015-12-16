using System;
using System.Collections.Generic;
using System.Text;

namespace A16_Ex02
{
    public class Move
    {
        private int m_IPreviousMove;
        private int m_JPreviousMove;
        private int m_INextMove;
        private int m_JNextMove;

        public Move(int i_IPreviousMove, int i_JPreviousMove, int i_INextMove, int i_JNextMove)
        {
            m_IPreviousMove = i_IPreviousMove;
            m_JPreviousMove = i_JPreviousMove;
            m_INextMove = i_INextMove;
            m_JNextMove = i_JNextMove;
        }

        public int IPreviousMove
        {
            get
            {
                return m_IPreviousMove;
            }
        }

        public int JPreviousMove
        {
            get
            {
                return m_JPreviousMove;
            }
        }

        public int INextMove
        {
            get
            {
                return m_INextMove;
            }
        }

        public int JNextMove
        {
            get
            {
                return m_JNextMove;
            }
        }
    }
}
