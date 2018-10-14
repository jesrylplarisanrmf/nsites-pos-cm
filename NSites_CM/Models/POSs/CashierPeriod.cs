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
    public class CashierPeriod
    {
        public string Id { get; set; }
        public DateTime DateOpen { get; set; }
        public DateTime DateClose { get; set; }
        public string PeriodStatus { get; set; }
        public string CashierId { get; set; }
        public decimal CashDeposit { get; set; }
        public decimal TotalSales { get; set; }
        public decimal ReturnedItemTotal { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetSales { get; set; }
        public decimal NonCashSales { get; set; }
        public decimal CashSales { get; set; }
        public decimal NetCashSales { get; set; }
        public decimal CashCount { get; set; }
        public decimal Variance { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }

        public DataTable getCashierPeriods(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetCashierPeriods('" + pDisplayType + "'," + (pPrimaryKey == null ? "0" : pPrimaryKey) + ",'" + pSearchString + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getCashierPeriodOpen()
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetCashierPeriodOpen();", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getCashierPeriodStockSold(string pCashierPeriodId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetCashierPeriodStockSold(" + pCashierPeriodId+");", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getCashierPeriodReturnedItem(string pCashierPeriodId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetCashierPeriodReturnedItem(" + pCashierPeriodId+");", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getCashierPeriodByDate(DateTime pStartDate,DateTime pEndDate)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetCashierPeriodByDate('" + String.Format("{0:yyyy-MM-dd}", pStartDate) + "','" + String.Format("{0:yyyy-MM-dd}", pEndDate) + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public string insertCashierPeriod(CashierPeriod pCashierPeriod)
        {
            string _Id = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spInsertCashierPeriod('" + String.Format("{0:yyyy-MM-dd}", pCashierPeriod.DateOpen) +
                    "','" + String.Format("{0:yyyy-MM-dd}", pCashierPeriod.DateClose) +
                    "','" + pCashierPeriod.PeriodStatus +
                    "'," + pCashierPeriod.CashierId +
                    "," + pCashierPeriod.CashDeposit +
                    "," + pCashierPeriod.TotalSales +
                    "," + pCashierPeriod.ReturnedItemTotal +
                    "," + pCashierPeriod.TotalDiscount +
                    "," + pCashierPeriod.NetSales +
                    "," + pCashierPeriod.NonCashSales +
                    "," + pCashierPeriod.CashSales +
                    "," + pCashierPeriod.NetCashSales +
                    "," + pCashierPeriod.CashCount +
                    "," + pCashierPeriod.Variance +
                    ",'" + pCashierPeriod.Remarks +
                    "','" + pCashierPeriod.UserId + "');", _conn);
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

        public string updateCashierPeriod(CashierPeriod pCashierPeriod)
        {
            string _Id = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spUpdateCashierPeriod('" + pCashierPeriod.Id +
                    "','" + String.Format("{0:yyyy-MM-dd}", pCashierPeriod.DateOpen) +
                    "','" + String.Format("{0:yyyy-MM-dd}", pCashierPeriod.DateClose) +
                    "','" + pCashierPeriod.PeriodStatus +
                    "'," + pCashierPeriod.CashierId +
                    "," + pCashierPeriod.CashDeposit +
                    "," + pCashierPeriod.TotalSales +
                    "," + pCashierPeriod.ReturnedItemTotal +
                    "," + pCashierPeriod.TotalDiscount +
                    "," + pCashierPeriod.NetSales +
                    "," + pCashierPeriod.NonCashSales +
                    "," + pCashierPeriod.CashSales +
                    "," + pCashierPeriod.NetCashSales +
                    "," + pCashierPeriod.CashCount +
                    "," + pCashierPeriod.Variance +
                    ",'" + pCashierPeriod.Remarks +
                    "','" + pCashierPeriod.UserId + "');", _conn);
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

        public bool openCashierPeriod(string pCashierId,decimal pCashDeposit,string pRemarks,string pUserId)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spOpenCashierPeriod(" + pCashierId +
                    "," + pCashDeposit +
                    ",'" + pRemarks +
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

        public bool closeCashierPeriod(string pId, string pCashierId, decimal pTotalSales, decimal pReturnedItemTotal,
            decimal pTotalDiscount, decimal pNetSales, decimal pNonCashSales,
            decimal pCashSales, decimal pNetCashSales, decimal pCashCount, decimal pVariance, 
            string pRemarks,string pUserId)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spCloseCashierPeriod(" + pId +
                    "," + pCashierId +
                    "," + pTotalSales +
                    "," + pReturnedItemTotal +
                    "," + pTotalDiscount +
                    "," + pNetSales +
                    "," + pNonCashSales +
                    "," + pCashSales +
                    "," + pNetCashSales +
                    "," + pCashCount +
                    "," + pVariance +
                    ",'" + pRemarks +
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

        public bool removeCashierPeriod(string pId, string pUserId)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spRemoveCashierPeriod('" + pId +
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