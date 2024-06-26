/* Developed by Ertan Tike (ertan.tike@moreum.com) */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace yixiaozi.WinForm.Control.Calendar
{
    public class DrawTool : ITool
    {
        public DateTime m_SelectionStart;
        public bool m_SelectionStarted;

        public void Reset()
        {
            m_SelectionStarted = false;
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (m_SelectionStarted)
                {
                    DateTime m_Time = m_DayView.GetTimeAt(e.X, e.Y);
                    m_Time = m_Time.AddMinutes(1);
                    if (m_Time < m_SelectionStart)
                    {
                        m_DayView.SelectionStart = m_Time;
                        m_DayView.SelectionEnd = m_SelectionStart;
                    }
                    else
                    {
                        m_DayView.SelectionEnd = m_Time;
                    }

                    m_DayView.Invalidate();
                }
            }
            else if (e.Button == MouseButtons.None)
            {

            }
        }

        public void MouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_DayView.Capture = false;
                m_SelectionStarted = false;

                m_DayView.RaiseSelectionChanged(EventArgs.Empty);

                if (Complete != null)
                    Complete(this, EventArgs.Empty);
            }
        }

        public void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_SelectionStart = m_DayView.GetTimeAt(e.X, e.Y);

                m_DayView.SelectionStart = m_SelectionStart;
                m_DayView.SelectionEnd = m_SelectionStart.AddMinutes(1);

                m_SelectionStarted = true;

                m_DayView.Invalidate();
                m_DayView.Capture = true;
            }
        }

        private DayView m_DayView;

        public DayView DayView
        {
            get { return m_DayView; }
            set { m_DayView = value; }
        }

        public event EventHandler Complete;
    }
}
