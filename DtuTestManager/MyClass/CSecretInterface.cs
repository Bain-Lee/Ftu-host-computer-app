using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

namespace DtuTestManager
{
    public class CSecretInterface
    {
        public static object m_SecretLock = new object();
        public virtual int Secret_ConnectHSM(string cHostIp, int inPort, int timeout)
        {
            return -100;
        }
        public virtual int Secret_DisConnect(int sConnect)
        {
            return -100;
        }
        public virtual int Secret_GenRand(int sConnect, int sRandLen, Byte[] random)
        {
            return -100;
        }
        public virtual int Secret_InternalSign(int sConnect, int keyIndex, int inDataLen, Byte[] inData, Byte[] sData, ref int sDataLen)
        {
            return -100;
        }
        public virtual int Secret_VerSign(int sConnect, int cerLen, Byte[] cer, int oriDataLen, Byte[] oriData, int sDataLen, Byte[] sData)
        {
            return -100;
        }
        public virtual int Secret_EnData(int sConnect, int keyVersion, int keyIndex, int divTimes, Byte[] divData, Byte[] ivData, int plainDataLen, Byte[] plainData, Byte[] enData, ref int enDataLen)
        {
            return -100;
        }
        public virtual int Secret_DeData(int sConnect, int keyVersion, int keyIndex, int divTimes, Byte[] divData, Byte[] ivData, int enDataLen, Byte[] enData, Byte[] plainData, ref int plainDataLen)
        {
            return -100;
        }
        public virtual int Secret_GetUpdateKey(int sConnect, int oldKeyVersion, int NewKeyVersion, int divTimes, Byte[] divData, Byte[] ivData, int asKid, Byte[] updateKeyData, ref int olen)
        {
            return -100;
        }
        public virtual int Secret_GetCertInfoYW(int sConnect, byte[] certID, byte[] certData, ref int certDataLen)
        {
            return -100;
        }
        public virtual int Secret_SignYW(int sConnect, int inDataLen, byte[] inData, byte[] sData, ref int sDataLen, int keyIndex)
        {
            return -1;
        }
        public virtual int Secret_EnDataYW(int sConnect, int keyVersion, int keyIndex, int divTimes, byte[] divData, byte[] ivData, int plainDataLen, byte[] plainData, byte[] enData, ref int enDataLen)
        {
            return -1;
        }
        public virtual int Secret_DeDataYW(int sConnect, int keyVersion, int keyIndex, int divTimes, byte[] divData, byte[] ivData, int enDataLen, byte[] enData, byte[] plainData, ref int plainDataLen)
        {
            return -1;
        }
        public virtual int Secret_GetUpdateKeyYW(int sConnect, int oldKeyVersion, int NewKeyVersion, int divTimes, byte[] divData, byte[] ivData, int asKid, byte[] updateKeyData, ref int olen)
        {
            return -1;
        }
        public virtual bool Secret_GetYW_CertAndID(ref byte[] pCertBuff,ref int nCertLeng,ref byte[] pIDBuff,ref int nIDLeng)
        {
            return false;
        }
        public virtual  int Secret_DeAuthData(int sConnect, int keyVersion, int divTimes, byte[] divData, int enDataLen, byte[] enData, Byte[] plainData, ref int plainDataLen)
        {
            return -100;
        }

    }
    public class C11SecretInterface : CSecretInterface
    {
        [DllImport("TZhuZhanDll.dll")]
        private static extern int ConnectHSM(string cHostIp, int inPort, int timeout);
        [DllImport("TZhuZhanDll.dll")]
        private static extern int DisConnect(int sConnect);
        [DllImport("TZhuZhanDll.dll")]
        private static extern int GenRand(int sConnect, int sRandLen, byte[] random);
        [DllImport("TZhuZhanDll.dll")]
        private static extern int InternalSign(int sConnect, int keyIndex, int inDataLen, byte[] inData, byte[] sData, ref int sDataLen);
        [DllImport("TZhuZhanDll.dll")]
        private static extern int VerSign(int sConnect, int cerLen, byte[] cer, int oriDataLen, byte[] oriData, int sDataLen, byte[] sData);
        [DllImport("TZhuZhanDll.dll")]
        private static extern int EnData(int sConnect, int keyVersion, int keyIndex, int divTimes, byte[] divData, byte[] ivData, int plainDataLen, byte[] plainData, byte[] enData, ref int enDataLen);
        [DllImport("TZhuZhanDll.dll")]
        private static extern int DeData(int sConnect, int keyVersion, int keyIndex, int divTimes, byte[] divData, byte[] ivData, int plainDataLen, byte[] plainData, byte[] enData, ref int enDataLen);
        [DllImport("TZhuZhanDll.dll")]
        private static extern int GetUpdateKey(int sConnect, int oldKeyVersion, int NewKeyVersion, int divTimes, byte[] divData, byte[] ivData, int asKid, byte[] updateKeyData, ref int olen);

