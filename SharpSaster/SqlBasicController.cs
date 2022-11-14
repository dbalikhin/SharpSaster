using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpSaster.Data;

namespace SharpSaster
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlBasicController : ControllerBase
    {
        private readonly DataContext _context;

        public SqlBasicController(DataContext context)
        {
            _context= context; 
        }

        public IActionResult EfCoreInjectionSafe(string user)
        {
            // Raw Sql Same - one line
            // SAFE
            _context.Accounts
                .FromSqlRaw("SELECT * FROM ACCOUNTS WHERE login={0}", user);

            // ExecuteSqlRaw - one line
            // SAFE
            _context.Database.ExecuteSqlRaw("SELECT * FROM ACCOUNTS WHERE login={0}", user);

            // Raw SQL multi line
            // SAFE
            _context.Accounts
                .FromSqlRaw(
                    "SELECT * FROM ACCOUNTS WHERE login={0}",
                    user
                );

            // ExecuteSqlRaw - multi line
            // SAFE
            _context.Database.ExecuteSqlRaw(
                "SELECT * FROM ACCOUNTS WHERE login={0}",
                user
            );

            // Interpolated Sql / ExecuteSqlInterpolated with an interpolated sql str
            // SAFE
            _context.Accounts
                .FromSqlInterpolated($"SELECT * FROM ACCOUNTS WHERE login = '{user}'");
            // SAFE
            _context.Database.ExecuteSqlInterpolated($"SELECT * FROM ACCOUNTS WHERE login = '{user}'");

            // Sql Raw / ExecuteSqlRaw - Implicit Db Parameter
            // SAFE
            _context.Accounts
                .FromSqlRaw("SELECT * FROM ACCOUNTS WHERE login = {0}", user);
            // SAFE
            _context.Database.ExecuteSqlRaw("SELECT * FROM ACCOUNTS WHERE login = {0}", user);

            // Interpolated Sql - Implicit Db Parameter
            // SAFE
            _context.Accounts
                .FromSqlInterpolated($"SELECT * FROM ACCOUNTS WHERE login = {user}");
            // SAFE
            _context.Database.ExecuteSqlInterpolated($"SELECT * FROM ACCOUNTS WHERE login = {user}");

            return new OkResult();
        }

        public IActionResult EfCoreInjectionVulnerable1(string user)
        {
            var concatSql = "SELECT * FROM ACCOUNTS WHERE login = '" + user + "'";
            _context.Accounts
                .FromSqlRaw(concatSql);
            return new OkResult();
        }

        public IActionResult EfCoreInjectionVulnerable2(string user)
        {
             var concatSql = "SELECT * FROM ACCOUNTS WHERE login = '" + user + "'";
            _context.Database.ExecuteSqlRaw(concatSql); ;
            return new OkResult();
        }

        public IActionResult EfCoreInjectionVulnerable3(string user)
        {
            var formatStringSql = string.Format("SELECT * FROM ACCOUNTS WHERE login = '{0}'", user);
            _context.Accounts
                .FromSqlRaw(formatStringSql);
            return new OkResult();
        }

        public IActionResult EfCoreInjectionVulnerable4(string user)
        {
            var formatStringSql = string.Format("SELECT * FROM ACCOUNTS WHERE login = '{0}'", user);
            _context.Database.ExecuteSqlRaw(formatStringSql);
            return new OkResult();
        }

        public IActionResult EfCoreInjectionVulnerable5(string user)
        {
            var interpolatedStringSql = $"SELECT * FROM ACCOUNTS WHERE login = '{user}'";
            _context.Accounts
                .FromSqlRaw(interpolatedStringSql);
            return new OkResult();
        }

        public IActionResult EfCoreInjectionVulnerable6(string user)
        {
            var interpolatedStringSql = $"SELECT * FROM ACCOUNTS WHERE login = '{user}'";
            _context.Database.ExecuteSqlRaw(interpolatedStringSql);
            return new OkResult();
        }

        public IActionResult EfCoreInjectionVulnerable7(string user)
        {
            _context.Accounts
                .FromSqlRaw($"SELECT * FROM ACCOUNTS WHERE login = '{user}'");
            return new OkResult();
        }

        public IActionResult EfCoreInjectionVulnerable8(string user)
        {
            var interpolatedStringSql = $"SELECT * FROM ACCOUNTS WHERE login = '{user}'";

            _context.Database.ExecuteSqlRaw(interpolatedStringSql);
            return new OkResult();
        }

    }
}
