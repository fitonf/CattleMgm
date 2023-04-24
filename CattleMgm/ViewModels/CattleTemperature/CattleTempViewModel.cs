﻿using CattleMgm.ViewModels.Menu;
using System.ComponentModel.DataAnnotations;

namespace CattleMgm.ViewModels.CattleTemperature
{
    public class CattleTempViewModel
    {
        public int Id { get; set; }

        public int CattleId { get; set; }

        public string CattleName { get; set; }

        public double Temperature { get; set; }

        public DateTime DateMeasured { get; set; }

        public string CreatedBy { get;set; }
    }

    public class CattleTempCreateViewModel
    {
        //[Required(ErrorMessage = "Ju lutem zgjedhni gjedhen")]
        //[Display(Name = "Gjedhja")]
        //public int Id { get; set; }


        [Display(Name = "Gjedhja")]
        public int CattleId { get;set; }

        [Display(Name = "Temperatura")]
        public double Temperature { get; set; }

        [Display(Name = "Data")]
        public int DateMeasured { get; set; }


        [Display(Name = "Krijuar nga")]
        public int CreatedBy { get; set; }



    }
    //Ndryshimi fundit
    public class CattleTempEditViewModel : CattleTempCreateViewModel
    {
        public int Id { get; set; }
       
    }


}