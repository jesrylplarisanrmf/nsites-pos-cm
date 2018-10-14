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
    public class Inventory
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Final { get; set; }
        public string Cancel { get; set; }
        public string Type { get; set; }
        public string POId { get; set; }
        public string SOId { get; set; }
        public string STInId { get; set; }
        public string STOutId { get; set; }
        public string Reference { get; set; }
        public string SupplierId { get; set; }
        public string CustomerId { get; set; }
        public decimal TotalPOQty { get; set; }
        public decimal TotalQtyIn { get; set; }
        public decimal TotalSOQty { get; set; }
        public decimal TotalQtyOut { get; set; }
        public decimal TotalVariance { get; set; }
        public string PreparedBy { get; set; }
        public string FinalizedBy { get; set; }
        public DateTime DateFinalized { get; set; }
        public string CancelledBy { get; set; }
        public string CancelledReason { get; set; }
        public DateTime DateCancelled { get; set; }
        public string FromLocationId { get; set; }
        public string ToLocationId { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }

        public DataTable getInventorys(string pType, string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetInventorys('" + pType + "','" + pDisplayType + "'," + (pPrimaryKey == null ? "0" : pPrimaryKey) + ",'" + pSearchString + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getNextInventoryId()
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetNextInventoryId()", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getInventoryStatus(string pId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetInventoryStatus('" + pId + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getStockTransferOut(string pToLocationId, string pSearchString)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStockTransferOut('" + pToLocationId + "','" + pSearchString + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public string insertInventory(Inventory pInventory)
        {
            string _result = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spInsertInventory('" + String.Format("{0:yyyy-MM-dd}", pInventory.Date) +
                    "','" + pInventory.Type +
                    "','" + pInventory.POId +
                    "','" + pInventory.SOId +
                    "','" + pInventory.STInId +
                    "','" + pInventory.STOutId +
                    "','" + pInventory.Reference +
                    "','" + pInventory.CustomerId +
                    "','" + pInventory.SupplierId +
                    "','" + pInventory.TotalPOQty +
                    "','" + pInventory.TotalQtyIn +
                    "','" + pInventory.TotalSOQty +
                    "','" + pInventory.TotalQtyOut +
                    "','" + pInventory.TotalVariance +
                    "','" + pInventory.UserId +
                    "','" + pInventory.FromLocationId +
                    "','" + pInventory.ToLocationId +
                    "','" + pInventory.Remarks +
                    "','" + pInventory.UserId + "');", _conn);
                try
                {
                    _cmd.Transaction = _trans;
                    _result = _cmd.ExecuteScalar().ToString();
                    _trans.Commit();
                    _conn.Close();
                }
                catch
                {
                    _trans.Rollback();
                    _result = "";
                }
            }
            return _result;
        }

        public string updateInventory(Inventory pInventory)
        {
            string _result = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spUpdateInventory('" + pInventory.Id +
                    "','" + String.Format("{0:yyyy-MM-dd}", pInventory.Date) +
                    "','" + pInventory.Type +
                    "','" + pInventory.POId +
                    "','" + pInventory.SOId +
                    "','" + pInventory.STInId +
                    "','" + pInventory.STOutId +
                    "','" + pInventory.Reference +
                    "','" + pInventory.CustomerId +
                    "','" + pInventory.SupplierId +
                    "','" + pInventory.TotalPOQty +
                    "','" + pInventory.TotalQtyIn +
                    "','" + pInventory.TotalSOQty +
                    "','" + pInventory.TotalQtyOut +
                    "','" + pInventory.TotalVariance +
                    "','" + pInventory.UserId +
                    "','" + pInventory.FromLocationId +
                    "','" + pInventory.ToLocationId +
                    "','" + pInventory.Remarks +
                    "','" + pInventory.UserId + "');", _conn);
                try
                {
                    _cmd.Transaction = _trans;
                    _result = _cmd.ExecuteScalar().ToString();
                    _trans.Commit();
                    _conn.Close();
                }
                catch
                {
                    _trans.Rollback();
                    _result = "";
                }
            }
            return _result;
        }

        public bool removeInventory(string pId, string pUserId)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spRemoveInventory('" + pId +
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

        public bool finalInventory(string pId, string pUserId)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spFinalizeInventory('" + pId +
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

        public bool cancelInventory(string pId,string pCancelledReason, string pUserId)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spCancelInventory('" + pId +
                    "','" + pCancelledReason +
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