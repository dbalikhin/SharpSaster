using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SharpSaster.Data;
using System.Collections;
using System.Data;

namespace SharpSaster.InterfaceTests
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlBasicInterfaceInstanceController : ControllerBase
    {
        //public Repo _repoInstance = new Repo();

        private readonly IRepo _repoInjected;

        public SqlBasicInterfaceInstanceController(IRepo repo)
        {
            // not in use intentionally
            _repoInjected = repo;
        }

        /*
        public IActionResult SqlClientVulnerableRepoInstance(string username, string name)
        {
            _repoInstance.DoRepoStuff(username, name);

            return new OkResult();
        }
        */

    }
}
