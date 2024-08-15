using Ayniyat.Dal.Abstract;
using Ayniyat.Models.Dtos;
using Ayniyat.Models.Entities;
using Ayniyat.Models.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginFormDto loginFormDto)
        {
            var kullanici = await _kullaniciDal.Getir(loginFormDto.KullaniciAdi);
            if (kullanici == null) 
            {
                return BadRequest("Kullanıcı bulunamadı");
            }
            //ldap'ta böyle bir kullanıcı var mı 


        }



        [AllowAnonymous]
        public string TokenUret(Kullanici kullanici)
        {
            DateTime TokenBitis = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        }
    }
}
