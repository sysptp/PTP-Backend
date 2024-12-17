using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Models.ModuloHelpDesk
{
    public class HdkTickets : AuditableEntities

    {
        [Key]
        public int IdTicket { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? IdUsuarioAsignado { get; set; }
        public int idTicketPadre { get; set; }
        public string? ReferenciasTicketExterno { get; set; }
        public string? Solucion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaCierre { get; set; }
        public int IdTipoTicket { get; set; }
        [ForeignKey("IdTipoTicket")]
        public HdkTypeTicket? HdkTypeTicket { get; set; }
        public int IdDepartamentos { get; set; }
        [ForeignKey("IdDepartamentos")]
        public HdkDepartaments? HdkDepartaments { get; set; }
        public int IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]
        public HdkCategoryTicket? HdkCategoryTicket { get; set; }
        public int IdSubCategoria { get; set; }
        [ForeignKey("IdSubCategoria")]
        public HdkSubCategory? HdkSubCategory { get; set; }
        public int IdErrorCategoria { get; set; }
        [ForeignKey("IdErrorCategoria")]
        public HdkErrorSubCategory? HdkErrorSubCategory { get; set; }
        public int IdEstado { get; set; }
        [ForeignKey("IdEstado")]
        public HdkStatusTicket? HdkStatusTicket { get; set; }
        public int IdTipoSolucion { get; set; }
        [ForeignKey("IdTipoSolucion")]
        public HdkSolutionTicket? HdkSolutionTicket { get; set; }
        public long IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public GnEmpresa? GnEmpresa { get; set; }
        public int IdPrioridad { get; set; }
        [ForeignKey("IdPrioridad")]
        public HdkPrioridadTicket? HdkPrioridadTicket { get; set; }

    }
}
