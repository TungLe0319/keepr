using System.ComponentModel.DataAnnotations;

namespace keepr.Models;

public class Tag : IDbItem<int>
{

  [Required]
  public string name { get; set; }
  [Required]
  public int keepId { get; set; }
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}