using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SharpSaster.Data;
using System.Collections;
using System.Data;

namespace SharpSaster
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlBasicInterfaceController : ControllerBase
    {
        public IRepo _repo1 = new Repo();

        private readonly IRepo _repo2;

        public SqlBasicInterfaceController(IRepo repo)
        {            
            _repo2 = repo;
        }


        public IActionResult SqlClientVulnerable1(string username, string name)
        {
            _repo1.DoRepoStuff(username, name);

            return new OkResult();
        }

        public IActionResult SqlClientVulnerable2(string username, string name)
        {
            _repo2.DoRepoStuff(username, name);

            return new OkResult();
        }
    }


    public interface IRepo
    {
        void DoRepoStuff(string username, string name);
    }

    public class Repo : IRepo
    {
        public void DoRepoStuff(string username, string name)
        {
            string concatSql = "SELECT * FROM Accounts WHERE Username='" + username + "' OR  Name='" + name + "'";

            using (SqlConnection connection = new SqlConnection("dummyconnectionstring"))
            {
                SqlCommand concatSqlCommand = new SqlCommand()
                {
                    CommandText = concatSql,
                    CommandType = CommandType.Text,
                };

                concatSqlCommand.ExecuteReader();
            }
        }
    }
}
