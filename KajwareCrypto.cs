using System;
using System.Security.Cryptography;

namespace KWHotelDecryptor
{
    public class KajwareCrypto : IDisposable 
    {
        private readonly DES _des;
        
        public KajwareCrypto(byte[] key, byte[] iv)
        {
            _des = DES.Create();
            _des.Key = key;
            _des.IV = iv;
        }

        public byte[] Decrypt(byte[] ciphertext)
        {
            var plaintext = new byte[ciphertext.Length];
            using (var decryptor = _des.CreateDecryptor())
            {
                _ = decryptor.TransformBlock(
                    ciphertext, 0, ciphertext.Length,
                    plaintext, 0);
            }

            return plaintext;
        }
        
        /// <inheritdoc/>
        public void Dispose() => 
            _des.Dispose();
    }
}