using SSUAchievementComparer.Core;
using SSUAchievementComparer.Core.Entities.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSUAchievementComparer.Data
{
    public interface IGameDetailsData
    {
        IEnumerable<GameDetailsDb> GetGamesByName(string name);
        GameDetailsDb GetGameById(int id);
        GameDetailsDb Update(GameDetailsDb updatedGame);
        GameDetailsDb Add(GameDetailsDb newGame);
        GameDetailsDb Delete(int id);
        int GetCountOfGames();
        int Commit();
        void FetchAllGames();
    }
}
