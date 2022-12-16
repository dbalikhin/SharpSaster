using Microsoft.AspNetCore.Mvc;

namespace SharpSaster.InterfaceTests
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlBasicInterfaceController : ControllerBase
    {
        public IRepo _repo = new Repo();

        private readonly IRepo _repoInjected;

        public SqlBasicInterfaceController(IRepo repo)
        {
            _repoInjected = repo;
        }


        public IActionResult SqlClientVulnerableRepo(string username, string name)
        {
            _repo.DoRepoStuff(username, name);

            return new OkResult();
        }

        public IActionResult SqlClientVulnerableRepoInjected(string username, string name)
        {
            _repoInjected.DoRepoStuff(username, name);

            return new OkResult();
        }

    }
}
