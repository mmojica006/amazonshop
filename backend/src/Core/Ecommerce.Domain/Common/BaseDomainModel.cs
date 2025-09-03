//clase padre de todas las entidades de mi proyecto, excepto las clases que tengan que ver con el 
//modelo de seguridad
namespace Ecommerce.Domain.Common;
//Al ser una clase padre tiene que ser una clase abstracta
public abstract class BaseDomainModel
{    public int Id { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy {get;set;}

    public DateTime? LastModifiedDate {get;set;}

    public string? LastModifiedBy { get; set; }
    
}