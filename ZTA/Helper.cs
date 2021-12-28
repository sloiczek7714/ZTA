using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Data.SqlClient;
using System.Configuration;

namespace ZTA
{
    public class Helper
    {

        private static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        private static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
                new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }

            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        public static string HashPassword(string password, string username)
        {
            return ByteArrayToString(
                GenerateSaltedHash(
                    Encoding.ASCII.GetBytes(password),
                    Encoding.ASCII.GetBytes(username))
            );
        }
             

        public static bool DoesUserHasPermission(string Id, string role)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
            connection.Open();
            string insert = "Select Role FROM Users where User_ID = @ID";
            SqlCommand command = new SqlCommand(insert, connection);
            command.Parameters.AddWithValue("ID", Id);
            string userRole = command.ExecuteScalar().ToString();
            Console.WriteLine(role);
            Console.WriteLine(userRole);
            if (userRole.TrimEnd(' ').Equals(role.ToString()))
            {
                return true;
            }

            else 
            return false;
        }
    }
}