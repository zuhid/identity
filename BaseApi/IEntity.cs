using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zuhid.BaseApi;

public interface IEntity
{
  Guid Id { get; set; }

  // Guid UpdatedById { get; set; }

  DateTime UpdatedDate { get; set; }
}
