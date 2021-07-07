using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyDiaryApp
{
    public class DiaryDatabase
    {
        private SQLiteConnection connection;

        public string ErrorMessage { get; set; }

        public DiaryDatabase()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            connection = new SQLiteConnection(Path.Combine(path, "diary.db"));
            CreateDB();
        }

        public void CreateDB()
        {
            try
            {
                connection.CreateTable<User>();
                connection.CreateTable<Entry>();
            }
            catch (Exception ex)
            {

            }
        }

        public bool ValidUser(string username, string password)
        {
            List<User> users = connection.Query<User>("Select * from User");
            if (users != null && users.Count > 0)
            {
                foreach (User user in users)
                {
                    if (user.UserName.Equals(username) && user.Password.Equals(password))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool SaveUser(User user)
        {
            try
            {
                connection.Insert(user);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool SaveEntry(Entry entry)
        {
            try
            {
                connection.Insert(entry);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public List<Entry> GetAllEntries()
        {
            List<Entry> entries = connection.Query<Entry>("Select * from Entry");
            return entries;
        }

        public List<Entry> GetUserEntries(string username)
        {
            List<Entry> entries = new List<Entry>();
            List<Entry> allEntries = GetAllEntries();
            if (allEntries != null && allEntries.Count > 0)
            {
                foreach (Entry entry in allEntries)
                {
                    if (entry.UserName.Equals(username))
                    {
                        entries.Add(entry);
                    }
                }
            }
            return entries;
        }

        public bool DeleteEntry(Entry entry)
        {
            try
            {
                connection.Delete(entry);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
    }
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }

        [Unique]
        public string UserName { get; set; }

        public string Password { get; set; }
    }
    public class Entry
    {
        [PrimaryKey, AutoIncrement]
        public int EntryID { get; set; }

        public string Subject { get; set; }

        public string UserName { get; set; }

        public long EntryDate { get; set; }

        public string Details { get; set; }
    }
}