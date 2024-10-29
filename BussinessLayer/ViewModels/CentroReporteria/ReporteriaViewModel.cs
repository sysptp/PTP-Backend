using BussinessLayer.DTOs.CentroReporteriaDTOs;
using System;
using System.Collections.Generic;

namespace BussinessLayer.ViewModels
{
    // CREADO POR MANUEL 3/10/2024 - MODELVIEW PARA EL MANTENIMIENTO DE REPORTES
    public class ReporteriaViewModel
    {
        public int? Id { get; set; }

        public string NombreReporte { get; set; }

        public string Estado { get; set; }

        public string DescripcionReporte { get; set; }

        public DateTime? FechaAdicion { get; set; }

        public string AdicionadoPor { get; set; }

        public int? NumQuery { get; set; }

        public bool EsPesado { get; set; }

        public bool EsSubquery { get; set; }

        public string QueryCommand { get; set; }

        public List<JsonVariables> Variables { get; set; } = new List<JsonVariables>();

    }
}
