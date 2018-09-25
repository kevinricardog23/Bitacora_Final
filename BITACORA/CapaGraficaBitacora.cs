using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using logica;
using System.Data;

namespace BITACORA
{
    public class CapaGraficaBitacora
    {

        capaLogicaBitacora cpl = new capaLogicaBitacora();

        //ENVIAR ACCION A CAPA DE DATOS
        public void InsertBitacora(string accion)
        {
                //ENVIA A LA CAPA LOGICA
                cpl.insertBitacora(accion);
        }


        //OBTENER INFO BITACORA DE CAPA DE DATOS
        public DataTable MostrarBitacora()
        {
            //RETORNA VALORES DE CAPA LOGICA
            return cpl.mostrarBitacora();
        }

    }
}
