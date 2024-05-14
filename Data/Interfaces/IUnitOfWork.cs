using UnitOfWork.Data.Interfaces.Security;

namespace UnitOfWork.Data.Interfaces;

public interface IUnitOfWork
{
    IUserRepository User { get; }
    IProfileRepository Profile { get; }

    Task CompleteAsync();
}