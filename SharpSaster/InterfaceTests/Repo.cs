using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SharpSaster.Data;
using System.Data;

namespace SharpSaster.InterfaceTests
{
    public class Repo : IRepo
    {
        private readonly DataContext _context;

        public Repo(DataContext context)
        {
            _context = context;
        }

        public void DoRepoEFCoreStuff(string username, string name)
        {
            var concatSql = "SELECT * FROM ACCOUNTS WHERE username = '" + username + "' AND name='" + name + "'";
            _context.Accounts
                .FromSqlRaw(concatSql);        
        }

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
