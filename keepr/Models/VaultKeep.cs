namespace keepr.Models;

public class VaultKeep : IDbItem<int>
{
  public int Id { get ; set ; }

  public string AccountId {get;set;}
  
  public DateTime CreatedAt { get ; set ; }
  public DateTime UpdatedAt { get ; set ; }
}