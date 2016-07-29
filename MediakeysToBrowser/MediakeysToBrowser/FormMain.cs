using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediakeysToBrowser
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == KeyHelper.WM_HOTKEY)
            {
                try {
                    switch (m.WParam.ToInt32())
                    {
                        case (int)Keys.MediaPlayPause:
                            MessageHelper.SendSpaceBar("Twitch");
                            break;
                    }
                }
                catch (OverflowException e)
                {
                    KeyHelper.RemoveHook(this.Handle);
                    throw e;
                }
            }
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            KeyHelper.InstallHook(this.Handle);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            KeyHelper.RemoveHook(this.Handle);
        }
    }
}
