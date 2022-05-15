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
using System.Windows.Threading;

namespace AppRedeSocket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public int toutSenhaServidor = 0;
        public string txtSenhaServidor = "";


        public ENUM_TELA telaAtual = ENUM_TELA.USC_NENHUM;


        public enum ENUM_TELA
        {
            USC_NENHUM,
            USC_INICIO,
            USC_SERVIDOR

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                uscServidor.OnVoltar += UscServidor_OnVoltar;


                OcultaTodasTelas();
                ExibeTela(ENUM_TELA.USC_INICIO);
                try
                {
                    dispatcherTimer.Tick += DispatcherTimer_Tick;
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
                    dispatcherTimer.Start();
                }
                catch (Exception ex)
                {

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButton.OK);
            }
        }

        private void UscServidor_OnVoltar()
        {
            try
            {
                ExibeTela(ENUM_TELA.USC_INICIO);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButton.OK);

            }
        }

        public void OcultaTodasTelas()
        {
            uscInicio.Visibility = Visibility.Hidden;
            uscServidor.Visibility = Visibility.Hidden;
        }
        public void OcultaTela(ENUM_TELA tela)
        {
            try
            {
                switch (tela)
                {
                    case ENUM_TELA.USC_INICIO:
                        OcultaTodasTelas();
                        uscInicio.Finaliza();
                        break;
                    case ENUM_TELA.USC_SERVIDOR:
                        OcultaTodasTelas();
                        uscServidor.Finaliza();
                        break;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error", ex.Message, MessageBoxButton.OK);
            }
        }

        public void ExibeTela(ENUM_TELA tela)
        {
            try
            {
                switch (tela)
                {
                    case ENUM_TELA.USC_INICIO:
                        OcultaTela(telaAtual);
                        uscInicio.Inicializa();
                        uscInicio.UpdateLayout();
                        uscInicio.Visibility = Visibility.Visible;
                        break;
                    case ENUM_TELA.USC_SERVIDOR:
                        OcultaTela(telaAtual);
                        uscServidor.Inicializa();
                        uscServidor.UpdateLayout();
                        uscServidor.Visibility = Visibility.Visible;
                        break;
                }

                telaAtual = tela;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error", ex.Message, MessageBoxButton.OK);
            }
        }

        private void BrdSenhaServidor_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                txtSenhaServidor += ((Border)sender).Tag.ToString();
                toutSenhaServidor = 2;


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error", ex.Message, MessageBoxButton.OK);
            }



        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (toutSenhaServidor > 0)
                    {

                        if (--toutSenhaServidor == 0)
                        {
                            if (txtSenhaServidor == "123321")
                            {
                                txtSenhaServidor = "";

                                ExibeTela(ENUM_TELA.USC_SERVIDOR);

                            }
                            else
                            {
                                txtSenhaServidor = "";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
