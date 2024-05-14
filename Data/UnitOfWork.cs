using UnitOfWork.Data.Interfaces;
using UnitOfWork.Data.Interfaces.Security;
using UnitOfWork.Data.Repositories.Security;

namespace UnitOfWork.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger _logger;

    #region Security
    private IUserRepository _user;
    private IProfileRepository _profile;
    #endregion

    public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("logs");
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
         _context.Dispose();
    }

    #region Implementation :: Security
    
    public IUserRepository User
    {
        get
        {
            _user ??= new UserRepository(_context, _logger);
            return _user;
        }
    }

   public IProfileRepository Profile
    {
        get
        {
            _profile ??= new ProfileRepository(_context, _logger);
            return _profile;
        }
    }

    #endregion
}