        public override int Secret_ConnectHSM(string cHostIp, int inPort, int timeout)
        {         
            Monitor.Enter(m_SecretLock);
            int result = ConnectHSM(cHostIp, inPort, timeout);
            Monitor.Exit(m_SecretLock);
            return result;
        }
        public override int Secret_DisConnect(int sConnect)
        {
            Monitor.Enter(m_SecretLock);
            int result= DisConnect(sConnect);
            Monitor.Exit(m_SecretLock);
            return result;
        }
        public override int Secret_GenRand(int sConnect, int sRandLen, Byte[] random)
        {
            Monitor.Enter(m_SecretLock);
            int result = GenRand(sConnect, sRandLen, random);
            Monitor.Exit(m_SecretLock);
            return result;
            
        }
        public override int Secret_InternalSign(int sConnect, int keyIndex, int inDataLen, Byte[] inData, Byte[] sData, ref int sDataLen)
        {
            Monitor.Enter(m_SecretLock);
            int result = InternalSign(sConnect, keyIndex, inDataLen, inData, sData, ref sDataLen);
            Monitor.Exit(m_SecretLock);
            return result;
             
        }
        public override int Secret_VerSign(int sConnect, int cerLen, Byte[] cer, int oriDataLen, Byte[] oriData, int sDataLen, Byte[] sData)
        {
            Monitor.Enter(m_SecretLock);
            int result = VerSign(sConnect, cerLen, cer, oriDataLen, oriData, sDataLen, sData);
            Monitor.Exit(m_SecretLock);
            return result;
             
             
        }
        public override int Secret_EnData(int sConnect, int keyVersion, int keyIndex, int divTimes, Byte[] divData, Byte[] ivData, int plainDataLen, Byte[] plainData, Byte[] enData, ref int enDataLen)
        {
            Monitor.Enter(m_SecretLock);
            int result =EnData(sConnect, keyVersion, keyIndex, divTimes, divData, ivData, plainDataLen, plainData, enData, ref enDataLen);
            Monitor.Exit(m_SecretLock);
            return result;
             
             
        }
        public override int Secret_DeData(int sConnect, int keyVersion, int keyIndex, int divTimes, Byte[] divData, Byte[] ivData, int enDataLen, Byte[] enData, Byte[] plainData, ref int plainDataLen)
        {
            Monitor.Enter(m_SecretLock);
            int result =  DeData(sConnect, keyVersion, keyIndex, divTimes, divData, ivData, enDataLen, enData, plainData, ref plainDataLen);
            Monitor.Exit(m_SecretLock);
            return result;
             
            
        }
        public override int Secret_GetUpdateKey(int sConnect, int oldKeyVersion, int NewKeyVersion, int divTimes, Byte[] divData, Byte[] ivData, int asKid, Byte[] updateKeyData, ref int olen)
        {
            Monitor.Enter(m_SecretLock);
            int result = GetUpdateKey(sConnect, oldKeyVersion, NewKeyVersion, divTimes, divData, ivData, asKid, updateKeyData, ref olen);
            Monitor.Exit(m_SecretLock);
            return result;
             
            
        }
        public override int Secret_GetCertInfoYW(int sConnect, byte[] certID, byte[] certData, ref int certDataLen)
        {
            return -1;
        }
        public override int Secret_SignYW(int sConnect, int inDataLen, byte[] inData, byte[] sData, ref int sDataLen, int keyIndex)
        {
            Monitor.Enter(m_SecretLock);
            int result = InternalSign(sConnect, keyIndex, inDataLen, inData, sData, ref sDataLen);
            Monitor.Exit(m_SecretLock);
            return result;
             
           
        }
        public override int Secret_EnDataYW(int sConnect, int keyVersion, int keyIndex, int divTimes, byte[] divData, byte[] ivData, int plainDataLen, byte[] plainData, byte[] enData, ref int enDataLen)
        {
            return -1;
        }
        public override int Secret_DeDataYW(int sConnect, int keyVersion, int keyIndex, int divTimes, byte[] divData, byte[] ivData, int enDataLen, byte[] enData, byte[] plainData, ref int plainDataLen)
        {
            return -1;
        }
        public override int Secret_GetUpdateKeyYW(int sConnect, int oldKeyVersion, int NewKeyVersion, int divTimes, byte[] divData, byte[] ivData, int asKid, byte[] updateKeyData, ref int olen)
        {
            return -1;
        }
        public override bool Secret_GetYW_CertAndID(ref byte[] pCertBuff, ref int nCertLeng, ref byte[] pIDBuff, ref int nIDLeng)
        {
            CEnCodePtr.m_YWDowFileType = 1;
            CEnCodePtr.m_Cerfilename =  "YW_certi.txt";
            CEnCodePtr.m_CerBiaosi = 7;

            pIDBuff = new byte[8];
            nIDLeng = 8;
            if (CGlobal.m_SecretEncode.GetRand(pIDBuff) == false) return false;
            CEnCodePtr.SetRandData(pIDBuff, 0, 8, 9);

            nCertLeng = CEnCodePtr.ReadCerfileCmd(ref pCertBuff, CEnCodePtr.m_Cerfilepath + CEnCodePtr.m_Cerfilename);
            
            return true;
        }
    }

