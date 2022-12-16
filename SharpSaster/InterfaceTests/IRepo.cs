namespace SharpSaster.InterfaceTests
{
    public interface IRepo
    {
        void DoRepoStuff(string username, string name);

        void DoRepoEFCoreStuff(string username, string name);
    }
}
