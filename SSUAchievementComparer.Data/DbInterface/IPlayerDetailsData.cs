using SSUAchievementComparer.Core;
using SSUAchievementComparer.Core.Entities.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSUAchievementComparer.Data
{
    public interface IPlayerDetailsData
    {
        IEnumerable<PlayerDetailsDb> GetPlayersByName(string name);
        PlayerDetailsDb GetPlayerById(int id);
        PlayerDetailsDb Update(PlayerDetailsDb updatedPlayer);
        PlayerDetailsDb Add(PlayerDetailsDb newPlayer);
        PlayerDetailsDb Delete(int id);
        PlayerDetailsDb GetPlayerByPlayerId(long playerId);
        int GetCountOfPlayers();
        int Commit();
    }
}
