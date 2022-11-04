namespace keepr.Repositories;

public class KeepsRepository : BaseRepo
{
  public KeepsRepository(IDbConnection db) : base(db)
  {
  }

  internal Keep GetById(int id)
  {
    var sql = @"
        SELECT 
          k.*,
          a.* 
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        WHERE k.id =  @id 
        ;";

    return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
    {
      k.Creator = p;
      return k;
    }, new { id }).FirstOrDefault();


  }

  internal bool DeleteKeep(int keepId)
  {
    return _db.Execute("DELETE FROM keeps WHERE id = @keepId", new { keepId }) == 1;
  }
}