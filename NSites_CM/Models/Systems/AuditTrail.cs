using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;

using System.DirectoryServices.AccountManagement;
using MySql.Data.MySqlClient;

namespace NSites_CM.Models.Systems
{
    public class AuditTrail
    {
        public string Id { get; set; }
        public string LogDescription { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }

        public DataTable getAuditTrailByDate(DateTime pFrom, DateTime pTo)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetAuditTrailByDate('" + String.Format("{0:yyyy-MM-dd}", pFrom) + "','" + String.Format("{0:yyyy-MM-dd}", pTo) + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public bool removeAuditTrail(DateTime pFrom, DateTime pTo)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spRemoveAuditTrail('" + String.Format("{0:yyyy-MM-dd}", pFrom) +
                    "','" + String.Format("{0:yyyy-MM-dd}", pTo) + "');", _conn);
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