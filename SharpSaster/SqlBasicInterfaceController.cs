﻿using Microsoft.AspNetCore.Mvc;
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
        public IRepo _repoVulnerable1 = new Repo();

        public IRepo _repoDummy = new DummyRepo();

        private readonly IRepo _repoVulnerable2;

        public SqlBasicInterfaceController(IRepo repo)
        {
            _repoVulnerable2 = repo;
        }


        public IActionResult SqlClientVulnerable1(string username, string name)
        {

            _repoVulnerable1.DoRepoStuff(username, name);

            return new OkResult();
        }

        public IActionResult SqlClientVulnerable2(string username, string name)
        {
            _repoVulnerable2.DoRepoStuff(username, name);

            return new OkResult();
        }

        public IActionResult SqlClientDummySafe(string username, string name)
        {
            _repoDummy.DoRepoStuff(username, name);

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

    public class DummyRepo : IRepo
    {
        public void DoRepoStuff(string username, string name)
        {
            return;
        }
    }
}