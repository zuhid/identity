namespace Zuhid.BaseApi;

public interface IEntity {
  Guid Id { get; set; }

  // Guid UpdatedById { get; set; }

  DateTime UpdatedDate { get; set; }
}
