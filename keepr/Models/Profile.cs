namespace keepr.Models;

public class Profile
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string CoverImg { get; set; }
  public string Picture { get; set; }
  public bool? Theme { get; set; } = false;
}
