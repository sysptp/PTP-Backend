using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations.Schema;


public class CuentasPorCobrar : BaseModel
{
    public int FacturacionId { get; set; }

    [ForeignKey("FacturacionId")]
    public virtual Facturacion? Facturacion { get; set; }

    public int ClienteId { get; set; }

    public decimal MontoTotal { get; set; }

    public decimal MontoInicial { get; set; }

    public decimal MontoPendiente { get; set; }

    public DateTime FechaUltimoPago { get; set; }

    public DateTime FechaLimite { get; set; }

    [NotMapped]
    public ICollection<DetalleCuentasPorCobrar>? DetalleCuentasPorCobrar { get; set; }

    public bool IsPaid { get; set; }
}

