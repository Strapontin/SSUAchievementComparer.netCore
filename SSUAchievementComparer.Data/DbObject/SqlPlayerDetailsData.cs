using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SSUAchievementComparer.Core;
using SSUAchievementComparer.Core.Entities.DB;
using SSUAchievementComparer.Core.Shared;
using System.Collections.Generic;
using System.Linq;

namespace SSUAchievementComparer.Data
{
    public class SqlPlayerDetailsData : IPlayerDetailsData
    {
        private readonly SSUAchievementComparerDbContext db;

        public SqlPlayerDetailsData(SSUAchievementComparerDbContext db)
        {
            this.db = db;
        }

        public PlayerDetailsDb Add(PlayerDetailsDb newPlayer)
        {
            db.Add(newPlayer);
            return newPlayer;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public PlayerDetailsDb Delete(int id)
        {
            var player = GetPlayerById(id);

            if (player != null)
            {
                db.PlayerDetailsDb.Remove(player);
            }

            return player;
        }

        public int GetCountOfPlayers()
        {
            return db.PlayerDetailsDb.Count();
        }

        public PlayerDetailsDb GetPlayerById(int id)
        {
            return db.PlayerDetailsDb.Find(id);
        }

        public PlayerDetailsDb GetPlayerByPlayerId(long playerId)
        {
            var query = from p in db.PlayerDetailsDb
                        where p.playerId == playerId
                        select p;

            return query.FirstOrDefault();
        }

        public IEnumerable<PlayerDetailsDb> GetPlayersByName(string name)
        {
            var query = from g in db.PlayerDetailsDb
                        where g.name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby g.name
                        select g;

            return query;
        }

        public PlayerDetailsDb Update(PlayerDetailsDb updatedPlayer)
        {
            var entity = db.PlayerDetailsDb.Attach(updatedPlayer);
            entity.State = EntityState.Modified;

            return updatedPlayer;
        }
    }
}
