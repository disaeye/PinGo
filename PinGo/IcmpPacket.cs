using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 * 这是ICMP报文的类
 */

namespace PinGo
{
    class IcmpPacket
    {
        private Byte type;                  /*    类型      */
        private Byte code;                  /*    代码      */
        private UInt16 checkSum;            /*    检验和     */
        private UInt16 identifier;          /*    标识符     */
        private UInt16 sequenceNumber;      /*    序列号     */
        private Byte[] data;                /*    数据      */

        /*********************************************************
         * 
         * 函数功能：构造函数
         * 
         * 参数说明：
         *          type，类型
         *          code，代码
         *          checkSum，检验和
         *          identifier，id号
         *          sequenceNumber，序列号 
         *          dataSize，数据长度
         *          
         * *******************************************************/
        public IcmpPacket(Byte type, Byte code, UInt16 checkSum, UInt16 identifier, UInt16 sequenceNumber, int dataSize)
        {
            this.type = type;
            this.code = code;
            this.checkSum = checkSum;
            this.identifier = identifier;
            this.sequenceNumber = sequenceNumber;
            this.data = new Byte[dataSize];

            for (int i = 0; i < dataSize; i++)
            {
                this.data[i] = (Byte)'$';
            }
        }
        
        public UInt16 CheckSum
        {
            get
            {
                return this.checkSum;
            }
            set
            {
                this.checkSum = value;
            }
        }

        /**************************************************************
         * 
         * 函数功能：初始化报文，并返回报文长度
         * 
         * 参数说明：
         *          buffer，回传报文
         *          
         * 返回值：int，报文长度
         * 
         ***********************************************************/

        public int initPacket(Byte[] buffer)
        {
            int i = 0;
            Byte[] bufType = new Byte[1] { type };
            Byte[] bufCode = new Byte[1] { code };
            Byte[] bufCheckSum = BitConverter.GetBytes(checkSum);
            Byte[] bufId = BitConverter.GetBytes(identifier);
            Byte[] bufSeqNum = BitConverter.GetBytes(sequenceNumber);

            /*      生成buff      */
            Array.Copy(bufType, 0, buffer, i, bufType.Length);
            i = i + bufType.Length;
            Array.Copy(bufCode, 0, buffer, i, bufCode.Length);
            i = i + bufCode.Length;
            Array.Copy(bufCheckSum, 0, buffer, i, bufCheckSum.Length);
            i = i + bufCheckSum.Length;
            Array.Copy(bufId, 0, buffer, i, bufId.Length);
            i = i + bufId.Length;
            Array.Copy(bufSeqNum, 0, buffer, i, bufSeqNum.Length);
            i = i + bufSeqNum.Length;
            Array.Copy(data, 0, buffer, i, data.Length);
            i = i + data.Length;

            return i;
        }

        /******************************************
         * 
         * 函数功能：计算检验和
         * 
         * 参数说明：
         *          buffer，传入要求检验和的报文
         *          
         * 返回值：UInt16，检验和
         *
         ******************************************/
        public static UInt16 CKS(UInt16[] buffer)
        {
            int cks = 0;
            for (int i = 0; i < buffer.Length; i++)
                cks = cks + (int)buffer[i];

            /*          高16位与低16位相加，取反后得检验和         */

            cks = (cks >> 16) + (cks & 0xFFFF);
            cks = cks + (cks >> 16);

            return (UInt16)(~cks);
        }
    }
}
