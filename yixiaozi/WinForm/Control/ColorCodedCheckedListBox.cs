using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yixiaozi.WinForm.Control
{
    public class ColorCodedCheckedListBox : CheckedListBox
    {
        //public ColorCodedCheckedListBox()
        //{
        //    DoubleBuffered = true;
        //}
        //protected override void OnDrawItem(DrawItemEventArgs e)
        //{
        //    e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))))), e.Bounds);
        //    Size checkSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal);
        //    int dx = (e.Bounds.Height - checkSize.Width) / 2;
        //    e.DrawBackground();
        //    bool isChecked = GetItemChecked(e.Index);//For some reason e.State doesn't work so we have to do this instead.
        //    CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(dx, e.Bounds.Top + dx), isChecked ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
        //    using (StringFormat sf = new StringFormat { LineAlignment = StringAlignment.Center })
        //    {
        //        using (Brush brush = new SolidBrush(isChecked ? CheckedItemColor : checkedItemColor))//ForeColor
        //        {
        //            e.Graphics.DrawString(((DocearReminderForm.MyListBoxItem)this.Items[e.Index]).Text, Font, brush, new Rectangle(e.Bounds.Height, e.Bounds.Top, e.Bounds.Width - e.Bounds.Height, e.Bounds.Height), sf);
        //        }
        //    }
        //}
        //Color checkedItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
        //public Color CheckedItemColor
        //{
        //    get { return checkedItemColor; }
        //    set
        //    {
        //        checkedItemColor = value;
        //        Invalidate();
        //    }
        //}
    }
}
