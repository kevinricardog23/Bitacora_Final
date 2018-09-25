using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using datos;
using System.Data;

namespace logica
{
    public class capaLogicaBitacora
    {
        capaDatosBitacora cdt = new capaDatosBitacora();

        //ENVIAR ACCION A CAPA DE DATOS
        public bool insertBitacora(string accion)
        {
            if (accion!="")
            {
                cdt.setBitacora(accion);
                return true;
            }

            return false;
        }


        //OBTENER INFO BITACORA DE CAPA DE DATOS
        public DataTable mostrarBitacora()
        {

            return cdt.showBitacora();
        }
        

    }
}
