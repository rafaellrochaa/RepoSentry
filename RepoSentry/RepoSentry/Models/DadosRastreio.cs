using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepoSentry.Models
{
    public class DadosRastreio
    {
        //public string Id { get; set; }
        //public string cpf { get; set; }
        [Display(Name = "Contratos")]
        public string contratos { get; set; }
        [Display(Name = "Data de abertura")]
        public string dataAbertura { get; set; }
        [Display(Name = "Código de Rastreio")]
        public string codRastreio { get; set; }
        [Display(Name = "Última situação")]
        public string ultimaSituacao { get; set; }
    }
}