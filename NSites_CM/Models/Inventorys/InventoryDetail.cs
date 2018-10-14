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
    public class InventoryDetail
    {
        public string DetailId { get; set; }
        public string InventoryId { get; set; }
        public string PODetailId { get; set; }
        public string SODetailId { get; set; }
        public string StockId { get; set; }
        public string LocationId { get; set; }
        public decimal POQty { get; set; }
        public decimal QtyIn { get; set; }
        public decimal SOQty { get; set; }
        public decimal QtyOut { get; set; }
        public decimal Variance { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }

        public DataTable getInventoryDetails(string pDisplayType, string pId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetInventoryDetails('" + pDisplayType + "'," + pId + ");", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getStockInventory(string pSearchString)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStockInventory('" + pSearchString + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getStockInventoryByLocation(string pLocationId, string pSearchString)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStockInventoryByLocation('" + pLocationId + "','" + pSearchString + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getStockInventoryList(string pLocationId, string pSearchString)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStockInventoryList('" + pLocationId + "','" + pSearchString + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public bool insertInventoryDetail(InventoryDetail pInventoryDetail)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spInsertInventoryDetail(" + pInventoryDetail.InventoryId +
                    "," + pInventoryDetail.PODetailId +
                    "," + pInventoryDetail.SODetailId +
                    "," + pInventoryDetail.StockId +
                    "," + pInventoryDetail.LocationId +
                    "," + pInventoryDetail.POQty +
                    "," + pInventoryDetail.QtyIn +
                    "," + pInventoryDetail.SOQty +
                    "," + pInventoryDetail.QtyOut +
                    "," + pInventoryDetail.Variance +
                    ",'" + pInventoryDetail.Remarks +
                    "','" + pInventoryDetail.UserId + "');", _conn);
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

        public bool updateInventoryDetail(InventoryDetail pInventoryDetail)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spUpdateInventoryDetail(" + pInventoryDetail.DetailId +
                    "," + pInventoryDetail.InventoryId +
                    "," + pInventoryDetail.PODetailId +
                    "," + pInventoryDetail.SODetailId +
                    "," + pInventoryDetail.StockId +
                    "," + pInventoryDetail.LocationId +
                    "," + pInventoryDetail.POQty +
                    "," + pInventoryDetail.QtyIn +
                    "," + pInventoryDetail.SOQty +
                    "," + pInventoryDetail.QtyOut +
                    "," + pInventoryDetail.Variance +
                    ",'" + pInventoryDetail.Remarks +
                    "','" + pInventoryDetail.UserId + "');", _conn);
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

        public bool removeInventoryDetail(string pDetailId, string pUserId)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spRemoveInventoryDetail('" + pDetailId +
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