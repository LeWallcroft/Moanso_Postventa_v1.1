using CapaDominio;
using CapaPresentacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Mannucci_Motors.frmLogin;

namespace Mannucci_Motors
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]

        /*static void Main()
        {
           
            Application.EnableVisualStyles();            
            Application.SetCompatibleTextRenderingDefault(false);

            Sesion.UsuarioActual = new Usuario
            {
                Email = "admin@mannucci.com",
                Nombre = "Administrador",
                Apellido = "Sistema",
                Rol = "administrador"
            };
            Application.Run(new frmMenu());
        }*/
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
