using System;
using System.Windows.Forms;
using Stark.NumberGenerator.Library;
using Stark.NumberGenerator.Library.Banks;
using Boodle.NumberGenerator.Library.Other;
using Boodle.NumberGenerator.Library.CreditCards;

namespace Stark.NumberGenerator
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedValue = Convert.ToInt32(comboBox1.SelectedIndex);
            string result = "";

            switch (selectedValue)
            {
                case 0:
                    result = IDNumberModel.GenerateIDNumber().ToString();
                    break;
                case 1:
                    result = new ABSA().GenerateAccountNumber().ToString();
                    break;
                case 2:
                    result = new Capitec().GenerateAccountNumber().ToString();
                    break;
                case 3:
                    result = new FNB().GenerateAccountNumber().ToString();
                    break;
                case 4:
                    result = new Nedbank().GenerateAccountNumber().ToString();
                    break;
                case 5:
                    result = new StandardBank().GenerateAccountNumber().ToString();
                    break;
                case 6:
                    result = new GuidGenerator().GenerateGuid();
                    break;
                case 7:
                    var cardDetails = CreditCardGenerator.GenerateRandomCardNumber();
                    result = "Provider: " + cardDetails.Provider + Environment.NewLine
                        + "Card Number: " + cardDetails.CardNumber + Environment.NewLine
                        + "Expiry: " + cardDetails.ExpiryDate + Environment.NewLine
                        + "CVV: " + cardDetails.CVV;
                    break;
                default:
                    break;

            }
            txtResult.Text = result;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }
    }
}
