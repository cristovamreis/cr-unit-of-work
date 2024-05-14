using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWork.Domain.Models.Security;

[Table(name: "Profile")]
public class Profile
{
    public Guid Id { get; set;}  
    public string Name { get; set;}    
}
