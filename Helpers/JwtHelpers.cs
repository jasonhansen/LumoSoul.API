using LumoSoul.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LumoSoul.API.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
            
        // tạo JWT token cho user
        public string GenerateToken(User user) {

            // thông tin được lưu vào trong token
            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()), // lưu ID
                new Claim(ClaimTypes.Name, user.Username), // Lưu Username
                new Claim(ClaimTypes.Role, user.Role) // Lưu role
            };

            // key này là để ta tạo khóa bí mật để ký được Token từ appsetting.json -> cái này cần phải đọc thêm tài liệu về bảo mật password
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // dùng thuật toán có sẵn HmacSha512 để bảo mật
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Đây sẽ là định nghĩa của token
            var tokenDescriptor = new SecurityTokenDescriptor 
            {
                Subject = new ClaimsIdentity(claims), // gán các claims ở trên mình lưu vào Token
                Expires = DateTime.Now.AddDays(30), // Thiết lập thời gian hết hạn token (VD: chẳng hạn như mình bấm lưu mật khẩu trên web này - giới hạn là 30 ngày, sau 30 ngày, token đã lưu sẽ hết hạn -> thì user này sẽ auto phải đăng nhập lại)
                SigningCredentials = creds  // gán thông tin Token
            };

            // đây là xử lý token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor); // tạo token từ những thông tin trên đã được định nghĩa

            return tokenHandler.WriteToken(token); // chuyển token về dưới dạng chuỗi
        }

    }
}
