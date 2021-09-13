using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ServicioLocal.Business
{
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
    [XmlRoot(Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital", IsNullable = false)]
    public class TimbreFiscalDigital 
    {

        [XmlAttribute("schemaLocation", Namespace = XmlSchema.InstanceNamespace)]
        public string xsiSchemaLocation = "http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/TimbreFiscalDigital/TimbreFiscalDigital.xsd";

        private string versionField;
        private string uUIDField;
        private System.DateTime fechaTimbradoField;
        private string selloCFDField;
        private string noCertificadoSATField;
        private string selloSATField;

        [XmlIgnore]
        public string cadenaOriginal { get; set; }
        public TimbreFiscalDigital()
        {
            this.versionField = "1.0";
        }

        [XmlAttribute()]
        public string version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
               
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string UUID
        {
            get
            {
                return this.uUIDField;
            }
            set
            {
                this.uUIDField = value;
                
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public System.DateTime FechaTimbrado
        {
            get
            {
                return this.fechaTimbradoField;
            }
            set
            {
                this.fechaTimbradoField = value;
                
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string selloCFD
        {
            get
            {
                return this.selloCFDField;
            }
            set
            {
                this.selloCFDField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string noCertificadoSAT
        {
            get
            {
                return this.noCertificadoSATField;
            }
            set
            {
                this.noCertificadoSATField = value;

            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string selloSAT
        {
            get
            {
                return this.selloSATField;
            }
            set
            {
                this.selloSATField = value;
                
            }
        }

    }
}
