using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ColegioTerciario.DAL.Models;

namespace ColegioTerciario.Models.ViewModels
{
    public class AnaliticoViewModel
    {
        public Persona Persona { get; set; }
        public SituacionFinalesViewModel Analitico { get; set; }
    }
}
