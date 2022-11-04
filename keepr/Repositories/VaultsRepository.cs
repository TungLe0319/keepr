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

  internal Vault GetById(int vaultId)
  {
    var sql = @"
          SELECT 
            v.*,
            COUNT(k.id) AS KeepCount,
            a.* 
          FROM vaults v
          JOIN accounts a ON a.id = v.creatorId
          LETT JOIN keeps k ON k.vaultId = v.id
          WHERE v.id =  @vaultId 
          GROUP BY v.id
          ;";

    return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
    {
      v.Creator = p;
      return v;
    }, new { vaultId }).FirstOrDefault();


  }



  internal Vault CreateVault(Vault data)
  {
    var sql = @"
            INSERT INTO
            vaults (
              name,
              description,
              coverImg
              isPrivate,
              creatorId
            )
            VALUES (
              @Name,
              @Description,
              @CoverImg,
              @IsPrivate,
              @CreatorId
            );
            SELECT LAST_INSERT_ID()
                ; ";

    data.Id = _db.ExecuteScalar<int>(sql, data);

    return data;
  }

  internal Vault EditVault(Vault original)
  {
    string sql = @"
              UPDATE vaults SET
              name = @Name,
              description = @Description,
              coverImg = @CoverImg,
              isPrivate = @IsPrivate
              WHERE id = @Id LIMIT 1
                   ;";

    original.UpdatedAt = DateTime.Now;
    var rows = _db.Execute(sql, original);
    if (rows != 1)
    {
      throw new Exception("Unable to update");
    }

    return original;
  }

  internal void DeleteVault(int vaultId)
  {
    _db.Execute("DELETE FROM vaults WHERE id = @vaultId", new { vaultId });
  }
}