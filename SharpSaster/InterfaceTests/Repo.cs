using Microsoft.Data.SqlClient;
using System.Data;

namespace SharpSaster.InterfaceTests
{
    public class Repo : IRepo
    {
        public void DoRepoStuff(string username, string name)
        {

            using (SqlConnection connection = new SqlConnection("dummyconnectionstring"))
            {
                SqlCommand command = connection.CreateCommand();

                string concatSql = "SELECT * FROM Accounts WHERE Username='" + username + "' OR  Name='" + name + "'";

                command.CommandText = concatSql;
                command.CommandType = CommandType.Text;

                command.ExecuteReader();
            }
        }
    }
}
