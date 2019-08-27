using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

// Hashes and salts are binary blobs, you don't need to convert them to strings unless 
// you want to put them into text files.
// You can convert text to byte arrays using Encoding.UTF8.GetBytes(string).
// If you must convert a hash to its string representation you can use 
// Convert.ToBase64String and Convert.FromBase64String to convert it back


// Method 
// When user will sign up , he will provide us password.
// This password needs to be stored in Hash format along with salt 
// When user logs in to system we fetch Password hash and salt from database,
// recompute password hash using input password and database salt and compare with database password hash

namespace ConsoleApp7
{
    class Program
    {
        public byte[] _passwordHash { get; set; }
        public byte[] _passwordSalt { get; set; }

        public bool ConfirmPassword(string password)
        {
            byte[] passwordHash = Hash(password, _passwordSalt);

            return _passwordHash.SequenceEqual(passwordHash);
        }

        public static byte[] Hash(string value, byte[] salt)
        {
            return Hash(Encoding.UTF8.GetBytes(value), salt);
        }

        public static byte[] Hash(byte[] value, byte[] salt)
        {
            byte[] saltedValue = value.Concat(salt).ToArray();

            return new SHA256Managed().ComputeHash(saltedValue);
        }

        private static byte[] CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return buff;
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            string password = "girish123";

            // Store following binary blobs in database and retrieve from database when necessary 
            program._passwordSalt = CreateSalt(8);
            program._passwordHash = Hash(password, program._passwordSalt);

            // Accept user password 
            string inputpwd = Console.ReadLine();
            bool password_match = program.ConfirmPassword(inputpwd);
            if (password_match)
            {
                Console.WriteLine("correct password!!");
            }
            else
            {
                Console.WriteLine("Wrong password!!");
            }

        }
    }
}
