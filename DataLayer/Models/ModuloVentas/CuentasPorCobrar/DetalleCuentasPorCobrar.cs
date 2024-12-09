using DataLayer.Models.Otros;

public class DetalleCuentasPorCobrar : BaseModel
{
    public int FacturacionId { get; set; }

    public decimal Monto { get; set; }

    public DateTime FechaPago { get; set; }

    public bool IsCalceled { get; set; }

    public int IdUsuario { get; set; }
}

