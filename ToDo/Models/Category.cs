using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ToDoList.Models
{
    public class Category
    {
        private string _name;
        private int _id;
        public Category(string name, int id = 0)
        {
            _name = name;
            _id = id;
        }

        public List<Item> GetItems()
        {
            List<Item> allCategoryItems = new List<Item> {};
            return allCategoryItems;
        }
        
        public override bool Equals(System.Object otherCategory)
        {
            if (!(otherCategory is Category))
            {
                return false;
            }
            else
            {
                Category newCategory = (Category) otherCategory;
                return this.GetId().Equals(newCategory.GetId());
            }
        }
        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        }
        public string GetName()
        {
            return _name;
        }
        public int GetId()
        {
            return _id;
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO categories (name) VALUES (@name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
        public static List<Category> GetAll()
        {
            List<Category> allCategories = new List<Category> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM categories;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int CategoryId = rdr.GetInt32(0);
              string CategoryName = rdr.GetString(1);
              Category newCategory = new Category(CategoryName, CategoryId);
              allCategories.Add(newCategory);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCategories;
        }
        public static Category Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM categories WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int CategoryId = 0;
            string CategoryName = "";

            while(rdr.Read())
            {
              CategoryId = rdr.GetInt32(0);
              CategoryName = rdr.GetString(1);
            }
            Category newCategory = new Category(CategoryName, CategoryId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newCategory;
        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM categories;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}

//
// using System.Collections.Generic;
//
// namespace ToDoList.Models
// {
//   public class Category
//   {
//     private static List<Category> _instances = new List<Category> {};
//     private string _name;
//     private int _id;
//     private List<Item> _items;
//
//     public Category(string categoryName)
//     {
//       _name = categoryName;
//       _instances.Add(this);
//       _id = _instances.Count;
//       _items = new List<Item>{};
//     }
//     public List<Item> GetItems()
//     {
//       return _items;
//     }
//     public void AddItem(Item item)
//     {
//       _items.Add(item);
//     }
//     public string GetName()
//     {
//       return _name;
//     }
//     public int GetId()
//     {
//       return _id;
//     }
//     public static List<Category> GetAll()
//     {
//       return _instances;
//     }
//     public static void Clear()
//     {
//       _instances.Clear();
//     }
//     public static Category Find(int searchId)
//     {
//       return _instances[searchId-1];
//     }
//   }
// }
