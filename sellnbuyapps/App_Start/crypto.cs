using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Summary description for crypto
/// </summary>
public class crypto
{
	public crypto()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private byte[] m_KeyByte = { 129, 78, 230, 251, 173, 187, 96, 106, 169, 208, 218, 24, 27, 65, 5, 243, 197, 192, 70, 31, 236, 121, 196, 2, 217, 112, 185, 249, 21, 185, 146, 119 };
    private byte[] m_IVHashByte = { 236, 41, 79, 53, 203, 204, 119, 2, 23, 78, 233, 58, 167, 141, 175, 73 };
    //@param: string sourceString - string to be encrypted
    //@author: Suresh Babu
    //@since: 1.0
    //@version: 1.0
    //@exception: None
    //@comments: The function return encrypted string passed as parameter
    #region Encrypt
    public string EnCrypt(string sourceString)
    {
        try
        {
            if (sourceString.Trim() == "")
                sourceString = "";
            byte[] _StringAr = Encoding.ASCII.GetBytes(sourceString.ToCharArray());
            byte[] _EncodedBytes;
            MemoryStream _MemStream = new MemoryStream();
            RijndaelManaged _RijManaged = new RijndaelManaged();
            CryptoStream _CryptoStr;

            _CryptoStr = new CryptoStream(_MemStream, _RijManaged.CreateEncryptor(m_KeyByte, m_IVHashByte), CryptoStreamMode.Write);
            _CryptoStr.Write(_StringAr, 0, _StringAr.Length);
            _CryptoStr.FlushFinalBlock();
            _EncodedBytes = _MemStream.ToArray();
            _MemStream.Close();
            _CryptoStr.Close();
            return Convert.ToBase64String(_EncodedBytes);
        }
        catch
        {
            return "";
        }
    }
    #endregion
    //@param: string sourceString - string to be decrypted
    //@author: Suresh Babu
    //@since: 1.0
    //@version: 1.0
    //@exception: None
    //@comments: The function return decrypted string passed as parameter
    #region Decrypt
    public string DeCrypt(string sourceString)
    {
        try
        {
            if (sourceString.Trim() == "")
                sourceString = "";
            sourceString = sourceString.Replace(" ", "+");
            byte[] _StringAr = Convert.FromBase64String(sourceString);
            byte[] _InitialText = new byte[_StringAr.Length];
            //Dim _InitialText(_StringAr.Length) As Byte
            RijndaelManaged _RijManaged = new RijndaelManaged();
            MemoryStream _MemStream = new MemoryStream(_StringAr);
            CryptoStream _CryptoStr;
            StringBuilder _StrBuilder;
            int _Counti;
            _CryptoStr = new CryptoStream(_MemStream, _RijManaged.CreateDecryptor(m_KeyByte, m_IVHashByte), CryptoStreamMode.Read);

            _CryptoStr.Read(_InitialText, 0, _InitialText.Length);
            _MemStream.Close();
            _CryptoStr.Close();

            _StrBuilder = new StringBuilder(_InitialText.Length);
            for (_Counti = 0; _Counti <= _InitialText.Length - 1; _Counti++)
            {
                if (_InitialText[_Counti].ToString() != "0")
                {
                    _StrBuilder.Append((Char)_InitialText[_Counti]);
                }
            }
            return _StrBuilder.ToString();
        }
        catch
        {
            return "";
        }
    }
    #endregion
}
