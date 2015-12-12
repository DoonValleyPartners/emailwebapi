using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ysi.tools.email.webapi.App_BL
{
    public class JsonWebToken
    {
        static JsonWebToken _jsonWebToken = new JsonWebToken();

        private JsonWebToken()
        { }

        public static JsonWebToken Instance { get { return _jsonWebToken; } }

        public string GetToken(object payload, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException("apiKey");

            byte[] keyBytes = Encoding.UTF8.GetBytes(apiKey);
            var segments = new List<string>();
            var header = new { alg = "sha256", typ = "JWT" };

            byte[] headerBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(header, Formatting.None));
            byte[] payloadBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload, Formatting.None));

            segments.Add(Base64UrlEncode(headerBytes));
            segments.Add(Base64UrlEncode(payloadBytes));

            var stringToSign = string.Join(".", segments.ToArray());

            var bytesToSign = Encoding.UTF8.GetBytes(stringToSign);
            byte[] signature;
            using (var sha = new HMACSHA256(keyBytes))
            {
                signature = sha.ComputeHash(bytesToSign);
                segments.Add(Base64UrlEncode(signature));
            }
            return string.Join(".", segments.ToArray());
        }

        public bool VerifyToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return false;

            var parts = token.Split('.');
            if (parts.Length < 3)
                return false;

            var header = parts[0];
            var payload = parts[1];
            var payloadJson = Encoding.UTF8.GetString(Base64UrlDecode(payload));
            var payloadData = JObject.Parse(payloadJson);
            JToken jToken;
            if (payloadData.TryGetValue("exp", StringComparison.OrdinalIgnoreCase, out jToken))
            {
                DateTime tokenDateTime;
                if (DateTime.TryParse(jToken.ToString(), out tokenDateTime))
                {
                    double minutesDiff = tokenDateTime.Subtract(DateTime.UtcNow).TotalMinutes;
                    if (minutesDiff <= 0)
                        throw new JsonTokenExpireException();
                }
            }
            jToken = null;
            string key = string.Empty;
            if (payloadData.TryGetValue("key", StringComparison.OrdinalIgnoreCase, out jToken))
            {
                key = jToken.ToString();
            }
                byte[] crypto = Base64UrlDecode(parts[2]);

            //var headerJson = Encoding.UTF8.GetString(Base64UrlDecode(header));
            //var headerData = JObject.Parse(headerJson);

            var bytesToSign = Encoding.UTF8.GetBytes(string.Concat(header, ".", payload));
            var keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] signature;
            using (var sha = new HMACSHA256(keyBytes))
            {
                signature = sha.ComputeHash(bytesToSign);
            }
            string decodedCrypto = Convert.ToBase64String(crypto);
            string decodedSignature = Convert.ToBase64String(signature);

            return !string.IsNullOrWhiteSpace(decodedCrypto) && decodedCrypto.Equals(decodedSignature);
        }

        string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0];
            output = output.Replace('+', '-');
            output = output.Replace('/', '_');
            return output;
        }

        byte[] Base64UrlDecode(string input)
        {
            var output = input;
            output = output.Replace('-', '+');
            output = output.Replace('_', '/');
            int count = output.Length % 4;
            for (; count < 4 && count > 0; count++)
            {
                output += "=";
            }
            var converted = Convert.FromBase64String(output);
            return converted;
        }
    }

    public class JsonTokenExpireException : ApplicationException
    {
    }
}