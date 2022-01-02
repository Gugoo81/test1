using System.Threading.Tasks;
using TEST_API.Back;

namespace TEST_API_Database.Back
{
    public interface IUnitOfWork
    {
        IPersonneRepository Personnes { get; }

        Task CompleteAsync();
    }
}
