using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Pizzashop.Auth;

public class JwtService
{
    private readonly string _SECRETKEY = "blah123%%=*()234124124";
    public string GenerateToken(string userName)
    {
        var claim = new [] 
        {
             new Claim(JwtRegisteredClaimNames.Sub, userName),
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_SECRETKEY));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer:"https://localhost:7026", 
            audience:"https://localhost:7026", 
            claims: claim,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: cred
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
