using CSI_PPMS.IServices;
using CSI_PPMS.Models;
using Sharp7;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace CSI_PPMS.Services
{
    public class TCPServices : ITCPServices
    {
        private const string TMLFileName = "TEST.tml";


        public TcpClient ConnectTechofor(string IPAddress, int Portno)
        {
            TcpClient client = new TcpClient(IPAddress, Portno);
            return client;
        }

        public TcpClient ConnectPLC(string IPAddress, int Portno)
        {
            TcpClient client = new TcpClient(IPAddress, Portno);
            return client;
        }

        public List<byte[]> ConvertHexaFL1(FL1PunchingModel model)
        {
            var emptylines = FeedEmptyLines();

            var Line1 = ConvertToHexa("VS 0", model.Line1);
            var Line2 = ConvertToHexa("VS 1", model.Line2);
            var Line3 = ConvertToHexa("VS 2", model.Line3);
            var Line4 = ConvertToHexa("VS 3", model.Line4);
            var Line5 = ConvertToHexa("VS 4", model.Line5);
            var Line6 = ConvertToHexa("VS 5", model.Line6);
            var load = ConvertToHexa("LD", TMLFileName);
            emptylines.Add(Line1);
            emptylines.Add(Line2);
            emptylines.Add(Line3);
            emptylines.Add(Line4);
            emptylines.Add(Line5);
            emptylines.Add(Line6);
            emptylines.Add(load);

            return emptylines;

        }

        private List<byte[]> FeedEmptyLines()
        {
            var res = new List<byte[]>();
            var line1 = ConvertToHexa("VS 0", "");
            var line2 = ConvertToHexa("VS 1", "");
            var line3 = ConvertToHexa("VS 2", "");
            var line4 = ConvertToHexa("VS 3", "");
            var line5 = ConvertToHexa("VS 4", "");
            var line6 = ConvertToHexa("VS 5", "");
            res.Add(line1);
            res.Add(line2);
            res.Add(line3);
            res.Add(line4);
            res.Add(line5);
            res.Add(line6);
            return res;
        }

        private byte[] ConvertToHexa(string lineCode, string command)
        {
            //<ESC>[0][0][2]GO <CR>
            //1b 00 00 32 47 4f 0d
            var fullLine = $"{lineCode} \"{command}\"";
            if (lineCode.Contains("LD"))
            {
                fullLine = $"{lineCode} \"{command}\" 1 N";
            }

            byte[] byt = new byte[fullLine.Length + 5];

            byt[0] = 0x1B;
            byt[1] = 0x00;
            byt[2] = 0x00;
            byt[3] = (byte)fullLine.Length;
            for (int i = 0, j = 4; i < fullLine.Length; i++, j++)
            {
                byt[j] = Convert.ToByte(fullLine[i]);
            }
            byt[fullLine.Length + 4] = 0x0D;

            return byt;
        }






        public void downcoiler()
        {
            var client = new S7Client();
            int result = client.ConnectTo("10.10.2.33", 0, 2);
            if (result == 0)
            {
                byte[] db1Buffer = new byte[18];
                result = client.DBRead(1, 0, 18, db1Buffer);
                if (result != 0)
                {
                    Console.WriteLine("Error: " + client.ErrorText(result));
                }
                int db1dbw2 = S7.GetIntAt(db1Buffer, 2);
            }
            else
            {
            }

            // Disconnect the client
            client.Disconnect();
        }








    }
}
