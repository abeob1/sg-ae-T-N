using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace AE_TnN_Mobile_BLL
{
    public class clsEncryptDecrypt
    {
        private void EncryptFile(string inputFile, string outputFile)
        {
            try
            {
                string password = @"myKey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);

                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch 
            {
                //MessageBox.Show("Encryption failed!", "Error");
            }
        }

        private void DecryptFile(string inputFile, string outputFile)
        {

            {
                string password = @"myKey123"; // Your Key Here

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();

            }
        }

        //private void Encrypt(string inputFilePath, string outputfilePath)
        //{

        //    try
        //    {
        //        string EncryptionKey = "MAKV2SPBNI99212";
        //        using (Aes encryptor = Aes.Create())
        //        {
        //            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
        //        0x49,
        //        0x76,
        //        0x61,
        //        0x6e,
        //        0x20,
        //        0x4d,
        //        0x65,
        //        0x64,
        //        0x76,
        //        0x65,
        //        0x64,
        //        0x65,
        //        0x76
        //    });
        //            encryptor.Key = pdb.GetBytes(32);
        //            encryptor.IV = pdb.GetBytes(16);
        //            using (FileStream fs = new FileStream(outputfilePath, FileMode.Create))
        //            {
        //                using (CryptoStream cs = new CryptoStream(fs, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //                {
        //                    using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
        //                    {
        //                        int data = 0;
        //                        while ((Assign(data, fsInput.ReadByte())) != -1)
        //                        {
        //                            cs.WriteByte(Convert.ToByte(data));
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}


        //private void Decrypt(string inputFilePath, string outputfilePath)
        //{
        //    string EncryptionKey = "MAKV2SPBNI99212";
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
        //    0x49,
        //    0x76,
        //    0x61,
        //    0x6e,
        //    0x20,
        //    0x4d,
        //    0x65,
        //    0x64,
        //    0x76,
        //    0x65,
        //    0x64,
        //    0x65,
        //    0x76
        //});
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (FileStream fs = new FileStream(inputFilePath, FileMode.Open))
        //        {
        //            using (CryptoStream cs = new CryptoStream(fs, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
        //            {
        //                using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
        //                {
        //                    int data = 0;
        //                    while ((Assign(data, cs.ReadByte())) != -1)
        //                    {
        //                        fsOutput.WriteByte(Convert.ToByte(data));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //private static T Assign<T>(ref T source, T value)
        //{
        //    source = value;
        //    return value;
        //}
    }
}
