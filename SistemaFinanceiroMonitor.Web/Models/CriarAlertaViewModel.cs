using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaFinanceiroMonitor.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaFinanceiroMonitor.Web.Models
{
    public class CriarAlertaViewModel
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Selecione o tipo de alerta")]
        [Display(Name = "Tipo de Alerta")]
        public TipoAlerta TipoAlerta { get; set; }

        [Display(Name = "Moeda")]
        public TipoMoeda? TipoMoeda { get; set; }

        [Display(Name = "Indicador")]
        public TipoIndicador? TipoIndicador { get; set; }

        [Display(Name = "Valor Gatilho")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser maior que zero")]
        public decimal? ValorGatilho { get; set; }

        [Display(Name = "Percentual Gatilho")]
        public decimal? PercentualGatilho { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [Display(Name = "Descrição")]
        [MaxLength(500, ErrorMessage = "Descrição não pode ter mais de 500 caracteres")]
        public string Descricao { get; set; }

        // SelectLists para dropdowns
        public SelectList TiposAlerta { get; set; }
        public SelectList TiposMoeda { get; set; }
        public SelectList TiposIndicador { get; set; }
    }
}
