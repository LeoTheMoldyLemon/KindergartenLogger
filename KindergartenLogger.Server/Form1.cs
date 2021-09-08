using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KindergartenLogger.Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }
        static byte[] Encrypt(byte[] Message, byte[] HashKey)
        {
            byte[] Encrypted = new byte[Message.Length];
            for (int i = 0; i < Message.Length; i++) {
                Encrypted[i] = (byte)(Message[i] ^ HashKey[i%HashKey.Length]);
            }
            return Encrypted;
        }

    }
}
