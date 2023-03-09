using ClientViTrader.DTOs;
using ClientViTrader.Utils;
using System;
using System.Text;
using System.Windows.Forms;
using WebClient;

namespace ClientViTrader
{
    public partial class UpdatePositionForm : Form
    {
        UserDTO user;
        PositionDTO position;
        string serverURL;

        public UpdatePositionForm(UserDTO user, PositionDTO position, string serverURL)
        {
            InitializeComponent();
            this.user = user;
            this.serverURL = serverURL;
            this.position = position;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            newAmountTextBox_Validated(sender, e);

            if (!errorProvider1.GetError(newAmountTextBox).Equals(""))
                return;

            position.amount = decimal.Parse(newAmountTextBox.Text);
            RestClient client = new(HTTP_VERB.PUT, serverURL + "/position?id=" + position.id + "&userId=" + user.id, Encoding.Unicode.GetBytes(Util.Serialize(position)));
            client.SetHeader("Authorization", Util.GetAuthHeaderValue(user.username, user.password));
            RestResponse<string> response = client.MakeRequestText();

            if (response.Code != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Position updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void newAmountTextBox_Validated(object sender, EventArgs e)
        {
            string text = newAmountTextBox.Text;
            decimal amount;

            if (text.Contains('.'))
            {
                errorProvider1.SetError(newAmountTextBox, "Please use , as decimal separator");
                return;
            }

            try
            {
                amount = decimal.Parse(text);
            }
            catch (FormatException)
            {
                errorProvider1.SetError(newAmountTextBox, "Please enter a valid number");
                return;
            }

            if (amount <= 0)
            {
                errorProvider1.SetError(newAmountTextBox, "Please enter a positive value");
            }
            else
            {
                errorProvider1.SetError(newAmountTextBox, "");
            }
        }

        private void UpdatePositionForm_Load(object sender, EventArgs e)
        {
            posUpdateLabel.Select();
        }
    }
}
