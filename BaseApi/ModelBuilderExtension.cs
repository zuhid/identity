using Microsoft.EntityFrameworkCore;
using Zuhid.Tools;

namespace Zuhid.BaseApi;

/// <summary>
/// Extension methods for ModelBuilder
/// </summary>
public static class ModelBuilderExtension {

  /// <summary>
  /// Converts all table, column, keys, and indexes names in the model to snake_case.
  /// </summary>
  public static ModelBuilder ToSnakeCase(this ModelBuilder builder) {
    foreach (var entity in builder.Model.GetEntityTypes()) {
      entity.SetTableName(entity.GetTableName()!.ToSnakeCase());
      entity.GetProperties().ToList().ForEach(property => property.SetColumnName(property.Name!.ToSnakeCase()));
      entity.GetKeys().ToList().ForEach(key => key.SetName(key.GetName()?.ToLower()));
      entity.GetForeignKeys().ToList().ForEach(fk => fk.SetConstraintName(fk.GetConstraintName()?.ToLower()));
      entity.GetIndexes().ToList().ForEach(index => index.SetDatabaseName(index.GetDatabaseName()?.ToLower()));
    }
    return builder;
  }
}
