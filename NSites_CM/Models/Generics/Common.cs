using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using MySql.Data.MySqlClient;
using System.IO;
using System.Diagnostics;

namespace NSites_CM.Models.Generics
{
    public class Common
    {
        public DataTable getDataFromSearch(string pQueryString)
        {
            DataTable _dt = new DataTable();
            using(MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter(pQueryString, _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getUserGroupMenuItems(string pUsername)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetUserGroupMenuItems('" + pUsername + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getUserGroupRights(string pUsername)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetUserGroupRights('" + pUsername + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getMenuItems()
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetMenuItems()", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getAllMenuItems()
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetAllMenuItems();", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getAllRights(string pItemName)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetAllRights('" + pItemName + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getMenuItemsByGroup(string pUserGroupId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetMenuItemsByUserGroup('" + pUserGroupId + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getEnableRights(string pItemName, string pUserGroupId)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetEnableRights('" + pItemName + "','" + pUserGroupId + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        public DataTable getEnableCompanys(string pUserGroupId)
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetEnableCompanys('" + pUserGroupId + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool sendEmail(string pFrom, string pTo, string pCC, string pSubject, string pBody, string pUsername, string pUserPassword)
        {
            bool _return = false;
            try
            {
                //NEW USING AEV NO-REPLY
                MailMessage objeto_mail = new MailMessage();
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.Host = "192.168.2.11";
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(pUsername, pUserPassword);
                objeto_mail.From = new MailAddress(pFrom);
                objeto_mail.To.Add(new MailAddress(pTo));
                if (pCC != "")
                {
                    objeto_mail.CC.Add(new MailAddress(pCC));
                }
                objeto_mail.Subject = pSubject;
                objeto_mail.IsBodyHtml = true;

                objeto_mail.Body = pBody;
                client.Send(objeto_mail);
                _return = true;
            }
            catch
            {
                _return = true;
            }
            return _return;
        }

        public DataTable getTemplateNames(string pMenuName, string pUserId)
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetTemplateNames('" + pMenuName + "','" + pUserId + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();
                return _dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getTemplateName(string pId)
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetTemplateName('" + pId + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();
                return _dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getSearchFilters(string pTemplateId)
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetSearchFilters('" + pTemplateId + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();
                return _dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string insertSearchTemplate(string pTemplateName, string pItemName, string pPrivate, string pUserId)
        {
            string _Id = "";
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spInsertSearchTemplate('" + pTemplateName +
                                "','" + pItemName +
                                "','" + pPrivate +
                                "','" + pUserId + "');", _conn);
                try
                {
                    _cmd.Transaction = _trans;
                    _Id = _cmd.ExecuteScalar().ToString();
                    _trans.Commit();
                    _conn.Close();


                }
                catch { }
                {
                    _trans.Rollback();
                }
            }
            catch { }
            return _Id;
        }

        public bool updateSearchTemplate(string pId, string pTemplateName, string pItemName, string pPrivate)
        {
            bool _success = false;
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spUpdateSearchTemplate('" + pId +
                                "','" + pTemplateName +
                                "','" + pItemName +
                                "','" + pPrivate + "');", _conn);
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

        public bool removeSearchFilter(string pTemplateId)
        {
            bool _success = false;
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spRemoveSearchFilter('" + pTemplateId + "');", _conn);
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

        public bool removeSearchTemplate(string pId)
        {
            bool _success = false;
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spRemoveSearchTemplate('" + pId + "');", _conn);
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

        public bool renameSearchTemplate(string pId, string pTemplateName)
        {
            bool _success = false;
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spRenameSearchTemplate('" + pId +
                                "','" + pTemplateName + "');", _conn);
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

        public bool insertSearchFilter(string pTemplateId, string pField, string pOperator, string pValue, string pCheckAnd, string pCheckOr, int pSequence)
        {
            bool _success = false;
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlTransaction _trans = _conn.BeginTransaction();
                MySqlCommand _cmd = new MySqlCommand("call spInsertSearchFilter('" + pTemplateId +
                                "','" + pField +
                                "','" + pOperator +
                                "','" + pValue +
                                "','" + pCheckAnd +
                                "','" + pCheckOr +
                                "','" + pSequence + "');", _conn);
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

        public DataTable getViewDetails()
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetViewDetails();", _conn);
                _da.Fill(_dt);
                _conn.Close();
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getTableDetails()
        {
            DataTable _dt = new DataTable();
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                string _dbName = "";
                string[] _conn1 = _connectionString.Split(';');

                foreach (string _c1 in _conn1)
                {
                    string[] _conn2 = _c1.Split('=');
                    if (_conn2[0].ToString() == " DATABASE")
                    {
                        _dbName = _conn2[1].ToString();
                    }
                }

                MySqlConnection _conn = new MySqlConnection(_connectionString);
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetTableDetails('" + _dbName + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getMenuItemDetails()
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetMenuItemDetails()", _conn);
                _da.Fill(_dt);
                _conn.Close();
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getItemRightDetails()
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetItemRightDetails()", _conn);
                _da.Fill(_dt);
                _conn.Close();
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getSystemConfigurationDetails()
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetSystemConfigurationDetails();", _conn);
                _da.Fill(_dt);
                _conn.Close();
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getNextTabelSequenceId(string pDescription)
        {
            DataTable _dt = new DataTable();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetNextTableSequenceId('" + pDescription + "');", _conn);
                _da.Fill(_dt);
                _conn.Close();

                return _dt;
            }
        }

        #region "TECHNICAL UPDATE CODE"
        public DataTable getStoredProcedureDetails(string pDatabaseName)
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("select ROUTINE_NAME, ROUTINE_DEFINITION AS Routine_Definition " +
                         "from " + pDatabaseName + ".information_schema.routines  " +
                         "where routine_type = 'PROCEDURE' order by ROUTINE_NAME ASC;", _conn);
                _da.Fill(_dt);
                _conn.Close();
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getFunctionDetails(string pDatabaseName)
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlConnection _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
                _conn.Open();
                MySqlDataAdapter _da = new MySqlDataAdapter("select ROUTINE_NAME,concat(DATA_TYPE,'(',CHARACTER_MAXIMUM_LENGTH,')') as Return_Type,ROUTINE_DEFINITION as Routine_Definition " +
                             "from " + pDatabaseName + ".information_schema.routines " +
                             "where routine_type = 'FUNCTION' order by ROUTINE_NAME ASC;", _conn);
                _da.Fill(_dt);
                _conn.Close();
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region "Backup/Restore Database"
        public bool backupDatabase(string pSaveFileTo, string pBackupMySqlDumpAddress,
            string pUserId, string pPassword, string pServer, string pDatabase)
        {
            bool _success = false;
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;

                //Save file to C:\ with the current date as a filename
                string path;
                path = pSaveFileTo +"\\DBBackup"+ year + "-" + month + "-" + day +
            "-" + hour + "-" + minute + "-" + second + ".sql";

                StreamWriter file = new StreamWriter(path);

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = pBackupMySqlDumpAddress;
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                    pUserId, pPassword, pServer, pDatabase);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
                _success = true;
            }
            catch
            {
                _success = false;
            }

            return _success;
        }

        public bool restoreDatabase(string pSQLFileFrom, string pRestoreMySqlAddress,
            string pUserId, string pPassword, string pServer, string pDatabase)
        {
            bool _success = false;
            try
            {
                //Read file from C:\
                string path;
                path = pSQLFileFrom;
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = pRestoreMySqlAddress;
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                     pUserId, pPassword, pServer, pDatabase);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
                _success = true;
            }
            catch
            {
                _success = false;
            }

            return _success;
        }

        #endregion
    }
}