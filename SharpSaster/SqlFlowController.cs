using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpSaster.Data;

namespace SharpSaster
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlFlowController : ControllerBase
    {
        private readonly DataContext _context;

        public SqlFlowController(DataContext context)
        {
            _context= context; 
        }

        public IActionResult EfCoreFlow1(string input, string otherparam)
        {
            DoStuffLevel1(input);

            return new OkResult();
        }

        private void DoStuffLevel1(string inputLevel1)
        {
            var toLog = "logged: " + inputLevel1;
            DoStuffLevel2(inputLevel1);
        }

        private void DoStuffLevel2(string inputLevel2)
        {
            if (inputLevel2.Length == 0)
            {
                return;
            }
            var concatSql = "SELECT * FROM ACCOUNTS WHERE login = '" + inputLevel2 + "'";
            _context.Accounts
                .FromSqlRaw(concatSql);
        }


        public IActionResult EfCoreFlow2(string input, string otherparam)
        {            
            DoOtherStuffLevel1(input);            

            DoStuffLevel1(input);

            return new OkResult();
        }

        private void DoOtherStuffLevel1(string otherStuffLevel1)
        {
            string storingOtherStuff = "stored: " + otherStuffLevel1;
        }


        public IActionResult EfCoreFlow3(string input, string otherparam)
        {
            var otherVar = input + "tainted";

            DoOtherStuffLevel1(otherVar);
            DoStuffLevel1(otherVar);

            return new OkResult();
        }

        public IActionResult EfCoreFlow4(string input, string otherparam)
        {
            var localCondition = true;

            if (localCondition)
            {
                DoStuffLevel1(input);
            }            

            return new OkResult();
        }



        // UNREACHABLE
        public IActionResult EfCoreFlow10(string input, string otherparam)
        {
            return new OkResult();

            DoStuffLevel1(input);
        }

        // UNREACHABLE
        public IActionResult EfCoreFlow11(string input, string otherparam)
        {
            var localCondition = false;

            if (localCondition)
            {
                DoStuffLevel1(input);
            }

            return new OkResult();
        }

        // HARMLESS (reachable with null or empty input
        public IActionResult EfCoreFlow12(string input, string otherparam)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return new OkResult();
            }

            DoStuffLevel1(input);

            return new OkResult();
        }
    }
}
