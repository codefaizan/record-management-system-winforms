using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordManagementSystem
{
    public static class AppSettings
    {
        public static string ConnectionString()
        {
            string conString = "Server=localhost;Database=rms_db;Uid=root;pwd=''";
            return conString;
        }
    }
}
