namespace Zuhid.Base;

public interface IMapper<TModel, TEntity>
{
  TEntity GetEntity(TModel model);
  TModel GetModel(TEntity model);
}

