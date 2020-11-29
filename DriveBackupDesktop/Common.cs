using System;

namespace DriveBackupDesktop
{
    class Common
    {
        public static string getDateToday(string format)
        {
            string dateToday = (string)DateTime.UtcNow.ToString(format);
            return dateToday;
        }
        
    }
}
