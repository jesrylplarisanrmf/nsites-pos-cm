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
    public class POSTransaction
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string CashierPeriodId { get; set; }
        public string CustomerId { get; set; }
        public string OrderType { get; set; }
        public string TableId { get; set; }
        public string ORNo { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalLessVAT { get; set; }
        public decimal TotalDue { get; set; }
        public decimal VATSale { get; set; }
        public decimal VATExemptSale { get; set; }
        public decimal VATAmount { get; set; }
        public decimal AmountTendered { get; set; }
        public string Paid { get; set; }
        public string OutletId { get; set; }
        public string DiscountId { get; set; }
        public string ModeOfPaymentId { get; set; }
        public string PaymentDetails { get; set; }
        public string CashierId { get; set; }
        public string Terminal { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }

        public DataTable getPOSTransactions(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetPOSTransactions('" + pDisplayType + "','" + pPrimaryKey + "','" + pSearchString + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getPOSTransactionsByDate(DateTime pStartDate, DateTime pEndDate)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetPOSTransactionsByDate('" + String.Format("{0:yyyy-MM-dd}", pStartDate) + "','" + String.Format("{0:yyyy-MM-dd}", pEndDate) + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getPOSTransaction(string pId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetPOSTransaction(" + pId+");", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getPOSTransactionLists(string pCashierPeriodId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetPOSTransactionLists("+pCashierPeriodId+");", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getTotalSalesByCashierPeriod(string pCashierPeriodId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetTotalSalesByCashierPeriod("+pCashierPeriodId+");", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getTotalReturnedByCashierPeriod(string pCashierPeriodId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetTotalReturnedByCashierPeriod("+pCashierPeriodId+");", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getTotalDiscountByCashierPeriod(string pCashierPeriodId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetTotalDiscountByCashierPeriod(" + pCashierPeriodId+");", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getSalesByCashierPeriod(string pCashierPeriodId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetSalesByCashierPeriod(" + pCashierPeriodId+");", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public string insertPOSTransaction(POSTransaction pPOSTransaction)
        {
            string _Id = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spInsertPOSTransaction('" + String.Format("{0:yyyy-MM-dd}", pPOSTransaction.Date) +
                    "'," + pPOSTransaction.CashierPeriodId +
                    "," + pPOSTransaction.CustomerId +
                    ",'" + pPOSTransaction.OrderType +
                    "','" + pPOSTransaction.TableId +
                    "','" + pPOSTransaction.ORNo +
                    "'," + pPOSTransaction.TotalPrice +
                    "," + pPOSTransaction.TotalDiscount +
                    "," + pPOSTransaction.TotalLessVAT +
                    "," + pPOSTransaction.TotalDue +
                    "," + pPOSTransaction.VATSale +
                    "," + pPOSTransaction.VATExemptSale +
                    "," + pPOSTransaction.VATAmount +
                    "," + pPOSTransaction.AmountTendered +
                    ",'" + pPOSTransaction.Paid +
                    "'," + pPOSTransaction.OutletId +
                    ",'" + pPOSTransaction.DiscountId +
                    "','" + pPOSTransaction.ModeOfPaymentId +
                    "','" + pPOSTransaction.PaymentDetails +
                    "','" + pPOSTransaction.CashierId +
                    "','" + pPOSTransaction.Terminal +
                    "','" + pPOSTransaction.Remarks +
                    "','" + pPOSTransaction.UserId + "');", _conn);
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

        public string updatePOSTransaction(POSTransaction pPOSTransaction)
        {
            string _Id = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spUpdatePOSTransaction(" + pPOSTransaction.Id +
                    ",'" + String.Format("{0:yyyy-MM-dd}", pPOSTransaction.Date) +
                    "'," + pPOSTransaction.CashierPeriodId +
                    "," + pPOSTransaction.CustomerId +
                    ",'" + pPOSTransaction.OrderType +
                    "','" + pPOSTransaction.TableId +
                    "','" + pPOSTransaction.ORNo +
                    "'," + pPOSTransaction.TotalPrice +
                    "," + pPOSTransaction.TotalDiscount +
                    "," + pPOSTransaction.TotalLessVAT +
                    "," + pPOSTransaction.TotalDue +
                    "," + pPOSTransaction.VATSale +
                    "," + pPOSTransaction.VATExemptSale +
                    "," + pPOSTransaction.VATAmount +
                    "," + pPOSTransaction.AmountTendered +
                    ",'" + pPOSTransaction.Paid +
                    "'," + pPOSTransaction.OutletId +
                    ",'" + pPOSTransaction.DiscountId +
                    "','" + pPOSTransaction.ModeOfPaymentId +
                    "','" + pPOSTransaction.PaymentDetails +
                    "','" + pPOSTransaction.CashierId +
                    "','" + pPOSTransaction.Terminal +
                    "','" + pPOSTransaction.Remarks +
                    "','" + pPOSTransaction.UserId + "');", _conn);
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

        public bool removePOSTransaction(string pId, string pUsername)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spRemovePOSTransaction(" + pId +
                    ",'" + pUsername + "');", _conn);
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