using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Net.NetworkInformation;
//using System.Management;

namespace PinGo
{
    public partial class MainWin : Form
    {
        //private delegate void FlushClient();
        //设置代理
        public MainWin()
        {
            InitializeComponent();
        }
       
        private void pingBtn_Click(object sender, EventArgs e)
        {
            PinGo ping = new PinGo();
            ping.send(this,hostName.Text);
        }

        private void clrBtn_Click(object sender, EventArgs e)
        {
            this.resultBox.Items.Clear();
        }

        private void scanBtn_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            scanBtn.Enabled = false;
            //stpBtn.Enabled = true;
            pingBtn.Enabled = false;

            resultBox.Items.Add("扫描开始！");
            resultBox.SelectedIndex = resultBox.Items.Count - 1;
/*            resultBox.Items.Add("起始地址为：");
            计算起始地址语句占位


            resultBox.Items.Add("结束地址为：");
            计算结束地址语句占位
*/
            resultBox.Items.Add("正在扫描子网中的主机存活情况……");
            resultBox.SelectedIndex = resultBox.Items.Count - 1;
            Thread scStrart = new Thread(new ThreadStart(scanGo));
            scStrart.Start();

            //scanGo();

            //resultBox.Items.Add("扫描完成！");
            //resultBox.Items.Add("");

            scanBtn.Enabled = true;
            //stpBtn.Enabled = false;
            pingBtn.Enabled = true;
        }

        
        /************************************************************
         * 函数功能：扫描函数
         *  
         * 参数说明：无
         * 
         * 返回值：void
         * **********************************************************/
        private void scanGo()
        {
            //Control.CheckForIllegalCrossThreadCalls = false;
            //此句用于防止检测多线程窗口控件调用，但是清屏功能有不定时的异常出现
            IPHostEntry myPoint = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] myIP = new IPAddress[myPoint.AddressList.Length];
            int i = 0;
            for(int n = 0 ;n < myPoint.AddressList.Length;n++)
            {
                if(myPoint.AddressList[n].AddressFamily == AddressFamily.InterNetwork)
                {
                    myIP[i] = myPoint.AddressList[n];
                    i++;
                }
            }

            for (int n = 0; n < i;n++ )
            {
                String strIP = myIP[n].ToString();
                int p = strIP.LastIndexOf(".");
                strIP = strIP.Substring(0, p+1);

                for(int ipEnd = 1 ;ipEnd < 256 ; ipEnd++ )
                {
                    String strIPEnd = strIP + ipEnd.ToString();
                    IPAddress scanIP = IPAddress.Parse(strIPEnd);

                    /*ParameterizedThreadStart scanThreadStart = new ParameterizedThreadStart(sPing);
                    Thread scanThread = new Thread(scanThreadStart);
                    scanThread.IsBackground = true;
                    scanThread.Start(scanIP);*/
                    

                    sPing(scanIP);

                    //PinGo ping = new PinGo();
                    //ping.send(this, strIPEnd);
                }
                this.resultBox.Items.Add("扫描完毕……");
                resultBox.SelectedIndex = resultBox.Items.Count - 1;
            }
                return;
            /***************************
            IPAddress[] myIP = new IPAddress[Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length];
            int i = 0;

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach(NetworkInterface adapter in nics)
            {
                if(adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet || adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    IPInterfaceProperties ip = adapter.GetIPProperties();
                    UnicastIPAddressInformationCollection ipCollection = ip.UnicastAddresses;
                    foreach(UnicastIPAddressInformation ipadd in ipCollection)
                        if (ipadd.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            myIP[i] = ipadd.Address;
                            i++;
                        }
                }
            }
             */
        }

        private void sPing(Object obj)
        {
            IPAddress host = (IPAddress)obj;
            Ping ping = new Ping();
            PingOptions option = new PingOptions();
            option.DontFragment = true;
            String data = "$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$";
            byte[] buff = Encoding.ASCII.GetBytes(data);
            int timeout = 1000;
            PingReply reply = ping.Send(host, timeout, buff, option);
            if(reply.Status == IPStatus.Success)
            {
               /* while(true)
                {
                    Thread.Sleep(10);
                    ThreadFunction();
                }
                */
                this.resultBox.Items.Add(host.ToString());
                this.resultBox.SelectedIndex = this.resultBox.Items.Count - 1;
            }
        }

        private void resultBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        /*private void ThreadFunction()
        {
            if(this.resultBox.InvokeRequired)
            {
                FlushClient fc = new FlushClient(ThreadFunction);
                this.Invoke(fc);
            }
            else
            {
                this.resultBox.Items.Add(host.ToString());
            }
        }
        */
    }
}