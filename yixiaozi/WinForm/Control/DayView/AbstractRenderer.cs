using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace yixiaozi.WinForm.Control.Calendar
{
    public abstract class AbstractRenderer
    {
        public virtual Color AllDayEventsBackColor
        {
            get
            {
                return InterpolateColors(this.BackColor, Color.Black, 0.5f);
            }
        }

        public virtual Font BaseFont
        {
            get
            {
                return System.Windows.Forms.Control.DefaultFont;
            }
        }

        public virtual Color HourSeperatorColor
        {
            get
            {
                //return System.Drawing.Color.FromArgb(234, 208, 152);
                return System.Drawing.Color.FromArgb(255, 228, 181);
            }
        }

        public virtual Color HalfHourSeperatorColor
        {
            get
            {
                //return System.Drawing.Color.FromArgb(243, 228, 177);
                //
                return System.Drawing.Color.FromArgb(224, 255, 255);
            }
        }

        public virtual Color HourColor
        {
            get
            {
                return System.Drawing.Color.FromArgb(255, 244, 188);
            }
        }

        public virtual Color WorkingHourColor
        {
            get
            {
                //return System.Drawing.Color.FromArgb(255, 255, 213);
                return System.Drawing.Color.FromArgb(240, 255, 240);
            }
        }

        public virtual Color BackColor
        {
            get
            {
                //return SystemColors.Control;
                return Color.White;
            }
        }

        public virtual Color SelectionColor
        {
            get
            {
                return SystemColors.Highlight;
            }
        }

        private Font hourFont;

        public virtual Font HourFont
        {
            get
            {
                if (hourFont == null)
                {
                    hourFont = new Font(BaseFont.FontFamily, 14, FontStyle.Regular);
                }

                return hourFont;
            }
        }

        private Font minuteFont;

        public virtual Font MinuteFont
        {
            get
            {
                if (minuteFont == null)
                {
                    //minuteFont = new Font(BaseFont.FontFamily, 8, FontStyle.Regular);
                    minuteFont = new Font(BaseFont.FontFamily, 7, FontStyle.Regular);
                }

                return minuteFont;
            }
        }

        public abstract void DrawHourLabel(Graphics g, Rectangle rect, int hour);

        public abstract void DrawDayHeader(Graphics g, Rectangle rect, DateTime date);

        public abstract void DrawDayBackground(Graphics g, Rectangle rect);

        public virtual void DrawHourRange(Graphics g, Rectangle rect, bool drawBorder, bool hilight,string minutes="")
        {
            if (minutes!="")
            {
                StringFormat m_Format = new StringFormat();
                m_Format.Alignment = StringAlignment.Center;
                m_Format.FormatFlags = StringFormatFlags.NoWrap;
                m_Format.LineAlignment = StringAlignment.Center;
                g.DrawString(minutes, BaseFont, SystemBrushes.WindowText, rect, m_Format);
            }
            else
            {
                using (SolidBrush brush = new SolidBrush(hilight ? this.SelectionColor : this.WorkingHourColor))
                {
                    g.FillRectangle(brush, rect);
                }

                if (drawBorder)
                    g.DrawRectangle(SystemPens.WindowFrame, rect);
            }
        }

        public virtual void DrawDayGripper(Graphics g, Rectangle rect, int gripWidth)
        {
            using (Brush m_Brush = new SolidBrush(Color.White))
                g.FillRectangle(m_Brush, rect.Left, rect.Top - 1, gripWidth, rect.Height);

            using (Pen m_Pen = new Pen(HourSeperatorColor))
                g.DrawRectangle(m_Pen, rect.Left, rect.Top - 1, 1, rect.Height);
        }

        public abstract void DrawAppointment(Graphics g, Rectangle rect, Appointment appointment, bool isSelected, int gripWidth);

        public void DrawAllDayBackground(Graphics g, Rectangle rect)
        {
            using (Brush brush = new SolidBrush(InterpolateColors(this.BackColor, Color.Black, 0.5f)))
                g.FillRectangle(brush, rect);
        }

        public static Color InterpolateColors(Color color1, Color color2, float percentage)
        {
            int num1 = ((int)color1.R);
            int num2 = ((int)color1.G);
            int num3 = ((int)color1.B);
            int num4 = ((int)color2.R);
            int num5 = ((int)color2.G);
            int num6 = ((int)color2.B);
            byte num7 = System.Convert.ToByte(((float)(((float)num1) + (((float)(num4 - num1)) * percentage))));
            byte num8 = System.Convert.ToByte(((float)(((float)num2) + (((float)(num5 - num2)) * percentage))));
            byte num9 = System.Convert.ToByte(((float)(((float)num3) + (((float)(num6 - num3)) * percentage))));
            return Color.FromArgb(num7, num8, num9);
        }
    }
}
