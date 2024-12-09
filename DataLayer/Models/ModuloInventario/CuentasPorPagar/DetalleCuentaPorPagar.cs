using DataLayer.Models.Otros;

public class DetalleCuentaPorPagar : BaseModel
{
    public int IdMovAlmacen { get; set; }


    public decimal Monto { get; set; }

    public DateTime FechaPago { get; set; }

    public int IdUsuario { get; set; }

    public bool IsCanceled { get; set; }
}

