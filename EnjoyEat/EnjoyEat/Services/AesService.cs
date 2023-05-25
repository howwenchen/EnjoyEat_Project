using System.Security.Cryptography;
using System.Text;

namespace EnjoyEat.Services
{
    public class AesService
    {
        //AES加密且轉為16進位
        public static string AesEncryptToHex(string input, string key, string iv)
        {
            using Aes aes = Aes.Create();//建立加密物件
            //string轉型到bytes
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

            aes.Mode = CipherMode.CBC;// 加密模式：密碼區塊鏈結 (Cipher Block Chaining，CBC) 
            aes.Padding = PaddingMode.PKCS7;//指定填補類型，在訊息資料區塊少於密碼編譯作業所需的位元組之全部數目時套用。
            aes.Key = keyBytes;
            aes.IV = ivBytes;

            ICryptoTransform encryptor = aes.CreateEncryptor();//使用加密物件的加密器方法
            byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            //(加密物件，開始座標，用來加密的總位元數）

            string hexResult = BitConverter.ToString(encryptedBytes).Replace("-", "").ToLower();//轉回16進位連續小寫資料
            return hexResult;
        }
        //去掉資料後填充的位元組(取得回應後需先執行這個再解密)
        public byte[] RemovePKCS7Padding(byte[] data) 
        {
            int indexLength = data[data.Length - 1];
            var outputData = new byte[data.Length - indexLength];
            Buffer.BlockCopy(data, 0, outputData, 0, outputData.Length);
            return outputData;
        }
        //AES解密
        public static string AesDecryptFromHex(string hexInput, string key, string iv)
        {
            byte[] inputBytes = ToByteArray(hexInput);

            using Aes aes = Aes.Create(); 
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = keyBytes;
            aes.IV = ivBytes;

            using ICryptoTransform decryptor = aes.CreateDecryptor();//引用解密器

            byte[] decryptedBytes = decryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            string decryptedString = Encoding.UTF8.GetString(decryptedBytes);//解密完轉型回字串
            return decryptedString;
        }

        public static byte[] ToByteArray(string hexString)
        {
            int length = hexString.Length / 2;
            byte[] byteArray = new byte[length];
            for (int i = 0; i < length; i++)
            {
                byteArray[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return byteArray;
        }

        //產生檢查碼(SHA256)
        public static string AddSHA256CheckCode(string input, string hashKey, string hashIV)
        {
            string temp = $"{hashKey}&{input}&{hashIV}";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(temp));
                string result = BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();//SHA不分大小寫，此處照文件格式
                temp += $":{result}";
            }

            return temp;

        }
    }
}
