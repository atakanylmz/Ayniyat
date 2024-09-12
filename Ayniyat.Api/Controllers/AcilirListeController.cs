using Ayniyat.Dal.Abstract;
using Ayniyat.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ayniyat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcilirListeController : ControllerBase
    {
        private IKullaniciDal _kullaniciDal;
        private ISubeDal _subeDal;

        public AcilirListeController(IKullaniciDal kullaniciDal,ISubeDal subeDal)
        {
            _kullaniciDal = kullaniciDal;
            _subeDal = subeDal;
        }

        [HttpGet( "kullanicilistesigetir")]
        [Authorize(Roles = "SisYon,Admin")]

        public async Task<IActionResult> KullaniciListesiGetir(int subeId)
        {
            var kullaniciListesi=await _kullaniciDal.ListeGetir(new KullaniciAraKriterDto { SubeId = subeId,Aktifmi=true });
            List<AcilirListeOgeDto> acilirListeElemanlari=kullaniciListesi.Select(x=>new AcilirListeOgeDto 
            {
                Text=x.Ad+" "+x.Soyad,
                Id=x.Id,
            }).ToList();

            return Ok(acilirListeElemanlari);
        }

        [HttpGet("subelistesigetir")]
        [Authorize(Roles = "SisYon,Admin")]

        public async Task<IActionResult> SubeListesiGetir()
        {
            var currentUserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var currentUser = await _kullaniciDal.SubeyleGetir(currentUserId);
            if (currentUser == null||currentUser.Sube==null)
                return NotFound();

            int daireId = currentUser.Sube.DaireId;
           

            var subeListesi = await _subeDal.TumunuGetir();
            List<AcilirListeOgeDto> acilirListeElemanlari = subeListesi.Where(x => x.DaireId == daireId).OrderBy(x=>x.Ad).Select(x => new AcilirListeOgeDto
            {
                Text = x.Ad,
                Id = x.Id,
            }).ToList();
            //acilirListeElemanlari.Insert(0, new AcilirListeOgeDto { Id = 0, Text = "Tümü" });
           
            return Ok(acilirListeElemanlari);
        }
    }
}
