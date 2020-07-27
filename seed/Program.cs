using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.EntityFrameworkCore;
using models;
using persistence_nosql;
using persistence_relational;

namespace seed
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<WvuFootballDataContext>();
            options.UseSqlServer(
                "Data Source=localhost;Initial Catalog=db_design_patterns_sql_dev;User Id=sa;Password=somethingStr0ng#;");

            var dbContext = new WvuFootballDataContext(options.Options);
            var dbRepo = new SqlServerFootballRepository(dbContext);
            var dbPlayers = await dbRepo.GetAllPlayers();

            var client = DynamoDbFootballRepository.GetDdbClient();
            var context = new DynamoDBContext(client);    
            var batch = context.CreateBatchWrite<Player>(new DynamoDBOperationConfig
            {
                OverrideTableName = "wvufootball.Players"
            });

            foreach (var dbPlayer in dbPlayers)
            {
                var fullPlayer = await dbRepo.GetPlayerById(dbPlayer.Id);
                fullPlayer.PlayerRosters = null;
                fullPlayer.Rosters?.ForEach(r =>
                {
                    r.Players?.RemoveAll(p => p.Id == dbPlayer.Id);
                    r.PlayerRosters = null;
                });
                batch.AddPutItem(fullPlayer);
            }
            await batch.ExecuteAsync();
            
            
            var dbPositions = await dbRepo.GetPositions();
            var positionBatch = context.CreateBatchWrite<Position>(new DynamoDBOperationConfig
            {
                OverrideTableName = "wvufootball.Positions"
            });

            positionBatch.AddPutItems(dbPositions);
            await positionBatch.ExecuteAsync();

            
            var rosterBatch = context.CreateBatchWrite<Roster>(new DynamoDBOperationConfig
            {
                OverrideTableName = "wvufootball.Rosters"
            });
            for (var i = 2020; i > 1920; i--)
            {
                var roster = await dbRepo.GetRoster(i);
                if (roster == null)
                {
                    continue;
                }

                roster.PlayerRosters = null;
                roster.Players?.ForEach(p =>
                {
                    p.Rosters = null;
                    p.PlayerRosters = null;
                });
                rosterBatch.AddPutItem(roster);
            }
            
            await rosterBatch.ExecuteAsync();
        }
    }
}