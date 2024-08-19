﻿using Ayniyat.Dal.Abstract;
using Ayniyat.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> KullaniciListesiGetir(int subeId)
        {
            var kullaniciListesi=await _kullaniciDal.ListeGetir(new KullaniciAraKriterDto { SubeId = subeId });
            List<AcilirListeOgeDto> acilirListeElemanlari=kullaniciListesi.Select(x=>new AcilirListeOgeDto 
            {
                Text=x.Ad+" "+x.Soyad,
                Id=x.Id,
            }).ToList();

            return Ok(acilirListeElemanlari);
        }

        [HttpGet("subelistesigetir")]
        public async Task<IActionResult> SubeListesiGetir(int daireId)
        {
            var subeListesi = await _subeDal.TumunuGetir();
            List<AcilirListeOgeDto> acilirListeElemanlari = subeListesi.Where(x => x.DaireId == daireId).Select(x => new AcilirListeOgeDto
            {
                Text = x.Ad,
                Id = x.Id,
            }).ToList();
            return Ok(acilirListeElemanlari);
        }
    }
}
