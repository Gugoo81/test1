

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEST_API_Database.Database;
using TEST_API_Database.Generic;

namespace TEST_API.Back
{
    public class PersonneRepository : GenericRepository<Personne> , IPersonneRepository
    {
        public PersonneRepository(Test_API_DbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Personne>> All()
        {
            try
            {
                return await dbSet.OrderBy(t=>t.nom).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(PersonneRepository));
                return new List<Personne>();
            }
        }

        public override async Task<bool> Upsert(Personne entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.PersonneID == entity.PersonneID)
                                                    .FirstOrDefaultAsync();

                if (existingUser == null)
                    return await Add(entity);

                existingUser.prenom = entity.prenom;
                existingUser.nom = entity.nom;
                existingUser.dateDeNaissance = entity.dateDeNaissance;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(PersonneRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.PersonneID == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(PersonneRepository));
                return false;
            }
        }
    }
}
