using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;

using System.DirectoryServices.AccountManagement;
using MySql.Data.MySqlClient;

namespace NSites_CM.Models.Inventorys
{
    public class Category
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string InventoryGroupId { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }

        public DataTable getCategorys(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetCategorys('" + pDisplayType + "'," + (pPrimaryKey == null ? "0" : pPrimaryKey) + ",'" + pSearchString + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public string insertCategory(Category pCategory)
        {
            string _Id = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spInsertCategory('" + pCategory.Description +
                    "','" + pCategory.InventoryGroupId +
                    "','" + pCategory.Remarks +
                    "','" + pCategory.UserId + "');", _conn);
                try
                {
                    _cmd.Transaction = _trans;
                    _Id = _cmd.ExecuteScalar().ToString();
                    _trans.Commit();
                    _conn.Close();
                }
                catch 
                {
                    _trans.Rollback();
                    _Id = "";
                }
            }
            return _Id;
        }

        public string updateCategory(Category pCategory)
        {
            string _Id = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spUpdateCategory('" + pCategory.Id +
                    "','" + pCategory.Description +
                    "','" + pCategory.InventoryGroupId +
                    "','" + pCategory.Remarks +
                    "','" + pCategory.UserId + "');", _conn);
                try
                {
                    _cmd.Transaction = _trans;
                    _Id = _cmd.ExecuteScalar().ToString();
                    _trans.Commit();
                    _conn.Close();
                }
                catch
                {
                    _trans.Rollback();
                    _Id = "";
                }
            }
            return _Id;
        }

        public bool removeCategory(string pId, string pUserId)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spRemoveCategory('" + pId +
                    "','" + pUserId+ "');", _conn);
                try
                {
                    _cmd.Transaction = _trans;
                    int _rowsAffected = _cmd.ExecuteNonQuery();
                    _trans.Commit();
                    _conn.Close();
                    if (_rowsAffected > 0)
                    {
                        _success = true;
                    }
                    else
                    {
                        _success = false;
                    }
                }
                catch
                {
                    _trans.Rollback();
                    _success = false;
                }
            }
            return _success;
        }
    }
}