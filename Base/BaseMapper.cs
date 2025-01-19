using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace Zuhid.Base
{
    public abstract class BaseMapper<TModel, TEntity> : IMapper<TModel, TEntity>
       where TModel : BaseModel
       where TEntity : BaseEntity
    {
        public abstract TEntity GetEntity(TModel model);

        public abstract TModel GetModel(TEntity entity);

        public List<TEntity> GetEntityList(List<TModel> modelList)
        {
            var entityList = new List<TEntity>();
            modelList.ForEach(model => entityList.Add(GetEntity(model)));
            return entityList;
        }

        public List<TModel> GetModelList(List<TEntity> entityList)
        {
            var modelList = new List<TModel>();
            entityList.ForEach(entity => modelList.Add(GetModel(entity)));
            return modelList;
        }


    }
}