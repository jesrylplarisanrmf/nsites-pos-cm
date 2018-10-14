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
    public class Stock
    {
        #region "PROPERTIES"
        public string Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string UnitId { get; set; }
        public decimal UnitCost { get; set; }
        public decimal BasePrice { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ReorderLevel { get; set; }
        public string Active { get; set; }
        public string Saleable { get; set; }
        public string NonInventory { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        public DataTable getStocks(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStocks('" + pDisplayType + "'," + (pPrimaryKey == null ? "0" : pPrimaryKey) + ",'" + pSearchString + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getStocksByCode(string pCode)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStocksByCode('" + pCode + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getSaleableStocks()
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetSaleableStocks();", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getSaleableStock(string pCode, string pDescription)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetSaleableStock('" + pCode + "','" + pDescription + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getStockCard(DateTime pFromDate, DateTime pToDate, string pStockId, string pLocationId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStockCard('" + String.Format("{0:yyyy-MM-dd}", pFromDate) + "','" + String.Format("{0:yyyy-MM-dd}", pToDate) + "','" + pStockId + "','" + pLocationId + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getStockCardBegBal(DateTime pFromDate, string pStockId, string pLocationId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStockCardBegBal('" + String.Format("{0:yyyy-MM-dd}", pFromDate) + "','" + pStockId + "','" + pLocationId + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getStockQtyOnHand(string pLocationId, string pStockId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStockQtyOnHand('" + pLocationId + "','" + pStockId + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getReorderLevel()
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetReorderLevel()", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public string insertStock(Stock pStock)
        {
            string _Id = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spInsertStock('" + pStock.Code +
                    "','" + pStock.Description +
                    "','" + (pStock.CategoryId == "" ? "0" : pStock.CategoryId) +
                    "','" + (pStock.UnitId == "" ? "0" : pStock.UnitId) +
                    "','" + pStock.UnitCost +
                    "','" + pStock.BasePrice +
                    "','" + pStock.UnitPrice +
                    "','" + pStock.ReorderLevel +
                    "','" + pStock.Active +
                    "','" + pStock.Saleable +
                    "','" + pStock.NonInventory +
                    "','" + pStock.Remarks +
                    "','" + pStock.UserId + "');", _conn);
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

        public string updateStock(Stock pStock)
        {
            string _Id = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spUpdateStock('" + pStock.Id +
                    "','" + pStock.Code +
                    "','" + pStock.Description +
                    "','" + (pStock.CategoryId == "" ? "0" : pStock.CategoryId) +
                    "','" + (pStock.UnitId == "" ? "0" : pStock.UnitId) +
                    "','" + pStock.UnitCost +
                    "','" + pStock.BasePrice +
                    "','" + pStock.UnitPrice +
                    "','" + pStock.ReorderLevel +
                    "','" + pStock.Active +
                    "','" + pStock.Saleable +
                    "','" + pStock.NonInventory +
                    "','" + pStock.Remarks +
                    "','" + pStock.UserId + "');", _conn);
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

        public bool removeStock(string pId, string pUsernId)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spRemoveStock('" + pId +
                    "','" + pUsernId + "');", _conn);
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