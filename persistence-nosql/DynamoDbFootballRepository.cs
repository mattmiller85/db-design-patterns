using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
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
            var item = await ddb.GetItemAsync(new GetItemRequest("wvufootball.players",
                new Dictionary<string, AttributeValue>
                {
                    {"id", new AttributeValue(playerId)}
                }));
            var player = GetPlayerFromItem(item.Item);
            //
            // var rosters = await ddb.BatchGetItemAsync(new BatchGetItemRequest { RequestItems = new Dictionary<string, KeysAndAttributes>()
            // {
            //     {"wvufootball.rosters", new KeysAndAttributes{ Keys = player.Rosters.Select(r => new Dic) }}
            // }})
            return player;
        }

        private static Player GetPlayerFromItem(Dictionary<string, AttributeValue> item)
        {
            var player = new Player
            {
                Id = item["id"].S,
                FirstName = item["firstName"].S,
                LastName = item["lastName"].S,
                Position = new Position
                {
                    Id = item["position"].M["id"].S,
                    Abbreviation = item["position"].M["abbreviation"].S,
                    Name = item["position"].M["name"].S,
                    IsOffense = item["position"].M["isOffense"].BOOL
                }
            };
            return player;
        }

        private static AmazonDynamoDBClient GetDdbClient()
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
            var items = await ddb.ScanAsync(new ScanRequest("wvufootball.players"));
            return items.Items.Select(GetPlayerFromItem).ToList();
        }

        public async Task<List<Player>> FindPlayersByPositionId(string positionId)
        {
            var ddb = GetDdbClient();
            var items = await ddb.ScanAsync(new ScanRequest("wvufootball.players")
            {
                FilterExpression = "position.id = :pos_Id", 
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {":pos_Id", new AttributeValue {S = positionId}}
                }
            });
            return items.Items.Select(GetPlayerFromItem).ToList();
        }

        public async Task<List<Position>> GetPositions()
        {
            return (await GetAllPlayers()).Select(p => p.Position).ToList();
        }

        public async Task<Roster> GetRoster(int year)
        {
            throw new NotImplementedException();
        }
    }
}