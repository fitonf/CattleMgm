using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace CattleMgm.ViewModels.Breed
{
    public class BreedViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Type { get; set; }
    }
    public class BreedCreateViewModel
    {

        [Required]
        [Remote("IsNameAvailable", "Breed", ErrorMessage = "Nje kafshe me kete emer ekziston tashme")]
        public string Name { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Numri i tipit nuk duhet te jete negativ.")]
        public int? Type { get; set; }
    }
    public class BreedEditViewModel :BreedCreateViewModel
    {
        public int Id { get; set; }
    }

}
