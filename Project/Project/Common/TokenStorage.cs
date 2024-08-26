using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    internal class TokenStorage
    {
        private static SecureString _secureToken;

        public static void SaveToken(string token)
        {
            if (_secureToken != null)
            {
                _secureToken.Dispose();
            }

            _secureToken = new SecureString();
            foreach (char c in token)
            {
                _secureToken.AppendChar(c);
            }

            _secureToken.MakeReadOnly();
        }

        public static string RetrieveToken()
        {
            if (_secureToken == null)
            {
                return null;
            }

            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.SecureStringToBSTR(_secureToken);
                return Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }
        }

        public static void ClearToken()
        {
            if (_secureToken != null)
            {
                _secureToken.Dispose();
                _secureToken = null;
            }
        }
    }
}
