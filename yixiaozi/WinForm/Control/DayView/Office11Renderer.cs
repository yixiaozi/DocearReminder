using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace yixiaozi.WinForm.Control.Calendar
{
    public class Office11Renderer : AbstractRenderer
    {
        private Font minuteFont;

        public override Font MinuteFont
        {
            get
            {
                if (minuteFont == null)
                    minuteFont = new Font(BaseFont, FontStyle.Italic);

                return minuteFont;
            }
        }

        public override void DrawHourLabel(Graphics g, Rectangle rect, int hour)
        {
            Color m_Color = ControlPaint.LightLight(SystemColors.WindowFrame);
            m_Color = ControlPaint.Light(m_Color);

            using (Pen m_Pen = new Pen(m_Color))
                g.DrawLine(m_Pen, rect.Left, rect.Y, rect.Width, rect.Y);
            StringFormat m_Format = new StringFormat();
            m_Format.Alignment = StringAlignment.Center;
            m_Format.FormatFlags = StringFormatFlags.NoWrap;
            m_Format.LineAlignment = StringAlignment.Center;

            g.DrawString(hour.ToString("##00"), HourFont, SystemBrushes.ControlText, rect, m_Format);

            //rect.X += 27;

            //g.DrawString("00", MinuteFont, SystemBrushes.ControlText, rect);
        }

        public override void DrawDayHeader(Graphics g, Rectangle rect, DateTime date)
        {
            StringFormat m_Format = new StringFormat();
            m_Format.Alignment = StringAlignment.Center;
            m_Format.FormatFlags = StringFormatFlags.NoWrap;
            m_Format.LineAlignment = StringAlignment.Center;
            Color m_Color = ControlPaint.LightLight(SystemColors.WindowFrame);
            m_Color = ControlPaint.Light(m_Color);
            ControlPaint.DrawBorder(g, rect, m_Color, ButtonBorderStyle.Solid);
            ////ControlPaint.DrawBorder3D(g, rect, Border3DStyle.Etched);

            g.DrawString(
                date.ToString("MM-dd")+" "+System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(date.DayOfWeek),
                BaseFont,
                SystemBrushes.WindowText,
                rect,
                m_Format
                );
        }

        public override void DrawDayBackground(Graphics g, Rectangle rect)
        {
            using (Brush m_Brush = new SolidBrush(this.HourColor))
                g.FillRectangle(m_Brush, rect);
        }

        public override void DrawAppointment(Graphics g, Rectangle rect, Appointment appointment, bool isSelected, int gripWidth)
        {
            StringFormat m_Format = new StringFormat();
            m_Format.Alignment = StringAlignment.Center;
            m_Format.LineAlignment = StringAlignment.Center;

            if ((appointment.Locked) && isSelected)
            {
                // Draw back
                using (Brush m_Brush = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Wave, Color.LightGray, appointment.Color))
                    g.FillRectangle(m_Brush, rect);
            }
            else
            {
                // Draw back
                using (SolidBrush m_Brush = new SolidBrush(appointment.Color))
                    g.FillRectangle(m_Brush, rect);
            }

            if (isSelected)
            {
                using (Pen m_Pen = new Pen(appointment.BorderColor, 4))
                    g.DrawRectangle(m_Pen, rect);

                Rectangle m_BorderRectangle = rect;

                m_BorderRectangle.Inflate(2, 2);

                using (Pen m_Pen = new Pen(SystemColors.WindowFrame, (float)0.25))
                    g.DrawRectangle(m_Pen, m_BorderRectangle);

                m_BorderRectangle.Inflate(-4, -4);

                using (Pen m_Pen = new Pen(SystemColors.WindowFrame, (float)0.25))
                    g.DrawRectangle(m_Pen, m_BorderRectangle);
            }
            else
            {
                // Draw gripper
                Rectangle m_GripRectangle = rect;
                m_GripRectangle.Width = gripWidth + 1;

                using (SolidBrush m_Brush = new SolidBrush(appointment.BorderColor))
                    g.FillRectangle(m_Brush, m_GripRectangle);

                using (Pen m_Pen = new Pen(SystemColors.WindowFrame, (float)0.25))
                    g.DrawRectangle(m_Pen, rect);
            }
            
            rect.X += gripWidth;
            g.DrawString(appointment.Title, new Font(BaseFont.FontFamily, getFontSize(rect.Height, appointment.Title, rect.X)), SystemBrushes.WindowText, rect, m_Format);
            
        }
        //如果是15分钟，就把字号变小
        public float getFontSize(int i,string title,int width)
        {
            if (i <= 10)
            {
                return 6;
            }
            else if (i < 15)
            {
                return i - 4;
            }
            else
            {
                if (title.Length>18&&title.Length*9>=width)
                {
                    return 8;
                }
                else
                {
                    return 8F;
                }
            }
        }
    }
}
