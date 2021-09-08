using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindergartenLogger.Server
{
    class Encryption
    {
        public Encryption()
        {
            var rand = new Random();
            byte[] SecretKey = new byte[4];
            rand.NextBytes(SecretKey);
            int SecretKeyInt=BitConverter.ToInt32(SecretKey, 0);
            int P = 1073741824;
            int Q = 737418;
            int PublicKeyInt = (int)(Math.Pow(Q, SecretKeyInt) % P);

        }









        static byte[] Encrypt(byte[] Message, byte[] HashKey)
        {
            byte[] Encrypted = new byte[Message.Length];
            for (int i = 0; i < Message.Length; i++)
            {
                Encrypted[i] = (byte)(Message[i] ^ HashKey[i % HashKey.Length]);
            }
            return Encrypted;
        }

    }
}
