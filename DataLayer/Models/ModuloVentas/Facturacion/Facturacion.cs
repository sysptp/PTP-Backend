using DataLayer.Models.Clients;
using DataLayer.Models.ModuloVentas.Caja;
using DataLayer.Models.Otros;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


    public class Facturacion
    {
        public Facturacion()
        {
            FechaCreacion = DateTime.Now;
            Borrado = false;
        }


        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Borrado { get; set; }

        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Client Cliente { get; set; }

        public string NoFactura { get; set; }

        public string Ncf { get; set; }

        public int SecuenciaId { get; set; }

        [ForeignKey("SecuenciaId")]
        public virtual DgiiNcfSecuencia DgiiNcfSecuencia { get; set; }

        public string Comentario { get; set; }

        [Required]
        public int TipoTransaccionId { get; set; }

        [ForeignKey("TipoTransaccionId")]
        public virtual TipoTransaccion TipoTransaccion { get; set; }

        public int TipoPagoId { get; set; }

        [ForeignKey("TipoPagoId")]
        public virtual TipoPago TipoPago { get; set; }

        [Required]
        public decimal MontoTotal { get; set; }

        public bool IsCanceled { get; set; }

        public bool IsReturned { get; set; }

        public string Justificacion { get; set; }

        public decimal TotalDescuento { get; set; }

        public decimal TotalItbis { get; set; }

        public int CantidadProductos { get; set; }

        public long IdEmpresa { get; set; }

        public int CodigoUsuario { get; set; }

        public decimal ValorEfectivo { get; set; }

        public decimal ValorTarjeta { get; set; }

        public string VTnoAuth { get; set; }

        public int VT4digit { get; set; }

        public string TipoTarjeta { get; set; }

        public string EstaDespachada { get; set; }

        public int CantidadImpresion { get; set; }

        public decimal Devuelta { get; set; }
    }