    public class C12SecretInterface : CSecretInterface
    {
        [DllImport("HsmPriDll.dll")]
        public static extern  int ConnectHSM(string cHostIp, int inPort, int timeout);
        [DllImport("HsmPriDll.dll")]
        public static extern int DisConnect(int sConnect);
        [DllImport("HsmPriDll.dll")]
        public static extern int GenRand(int sConnect, int sRandLen, Byte[] random);
        [DllImport("HsmPriDll.dll")]
        public static extern int InternalSign(int sConnect, int keyIndex, int inDataLen, Byte[] inData, Byte[] sData, ref int sDataLen);
        [DllImport("HsmPriDll.dll")]
        public static extern int VerSign(int sConnect, int cerLen, Byte[] cer, int oriDataLen, Byte[] oriData, int sDataLen, Byte[] sData);
        [DllImport("HsmPriDll.dll")]
        public static extern int EnData(int sConnect, int keyVersion, int keyIndex, int divTimes, Byte[] divData, Byte[] ivData, int plainDataLen, Byte[] plainData, Byte[] enData, ref int enDataLen);
        [DllImport("HsmPriDll.dll")]
        public static extern int DeData(int sConnect, int keyVersion, int keyIndex, int divTimes, Byte[] divData, Byte[] ivData, int enDataLen, Byte[] enData, Byte[] plainData, ref int plainDataLen);
        [DllImport("HsmPriDll.dll")]
        public static extern int GetUpdateKey(int sConnect, int oldKeyVersion, int NewKeyVersion, int divTimes, Byte[] divData, Byte[] ivData, int asKid, Byte[] updateKeyData, ref int olen);
        [DllImport("HsmPriDll.dll")]
        public static extern int GetCertInfoYW(int sConnect, byte[] certID, byte[] certData, ref int certDataLen);
       [DllImport("HsmPriDll.dll")]
        public static extern int SignYW(int sConnect, int inDataLen, byte[] inData, byte[] sData, ref int sDataLen);
        [DllImport("HsmPriDll.dll")]
        public static extern int EnDataYW(int sConnect, int keyVersion, int keyIndex, int divTimes, byte[] divData, byte[] ivData, int plainDataLen, byte[] plainData, byte[] enData, ref int enDataLen);
        [DllImport("HsmPriDll.dll")]
        public static extern int DeDataYW(int sConnect, int keyVersion, int keyIndex, int divTimes, byte[] divData, byte[] ivData, int enDataLen, byte[] enData, byte[] plainData, ref int plainDataLen);
        [DllImport("HsmPriDll.dll")]
        public static extern int GetUpdateKeyYW(int sConnect, int oldKeyVersion, int NewKeyVersion, int divTimes, byte[] divData, byte[] ivData, int asKid, byte[] updateKeyData, ref int olen);
        [DllImport("HsmPriDll.dll")]
        public static extern int DeAuthData(int sConnect, int keyVersion, int divTimes, byte[] divData, int enDataLen, byte[] enData, Byte[] plainData, ref int plainDataLen);

