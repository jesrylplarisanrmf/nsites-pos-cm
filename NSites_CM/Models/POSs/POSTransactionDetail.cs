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
    public class POSTransactionDetail
    {
        public string DetailId { get; set; }
        public string TransactionId { get; set; }
        public string StockId { get; set; }
        public string LocationId { get; set; }
        public string VATable { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitCost { get; set; }
        public decimal BasePrice { get; set; }
        public decimal UnitPrice { get; set; }
        public string DiscountId { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }

        public DataTable getPOSTransactionDetails(string pTransactionId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetPOSTransactionDetails('" + pTransactionId + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getPOSTransactionDetailsForEdit(string pTransactionId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetPOSTransactionDetailsForEdit('" + pTransactionId + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getSalesInventory(DateTime pStartDate, DateTime pEndDate)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetSalesInventory('" + String.Format("{0:yyyy-MM-dd}", pStartDate) + "','" + String.Format("{0:yyyy-MM-dd}", pEndDate) + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getSalesInventoryBy(DateTime pStartDate, DateTime pEndDate)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetSalesInventoryBy('" + String.Format("{0:yyyy-MM-dd}", pStartDate) + "','" + String.Format("{0:yyyy-MM-dd}", pEndDate) + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getReturnedItems(DateTime pStartDate, DateTime pEndDate)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetReturnedItems('" + String.Format("{0:yyyy-MM-dd}", pStartDate) + "','" + String.Format("{0:yyyy-MM-dd}", pEndDate) + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public bool insertPOSTransactionDetail(POSTransactionDetail pPOSTransactionDetail)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spInsertPOSTransactionDetail(" + pPOSTransactionDetail.TransactionId +
                    "," + pPOSTransactionDetail.StockId +
                    "," + pPOSTransactionDetail.LocationId +
                    ",'" + pPOSTransactionDetail.VATable +
                    "'," + pPOSTransactionDetail.Qty +
                    "," + pPOSTransactionDetail.UnitCost +
                    "," + pPOSTransactionDetail.BasePrice +
                    ", " + pPOSTransactionDetail.UnitPrice +
                    ", '" + pPOSTransactionDetail.DiscountId +
                    "', " + pPOSTransactionDetail.DiscountAmount +
                    ", " + pPOSTransactionDetail.TotalPrice +
                    ", '" + pPOSTransactionDetail.UserId + "');", _conn);
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

        public bool updatePOSTransactionDetail(POSTransactionDetail pPOSTransactionDetail)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spUpdatePOSTransactionDetail(" + pPOSTransactionDetail.DetailId +
                    ", " + pPOSTransactionDetail.TransactionId +
                    ", " + pPOSTransactionDetail.StockId +
                    ", " + pPOSTransactionDetail.LocationId +
                    ", '" + pPOSTransactionDetail.VATable +
                    "', " + pPOSTransactionDetail.Qty +
                    ", " + pPOSTransactionDetail.UnitCost +
                    ", " + pPOSTransactionDetail.BasePrice +
                    ", " + pPOSTransactionDetail.UnitPrice +
                    ", '" + pPOSTransactionDetail.DiscountId +
                    "', " + pPOSTransactionDetail.DiscountAmount +
                    ", " + pPOSTransactionDetail.TotalPrice +
                    ", '" + pPOSTransactionDetail.UserId + "');", _conn);
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

        public bool removePOSTransactionDetail(string pDetailId, string pUserId)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spRemovePOSTransactionDetail(" + pDetailId +
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