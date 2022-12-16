namespace SharpSaster.InterfaceTests
{
    public class DummyRepo : IRepo
    {
        public void DoRepoEFCoreStuff(string username, string name)
        {
            throw new NotImplementedException();
        }

        public void DoRepoStuff(string username, string name)
        {
            return;
        }
    }
}
