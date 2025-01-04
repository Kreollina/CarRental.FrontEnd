using System.Security.Claims;
using System.Text.Json;

namespace CarRental.FrontEnd.Helpers
{
    public class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            keyValuePairs.TryGetValue("email", out object email);
            keyValuePairs.TryGetValue("role", out object role);

            claims.Add(new Claim(ClaimTypes.Email, email.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));

            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 1:
                    throw new ArgumentException("Invalid Base64 string length.");
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
