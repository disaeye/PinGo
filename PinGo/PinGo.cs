using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace PinGo
{
    class PinGo
    {
        /**************************************************************
         * 
         * 函数功能：实现Ping
         * 
         * 参数说明：mainWin，窗口对象，用于实现对resultBox的输出控制
         *          host，主机名
         * 
         * 返回值：void
         * 
         **************************************************************/

        private static int port = 2333;/*       随意设置的端口号        */
        public void send(MainWin mainWin, String host)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Raw,
                ProtocolType.Icmp);

            socket.SetSocketOption(SocketOptionLevel.Socket, 
                SocketOptionName.SendTimeout, 
                1000); 

            IPHostEntry hostInfo = null;
            IPEndPoint hostPoint = null;

            IPHostEntry clientInfo = null;
            IPEndPoint clientPoint = null;

            int isGo = 0;
            int n = 0;//记录主机的IPV4索引
            int m = 0;//记录客户机的IPV4索引

            int start;
            int timeOut = 0;
            int end = 0;

            if (host == null)
            {
                mainWin.resultBox.Items.Add("请输入主机名！");
                mainWin.resultBox.SelectedIndex = mainWin.resultBox.Items.Count - 1;
                return;
            }

            /*      检查主机        */

            try
            {
                //IPAddress ip = IPAddress.Parse(host);
                hostInfo = Dns.GetHostByName(host);
            }
            catch (Exception)
            {
                mainWin.resultBox.Items.Add("没有发现主机！请检查该名称，然后重试。");
                mainWin.resultBox.SelectedIndex = mainWin.resultBox.Items.Count - 1;
                return;
            }

            /*          选择主机IPV4地址      */
            for (n = 0; n < hostInfo.AddressList.Length; n++)
            {
                if (hostInfo.AddressList[n].AddressFamily == AddressFamily.InterNetwork)
                    break;
            }

            hostPoint = new IPEndPoint(hostInfo.AddressList[n], port);

            clientInfo = Dns.GetHostByName(Dns.GetHostName());

            /*          选择客户机的IPV4地址        */
            for (m = 0; m < clientInfo.AddressList.Length; m++)
            {
                if (clientInfo.AddressList[m].AddressFamily == AddressFamily.InterNetwork)
                    break;
            }

            clientPoint = new IPEndPoint(clientInfo.AddressList[m], port);

            EndPoint clientPointRemote = (EndPoint)clientPoint;

            int dataSize = 32;
            int packetSize = dataSize + 8;
            const int echo = 8;
            IcmpPacket packet = new IcmpPacket(echo, 0, 0, 45, 0, dataSize);
            Byte[] buff = new Byte[packetSize];
            int index = packet.initPacket(buff);

            /*      检验报文是否出错        */

            if (index != packetSize)
            {
                mainWin.resultBox.Items.Add("创建报文出错！Ping过程终止！");
                mainWin.resultBox.SelectedIndex = mainWin.resultBox.Items.Count - 1;
                return;
            }

            int Cksum_buffer_length = (int)Math.Ceiling(((Double)index) / 2);
            UInt16[] Cksum_buffer = new UInt16[Cksum_buffer_length];
            int headerIndex = 0;
            for (int i = 0; i < Cksum_buffer_length; i++)
            {
                Cksum_buffer[i] = BitConverter.ToUInt16(buff, headerIndex);
                headerIndex = headerIndex + 2;
            }

            /*      保存检验和       */

            packet.CheckSum = IcmpPacket.CKS(Cksum_buffer);
            Byte[] pack = new Byte[packetSize];
            index = packet.initPacket(pack);

            /*      检验报文是否出错        */

            if (index != packetSize)
            {
                mainWin.resultBox.Items.Add("检验和出错！Ping过程终止！");
                mainWin.resultBox.SelectedIndex = mainWin.resultBox.Items.Count - 1;
                return;
            }
            isGo = 0;

            /*      计时go~       */

            start = Environment.TickCount;

            //EndPoint hostPointRemote = (EndPoint)hostPoint;
            //isGo = socket.SendTo(pack, hostPointRemote);
            //isGo = socket.Send(pack);

            isGo = socket.SendTo(pack, hostPoint);

            if (isGo == -1)
            {
                mainWin.resultBox.Items.Add("无法传送报文！");
                mainWin.resultBox.SelectedIndex = mainWin.resultBox.Items.Count - 1;
            }
            Byte[] rev = new Byte[256];

            isGo = 0;

            socket.ReceiveTimeout = 1000;//设置无响应时间
            
            while (true)
            {
                try
                { 
                    isGo = socket.ReceiveFrom(rev, 256 , 0 , ref clientPointRemote);
                }
                catch(Exception e)
                {
                    mainWin.resultBox.Items.Add("主机无响应！");
                    mainWin.resultBox.SelectedIndex = mainWin.resultBox.Items.Count - 1;
                    break;
                }

                timeOut = Environment.TickCount - start;             
                if (timeOut > 1000)
                {
                    mainWin.resultBox.Items.Add("Ping超时!");
                    mainWin.resultBox.SelectedIndex = mainWin.resultBox.Items.Count - 1;
                    //return;
                    break;
                }
                else if (isGo > 0)
                {
                    end = Environment.TickCount - start;
                    if (end < 10)
                    {
                        mainWin.resultBox.Items.Add("来自 " + hostInfo.HostName.ToString() + "[" + hostInfo.AddressList[n] + "]" + " 的回复：字节=" + isGo + "bytes 时间<10ms");
                        mainWin.resultBox.SelectedIndex = mainWin.resultBox.Items.Count - 1;
                    }
                    else
                    {
                        mainWin.resultBox.Items.Add("来自 " + hostInfo.HostName.ToString() + "[" + hostInfo.AddressList[n] + "]" + " 的回复：字节=" + isGo + "bytes 时间=" + end + "ms");
                        mainWin.resultBox.SelectedIndex = mainWin.resultBox.Items.Count - 1;
                    }
                    //return;
                    break;
                }
               
            }//while
            socket.Close();
        }//send
    }//class PinGo
}//package PinGo
