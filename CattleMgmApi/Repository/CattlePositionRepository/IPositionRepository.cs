﻿using CattleMgmApi.Data.Entities;

namespace CattleMgmApi.Repository.CattlePositionRepository
{
    public interface IPositionRepository
    {
        Task SaveChanges();

        Task<CattlePosition?> GetPositionById(int id);

        Task<IEnumerable<CattlePosition>> GetAllPositions();

        Task CreatePosition(CattlePosition position);

        //void DeleteCattle(CattlePosition position);
    }
}
