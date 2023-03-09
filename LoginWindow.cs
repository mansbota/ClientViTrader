using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using IniParser;
using IniParser.Model;
using System.Text.Json;
using System.IO;
using WebClient;
using System.Net;
using ClientViTrader.Utils;
using ClientViTrader.DTOs;
using System.Text;

namespace ClientViTrader
{
    public partial class LoginWindow : Form
    {
        private string serverURL;
        bool launchedMainWindow = false;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginWindow_Load(object sender, EventArgs e)
        {
            string fileName = "serverconfig.json";
            string jsonString = File.ReadAllText(fileName);

            using (JsonDocument doc = JsonDocument.Parse(jsonString))
            {
                JsonElement root = doc.RootElement;

                string serverAddress = root.GetProperty("address").GetString();
                string serverPort = root.GetProperty("port").GetString();
                serverURL = "http://" + serverAddress + ":" + serverPort;
            }

            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("settings.ini");

            if (bool.Parse(data["LoginWindow"]["firstTime"]))
            {
                data["LoginWindow"]["firstTime"] = "false";
                parser.WriteFile("settings.ini", data);

                return;
            }

            int x = int.Parse(data["LoginWindow"]["x"]);
            int y = int.Parse(data["LoginWindow"]["y"]);
            this.Location = new System.Drawing.Point(x, y);

            label1.Select();
        }

        private async void logButton_Click(object sender, EventArgs e)
        {
            userNameLog_Validating(label1, new CancelEventArgs());
            passwordLog_Validating(label2, new CancelEventArgs());

            if (!logUserNameErr.GetError(label1).Equals("") || !logPasswordErr.GetError(label2).Equals(""))
                return;

            RestClient client = new RestClient(HTTP_VERB.POST, serverURL + "/login");
            client.SetHeader("Authorization", Util.GetAuthHeaderValue(userNameLog.Text, passwordLog.Text));

            RestResponse<string> response = await client.MakeRequestAsyncText();
            if (response.Code != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UserDTO user = Util.Deserialize<UserDTO>(response.Response);
            user.password = passwordLog.Text;

            MainWindow window = new(user, serverURL);

            window.Show();
            launchedMainWindow = true;
            this.Close();
        }

        private async void regButton_Click(object sender, EventArgs e)
        {
            userNameReg_Validating(label3, new CancelEventArgs());
            passwordReg_Validating(label4, new CancelEventArgs());
            emailAddress_Validating(label5, new CancelEventArgs());

            if (!regUserNameErr.GetError(label3).Equals("") || !regPasswordErr.GetError(label4).Equals("") || !emailAddressErr.GetError(label5).Equals("") || !regPasswordErr.GetError(label6).Equals(""))
                return;

            if (!passwordReg.Text.Equals(passwordReg2.Text))
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UserDTO user = new UserDTO
            {
                username = userNameReg.Text,
                password = passwordReg.Text,
                email = emailAddress.Text
            };

            RestClient client = new RestClient(HTTP_VERB.POST, serverURL + "/user", Encoding.Unicode.GetBytes(Util.Serialize(user)));
            RestResponse<string> response = await client.MakeRequestAsyncText();

            if (response.Code != HttpStatusCode.Created)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoginWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("settings.ini");

            data["LoginWindow"]["x"] = this.Location.X.ToString();
            data["LoginWindow"]["y"] = this.Location.Y.ToString();

            parser.WriteFile("settings.ini", data);

            if (!launchedMainWindow)
            {
                Application.Exit();
            }
        }

        private void userNameLog_Validating(object sender, CancelEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z][a-zA-Z0-9]{3,9}$");

            if (!regex.IsMatch(userNameLog.Text))
            {
                e.Cancel = true;
                userNameLog.Select(0, userNameLog.Text.Length);
                logUserNameErr.SetError(label1, "Must have between 4 and 9 characters and can't start with a number.");
            }
        }

        private void userNameLog_Validated(object sender, EventArgs e)
        {
            logUserNameErr.SetError(label1, "");
        }

        private void userNameReg_Validating(object sender, CancelEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z][a-zA-Z0-9]{3,9}$");

            if (!regex.IsMatch(userNameReg.Text))
            {
                e.Cancel = true;
                userNameReg.Select(0, userNameReg.Text.Length);
                regUserNameErr.SetError(label3, "Must have between 4 and 9 characters and can't start with a number.");
            }
        }

        private void userNameReg_Validated(object sender, EventArgs e)
        {
            regUserNameErr.SetError(label3, "");
        }

        private void passwordLog_Validating(object sender, CancelEventArgs e)
        {
            string password = passwordLog.Text;

            if (password.Length < 8 || !password.Any(char.IsDigit))
            {
                e.Cancel = true;
                passwordLog.Select(0, passwordLog.Text.Length);
                logPasswordErr.SetError(label2, "Must have at least 8 characters including a number.");
            }
        }

        private void passwordLog_Validated(object sender, EventArgs e)
        {
            logPasswordErr.SetError(label2, "");
        }

        private void passwordReg_Validating(object sender, CancelEventArgs e)
        {
            string password = passwordReg.Text;

            if (password.Length < 8 || !password.Any(char.IsDigit))
            {
                e.Cancel = true;
                passwordReg.Select(0, passwordReg.Text.Length);
                regPasswordErr.SetError(label4, "Must have at least 8 characters including a number.");
            }
        }

        private void passwordReg_Validated(object sender, EventArgs e)
        {
            regPasswordErr.SetError(label4, "");
        }

        private void passwordReg2_Validated(object sender, EventArgs e)
        {
            regPasswordErr2.SetError(label6, "");
        }

        private void emailAddress_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (emailAddress.Text.Length <= 0 || emailAddress.Text.Length > 50)
                {
                    e.Cancel = true;
                    emailAddress.Select(0, emailAddress.Text.Length);
                    emailAddressErr.SetError(label5, "Invalid e-mail address");
                    return;
                }

                MailAddress mailAddress = new MailAddress(emailAddress.Text);

                if (mailAddress.Address != emailAddress.Text)
                {
                    e.Cancel = true;
                    emailAddress.Select(0, emailAddress.Text.Length);
                    emailAddressErr.SetError(label5, "Invalid e-mail address");
                }
            }
            catch (FormatException)
            {
                e.Cancel = true;
                emailAddress.Select(0, emailAddress.Text.Length);
                emailAddressErr.SetError(label5, "Invalid e-mail address");
            }
        }

        private void emailAddress_Validated(object sender, EventArgs e)
        {
            emailAddressErr.SetError(label5, "");
        }
    }
}