using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yixiaozi.WinForm.Control
{
    public class MyToolTip : ToolTip
    {
        public Color color { get; set; }
        public MyToolTip()
        {
            OwnerDraw = true;
            //Draw += MyToolTip_Draw;
        }

        private void MyToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Near;
                sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
                sf.FormatFlags = StringFormatFlags.LineLimit;
                e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds);
                e.DrawBorder();
                e.Graphics.DrawString(e.ToolTipText, e.Font, SystemBrushes.ActiveCaptionText, e.Bounds, sf);
            }
        }
    }
}
