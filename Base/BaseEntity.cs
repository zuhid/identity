using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Zuhid.Base;

public class BaseEntity
{
    public Guid Id { get; set; }

    [JsonIgnore]
    public Guid UpdatedById { get; set; }

    [ConcurrencyCheck]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public DateTime Updated { get; set; }
}
