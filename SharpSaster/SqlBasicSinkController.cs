using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SharpSaster.Data;
using System.Collections;
using System.Data;

namespace SharpSaster
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlBasicSinkController : ControllerBase
    {
        private readonly DataContext _context;

        public SqlBasicSinkController(DataContext context)
        {
            _context= context; 
        }


        public IActionResult SqlClientSafe1(string username, string name)
        {
            Hashtable parameters = new Hashtable()
            {
                { "username", username },
                { "name", name },
            };

            using (SqlConnection connection = new SqlConnection("dummyconnectionstring"))
            {
                var spName = "sp_GetNormalStuff";
                var dt = GetDatatable(connection, spName, parameters);
            }

            return new OkResult();
        }

        public IActionResult SqlClientSafe2(string username, string name)
        {
            string commandText = "SELECT * FROM Accounts WHERE Username=@username OR Name=@name";
            using (SqlConnection connection = new SqlConnection("dummyconnectionstring"))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@username", SqlDbType.VarChar);
                command.Parameters["@username"].Value = username;

                command.Parameters.AddWithValue("@name", name);

                connection.Open();
                var rowsAffected = command.ExecuteNonQuery();
            }

            return new OkResult();
        }

        public IActionResult SqlClientSafe3(string username, string name)
        {
            int.TryParse(username, out int usernameInt);
            int.TryParse(name, out int nameInt);
            string concatSql = "SELECT * FROM Accounts WHERE UserId=" + usernameInt + " OR  NameId=" + nameInt;

            using (SqlConnection connection = new SqlConnection("dummyconnectionstring"))
            {
                SqlCommand concatSqlCommand = new SqlCommand()
                {
                    CommandText = concatSql,
                    CommandType = CommandType.Text,
                };

                concatSqlCommand.ExecuteReader();
            }

            return new OkResult();
        }

        public IActionResult SqlClientVulnerable1(string username, string name)
        {
            string concatSql = "SELECT * FROM Accounts WHERE Username='" + username + "' OR  Name='" + name + "'";

            using(SqlConnection connection = new SqlConnection("dummyconnectionstring"))
            {
                SqlCommand concatSqlCommand = new SqlCommand()
                {
                    CommandText = concatSql,
                    CommandType = CommandType.Text,
                };
                
                concatSqlCommand.ExecuteReader();
            }

            return new OkResult();
        }

        public IActionResult SqlClientVulnerable2(string username, string name)
        {
            
            string formattedSql =
                string.Format("SELECT * FROM Accounts WHERE Username = '{0}' AND Name ='{1}'",
                    username, name);

            using (SqlConnection connection = new SqlConnection("dummyconnectionstring"))
            {
                SqlCommand formattedSqlCommand = new SqlCommand()
                {
                    CommandText = formattedSql,
                    CommandType = CommandType.Text,
                };

                formattedSqlCommand.ExecuteReader();
            }

            return new OkResult();
        }

        public IActionResult SqlClientVulnerable3(string username, string name)
        {
            string interpolatedSql = $"SELECT * FROM Accounts WHERE Username = '{username}' AND Name ='{name}'";

            using (SqlConnection connection = new SqlConnection("dummyconnectionstring"))
            {
                SqlCommand interpolatedSqlCommand = new SqlCommand()
                {
                    CommandText = interpolatedSql,
                    CommandType = CommandType.Text,
                };

                interpolatedSqlCommand.ExecuteReader();
            }

            return new OkResult();
        }

        public IActionResult SqlClientVulnerable4(string username, string name)
        {
            // user is actually UserId - int, a single source, only name is vulnerable
            int.TryParse(username, out int usernameInt);      
            string concatSql = "SELECT * FROM Accounts WHERE UserId=" + usernameInt + "' OR  Name='" + name + "'";

            using (SqlConnection connection = new SqlConnection("dummyconnectionstring"))
            {
                SqlCommand concatSqlCommand = new SqlCommand()
                {
                    CommandText = concatSql,
                    CommandType = CommandType.Text,
                };

                concatSqlCommand.ExecuteReader();
            }

            return new OkResult();
        }

        public IActionResult SqlClientVulnerable10(string username, string name, string spName)
        {
            // arbitrary stored procedure call
            Hashtable parameters = new Hashtable()
            {
                { "username", username },
                { "name", name },
            };

            using (SqlConnection connection = new SqlConnection("dummyconnectionstring"))
            {                
                var dt = GetDatatable(connection, spName, parameters);
            }

            return new OkResult();
        }      

        private DataTable GetDatatable(SqlConnection connection, string procName, Hashtable parms)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;
            if (parms.Count > 0)
            {
                foreach (DictionaryEntry de in parms)
                {
                    cmd.Parameters.AddWithValue(de.Key.ToString(), de.Value);
                }
            }
            da.SelectCommand = cmd;
            da.Fill(dt);
            return dt;
        }


    }
}
