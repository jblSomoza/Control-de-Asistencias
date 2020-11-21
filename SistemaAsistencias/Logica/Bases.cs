using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

namespace SistemaAsistencias.Logica
{
    class Bases
    {

        public static void DiseñoDG(ref DataGridView listado) 
        {
            listado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            listado.BackgroundColor = Color.FromArgb(29, 29, 29);
            listado.EnableHeadersVisualStyles = false;
            listado.BorderStyle = BorderStyle.None;
            listado.CellBorderStyle = DataGridViewCellBorderStyle.None;
            listado.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            listado.RowHeadersVisible = false;

            DataGridViewCellStyle cabecera = new DataGridViewCellStyle();
            cabecera.BackColor = Color.FromArgb(29, 29, 29);
            cabecera.ForeColor = Color.White;
            cabecera.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            listado.ColumnHeadersDefaultCellStyle = cabecera;
        }

        public static object Decimales(TextBox text, KeyPressEventArgs e) 
        {
            if ((e.KeyChar == '.') || (e.KeyChar == ',')) 
            {
                e.KeyChar = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            }

            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == '.' && (~text.Text.IndexOf(".")) != 0)
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '.')
            {
                e.Handled = false;
            } 
            else if (e.KeyChar == ',') 
            {
                e.Handled = false;
            }
            else 
            {
                e.Handled = true;               
            }

            return null;
        }

        public static void DiseñoDGEliminar(ref DataGridView listado) 
        {
            foreach (DataGridViewRow row in listado.Rows)
            {
                string estado;
                estado = row.Cells["estado"].Value.ToString();

                if (estado == "ELIMINADO") 
                {
                    row.DefaultCellStyle.Font = new Font("Sego UI", 10, FontStyle.Strikeout | FontStyle.Bold);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(255, 128, 128);
                }
            }            
        }

        public enum DateInterval 
        {
            Day,
            DayOfYear,
            Hour,
            Minute,
            Month,
            Quarter,
            Second,
            Weekday,
            WeekOfYear,
            Year
        }

        public static long DateDiff(DateInterval intervalType, DateTime dateOne, DateTime dateTwo) 
        {
            switch (intervalType)
            {
                case DateInterval.Day:
                case DateInterval.DayOfYear:
                    TimeSpan spanForDays = dateTwo - dateOne;
                    return (long)spanForDays.TotalDays;
                case DateInterval.Hour:
                    TimeSpan spanForHour = dateTwo - dateOne;
                    return (long)spanForHour.TotalHours;
                case DateInterval.Minute:
                    TimeSpan spanForMinutes = dateTwo - dateOne;
                    return (long)spanForMinutes.TotalMinutes;
                case DateInterval.Month:
                    return ((dateTwo.Year - dateOne.Year) * 12) + (dateTwo.Month - dateOne.Month);
                case DateInterval.Quarter:
                    long dateOneQuarter = (long)Math.Ceiling(dateOne.Month / 3.0);
                    long dateTwoQuarte = (long)Math.Ceiling(dateTwo.Month / 3.0);
                    return (4 * (dateTwo.Year - dateOne.Year)) + dateTwoQuarte - dateOneQuarter;
                case DateInterval.Second:
                    TimeSpan spanForSeconds = dateTwo - dateOne;
                    return (long)spanForSeconds.TotalSeconds;
                case DateInterval.Weekday:
                    TimeSpan spandForWeekdays = dateTwo - dateOne;
                    return (long)(spandForWeekdays.TotalDays / 7.0);
                case DateInterval.WeekOfYear:
                    DateTime dateOneModified = dateOne;
                    DateTime dateTwoModified = dateTwo;
                    while (dateTwoModified.DayOfWeek != DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek) 
                    {
                        dateTwoModified = dateTwoModified.AddDays(-1);
                    }
                    while (dateOneModified.DayOfWeek != DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek) 
                    {
                        dateOneModified = dateOneModified.AddDays(-1);
                    }
                    TimeSpan spanForWeekOfYear = dateTwoModified - dateOneModified;
                    return (long)(spanForWeekOfYear.TotalDays / 7.0);
                case DateInterval.Year:
                    return dateTwo.Year - dateOne.Year;
                default:
                    return 0;

            }
        }

    }
}
