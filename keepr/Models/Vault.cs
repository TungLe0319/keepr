using System.ComponentModel.DataAnnotations;

namespace keepr.Models;
public class Vault : IHasCreator
{
  public int Id { get; set; }

[Required]
  public string Name { get; set; }

[Required]
  public string Description { get; set; }

  [Required]
  public string CoverImg { get; set; }
[Required]
  public bool IsPrivate { get; set; } = false;


  // public string Tags {get;set; } = "";

  public int KeepCount { get; set; }

  public Profile Creator { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string CreatorId { get; set; }
}