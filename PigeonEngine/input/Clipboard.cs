using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace pigeonEngine.input
{
    public class Clipboard
    {
        private string clipboard="";

        public String GetClipboardText()
        {
            Thread t = new Thread(getClipboard);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            while(t.IsAlive)
            {

            }
            return clipboard;
        }

        [STAThread]
        private void getClipboard()
        {
            if (Clipboard.ContainsText())
            {
                clipboard = Clipboard.GetText(TextDataFormat.UnicodeText);
            }
        }
    }
}