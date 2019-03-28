using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;

namespace CaesarCipherService
{
    public class CaesarCipherServiceImpl : CaesarCipher.CaesarCipherBase
    {
        private readonly string _replicaId;
        public CaesarCipherServiceImpl(string replicaId)
        {
            _replicaId = replicaId;
        }

        public override Task<DecryptResponse> DecryptAsync(DecryptRequest request, ServerCallContext context)
        {
            return Task.FromResult(new DecryptResponse()
              {
                  ReplicaId = _replicaId,
                  Result = _Encrypt(request.Str, request.Shift)
              });
        }

        public override Task<EncryptResponse> Encrypt(EncryptRequest request, ServerCallContext context)
        {
            return Task.FromResult(new EncryptResponse()
            {
                ReplicaId = _replicaId,
                Result = _Encrypt(request.Str, request.Shift)
            });
        }

        public char _EncryptCharacter(char ch, int shift)
        {
            if (!char.IsLetter(ch)) return ch;

            // (A, a) => 0; (B, b) => 1; ... 
            char offset = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((ch + shift - offset) % 26 + offset);
        }

        public string _Decrypt(string str, int shift)
        {
            return _Encrypt(str, 26 - shift);
        }

        public string _Encrypt(string str, int shift)
        {
            return new string(str.Select(ch => _EncryptCharacter(ch, shift)).ToArray());
        }
    }
}
