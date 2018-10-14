using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;

using System.DirectoryServices.AccountManagement;
using MySql.Data.MySqlClient;

namespace NSites_CM.Models.POSs
{
    public class ReturnedItem
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public string CashierPeriodId { get; set; }
        public string StockId { get; set; }
        public string LocationId { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Reason { get; set; }
        public string ReceivedBy { get; set; }
        public string UserId { get; set; }

        public DataTable getReturnedItems(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetReturnedItems(''" + pDisplayType + "','" + pPrimaryKey + "','" + pSearchString + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public string insertReturnedItem(ReturnedItem pReturnedItem)
        {
            string _Id = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spInsertReturnedItem(" + pReturnedItem.CashierPeriodId +
                    ", " + pReturnedItem.StockId +
                    ", " + pReturnedItem.LocationId +
                    ", " + pReturnedItem.Qty +
                    ", " + pReturnedItem.UnitPrice +
                    ", " + pReturnedItem.TotalPrice +
                    ", '" + pReturnedItem.Reason +
                    "', '" + pReturnedItem.UserId + "');", _conn);
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

        public string updateReturnedItem(ReturnedItem pReturnedItem)
        {
            string _Id = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spUpdateReturnedItem(" + pReturnedItem.Id +
                    ", " + pReturnedItem.CashierPeriodId +
                    ", " + pReturnedItem.StockId +
                    ", " + pReturnedItem.LocationId +
                    ", " + pReturnedItem.Qty +
                    ", " + pReturnedItem.UnitPrice +
                    ", " + pReturnedItem.TotalPrice +
                    ", '" + pReturnedItem.Reason +
                    "', '" + pReturnedItem.UserId + "');", _conn);
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

        public bool removeReturnedItem(string pId, string pUserId)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spRemoveReturnedItem(" + pId +
                    ",'" + pUserId + "');", _conn);
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