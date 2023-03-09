using ClientViTrader.DTOs;
using System;
using System.Text;
using System.Windows.Forms;
using WebClient;
using ClientViTrader.Utils;
using System.Text.Json;

namespace ClientViTrader
{
    public partial class BuyForm : Form
    {
        UserDTO user;
        CryptoDTO crypto;
        string serverURL;
        string coingeckoURL;

        public BuyForm(UserDTO user, CryptoDTO crypto, string serverURL, string coingeckoURL)
        {
            InitializeComponent();
            this.user = user;
            this.serverURL = serverURL;
            this.crypto = crypto;
            this.coingeckoURL = coingeckoURL;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (usdCheckBox.Checked)
            {
                usdTextBox_Validated(sender, e);

                if (!usdTextBoxError.GetError(usdTextBox).Equals(""))
                    return;

                SendBuyRequest(decimal.Parse(usdTextBox.Text));
            }
            else
            {
                coinTextBox_Validated(sender, e);

                if (!coinTextBoxError.GetError(coinTextBox).Equals(""))
                    return;

                RestClient client = new(HTTP_VERB.GET, coingeckoURL + "/simple/price?ids=" + crypto.name + "&vs_currencies=usd");
                RestResponse<string> response = client.MakeRequestText();

                if (response.Code != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    decimal price;
                    using (var obj = JsonDocument.Parse(response.Response))
                    {
                        JsonElement root = obj.RootElement;
                        JsonElement coin = root.GetProperty(crypto.name.ToLower());
                        price = coin.GetProperty("usd").GetDecimal();
                    }

                    decimal coinAmount = decimal.Parse(coinTextBox.Text);
                    decimal usdVal = coinAmount * price;

                    SendBuyRequest(usdVal);
                }
            }
        }

        private void SendBuyRequest(decimal usdAmount)
        {
            TradeDTO trade = new()
            {
                amount = usdAmount,
                cryptoId = crypto.id,
                tradeTypeId = 1
            };

            RestClient client = new(HTTP_VERB.POST, serverURL + "/trade?userId=" + user.id, Encoding.Unicode.GetBytes(Util.Serialize(trade)));
            client.SetHeader("Authorization", Util.GetAuthHeaderValue(user.username, user.password));
            RestResponse<string> response = client.MakeRequestText();

            if (response.Code != System.Net.HttpStatusCode.Created)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Crypto bought", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void coinTextBox_Validated(object sender, EventArgs e)
        {
            string text = coinTextBox.Text;
            decimal amount;

            if (text.Contains('.'))
            {
                coinTextBoxError.SetError(coinTextBox, "Please use , as decimal separator");
                return;
            }

            try
            {
                amount = decimal.Parse(text);
            }
            catch (FormatException)
            {
                coinTextBoxError.SetError(coinTextBox, "Please enter a valid number");
                return;
            }

            if (amount <= 0)
            {
                coinTextBoxError.SetError(coinTextBox, "Please enter a positive value");
            }
            else
            {
                coinTextBoxError.SetError(coinTextBox, "");
            }
        }

        private void usdTextBox_Validated(object sender, EventArgs e)
        {
            string text = usdTextBox.Text;
            decimal amount;

            if (text.Contains('.'))
            {
                usdTextBoxError.SetError(usdTextBox, "Please use , as decimal separator");
                return;
            }

            try
            {
                amount = decimal.Parse(text);
            }
            catch (FormatException)
            {
                usdTextBoxError.SetError(usdTextBox, "Please enter a valid number");
                return;
            }

            if (amount <= 0)
            {
                usdTextBoxError.SetError(usdTextBox, "Please enter a positive value");
            }
            else
            {
                usdTextBoxError.SetError(usdTextBox, "");
            }
        }

        private void BuyForm_Load(object sender, EventArgs e)
        {
            usdLabel.Select();
        }
    }
}
