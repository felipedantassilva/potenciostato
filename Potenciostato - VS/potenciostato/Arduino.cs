using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace potenciostato
{
    class Arduino
    {
        SerialPort serialPort;
        bool ok;

        int v0da, v1da, vela;

        public Arduino(string portname, int bauds)
        {
            try
            {
                serialPort = new SerialPort(portname, bauds);
                ok = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error COM arduino. " + ex, "Error Init COM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ok = false;
            }
        }


        public bool Init_COM()
        {
            if (ok)
            {
                try
                {
                    if (!serialPort.IsOpen) //Se porta estiver fechada
                        serialPort.Open();
                    serialPort.ReadTimeout = 95;
                    serialPort.Write("square");
                    if (serialPort.ReadChar() == 'a')
                    {
                        MessageBox.Show("Conectado", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error COM arduino. " + ex, "Error Init COM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void FechaPorta()
        {
            if (serialPort.IsOpen)
                serialPort.Close();
        }

        public void Stop()
        {
            if (ok)
            {
                serialPort.Write("s");
            }
        }

        public int GetData()
        {
            int leitura = -1;
            string let;
            try
            {

                if (ok)
                {
                    serialPort.Write("r");
                    serialPort.Write(Convert.ToString(v0da));
                    serialPort.Write(Convert.ToString(v1da));
                    serialPort.Write(Convert.ToString(vela));

                    //leitura = serialPort.ReadChar();
                    

                }
            }
            catch
            {
                MessageBox.Show("Error ao obter leitura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            return leitura;
        }

    }
}
