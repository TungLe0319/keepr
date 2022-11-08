namespace keepr.Repositories;

public class AccountsRepository
{
  private readonly IDbConnection _db;

  public AccountsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Account GetByEmail(string userEmail)
  {
    string sql = "SELECT * FROM accounts WHERE email = @userEmail";
    return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
  }

  public Account GetById(string id)
  {
    string sql = "SELECT * FROM accounts WHERE id = @id";
    return _db.QueryFirstOrDefault<Account>(sql, new { id });
  }



  internal Account Create(Account newAccount)
  {
    string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
    _db.Execute(sql, newAccount);
    return newAccount;
  }

  internal Account Edit(Account update)
  {
    string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture,
              coverImg = @CoverImg,
              theme = @Theme
            WHERE id = @Id;";
    _db.Execute(sql, update);
    return update;
  }

  internal List<Vault> GetMyVaults(string accountId)
  {

    var sql = @"
           SELECT 
           v. *,
           a.*
           FROM vaults v
           JOIN accounts a ON a.id = v.creatorId   
           WHERE v.creatorId = @accountId
           GROUP BY v.id  
                ; ";
    return _db.Query<Vault, Profile, Vault>(sql, (myVaults, profile) =>
     {
       myVaults.Creator = profile;

       return myVaults;
     }, new {accountId}).ToList();



  }

  internal List<Keep> GetMyKeeps(string accountId)
  {
    var sql = @"
           SELECT 
           k.*,
           a.*
           FROM keeps k
           JOIN accounts a ON a.id = k.creatorId   
           WHERE k.creatorId = @accountId
           GROUP BY k.id  
           ORDER BY k.createdAt ASC
                ; ";
    return _db.Query<Keep, Profile, Keep>(sql, (myKeeps, profile) =>
     {
       myKeeps.Creator = profile;

       return myKeeps;
     }, new { accountId }).ToList();

  }
}

