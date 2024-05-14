using UnitOfWork.Data.Interfaces.Security;
using UnitOfWork.Domain.Models.Security;

namespace UnitOfWork.Data.Repositories.Security;

public class ProfileRepository : GenericRepository<Profile>, IProfileRepository
{
    public ProfileRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }   
}