        public override int Secret_DeAuthData(int sConnect, int keyVersion, int divTimes, byte[] divData, int enDataLen, byte[] enData, Byte[] plainData, ref int plainDataLen)
        {
         return DeAuthData( sConnect,  keyVersion,  divTimes,  divData,  enDataLen,  enData,  plainData, ref  plainDataLen);    
        }
        public override int Secret_ConnectHSM(string cHostIp, int inPort, int timeout)
        {
            return ConnectHSM(cHostIp, inPort, timeout);
        }
        public override int Secret_DisConnect(int sConnect)
        {
            return DisConnect(sConnect);
        }
        public override int Secret_GenRand(int sConnect, int sRandLen, Byte[] random)
        {
            return GenRand(sConnect, sRandLen, random);
        }
        public override int Secret_InternalSign(int sConnect, int keyIndex, int inDataLen, Byte[] inData, Byte[] sData, ref int sDataLen)
        {
            return InternalSign(sConnect, keyIndex, inDataLen, inData, sData, ref sDataLen);
        }
        public override int Secret_VerSign(int sConnect, int cerLen, Byte[] cer, int oriDataLen, Byte[] oriData, int sDataLen, Byte[] sData)
        {
            return VerSign(sConnect, cerLen, cer, oriDataLen, oriData, sDataLen, sData);
        }
        public override int Secret_EnData(int sConnect, int keyVersion, int keyIndex, int divTimes, Byte[] divData, Byte[] ivData, int plainDataLen, Byte[] plainData, Byte[] enData, ref int enDataLen)
        {
            return EnData(sConnect, keyVersion, keyIndex, divTimes, divData, ivData, plainDataLen, plainData, enData, ref enDataLen);
        }
        public override int Secret_DeData(int sConnect, int keyVersion, int keyIndex, int divTimes, Byte[] divData, Byte[] ivData, int enDataLen, Byte[] enData, Byte[] plainData, ref int plainDataLen)
        {
            return DeData(sConnect, keyVersion, keyIndex, divTimes, divData, ivData, enDataLen, enData, plainData, ref plainDataLen);
        }
        public override int Secret_GetUpdateKey(int sConnect, int oldKeyVersion, int NewKeyVersion, int divTimes, Byte[] divData, Byte[] ivData, int asKid, Byte[] updateKeyData, ref int olen)
        {
            return GetUpdateKey(sConnect, oldKeyVersion, NewKeyVersion, divTimes, divData, ivData, asKid, updateKeyData, ref olen);
        }
        public override int Secret_GetCertInfoYW(int sConnect, byte[] certID, byte[] certData, ref int certDataLen)
        {
            return GetCertInfoYW(sConnect, certID, certData, ref certDataLen);
        }
        public override int Secret_SignYW(int sConnect, int inDataLen, byte[] inData, byte[] sData, ref int sDataLen, int keyIndex)
        {
            return SignYW(sConnect, inDataLen, inData, sData, ref sDataLen);
        }
        public override int Secret_EnDataYW(int sConnect, int keyVersion, int keyIndex, int divTimes, byte[] divData, byte[] ivData, int plainDataLen, byte[] plainData, byte[] enData, ref int enDataLen)
        {
            return EnDataYW(sConnect, keyVersion, keyIndex, divTimes, divData, ivData, plainDataLen, plainData, enData, ref enDataLen);
        }
        public override int Secret_DeDataYW(int sConnect, int keyVersion, int keyIndex, int divTimes, byte[] divData, byte[] ivData, int enDataLen, byte[] enData, byte[] plainData, ref int plainDataLen)
        {
            return DeDataYW(sConnect, keyVersion, keyIndex, divTimes, divData, ivData, enDataLen, enData, plainData, ref plainDataLen);
        }
        public override int Secret_GetUpdateKeyYW(int sConnect, int oldKeyVersion, int NewKeyVersion, int divTimes, byte[] divData, byte[] ivData, int asKid, byte[] updateKeyData, ref int olen)
        {
            return GetUpdateKeyYW(sConnect, oldKeyVersion, NewKeyVersion, divTimes, divData, ivData, asKid, updateKeyData, ref olen);
        }
        public override bool Secret_GetYW_CertAndID(ref byte[] pCertBuff, ref int nCertLeng, ref byte[] pIDBuff, ref int nIDLeng)
        {
            string strCErt = "证书\\YW_Cert.txt";
            string strID = "证书\\YW_ID.txt";
            if (File.Exists(strCErt) == false) return false;
            if (File.Exists(strID) == false) return false;

            CEnCodePtr.m_YWDowFileType = 1;
            CEnCodePtr.UpdateCerFileMsg(7);
            //CEnCodePtr.m_CerBiaosi = 7;

            nCertLeng = CEnCodePtr.ReadCerfileCmd(ref pCertBuff, strCErt);
            nIDLeng = CEnCodePtr.ReadCerfileCmd(ref pIDBuff, strID);
            CEnCodePtr.SetRandData(pIDBuff, 0, 8, 9);
            /*pIDBuff = new byte[8];
            nIDLeng = 8;
            if (CGlobal.m_SecretEncode.GetRand(pIDBuff) == false) return false;
            CEnCodePtr.SetRandData(pIDBuff, 0, 8, 9);*/
            return true;
        }
    }
}
