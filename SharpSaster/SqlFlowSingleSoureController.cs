using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpSaster.Data;

namespace SharpSaster
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlFlowSingleSourceController : ControllerBase
    {
        private readonly DataContext _context;

        public SqlFlowSingleSourceController(DataContext context)
        {
            _context= context; 
        }

        #region Flow 1
        public IActionResult EfCoreFlow1(string input, string otherparam)
        {
            DoStuff1Level1(input);

            return new OkResult();
        }

        private void DoStuff1Level1(string inputLevel1)
        {
            var toLog = "logged: " + inputLevel1;
            DoStuff1Level2(inputLevel1);
        }

        private void DoStuff1Level2(string inputLevel2)
        {
            if (inputLevel2.Length == 0)
            {
                return;
            }
            var concatSql = "SELECT * FROM ACCOUNTS WHERE login = '" + inputLevel2 + "'";
            _context.Accounts
                .FromSqlRaw(concatSql);
        }
        #endregion

        #region Flow 2
        public IActionResult EfCoreFlow2(string input, string otherparam)
        {            
            DoOtherStuff2Level1(input);            

            DoStuff2Level1(input);

            return new OkResult();
        }

        private void DoStuff2Level1(string inputLevel1)
        {
            var toLog = "logged: " + inputLevel1;
            DoStuff2Level2(inputLevel1);
        }

        private void DoStuff2Level2(string inputLevel2)
        {
            if (inputLevel2.Length == 0)
            {
                return;
            }
            var concatSql = "SELECT * FROM ACCOUNTS WHERE login = '" + inputLevel2 + "'";
            _context.Accounts
                .FromSqlRaw(concatSql);
        }

        private void DoOtherStuff2Level1(string otherStuffLevel1)
        {
            string storingOtherStuff = "stored: " + otherStuffLevel1;
        }
        #endregion


        #region Flow 3
        public IActionResult EfCoreFlow3(string input, string otherparam)
        {
            var otherVar = input + "tainted";

            DoOtherStuff3Level1(otherVar);
            DoStuff3Level1(otherVar);

            return new OkResult();
        }

        private void DoStuff3Level1(string inputLevel1)
        {
            var toLog = "logged: " + inputLevel1;
            DoStuff3Level2(inputLevel1);
        }

        private void DoStuff3Level2(string inputLevel2)
        {
            if (inputLevel2.Length == 0)
            {
                return;
            }
            var concatSql = "SELECT * FROM ACCOUNTS WHERE login = '" + inputLevel2 + "'";
            _context.Accounts
                .FromSqlRaw(concatSql);
        }

        private void DoOtherStuff3Level1(string otherStuffLevel1)
        {
            string storingOtherStuff = "stored: " + otherStuffLevel1;
        }
        #endregion

        #region Flow 4
        public IActionResult EfCoreFlow4(string input, string otherparam)
        {
            var localCondition = true;

            if (localCondition)
            {
                DoStuff4Level1(input);
            }            

            return new OkResult();
        }

        private void DoStuff4Level1(string inputLevel1)
        {
            var toLog = "logged: " + inputLevel1;
            DoStuff4Level2(inputLevel1);
        }

        private void DoStuff4Level2(string inputLevel2)
        {
            if (inputLevel2.Length == 0)
            {
                return;
            }
            var concatSql = "SELECT * FROM ACCOUNTS WHERE login = '" + inputLevel2 + "'";
            _context.Accounts
                .FromSqlRaw(concatSql);
        }
        #endregion


        #region Flow 10
        // UNREACHABLE
        public IActionResult EfCoreFlow10(string input, string otherparam)
        {
            return new OkResult();

            DoStuff10Level1(input);
        }

        private void DoStuff10Level1(string inputLevel1)
        {
            var toLog = "logged: " + inputLevel1;
            DoStuff10Level2(inputLevel1);
        }

        private void DoStuff10Level2(string inputLevel2)
        {
            if (inputLevel2.Length == 0)
            {
                return;
            }
            var concatSql = "SELECT * FROM ACCOUNTS WHERE login = '" + inputLevel2 + "'";
            _context.Accounts
                .FromSqlRaw(concatSql);
        }
        #endregion

        #region Flow 11
        // UNREACHABLE
        public IActionResult EfCoreFlow11(string input, string otherparam)
        {
            var localCondition = false;

            if (localCondition)
            {
                DoStuff11Level1(input);
            }

            return new OkResult();
        }

        private void DoStuff11Level1(string inputLevel1)
        {
            var toLog = "logged: " + inputLevel1;
            DoStuff11Level2(inputLevel1);
        }

        private void DoStuff11Level2(string inputLevel2)
        {
            if (inputLevel2.Length == 0)
            {
                return;
            }
            var concatSql = "SELECT * FROM ACCOUNTS WHERE login = '" + inputLevel2 + "'";
            _context.Accounts
                .FromSqlRaw(concatSql);
        }
        #endregion

        #region Flow 12
        // HARMLESS (reachable with null or empty input
        public IActionResult EfCoreFlow12(string input, string otherparam)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return new OkResult();
            }

            DoStuff12Level1(input);

            return new OkResult();
        }

        private void DoStuff12Level1(string inputLevel1)
        {
            var toLog = "logged: " + inputLevel1;
            DoStuff12Level2(inputLevel1);
        }

        private void DoStuff12Level2(string inputLevel2)
        {
            if (inputLevel2.Length == 0)
            {
                return;
            }
            var concatSql = "SELECT * FROM ACCOUNTS WHERE login = '" + inputLevel2 + "'";
            _context.Accounts
                .FromSqlRaw(concatSql);
        }

        #endregion
    }
}
