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
    public class Supplier
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TIN { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string BankName { get; set; }
        public string BankAccountNo { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }

        public DataTable getSuppliers(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetSuppliers('" + pDisplayType + "'," + (pPrimaryKey == null ? "0" : pPrimaryKey) + ",'" + pSearchString + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public string insertSupplier(Supplier pSupplier)
        {
            string _Id = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spInsertSupplier('" + pSupplier.Name +
                    "','" + pSupplier.Address +
                    "','" + pSupplier.TIN +
                    "','" + pSupplier.ContactPerson +
                    "','" + pSupplier.ContactNo +
                    "','" + pSupplier.EmailAddress +
                    "','" + pSupplier.BankName +
                    "','" + pSupplier.BankAccountNo +
                    "','" + pSupplier.Remarks +
                    "','" + pSupplier.UserId + "');", _conn);
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

        public string updateSupplier(Supplier pSupplier)
        {
            string _Id = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spUpdateSupplier('" + pSupplier.Id +
                    "','" + pSupplier.Name +
                    "','" + pSupplier.Address +
                    "','" + pSupplier.TIN +
                    "','" + pSupplier.ContactPerson +
                    "','" + pSupplier.ContactNo +
                    "','" + pSupplier.EmailAddress +
                    "','" + pSupplier.BankName +
                    "','" + pSupplier.BankAccountNo +
                    "','" + pSupplier.Remarks +
                    "','" + pSupplier.UserId + "');", _conn);
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

        public bool removeSupplier(string pId, string pUserId)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spRemoveSupplier('" + pId +
                    "','" + pUserId + "');", _conn);
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