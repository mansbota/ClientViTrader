using ClientViTrader.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientViTrader
{
    public partial class CreateTriggerForm : Form
    {
        TriggerDTO trigger;

        public CreateTriggerForm(TriggerDTO trigger)
        {
            InitializeComponent();
            this.trigger = trigger;
        }

        private void CreateTriggerForm_Load(object sender, EventArgs e)
        {
            label1.Select();
            radioButtonUnder.Checked = true;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            valueTextBox_Validated(sender, e);

            if (!valueTextBoxError.GetError(valueTextBox).Equals(""))
                return;

            trigger.indicatorValue = decimal.Parse(valueTextBox.Text);
            
            if (radioButtonUnder.Checked)
            {
                trigger.triggerTypeId = 1;
                trigger.typeName = "UNDER";
            }
            else if (radioButtonOver.Checked)
            {
                trigger.triggerTypeId = 2;
                trigger.typeName = "OVER";
            }

            MessageBox.Show("Trigger created", "Add Trigger", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void valueTextBox_Validated(object sender, EventArgs e)
        {
            string text = valueTextBox.Text;
            decimal amount;

            try
            {
                amount = decimal.Parse(text);
            }
            catch (FormatException)
            {
                valueTextBoxError.SetError(valueTextBox, "Please enter a valid number");
                return;
            }

            if (amount <= 0)
            {
                valueTextBoxError.SetError(valueTextBox, "Please enter a positive value");
            }
            else
            {
                valueTextBoxError.SetError(valueTextBox, "");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
