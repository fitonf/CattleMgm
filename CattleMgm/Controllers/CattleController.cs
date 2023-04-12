using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Helpers;
using CattleMgm.Models;
using CattleMgm.Repository.Cattles;
using CattleMgm.ViewModels.Cattle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Controllers
{
    public class CattleController : BaseController
    {
        
        private ICattleRepository _cattleRepository;

        public CattleController(ApplicationDbContext context, praktikadbContext db, 
                                ICattleRepository cattleRepository) : base(context,db)
        {
            
            _cattleRepository = cattleRepository;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Lista e gjedheve";

            var lista = _cattleRepository.GetCattles();

            List<CattleViewModel> listaViewModel = new List<CattleViewModel>();
            
            foreach (var cattle in lista)
            {
                listaViewModel.Add(new CattleViewModel { 
                    Id = cattle.Id,
                    Name = cattle.Name,
                    Gender = cattle.Gender == (int)Gender.Male ? "Mashkull" : "Femer",
                    BirthDate = cattle.BirthDate.ToShortDateString(),
                    FarmName = cattle.Farm.Name,
                    FarmerName = cattle.Farm.Farmer.FirstName + 
                    " " + cattle.Farm.Farmer.LastName,
                    Breed = cattle.Breed.Name,
                    UniqueIdentifier = cattle.UniqueIdentifier.ToString(),
                    Weight = cattle.Weight
                });
            }

            listaViewModel = listaViewModel.OrderByDescending(q => q.Id).ToList();

            return View(listaViewModel);
        }

        public IActionResult Visari()
        {
            var lista = new List<int>();
            for(int i = 0; i<=10; i++)
            {
                lista.Add(i);  
            }

            return View("Index", lista);
        }
    }
}
