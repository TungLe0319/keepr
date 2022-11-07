namespace keepr.Models;
public class Keep : IHasCreator
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public int Views { get; set; } = 0;




   // --------VIRTUALS-------
  public int Kept { get; set; }
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}

