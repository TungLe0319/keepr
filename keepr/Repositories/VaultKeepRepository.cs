namespace keepr.Repositories;

public class VaultKeepRepository : BaseRepo
{

  public VaultKeepRepository(IDbConnection db) : base(db)
  {
  }

  public VaultKeep CreateVaultKeep(VaultKeep vaultKeep)
  {
    var sql = @"
            INSERT INTO
            vaultKeeps(
            creatorId,
            vaultId,
            keepId
             )
            VALUES (
            @CreatorId,
            @VaultId,
            @KeepId
             );
             SELECT LAST_INSERT_ID()
                 ; ";

    vaultKeep.Id = _db.ExecuteScalar<int>(sql, vaultKeep);
    // vaultKeep.CreatedAt = new DateTime();
    // vaultKeep.UpdatedAt  = new DateTime(); TO AVOID THIS


    return GetById(vaultKeep.Id);
  }
  //when doing anything, return getbyId


  internal List<VaultedKeep> GetKeepsByVaultId(int vaultId)
  {
    string sql = @"
              SELECT
              vKeep.*,
              vKeep.id AS VaultKeepId,
              
              k.*,
              a.*
              FROM vaultKeeps vKeep
              JOIN accounts a ON a.id = vKeep.creatorId
              JOIN keeps k ON k.id =  vKeep.keepId
              
              WHERE vKeep.vaultId = @vaultId
              GROUP BY vKeep.id
                   ;";
    return _db.Query<VaultedKeep, Profile, VaultedKeep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { vaultId }).ToList();
  }

  internal List<Vault> GetVaultsByProfileId(string profileId)
  {
    string sql = @"
                SELECT 
                v.*,
                a.*
                FROM vaults v
                JOIN accounts a ON a.id = v.creatorId
                WHERE v.creatorId = @profileId
                AND v.isPrivate = false
                GROUP BY v.id
                     ;";
    return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { profileId }).ToList();
  }

  internal bool DeleteVaultKeep(int id)
  {
    return _db.Execute("DELETE FROM vaultKeeps WHERE id =  @id", new { id }) == 1;
  }

  internal List<VaultKeep> GetAllVaultKeeps()
  {
     string sql = @"
                 SELECT
                 *
                 FROM vaultKeeps
                      ;";
    return _db.Query<VaultKeep>(sql).ToList();
  }

  internal VaultKeep GetById(int vaultKeepId)
  {
    string sql = @"
                 SELECT *
                 FROM vaultKeeps
                 WHERE id = @vaultKeepId
                      ;";
    return _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();

  }
}