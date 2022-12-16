using Microsoft.AspNetCore.Mvc;
using SharpSaster.Data;

namespace SharpSaster.InterfaceTests
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlBasicInterfaceController : ControllerBase
    {
        private readonly IRepo _repoInjected;

        public SqlBasicInterfaceController(IRepo repo)
        {
            _repoInjected = repo;
        }

        public IActionResult SqlClientVulnerableRepoInjected(string username, string name)
        {
            _repoInjected.DoRepoStuff(username, name);

            return new OkResult();
        }

    }
}
