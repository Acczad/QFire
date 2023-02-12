using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QFire.Helpers
{
    public static class StringHelper
    {
        public static string GetRandomString(int lenth)
        {
            Random random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var randomString = new StringBuilder();
            for (int i = 0; i < lenth; i++)
                randomString.Append(chars[random.Next(chars.Length)]);

            return randomString.ToString();
        }
    }
}
