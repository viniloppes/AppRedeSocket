using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AppRedeSocket.CLASSES
{
    static class DadosGerais
    {
        public static List<Socket> clientSockets = new List<Socket>();
        public static ServerSocketConnection serverSocketConnection;
        public static ClientSocketConnection clientSocketConnection;

  


        public static string ip = "";
        public static string porta = "";

        public static int toutEnviaRecebimento { get; internal set; }
        public static int toutEnviaRecebimentoServidor { get; internal set; }

        public delegate void MensagemLog(string mensagem);
        public static event MensagemLog OnEnviaMensagem;

        public delegate void RecebeResposta(string resposta);
        public static event RecebeResposta OnRecebeResposta;
        public delegate void ToutRecebimento();


        public delegate void RecebeRespostaServidor(string resposta);

        public static event RecebeResposta OnRecebeRespostaServidor;

        public static List<string> listaMensagens = new List<string>();
        public static List<string> listaMensagensServer = new List<string>();
        public static event ToutRecebimento OnToutRecebimento;
        public static event ToutRecebimento OnToutRecebimentoServidor;


        public static void RecebeRespostaCliente(string resposta)
        {
            try
            {
                if(OnRecebeResposta != null)
                {
                    OnRecebeResposta(resposta);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public static void RetornoServidor(string resposta)
        {
            try
            {
                if (OnRecebeRespostaServidor != null)
                {
                    OnRecebeRespostaServidor(resposta);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static void EnviaToutRecebimento()
        {
            if (OnToutRecebimento != null)
            {
                OnToutRecebimento();
            }
        }

        public static void EnviaToutRecebimentoServidor()
        {
            if (OnToutRecebimentoServidor != null)
            {
                OnToutRecebimentoServidor();
            }
        }
        public static void EnviaMensagem(string mensagem)
        {

            try
            {
                if(OnEnviaMensagem != null)
                {
                    OnEnviaMensagem(mensagem);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
