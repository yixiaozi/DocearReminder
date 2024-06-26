/* Developed by Ertan Tike (ertan.tike@moreum.com) */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace yixiaozi.WinForm.Control.Calendar
{
    public interface ITool
    {
        DayView DayView
        {
            get;
            set;
        }

        void Reset();

        void MouseMove( MouseEventArgs e );
        void MouseUp( MouseEventArgs e );
        void MouseDown( MouseEventArgs e );
    }
}
