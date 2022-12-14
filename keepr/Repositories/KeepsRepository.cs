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
          COUNT(vKeep.id) AS Kept,
          a.* 
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        LEFT JOIN vaultKeeps vKeep ON vKeep.keepId = k.id
        WHERE k.id =  @id 
        GROUP BY k.id
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

  internal Keep CreateKeep(Keep data)
  {
     var sql = @"
             INSERT INTO
             keeps (
              name,
              description,
              img,
              views,
              creatorId
              )
             VALUES (
              @Name,
              @Description,
              @Img,
              @Views,
              @CreatorId
             );
             SELECT LAST_INSERT_ID()
                 ; ";
   
       data.Id = _db.ExecuteScalar<int>(sql, data);
   
   return GetById(data.Id);
      //  return data;
  }

  internal List<Keep> GetKeepsForProfiles(string id)
  {
     var sql = @"
                 SELECT 
                 k.*,
                 COUNT(vKeep.id) AS Kept,
                 a.*
                 FROM keeps k
                 JOIN accounts a ON a.id = k.creatorId
                 LEFT JOIN vaultKeeps vKeep ON vKeep.keepId = k.Id
                 WHERE k.creatorId = @id
                 GROUP BY k.id
                      ;";
    return _db.Query<Keep,Profile,Keep>(sql,(keep,profile)=>{
      keep.Creator = profile;
      return keep;
    }, new {id}).ToList();
  }

  internal List<Keep> GetAllKeepsBySearch()
  {
    var sql = @"
           SELECT 
           k. *,
           COUNT(vKeep.id) AS Kept,
           a.*
           FROM keeps k
           JOIN accounts a ON a.id = k.creatorId
           LEFT JOIN vaultKeeps vKeep ON vKeep.keepId = k.id
           GROUP BY k.id
                ; ";
    return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
     {
       keep.Creator = profile;

       return keep;
     }).ToList();

  }

  internal List<Keep> GetAllKeeps(int offSet)
  {
    var sql = @"
           SELECT 
           k. *,
           COUNT(vKeep.id) AS Kept,
           a.*
           FROM keeps k
           JOIN accounts a ON a.id = k.creatorId
           LEFT JOIN vaultKeeps vKeep ON vKeep.keepId = k.id
           GROUP BY k.id
           ORDER BY k.createdAt DESC
           LIMIT 24
           OFFSET @offSet
                ; ";
     return _db.Query<Keep, Profile,Keep >(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        
        return keep;
      }, new {offSet}).ToList();
  
  }


  internal Keep EditKeep(Keep original)
  {
  string sql = @"
               UPDATE keeps SET
               
               name = @Name,
               img = @Img,
               description = @Description,
               views = @Views
               
               WHERE id = @Id LIMIT 1
                    ;";
     var rows = _db.Execute(sql, original);
     if (rows != 1)
     {
       throw new Exception("Unable to update" );
     }
 
     return GetById(original.Id);
  }
}