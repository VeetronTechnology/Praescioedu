using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praescio.BusinessEntities.AppCode
{
    public static class Common
    {
        public static string PasswordResetLink = "http://localhost:50268/Account/PasswordReset";
        public static string BlockPassword = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["BlockPassword"]);

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
