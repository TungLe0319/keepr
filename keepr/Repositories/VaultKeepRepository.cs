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
            vaultkeeps(
            vaultId,
            keepId
             )
            VALUES (
            @VaultId,
            @KeepId
             );
             SELECT LAST_INSERT_ID()
                 ; ";

    vaultKeep.Id = _db.ExecuteScalar<int>(sql, vaultKeep);

    return vaultKeep;
  }


}