using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpSaster.Data;

namespace SharpSaster
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlAdvancedController : ControllerBase
    {
        private readonly DataContext _context;

        public SqlAdvancedController(DataContext context)
        {
            _context= context; 
        }

        public IActionResult EfCoreInjectionSafeParse1(string user, string name)
        {
            // integer input
            int.TryParse(user, out int userId);
            var formatStringSql = string.Format("SELECT * FROM ACCOUNTS WHERE login = '{0}'", userId);
            
            _context.Accounts
                .FromSqlRaw(formatStringSql);
            return new OkResult();
        }

        public IActionResult EfCoreInjectionSafeParse2(string user, string name)
        {
            // double conversion
            int.TryParse(user, out int userId);
            var formatStringSql = string.Format("SELECT * FROM ACCOUNTS WHERE login = '{0}'", userId.ToString());

            _context.Accounts
                .FromSqlRaw(formatStringSql);
            return new OkResult();
        }

        public IActionResult EfCoreInjectionSafeCondition(string user, string name)
        {
            // input is a constant
            if (user != "Alice" || user != "Bob")
            {
                return new OkResult();
            }
            var formatStringSql = string.Format("SELECT * FROM ACCOUNTS WHERE login = '{0}'", user);

            _context.Accounts
                .FromSqlRaw(formatStringSql);
            return new OkResult();
        }

        public IActionResult EfCoreInjectionVulnerable1(string user, string name)
        {
            // All params are vulnerable
            var formatStringSql = string.Format("SELECT * FROM ACCOUNTS WHERE login = '{0}' AND name='{1}", user, name);

            _context.Accounts
                .FromSqlRaw(formatStringSql);
            return new OkResult();
        }

        public IActionResult EfCoreInjectionVulnerable2(string user, string name)
        {
            // One param is vulnerable
            int.TryParse(user, out int userId);
            var formatStringSql = string.Format("SELECT * FROM ACCOUNTS WHERE login = '{0}' AND name='{1}", userId, name);

            _context.Accounts
                .FromSqlRaw(formatStringSql);
            return new OkResult();
        }
    }
}
