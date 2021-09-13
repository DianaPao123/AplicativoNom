using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicioLocalContract
{
    public partial class Pagos
    {
        public string Estatus
        {
            get
            {
                if (this.Status)
                    return "Aplicado";
                else return "Sin Aplicar";
            }
        }

        public string StatusCan { get { if (this.Cancelado) return "Cancelado"; else return "Activo"; } }

        public string TipoPago
        {
            get
            {
                if (!Tipo) return "Pago";
                else return "Anticipo";

            }
        }
    }
}
