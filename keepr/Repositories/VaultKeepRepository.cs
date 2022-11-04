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

    return vaultKeep;
  }

}