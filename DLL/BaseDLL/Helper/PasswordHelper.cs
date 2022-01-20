using System;
using System.Security.Cryptography;

namespace BaseDLL.Helper
{
    /// <summary>
    /// Hash 加密
    /// </summary>
    public class PasswordHelper
    {
        /// <summary>
        /// 
        /// </summary>
        protected const int SALT_BYTE_SIZE = 24;
        /// <summary>
        /// 
        /// </summary>
        protected const int HASH_BYTE_SIZE = 24;
        /// <summary>
        /// 
        /// </summary>
        protected const int PBKDF2_ITERATIONS = 1000;
        /// <summary>
        /// 
        /// </summary>
        protected const int ITERATION_INDEX = 0;
        /// <summary>
        /// 
        /// </summary>
        protected const int SALT_INDEX = 1;
        /// <summary>
        /// 
        /// </summary>
        protected const int PBKDF2_INDEX = 2;


        /// <summary>
        /// 判断密码格式是否符合条件
        /// </summary>
        /// <param name="pwd"></param>
        public static bool IsValid(string pwd) 
        {
            return true;
        }

        /// <summary>
        /// 创建加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string CreateHash(string password)
        {
#if NET6_0_OR_GREATER
            RandomNumberGenerator csprng = RandomNumberGenerator.Create();
#else
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
#endif
            byte[] salt = new byte[SALT_BYTE_SIZE];
            csprng.GetBytes(salt);
            byte[] hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
            return PBKDF2_ITERATIONS + ":" +
                Convert.ToBase64String(salt) + ":" +
                Convert.ToBase64String(hash);
        }

        /// <summary>
        /// 验证加密有效性
        /// </summary>
        /// <param name="password"></param>
        /// <param name="correctHash"></param>
        /// <returns></returns>
        public static bool ValidatePassword(string password, string correctHash)
        {
            try
            {
                // Extract the parameters from the hash
                char[] delimiter = { ':' };
                string[] split = correctHash.Split(delimiter);
                int iterations = Int32.Parse(split[ITERATION_INDEX]);
                byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
                byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

                byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
                return SlowEquals(hash, testHash);
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="outputBytes"></param>
        /// <returns></returns>
        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }
} 