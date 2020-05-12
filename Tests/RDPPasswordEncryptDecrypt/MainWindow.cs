using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DataProtection;

namespace RDPPasswordEncryptDecrypt
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            txHash.Text = DataProtectionForRDPWrapper.Encrypt(txPassword.Text);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            txPassword.Text = DataProtectionForRDPWrapper.Decrypt(txHash.Text);
        }
    }
}
