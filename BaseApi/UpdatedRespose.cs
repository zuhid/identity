namespace Zuhid.BaseApi;

public class SaveRespose {
  public DateTime Updated { get; set; }
  public IEnumerable<KeyValuePair<string, string>> Errors { get; set; } = [];
}
