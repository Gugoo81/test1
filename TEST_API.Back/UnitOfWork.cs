using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEST_API.Back;
using TEST_API_Database.Database;

namespace TEST_API_Database.Back
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Test_API_DbContext _context;
        private readonly ILogger _logger;

        public IPersonneRepository Personnes { get; private set; }

        public UnitOfWork(
            Test_API_DbContext context,
            ILoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Personnes = new PersonneRepository(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
