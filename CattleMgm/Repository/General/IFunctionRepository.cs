﻿using CattleMgm.Helpers;
using CattleMgm.Models.Menu;

namespace CattleMgm.Repository.General
{
    public interface IFunctionRepository
    {
        Task<List<ListOfMenus>> GetListOfMenus(string Role, LanguageEnum lang);
    }
}
