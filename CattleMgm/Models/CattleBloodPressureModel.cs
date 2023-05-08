using CattleMgm.Helpers;
using Microsoft.VisualBasic;

namespace CattleMgm.Models
{
    public class CattleBloodPressureModel
    {
         
        public int Id { get; set; }
        public string Name { get; set; }
        public string Weight { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public int BloodPressure { get; set; }
    }
}

