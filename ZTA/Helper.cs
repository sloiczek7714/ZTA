using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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

        //public static bool IsUserLoggedIn(MyDbContext _context, ITempDataDictionary TempData)
        //{
        //    return IsUserLoggedIn(_context, TempData, out User currentUser);
        //}

        //public static bool IsUserLoggedIn(MyDbContext _context, ITempDataDictionary TempData, out User currentUser)
        //{
        //    try
        //    {
        //        int id = Int32.Parse((string)TempData.Peek("UserID"));
        //        currentUser = _context.Users.FirstOrDefault(x => x.ID == id);
        //        _context.Entry(currentUser).Collection(x => x.Permissions).Load();
        //        _context.Entry(currentUser)
        //            .Collection(x => x.Permissions)
        //            .Load();
        //        _context.Entry(currentUser)
        //            .Reference(x => x.Group)
        //            .Load();

        //        if (currentUser.GroupID != null)
        //        {
        //            _context.Entry(currentUser.Group)
        //                .Collection(x => x.Permissions)
        //                .Query()
        //                .Include(x => x.Permission)
        //                .Load();
        //        }
        //        _context.Entry(currentUser).State = EntityState.Detached;
        //        if (currentUser == null)
        //            return false;
        //    }
        //    catch (Exception)
        //    {
        //        currentUser = null;
        //        return false;
        //    }

        //    return true;
        //}

        //public static void LogIn(int id, string name, ITempDataDictionary TempData)
        //{
        //    TempData["UserID"] = id.ToString();
        //    TempData["UserName"] = name;

        //    TempData.Keep("UserID");
        //    TempData.Keep("UserName");
        //}

        //public static void LogOut(MyDbContext _context, ITempDataDictionary TempData)
        //{
        //    TempData.Remove("UserID");
        //    TempData.Remove("UserName");
        //}

        //public static User GetCurrentUser(MyDbContext _context, ITempDataDictionary TempData)
        //{
        //    return _context.Users.FirstOrDefault(x => x.ID == Int32.Parse(TempData.Peek("UserID").ToString()));
        //}

        //public static bool DoesUserHasPermission(DbContext _context, User user, string viewName)
        //{
        //    if (user.Permissions != null && user.Permissions.Any(x => x.ViewName == viewName))
        //    {
        //        return true; 
        //    }

        //    if (user.Group != null && user.Permissions != null && user.Group.Permissions.Any(x => x.Permission.ViewName == viewName))
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    }
}