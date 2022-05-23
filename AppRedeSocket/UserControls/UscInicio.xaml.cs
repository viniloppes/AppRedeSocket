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

        public void Inicializa(string ip = "", string porta = "")
        {
            try
            {
                //stpChat.Children.Clear();
                //foreach (string s in DadosGerais.clientSocketConnection.ArrayReceiveResponde)
                //{
                //    EscreveMensagemChat(s);
                //}
                //DadosGerais.toutEnviaRecebimento = 3;
                DadosGerais.EnviaMensagem("Conecte-se a um servidor ou cliente!");

                txtCampoChat.Text = "";
                txtChat.Text = "";
                txtIp.Text = ip;
                txtPorta.Text = porta;
                txtNome.Text = "";
                
                DadosGerais.OnToutRecebimento += DadosGerais_OnToutRecebimento;
                DadosGerais.OnRecebeResposta += DadosGerais_OnRecebeResposta;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);

            }

        }

        private void DadosGerais_OnToutRecebimento()
        {
            try
            {
                int lenght = DadosGerais.listaMensagens.Count;
                EscreveMensagemChat(DadosGerais.listaMensagens[lenght - 1]);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);

            }
        }

        private void DadosGerais_OnRecebeResposta(string resposta)
        {
            try
            {
                DadosGerais.toutEnviaRecebimento = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);

            }
        }

        public void Finaliza()
        {
            try
            {
                DadosGerais.OnRecebeResposta -= DadosGerais_OnRecebeResposta;

                Storyboard sb = FindResource("StbTxtMensagemLog") as Storyboard;
                sb.Pause();
                sb.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        //private void DadosGerais_OnToutRecebimento()
        //{
        //    try
        //    {
        //        if (DadosGerais.clientSocketConnection != null)
        //        {

        //            string aux = DadosGerais.clientSocketConnection.;
        //            if (aux != "")
        //            {
        //                EscreveMensagemChat(aux);
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);

        //    }
        //}



        public void EscreveMensagemChat(string mensagem)
        {
            try
            {
                JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                dynamic resultado = serializer.DeserializeObject(mensagem);
                
               

                TextBlock textBlock = new TextBlock();
                textBlock.Foreground = Brushes.White;
                textBlock.FontSize = 20;
                textBlock.TextWrapping = TextWrapping.Wrap;
                textBlock.Margin = new Thickness(10, 0, 10, 0);
                Run runNome = new Run("\r\n" + (resultado["usuario"] != "" ? resultado["usuario"] : "desconhecido") + " - ");
                runNome.Foreground = Brushes.Green;
                runNome.FontSize = 15;

                Run runMensagem = new Run(mensagem);
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




        public void EnviaMensagem()
        {
            if (txtCampoChat.Text != "")
            {
          
                var json = "{\"usuario\":\"" + txtNome.Text + "\",\"mensagem\":\""+ txtCampoChat.Text + "\"}";
                DadosGerais.clientSocketConnection.SendRequest(json);
                DadosGerais.clientSocketConnection.SendRequest(json);
                //Run runNome = new Run("\r\n" + (txtNome.Text != "" ? txtNome.Text : "Usuario" + "1") + " - ");
                //runNome.Foreground = Brushes.Azure;

                //txtChat.Inlines.Add(runNome);

                ////escreve a mensagem
                //txtChat.Text += txtCampoChat.Text;

                txtCampoChat.Text = "";

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
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void brdConectar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {


                ////Inicializa SERVIDOR
                //if (DadosGerais.serverSocketConnection == null)
                //{
                //    DadosGerais.serverSocketConnection = new ServerSocketConnection(
                //                     new Socket(
                //                         AddressFamily.InterNetwork,
                //                         SocketType.Stream, ProtocolType.Tcp)
                //                     );
                //    DadosGerais.serverSocketConnection.SetupServer(txtIp.Text, txtPorta.Text);
                //    DadosGerais.EnviaMensagem("Configuração do servidor concluída");


                //}

                //ServerSocketConnection.SetupServer(txtIp.Text, txtPorta.Text);



                // Verifica se o usuario existe
                if (DadosGerais.clientSocketConnection == null)
                {
                    //INICIALIZAÇÃO DA CONECÇÃO DO CLIENTE

                    DadosGerais.
                                     clientSocketConnection =
                                     new ClientSocketConnection(
                                         new Socket
                                         (AddressFamily.InterNetwork,
                                         SocketType.Stream, ProtocolType.Tcp));

                    DadosGerais.clientSocketConnection.ConnectToServer(txtIp.Text, int.Parse(txtPorta.Text));
                    //DadosGerais.clientSocketConnection.RequestLoop();
                    DadosGerais.EnviaMensagem("Configuração do cliente concluída");

                }
                else
                {
                    DadosGerais.EnviaMensagem("Cliente já está conectado");

                }
                //DadosGerais.clientSocketConnection.RequestLoop();

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
                if (DadosGerais.clientSocketConnection != null)
                {
                    DadosGerais.clientSocketConnection.SendRequest("exit");
                    DadosGerais.clientSocketConnection = null;
                    DadosGerais.EnviaMensagem("Cliente desconectado!");
                }


                //DadosGerais.clientSocketConnection.Exit();
                //DadosGerais.serverSocketConnection.CloseAllSockets();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);

            }
        }


    }
}
