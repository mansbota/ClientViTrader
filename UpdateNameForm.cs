using ClientViTrader.DTOs;
using ClientViTrader.Utils;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WebClient;

namespace ClientViTrader
{
    public partial class UpdateNameForm : Form
    {
        UserDTO user;
        string serverURL;

        public UpdateNameForm(UserDTO user, string serverURL)
        {
            InitializeComponent();
            this.user = user;
            this.serverURL = serverURL;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            nameTextBox_Validated(sender, e);

            if (!errorProvider1.GetError(nameTextBox).Equals(""))
                return;

            string oldUsername = user.username;
            user.username = nameTextBox.Text;

            RestClient client = new(HTTP_VERB.PUT, serverURL + "/user?id=" + user.id, Encoding.Unicode.GetBytes(Util.Serialize(user)));
            client.SetHeader("Authorization", Util.GetAuthHeaderValue(oldUsername, user.password));
            RestResponse<string> response = client.MakeRequestText();

            if (response.Code != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Username updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void nameTextBox_Validated(object sender, EventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z][a-zA-Z0-9]{3,9}$");

            if (!regex.IsMatch(nameTextBox.Text))
            {
                errorProvider1.SetError(nameTextBox, "Must have between 4 and 9 characters and can't start with a number.");
            }
            else
            {
                errorProvider1.SetError(nameTextBox, "");
            }
        }

        private void UpdateNameForm_Load(object sender, EventArgs e)
        {
            nameUpdateLabel.Select();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
