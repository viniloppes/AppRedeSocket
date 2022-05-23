using AppRedeSocket.CLASSES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
        public delegate void OnEventoObj(string ip, string porta);

        public event OnEventoObj OnUsuario;


        public void Inicializa()
        {
            try
            {
                txtChat.Text = "";
                txtIp.Text = "";
                txtPorta.Text = "";
                txtUsuOnline.Text = "";
                brdDesconectar.Visibility = Visibility.Hidden;


                DadosGerais.OnRecebeRespostaServidor += DadosGerais_OnRecebeRespostaServidor;
                DadosGerais.OnToutRecebimentoServidor += DadosGerais_OnToutRecebimentoServidor;

                if (DadosGerais.serverSocketConnection != null)
                {
                    brdDesconectar.Visibility = Visibility.Visible;

                    int i = 0;
                    foreach(Socket skt in ServerSocketConnection.clientSockets)
                    {
                        txtUsuOnline.Text += "Usuario " + i + "\r\n";
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error",  MessageBoxButton.OK);
            }
        }

        private void DadosGerais_OnToutRecebimentoServidor()
        {
            try
            {
                int lenght = DadosGerais.listaMensagensServer.Count;
                EscreveMensagemChat(DadosGerais.listaMensagensServer[lenght - 1]);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Finaliza()
        {
            try
            {


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }

        }

        private void DadosGerais_OnRecebeRespostaServidor(string resposta)
        {
            try
            {
                DadosGerais.toutEnviaRecebimentoServidor = 1;
            }
            catch (Exception ex)
            {

            }
        }
        public void EscreveMensagemChat(string resposta)
        {
            try
            {
                JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                dynamic resultado = serializer.DeserializeObject(resposta);
                txtUsuOnline.Text = "";
                int i = 0;
                foreach (Socket skt in ServerSocketConnection.clientSockets)
                {
                    txtUsuOnline.Text += "Usuario " + (i + 1) + "\r\n";
                    i++;
                }

                TextBlock textBlock = new TextBlock();
                textBlock.Foreground = Brushes.White;
                textBlock.FontSize = 20;
                textBlock.TextWrapping = TextWrapping.Wrap;
                textBlock.Margin = new Thickness(10, 0, 10, 0);
                Run runNome = new Run("\r\n" + (resultado["usuario"] != "" ? resultado["usuario"] : "desconhecido") + " - ");
                runNome.Foreground = Brushes.Green;
                runNome.FontSize = 15;

                Run runMensagem = new Run(resposta);
                runMensagem.Foreground = Brushes.Azure;
                runMensagem.FontSize = 220;

                textBlock.Inlines.Add(runNome);
                textBlock.Inlines.Add(resultado["mensagem"]);
                //escreve a mensagem
                //textBlock.Text += mensagem;
                stpChat.Children.Add(textBlock);
                svChat.ScrollToEnd();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);

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
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

     



        private void brdConectar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {


                //Inicializa SERVIDOR
                if (DadosGerais.serverSocketConnection == null)
                {
                    DadosGerais.serverSocketConnection = new ServerSocketConnection(
                                     new Socket(
                                         AddressFamily.InterNetwork,
                                         SocketType.Stream, ProtocolType.Tcp)
                                     );
                    DadosGerais.serverSocketConnection.SetupServer(txtIp.Text, txtPorta.Text);
                    brdDesconectar.Visibility = Visibility.Visible;
                    DadosGerais.EnviaMensagem("Configuração do servidor concluída");

                }
                else
                {
                    DadosGerais.EnviaMensagem("Você já esta conectado!");
                }

                //ServerSocketConnection.SetupServer(txtIp.Text, txtPorta.Text);

            }
            catch (Exception ex)
            {
                //DadosGerais.serverSocketConnection.CloseAllSockets();
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);

            }
        }

        private void brdDesconectar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {

                if(DadosGerais.serverSocketConnection != null)
                {
                    DadosGerais.serverSocketConnection.CloseAllSockets();
                    txtChat.Text = "";
                    txtIp.Text = "";
                    txtPorta.Text = "";
                    DadosGerais.EnviaMensagem("Server desconectado");
                    DadosGerais.serverSocketConnection = null;
                } else
                {
                    DadosGerais.EnviaMensagem("Você já esta desconectado");

                }



                //DadosGerais.clientSocketConnection.Exit();
                //DadosGerais.serverSocketConnection.CloseAllSockets();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);

            }
        }

        private void btnConectarUsuario_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if(OnUsuario != null)
                {
                    OnUsuario(txtIp.Text, txtPorta.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);

            }
        }
    }
}
