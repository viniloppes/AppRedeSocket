using AppRedeSocket.CLASSES;
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
    /// Interaction logic for UscInicio.xaml
    /// </summary>
    public partial class UscInicio : UserControl
    {
        public UscInicio()
        {
            InitializeComponent();
        }

        public void Inicializa()
        {
            txtCampoChat.Text = "";
            txtChat.Text = "";
            txtIp.Text = "";
            txtPorta.Text = "";
            txtNome.Text = "";
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
        public void EnviaMensagem()
        {
            if (txtCampoChat.Text != "")
            {
                Run runNome = new Run("\r\n" + (txtNome.Text != "" ? txtNome.Text : "Usuario" + "1") + " - ");
                runNome.Foreground = Brushes.Azure;

                txtChat.Inlines.Add(runNome);

                //escreve a mensagem
                txtChat.Text += txtCampoChat.Text;

                txtCampoChat.Text = "";
                svChat.ScrollToEnd();

            }
        }
        private void imgBtnEnviar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                EnviaMensagem();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButton.OK);
            }
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        EnviaMensagem();

                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButton.OK);
            }
        }

        private void brdConectar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ServerSocketConnection.SetupServer(txtIp.Text, int.Parse(txtPorta.Text));
                ClientSocketConnection.ConnectToServer(txtIp.Text, int.Parse(txtPorta.Text));
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error", ex.Message, MessageBoxButton.OK);

            }
        }
    }
}
