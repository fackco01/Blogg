using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Service
{
    public class BloggService
    {
        // Tạo token JWT từ danh sách claims
        public string CreateToken(List<Claim> authClaims, IConfiguration config)
        {
            // Kiểm tra cấu hình
            if (string.IsNullOrEmpty(config["JWT:Key"]))
                throw new Exception("JWT Key is not configured.");
            if (string.IsNullOrEmpty(config["JWT:Issuer"]))
                throw new Exception("JWT Issuer is not configured.");
            if (string.IsNullOrEmpty(config["JWT:Audience"]))
                throw new Exception("JWT Audience is not configured.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                expires: DateTime.UtcNow.AddMinutes(1),
                claims: authClaims,
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Mã hóa chuỗi với AES
        public string EncryptString(string plainText, IConfiguration config)
        {
            byte[] iv;
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.GenerateIV(); // Tạo IV ngẫu nhiên
                iv = aes.IV; // Lưu IV
                var key = Encoding.UTF8.GetBytes(config["security_key"]);
                aes.Key = key;

                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        // Ghi IV vào đầu chuỗi mã hóa
                        memoryStream.Write(iv, 0, iv.Length);

                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                            {
                                streamWriter.Write(plainText);
                            }
                            array = memoryStream.ToArray();
                        }
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        // Giải mã chuỗi với AES
        public string DecryptString(string cipherText, IConfiguration config)
        {
            // Chuyển đổi chuỗi mã hóa từ base64
            byte[] buffer = Convert.FromBase64String(cipherText);
            byte[] iv = new byte[16]; // IV kích thước 16 byte

            // Lấy IV từ dữ liệu mã hóa
            Array.Copy(buffer, iv, iv.Length);

            using (Aes aes = Aes.Create())
            {
                var key = Encoding.UTF8.GetBytes(config["security_key"]);
                aes.Key = key;
                aes.IV = iv; // Thiết lập IV

                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    using (MemoryStream memoryStream = new MemoryStream(buffer, iv.Length, buffer.Length - iv.Length))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader(cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
    }
}