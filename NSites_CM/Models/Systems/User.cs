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
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string UserGroupId { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }

        public DataTable authenticateUser(string pUsername, string pPassword)
        {
            DataTable _dt = new DataTable();
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spAuthenticateUser('" + pUsername + "','" + pPassword + "')", _conn);
                _da.Fill(_dt);
                _conn.Close();
            }

            return _dt;

            #region "IF LOG IN VIA ACTIVE DIRECTORY"
            /*
            try
            {
                DataTable _dt = new DataTable();
                bool _activeUser = false;

                // create a "principal context" - e.g. your domain (could be machine, too)
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, ConfigurationManager.ConnectionStrings["LocalDomain"].ConnectionString))
                {
                    // validate the credentials
                    bool isValid = pc.ValidateCredentials(pUsername, pPassword);
                    if (isValid)
                    {
                        using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
                        {
                            _conn.Open();
                            MySqlDataAdapter _da = new MySqlDataAdapter("exec spAuthenticateUser @Username=N'" + pUsername + "'", _conn);
                            _da.Fill(_dt);
                            _conn.Close();

                            if (_dt.Rows.Count > 0)
                            {
                                _activeUser = true;
                            }
                            else
                            {
                                _activeUser = false;
                            }
                        }
                    }
                    else
                    {
                        _activeUser = false;
                    }
                }

                return _activeUser;
            }
            catch
            {
                return false;
            }*/
            #endregion
        }
       
        public DataTable getUsers(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            DataTable _dt = new DataTable();
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetUsers('" + pDisplayType + "','" + pPrimaryKey + "','" + pSearchString + "')", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public bool checkUserPassword(string pUserId, string pCurrentPassword)
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spCheckUserPassword('" + pUserId + "','" + pCurrentPassword + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                if (_dt.Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool changePassword(string pUserId,string pNewPassword)
        {
            if (pNewPassword == null)
            {
                pNewPassword = "";
            }
            bool _success = false;
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spChangePassword('" + pUserId +
                               "','" + pNewPassword + "');", _conn);
                try
                {
                    _cmd.Transaction = _trans;
                    int _RowsAffected = _cmd.ExecuteNonQuery();
                    _trans.Commit();
                    _conn.Close();
                    if (_RowsAffected > 0)
                    {
                        _success = true;
                    }
                    else
                    {
                        _success = false;
                    }
                    return _success;
                }
                catch (Exception ex)
                {
                    _trans.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string insertUser(User pUser)
        {
            string _Id = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spInsertUser('" + pUser.Username +
                    "','" + pUser.Password +
                    "','" + pUser.Fullname +
                    "','" + pUser.UserGroupId +
                    "','" + pUser.Remarks +
                    "','" + pUser.UserId + "');", _conn);
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

        public string updateUser(User pUser)
        {
            string _Id = "";
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spUpdateUser('" + pUser.Id +
                    "','" + pUser.Username +
                    "','" + pUser.Password +
                    "','" + pUser.Fullname +
                    "','" + pUser.UserGroupId +
                    "','" + pUser.Remarks +
                    "','" + pUser.UserId + "');", _conn);
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

        public bool removeUser(string pId, string pUserId)
        {
            bool _success = false;
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spRemoveUser('" + pId +
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