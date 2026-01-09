namespace HDI.Domain.Common;

public interface IBaseEntity<TId>
{
   TId Id { get; set; }
}
