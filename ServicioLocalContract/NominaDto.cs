using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ServicioLocalContract
{
  
    
    [Serializable]
    [DataContract]
    public class NominaDto
    {
        public NominaDto()
        {
            this.Version = "1.2";
        }
        [DataMember]
        public Emisor emisor { get; set; }
        [DataMember]
        public Receptor receptor { get; set; }
        
        [DataMember]
        public Percepciones Percepciones { get; set; }
        [DataMember]
        public Deducciones Deducciones { get; set; }
        [DataMember]
        public List<OtroPago> otroPago { get; set; }
                
        [DataMember]
        public List<Incapacidad> incapacidades { get; set; }
        [DataMember]
        public string Version { get; set; }
        [DataMember]
        public string TipoNomina { get; set; }
        [DataMember]
        public string FechaPago { get; set; }
        [DataMember]
        public string FechaInicialPago { get; set; }
        [DataMember]
        public string FechaFinalPago { get; set; }
        [DataMember]
        public decimal NumDiasPagados { get; set; }
        [DataMember]
        public decimal TotalPercepciones { get; set; }
        [DataMember]
        public decimal TotalDeducciones { get; set; }
        [DataMember]
        public decimal TotalOtrosPagos { get; set; }
        
        
    }

    [Serializable]
    [DataContract]
    public class Emisor
    {
        [DataMember]
        public string OrigenRecurso { get; set; }
        [DataMember]
        public decimal MontoRecursoPropio { get; set; }

        [DataMember]
        public string Curp { get; set; }
        [DataMember]
        public string RegistroPatronal { get; set; }

        [DataMember]
        public string RfcPatronOrigen { get; set; }
    }
    [Serializable]
    [DataContract]
    public class Receptor
    {
        [DataMember]
        public string NumEmpleado { get; set; }
        [DataMember]
        public string Departamento { get; set; }

        [DataMember]
        public string Puesto { get; set; }
        [DataMember]
        public string RiesgoPuesto { get; set; }

        [DataMember]
        public string PeriodicidadPago { get; set; }

        [DataMember]
        public string Banco { get; set; }
        [DataMember]
        public string CuentaBancaria { get; set; }

        [DataMember]
        public decimal SalarioBaseCotApor { get; set; }
        [DataMember]
        public decimal SalarioDiarioIntegrado { get; set; }

        [DataMember]
        public string ClaveEntFed { get; set; }
        [DataMember]
        public string Curp { get; set; }
        [DataMember]
        public string NumSeguridadSocial { get; set; }

        [DataMember]
        public string FechaInicioRelLaboral { get; set; }
        [DataMember]
        public string Antigüedad { get; set; }

        [DataMember]
        public string TipoContrato { get; set; }

        [DataMember]
        public string Sindicalizado { get; set; }
        [DataMember]
        public string TipoJornada { get; set; }
        [DataMember]
        public string UsoCFDI { get; set; }

        [DataMember]
        public string TipoRegimen { get; set; }
       
        [DataMember]
        public List<Subcontratacion> subContratacion { get; set; }

    }

    [Serializable]
    [DataContract]
    public class Subcontratacion
    {
        [DataMember]
        public string RfcLabora { get; set; }
        [DataMember]
        public decimal PorcentajeTiempo { get; set; }

        
    }

    [Serializable]
    [DataContract]
    public class Percepciones
    {
        [DataMember]
        public decimal TotalSueldos { get; set; }
        [DataMember]
        public decimal TotalSeparacionIndemnizacion { get; set; }

        [DataMember]
        public decimal TotalJubilacionPensionRetiro { get; set; }
        [DataMember]
        public decimal TotalGravado { get; set; }

        [DataMember]
        public decimal TotalExento { get; set; }

        [DataMember]
        public decimal TotalUnaExhibicion { get; set; }
        [DataMember]
        public decimal TotalParcialidad { get; set; }

        [DataMember]
        public decimal MontoDiario { get; set; }
        [DataMember]
        public decimal IngresoAcumulable { get; set; }

        [DataMember]
        public decimal IngresoNoAcumulable { get; set; }
        [DataMember]
        public string Curp { get; set; }
        [DataMember]
        public decimal TotalPagado { get; set; }

        [DataMember]
        public int NumAñosServicio { get; set; }
        [DataMember]
        public decimal UltimoSueldoMensOrd { get; set; }

        [DataMember]
        public decimal SeparacionIndemnizacionIngresoAcumulable { get; set; }

        [DataMember]
        public decimal SeparacionIndemnizacionIngresoNoAcumulable { get; set; }

        [DataMember]
        public List<Percepcion> percepcion { get; set; }
    }
    [Serializable]
    [DataContract]
    public class Percepcion
    {
        [DataMember]
        public decimal? ValorMercado { get; set; }
        [DataMember]
        public decimal? PrecioAlOtorgarse { get; set; }
        [DataMember]
        public string TipoPercepcion { get; set; }
        [DataMember]
        public string Clave { get; set; }
        [DataMember]
        public string Concepto { get; set; }
        [DataMember]
        public decimal ImporteGravado { get; set; }
        [DataMember]
        public decimal ImporteExento { get; set; }
        [DataMember]
        public List<HorasExtra> horasExtra { get; set; }

    }
    [Serializable]
    [DataContract]
    public class HorasExtra
    {
        [DataMember]
        public int Dias { get; set; }

        [DataMember]
        public string TipoHoras { get; set; }
        [DataMember]
        public int HoraExtra { get; set; }
        [DataMember]
        public decimal ImportePagado { get; set; }
        [DataMember]
        public string clave { get; set; }
        
    }

    [Serializable]
    [DataContract]
    public class Deducciones
    {
        [DataMember]
        public List<Deduccion> Deduccion { get; set; }
        [DataMember]
        public decimal TotalOtrasDeducciones { get; set; }
        [DataMember]
        public decimal TotalImpuestosRetenidos { get; set; }

        

    }
    [Serializable]
    [DataContract]
    public partial class Deduccion
    {
        [DataMember]
        public string TipoDeduccion { get; set; }

        [DataMember]
        public string Clave { get; set; }
        [DataMember]
        public string Concepto { get; set; }

        [DataMember]
        public decimal Importe { get; set; }

    }

    [Serializable]
    [DataContract]
    public class OtroPago
    {
        [DataMember]
        public decimal SubsidioCausado { get; set; }
        [DataMember]
        public decimal SaldoAFavor { get; set; }

        [DataMember]
        public short Año { get; set; }
        [DataMember]
        public decimal RemanenteSalFav { get; set; }

        [DataMember]
        public string TipoOtroPago { get; set; }
        [DataMember]
        public string Clave { get; set; }
        [DataMember]
        public string Concepto { get; set; }
        [DataMember]
        public decimal Importe { get; set; }

    }
    [Serializable]
    [DataContract]
    public class Incapacidad
    {
        [DataMember]
        public int DiasIncapacidad { get; set; }
        [DataMember]
        public string TipoIncapacidad { get; set; }

        [DataMember]
        public decimal ImporteMonetario { get; set; }

    }
}
