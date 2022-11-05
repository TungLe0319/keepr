namespace keepr.Repositories;
public class VaultsRepository : BaseRepo
{


  public VaultsRepository(IDbConnection db) : base(db)
  {
  }

  internal List<Vault> GetAllVaults()
  {
    var sql = @"
           SELECT 
           v. *,
           a.*
           FROM vaults v
           JOIN accounts a ON a.id = v.creatorId
           GROUP BY v.id
        
                ; ";
    return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
     {
       vault.Creator = profile;

       return vault;
     }).ToList();

  }

  public Vault GetById(int vaultId)
  {
    var sql = @"
          SELECT 
            v.*,
            a.* 
          FROM vaults v
          JOIN accounts a ON a.id = v.creatorId
          WHERE v.id =  @vaultId
          ;";

    return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
    {
      v.Creator = p;
      return v;
    }, new { vaultId }).FirstOrDefault();


  }

  internal List<Vault> GetMyVaults(string creatorId)
  {
    var sql = @"
           SELECT 
           v. *,
           COUNT(k.id) AS KeepCount,
           a.*
           FROM vaults v
           JOIN accounts a ON a.id = v.creatorId
           JOIN keeps k ON k.vaultId = v.id   
           GROUP BY v.id  
                ; ";
    return _db.Query<Vault, Profile, Vault>(sql, (myVault, profile) =>
     {
       myVault.Creator = profile;
 
       return myVault;
     }).ToList();

  }

  internal Vault CreateVault(Vault data)
  {
    var sql = @"
            INSERT INTO
            vaults (
              name,
              description,
              img,
              isPrivate,
              creatorId
            )
            VALUES (
              @Name,
              @Description,
              @Img,
              @IsPrivate,
              @CreatorId
            );
            SELECT LAST_INSERT_ID()
                ; ";

    data.Id = _db.ExecuteScalar<int>(sql, data);

    return GetById(data.Id);
  }

  internal Vault EditVault(Vault original)
  {
    string sql = @"
              UPDATE vaults SET
              name = @Name,
              img = @Img,
              isPrivate = @IsPrivate
              WHERE id = @Id 
                   ;";

    original.UpdatedAt = DateTime.Now;
    var rows = _db.Execute(sql, original);
    if (rows != 1)
    {
      throw new Exception("Unable to update");
    }

    return GetById(original.Id);
  }

  internal void DeleteVault(int vaultId)
  {
    _db.Execute("DELETE FROM vaults WHERE id = @vaultId", new { vaultId });
  }
}