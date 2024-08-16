using Ayniyat.Dal.Abstract;
using Ayniyat.Models.Dtos;
using Ayniyat.Models.Entities;
using Ayniyat.Models.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Ayniyat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        IKullaniciDal _kullaniciDal;
        public IConfiguration _configuration;
        private TokenOptions _tokenOptions;
        public KullaniciController(IKullaniciDal kullaniciDal,IConfiguration configuration)
        {
            _kullaniciDal = kullaniciDal;
            _configuration = configuration;
            _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        #region login_islemleri
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginFormDto loginFormDto)
        {
            var kullanici = await _kullaniciDal.Getir(loginFormDto.KullaniciAdi);
            if (kullanici == null) 
            {
                return BadRequest("Kullanıcı bulunamadı");
            }
            //ldap'ta böyle bir kullanıcı var mı 
            
            var tokenString=TokenUret(kullanici);
            ResponseLoginDto responseLoginDto = new ResponseLoginDto
            {
                Eposta=kullanici.Eposta,
                KullaniciId=kullanici.Id,
                RolId=kullanici.RolId,
                Token=tokenString,
                Ad=kullanici.Ad,
                Soyad=kullanici.Soyad,
            };

            return Ok(responseLoginDto);
        }



        [AllowAnonymous]
        private string TokenUret(Kullanici kullanici)
        {
            DateTime TokenBitis = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, kullanici.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, kullanici.Eposta));
            claims.Add(new Claim(ClaimTypes.Name, kullanici.Ad));
            claims.Add(new Claim(ClaimTypes.Surname, kullanici.Soyad));
            claims.Add(new Claim(ClaimTypes.Role, kullanici.Rol.Ad));
          
            var jwt = new JwtSecurityToken(
              issuer: _tokenOptions.Issuer,
              audience: _tokenOptions.Audience,
              expires: TokenBitis,
              notBefore: DateTime.Now,
              claims: claims,
              signingCredentials: signingCredentials
          );

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            return token;
        }

        #endregion

        #region personel_islemleri

        [HttpGet("getir")]
        //[Authorize(Roles ="SisYon,Admin")]
        public async Task<IActionResult> Getir(int id)
        {
            var kullanici=await _kullaniciDal.Getir(id);
            if (kullanici == null)
            {
                return NotFound();
            }

            KullaniciDto kullaniciDto = new KullaniciDto
            {
                Ad = kullanici.Ad,
                Eposta = kullanici.Eposta,
                Aktifmi=kullanici.Aktifmi,
                Id=id,
                Soyad=kullanici.Soyad,
                SubeId=kullanici.SubeId,
                Unvan=kullanici.Unvan,
            };

            return Ok(kullaniciDto);
        }

        [HttpPost("listeGetir")]
        public async Task<IActionResult> ListeGetir([FromBody] KullaniciAraKriterDto kriterDto)
        {



            return Ok(kriterDto);
        }
        #endregion
    }
}
