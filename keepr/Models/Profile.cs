namespace keepr.Models;

public class Profile
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string CoverImg { get; set; } = "https://www.thoughtco.com/thmb/EKnmgoAr_X4TrIpxBiPYu9lao9U=/2000x1333/filters:fill(auto,1)/GettyImages-468963673-5ad40ad2fa6bcc0036add08a.jpg";
  public string Picture { get; set; }
  public bool? Theme { get; set; } = false;
}
