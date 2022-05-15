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
    /// Interaction logic for UscServidor.xaml
    /// </summary>
    public partial class UscServidor : UserControl
    {
        public UscServidor()
        {
            InitializeComponent();
        }

        public delegate void OnEvento();
        public event OnEvento OnVoltar;

  
        public void Inicializa()
        {
            try
            {

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error", ex.Message, MessageBoxButton.OK);
            }
        }

        public void Finaliza()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButton.OK);
            }

        }

        private void imgVoltar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (OnVoltar != null)
                {
                    OnVoltar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButton.OK);
            }
        }
    }
}
