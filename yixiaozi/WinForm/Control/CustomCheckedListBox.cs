using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using yixiaozi.Model.DocearReminder;
namespace yixiaozi.WinForm.Control
{
	public class CustomCheckedListBox : CheckedListBox
	{
		public delegate Color GetColorDelegate(CustomCheckedListBox listbox, DrawItemEventArgs e);
		public delegate Font GetFontDelegate(CustomCheckedListBox listbox, DrawItemEventArgs e);

        [Description("Supply a foreground color for each item")]
        public event GetColorDelegate GetForeColor = null;
        [Description("Supply a background color for each item")]
        public event GetColorDelegate GetBackColor = null;
        [Description("Supply a font for each item")]
        public event GetFontDelegate GetFont = null;

        [Description("Set this if you don't like the standard selection appearance")]
        public bool DrawFocusedIndicator { get; set; }

        public override int ItemHeight { get; set; }

		/// ////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Parameterless ctor for Visual Studio.
		/// </summary>
		public CustomCheckedListBox() : this (null, null, null) { }

		/// ////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Standard ctor for everyone else.
		/// </summary>
		/// <param name="back">Delegate to provide a background color</param>
		/// <param name="fore">Delegate to provide a foreground color</param>
		/// <param name="font">Delegate to provide a font</param>
		public CustomCheckedListBox(GetColorDelegate back = null, GetColorDelegate fore = null, GetFontDelegate font = null)
		{
			GetForeColor = fore;
			GetBackColor = back;
			GetFont = font;
            ItemHeight = 14;
        }

		/// ////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Override the CheckedListBox method to allow changes to the
		/// foreground, background, and font.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
            if (e.Index < 0) return;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e = new DrawItemEventArgs(e.Graphics,
                                            e.Font,
                                            e.Bounds,
                                            e.Index,
                                            e.State ^ DrawItemState.Selected,
                                            e.ForeColor,
                                            Color.LightGray);//Yellow
            }
            else
            {
                if (this.CheckedIndices.IndexOf(e.Index)<0)
                {
                    e = new DrawItemEventArgs(e.Graphics,
                                            e.Font,
                                            e.Bounds,
                                            e.Index,
                                            e.State,
                                            e.ForeColor,
                                            Color.FromArgb(245,245,245));
                }
			}
			e.DrawBackground();
            if (e.Index>0)
            {
                e.Graphics.DrawString(((MyListBoxItem)Items[e.Index]).Text, e.Font, Brushes.Gray, e.Bounds, StringFormat.GenericDefault);
            }
            else
            {
                string text = "";
                try
                {
                    text = ((MyListBoxItem)Items[0]).Text;
                }
                catch (Exception)
                {
                }
                e.Graphics.DrawString(text, e.Font, Brushes.Gray, e.Bounds, StringFormat.GenericDefault);
            }
        }

		/// <summary>
		/// If base.OnFontChanged fires, and we have changed the font size,
		/// that could cause problems.  Fire base.OnFontChanged only if
		/// we have not supplied a font.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnFontChanged(EventArgs e)
		{
			if (GetFont == null) base.OnFontChanged(e);
		}
		protected override void Sort()
		{
			if (Items.Count > 1)
			{
				bool swapped;
				do
				{
                    int counter = Items.Count - 1;
                    swapped = false;
                    while (counter > 0)
                    {
                        if (((MyListBoxItem)Items[counter - 1]).Text.Substring(0,2).CompareTo(((MyListBoxItem)Items[counter]).Text.Substring(0,2)) == -1)
                        {
                            object temp = Items[counter];
                            Items[counter] = Items[counter - 1];
                            Items[counter - 1] = temp;
                            swapped = true;
                        }
                        counter -= 1;
                    }
                }
                while ((swapped == true));
			}
		}
	}
}
