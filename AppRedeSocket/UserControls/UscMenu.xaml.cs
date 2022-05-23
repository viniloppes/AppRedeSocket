using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppRedeSocket.UserControls
{
    /// <summary>
    /// Interaction logic for UscMenu.xaml
    /// </summary>
    public partial class UscMenu : UserControl
    {

        public delegate void OnEvento();
        public event OnEvento OnCliente;
        public event OnEvento OnServidor;
        public UscMenu()
        {
            InitializeComponent();
        }

        public void Inicializa()
        {

        }
        public void Finaliza()
        {

        }

        private void brdServidor_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (OnServidor != null)
                {
                    OnServidor();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void brdCliente_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if(OnCliente != null )
                {
                    OnCliente();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
