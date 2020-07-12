using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using app;
using models;

namespace persistence_nosql
{
    public class DynamoDbFootballRepository : IFootballRepository
    {
        public async Task<Player> GetPlayerById(string playerId)
        {
            CredentialProfile creds;
            if (!new NetSDKCredentialsFile().TryGetProfile("matt.miller", out creds))
            {
                throw new ApplicationException($"Couldn't load profile: matt.miller");
            }
            var ddb = new AmazonDynamoDBClient(creds.GetAWSCredentials(new NetSDKCredentialsFile()));
            var item = await ddb.GetItemAsync(new GetItemRequest("wvufootball.players",
                new Dictionary<string, AttributeValue>
                {
                    {"id", new AttributeValue(playerId)}
                }));
            var player = new Player
            {
                Id = item.Item["id"].S,
                FirstName = item.Item["firstName"].S,
                LastName = item.Item["lastName"].S,
                Position = new Position
                {
                    Id = item.Item["position"].M["id"].S,
                    Abbreviation = item.Item["position"].M["abbreviation"].S,
                    Name = item.Item["position"].M["name"].S,
                    IsOffense = item.Item["position"].M["isOffense"].BOOL
                },
                Rosters = item.Item["rosters"].L.Select(m => new Roster
                {
                    Id = m.M["id"].S
                }).ToList()
            };
            
            // var rosters = await ddb.BatchGetItemAsync(new BatchGetItemRequest { RequestItems = new Dictionary<string, KeysAndAttributes>()
            // {
            //     {"wvufootball.rosters", new KeysAndAttributes{ Keys = player.Rosters.Select(r => new Dic) }}
            // }})
            return player;
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Player>> FindPlayersByPositionId(string positionId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Position>> GetPositions()
        {
            throw new NotImplementedException();
        }

        public async Task<Roster> GetRoster(int year)
        {
            throw new NotImplementedException();
        }
    }
}