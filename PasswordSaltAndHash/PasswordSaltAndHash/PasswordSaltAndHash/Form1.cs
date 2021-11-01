using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordSaltAndHash
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbOutput.Clear();
        }

        private void w(string text)
        {
            rtbOutput.AppendText(text);
            rtbOutput.ScrollToBottom();
        }

        private void wl(string text)
        {
            w(text + Environment.NewLine);
        }

        private void btnHash_Click(object sender, EventArgs e)
        {

            var hashResult = PasswordHash.PasswordStorage.CreateHash(txtPassword.Text);

            wl(hashResult); //sha1:64000:18:TOVmseJO7d2rHXT0tQx0gjQTgebqsXXU:UdKVbUNvcd9QAxTC7x/kO1jc

            var resultArray = hashResult.Split(':');

            var passwordSalt = resultArray[1];
            var passwordHash = resultArray[2];

            wl($"Salt: {passwordSalt}");
            wl($"Hash: {passwordHash}");

            var outcome = PasswordHash.PasswordStorage.VerifyPassword(txtPassword.Text, hashResult);
            wl(outcome.ToString());
        }
    }
}
