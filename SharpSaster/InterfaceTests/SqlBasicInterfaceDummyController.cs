using Microsoft.AspNetCore.Mvc;

namespace SharpSaster.InterfaceTests
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlBasicInterfaceDummyController : ControllerBase
    {
        public IRepo _repoDummy = new DummyRepo();

        public IRepo _repoInjected;

        public DummyRepo _repoDummyInstance = new DummyRepo();

        public SqlBasicInterfaceDummyController(IRepo repo)
        {
            _repoInjected = repo;
        }


        public IActionResult SqlClientDummySafe(string username, string name)
        {
            _repoDummy.DoRepoStuff(username, name);

            return new OkResult();
        }

        public IActionResult SqlClientDummySafe2(string username, string name)
        {
            _repoDummy.DoRepoEFCoreStuff(username, name);

            return new OkResult();
        }

        public IActionResult SqlClientDummyInstanceSafe(string username, string name)
        {
            _repoDummyInstance.DoRepoStuff(username, name);

            return new OkResult();
        }
    }


}
