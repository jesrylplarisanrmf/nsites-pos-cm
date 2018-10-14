using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace NSites_CM.Models.Systems
{
    public class SystemConfiguration
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public DataTable getSystemConfigurations()
        {
            /*
            DataTable _dt = new DataTable();
            using(SqlConnection _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
            {
                _conn.Open();
                SqlDataAdapter _da = new SqlDataAdapter("exec spGetSystemConfigurations", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }*/
            DataTable _dt = new DataTable();
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetSystemConfigurations()", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public bool updateSystemConfiguration(SystemConfiguration pSystemConfiguration)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spUpdateSystemConfiguration('" + pSystemConfiguration.Key +
                    "','" + pSystemConfiguration.Value + "');", _conn);
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