﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SSUAchievementComparer.Core;
using SSUAchievementComparer.Core.Entities.DB;
using SSUAchievementComparer.Core.Shared;
using System.Collections.Generic;
using System.Linq;

namespace SSUAchievementComparer.Data
{
    public class SqlGameDetailsData : IGameDetailsData
    {
        private readonly SSUAchievementComparerDbContext db;

        public SqlGameDetailsData(SSUAchievementComparerDbContext db)
        {
            this.db = db;
        }

        public GameDetailsDb Add(GameDetailsDb newGame)
        {
            db.Add(newGame);
            return newGame;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public GameDetailsDb Delete(int id)
        {
            var game = GetGameById(id);

            if (game != null)
            {
                db.GameDetailsDb.Remove(game);
            }

            return game;
        }

        public void FetchAllGames()
        {
            db.GameDetailsDb.RemoveRange(GetGamesByName(null));
            Commit();

            //var link = "https://api.steampowered.com/ISteamApps/GetAppList/v2/";
            var link = "https://api.steampowered.com/IStoreService/GetAppList/v1/?key=67A8BA5F6A8E24721002FFA67FEFFFA5&if_modified_since=01%2F01%2F2015&max_results=50000/";

            string content = HtmlCommon.GetPageContent(link);

            JObject obj = JObject.Parse(content);
            var res = obj["response"]["apps"].ToList();

            // Gets all the elements from the link
            List<GameDetailsDb> games = new List<GameDetailsDb>();
            foreach (var result in res)
            {
                GameDetailsDb newGame = result.ToObject<GameDetailsDb>();
                newGame.name = newGame.name.Trim();

                if (!string.IsNullOrEmpty(newGame.name))
                {
                    games.Add(newGame);
                }
            }

            // Delete dupplicates before adding them to the database
            games = games.GroupBy(g => g.appid).Select(g => g.First()).OrderBy(g => g.name).ToList();

            foreach (var game in games)
            {
                Add(game);
            }

            Commit();
        }

        public int GetCountOfGames()
        {
            return db.GameDetailsDb.Count();
        }

        public GameDetailsDb GetGameById(int id)
        {
            return db.GameDetailsDb.Find(id);
        }

        public IEnumerable<GameDetailsDb> GetGamesByName(string name)
        {
            var query = db.GameDetailsDb.AsEnumerable();

            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToLowerInvariant();
                query = query.Where(g => g.name.ToLowerInvariant().StartsWith(name));
            }

            var result = query.OrderBy(g => g.name)
                .Select(g => new GameDetailsDb
                {
                    appid = g.appid,
                    name = g.name
                });

            return result;
        }

        public GameDetailsDb Update(GameDetailsDb updatedGame)
        {
            var entity = db.GameDetailsDb.Attach(updatedGame);
            entity.State = EntityState.Modified;

            return updatedGame;
        }
    }
}
