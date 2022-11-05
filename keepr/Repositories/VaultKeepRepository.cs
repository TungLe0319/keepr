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


  internal List<KeptKeep> GetKeepsByVaultId(int vaultId)
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
    return _db.Query<KeptKeep, Profile, KeptKeep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { vaultId }).ToList();
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