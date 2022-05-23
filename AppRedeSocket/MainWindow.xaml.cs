using AppRedeSocket.CLASSES;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        Thread thread;
        public int toutSenhaServidor = 0;
        public string txtSenhaServidor = "";


        public ENUM_TELA telaAtual = ENUM_TELA.USC_NENHUM;
        public int toutEnviaRecebimento;

        public enum ENUM_TELA
        {
            USC_NENHUM,
            USC_INICIO,
            USC_SERVIDOR,
            USC_MENU
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                uscServidor.OnVoltar += UscServidor_OnVoltar;

                DadosGerais.OnEnviaMensagem += DadosGerais_OnEnviaMensagem;
                uscServidor.OnUsuario += UscServidor_OnUsuario;
                uscMenu.OnCliente += UscMenu_OnCliente;
                uscMenu.OnServidor += UscMenu_OnServidor;
                txtMensagemLog.Text = "";
                OcultaTodasTelas();
                ExibeTela(ENUM_TELA.USC_MENU);
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);

            }
        }

        private void UscMenu_OnServidor()
        {
            try
            {
                ExibeTela(ENUM_TELA.USC_SERVIDOR);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);

            }
        }

        private void UscMenu_OnCliente()
        {
            try
            {
                ExibeTela(ENUM_TELA.USC_INICIO);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);

            }
        }

        private void UscServidor_OnUsuario(string ip, string porta)
        {
            try
            {

                string json = @"{
  
    'ip': '" + ip + @"',
   'porta': '" + porta + @"',
  
}";

                JObject rss = JObject.Parse(json);
                ExibeTela(ENUM_TELA.USC_INICIO, rss);
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
            uscMenu.Visibility = Visibility.Hidden;
        }
        public void OcultaTela(ENUM_TELA tela, object obj = null)
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
                    case ENUM_TELA.USC_MENU:
                        OcultaTodasTelas();
                        uscMenu.Finaliza();
                        break;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error", ex.Message, MessageBoxButton.OK);
            }
        }

        public void ExibeTela(ENUM_TELA tela, JObject jObj = null)
        {
            try
            {
                switch (tela)
                {
                    case ENUM_TELA.USC_INICIO:
                        OcultaTela(telaAtual);
                        if (jObj != null)
                        {
                            uscInicio.Inicializa(jObj["ip"].ToObject<string>(), jObj["porta"].ToObject<string>());
                        }
                        else
                        {
                            uscInicio.Inicializa();

                        }
                        uscInicio.UpdateLayout();
                        uscInicio.Visibility = Visibility.Visible;
                        break;
                    case ENUM_TELA.USC_SERVIDOR:
                        OcultaTela(telaAtual);
                        uscServidor.Inicializa();
                        uscServidor.UpdateLayout();
                        uscServidor.Visibility = Visibility.Visible;
                        break;
                    case ENUM_TELA.USC_MENU:
                        OcultaTela(telaAtual);
                        uscMenu.Inicializa();
                        uscMenu.UpdateLayout();
                        uscMenu.Visibility = Visibility.Visible;
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
                    if (DadosGerais.toutEnviaRecebimentoServidor > 0)
                    {
                        if (--DadosGerais.toutEnviaRecebimentoServidor == 0)
                        {
                            DadosGerais.EnviaToutRecebimentoServidor();

                        }
                    }
                }
                catch (Exception ex)
                {


                }
                try
                {
                    if (DadosGerais.toutEnviaRecebimento > 0)
                    {
                        if (--DadosGerais.toutEnviaRecebimento == 0)
                        {
                            DadosGerais.EnviaToutRecebimento();

                        }
                    }
                }
                catch (Exception ex)
                {


                }
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

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                if(DadosGerais.serverSocketConnection != null)
                {
                    DadosGerais.serverSocketConnection.CloseAllSockets();


                }
            }
            catch (Exception ex)
            {

            }
        }
        private void DadosGerais_OnEnviaMensagem(string mensagem)
        {
            try
            {
                txtMensagemLog.Text = mensagem;
                Storyboard sb = FindResource("StbTxtMensagemLog") as Storyboard;
                sb.Pause();
                sb.BeginTime = new TimeSpan(0, 0, 0, 0);
                sb.Begin();
            }
            catch (Exception ex)
            {
           
                //MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);

            }
        }
        private void Storyboard_Completed(object sender, EventArgs e)
        {
            try
            {
                Storyboard sb = FindResource("StbTxtMensagemLog") as Storyboard;

                sb.Pause();
                sb.Stop();
                txtMensagemLog.Opacity = 0;

            }
            catch (Exception ex)
            {

            }
        }
    }
}
