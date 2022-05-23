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
    /// Interaction logic for MensagemTextoChat.xaml
    /// </summary>
    public partial class MensagemTextoChat : UserControl
    {
        public MensagemTextoChat()
        {
            InitializeComponent();
        }

        

        public string Mensagem
        {
            get { return txtMensagem.Text; }
            set { txtMensagem.Text = value; }
        }

    }
}
