using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime.CredentialManagement;
using app;
using models;

namespace persistence_nosql
{
    public class DynamoDbFootballRepository : IFootballRepository
    {
        public async Task<Player> GetPlayerById(string playerId)
        {
            var ddb = GetDdbClient();
            var context = new DynamoDBContext(ddb);
            var config = new DynamoDBOperationConfig {OverrideTableName = "wvufootball.Players"};
            var get = context.CreateBatchGet<Player>(config);
            get.AddKey(playerId);
            await get.ExecuteAsync();
            
            return get.Results.FirstOrDefault();
        }

        public static AmazonDynamoDBClient GetDdbClient()
        {
            CredentialProfile creds;
            if (!new SharedCredentialsFile().TryGetProfile("matt.miller", out creds))
            {
                throw new ApplicationException($"Couldn't load profile: matt.miller");
            }

            var ddb = new AmazonDynamoDBClient(creds.GetAWSCredentials(new SharedCredentialsFile()));
            return ddb;
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            var ddb = GetDdbClient();
            var context = new DynamoDBContext(ddb);
            var config = new DynamoDBOperationConfig {OverrideTableName = "wvufootball.Players"};
            var get = context.ScanAsync<Player>(new ScanCondition[]{}, config);
            
            var ret = await get.GetRemainingAsync();
            return ret;
        }

        public async Task<List<Player>> FindPlayersByPositionId(string positionId)
        {
            var ddb = GetDdbClient();
            var context = new DynamoDBContext(ddb);
            var config = new DynamoDBOperationConfig {OverrideTableName = "wvufootball.Players"};
            var result = context.ScanAsync<Player>(new []
            {
                new ScanCondition("Id", ScanOperator.Equal, positionId),
            }, config);

            return await result.GetRemainingAsync();
        }

        public async Task<List<Position>> GetPositions()
        {
            return (await GetAllPlayers()).Select(p => p.Position).ToList();
        }

        public async Task<Roster> GetRoster(int year)
        {
            var ddb = GetDdbClient();
            var context = new DynamoDBContext(ddb);
            var config = new DynamoDBOperationConfig {OverrideTableName = "wvufootball.Rosters"};
            var result = context.ScanAsync<Roster>(new []
            {
                new ScanCondition("Year", ScanOperator.Equal, year),
            }, config);

            return (await result.GetRemainingAsync()).FirstOrDefault();
        }
    }
}