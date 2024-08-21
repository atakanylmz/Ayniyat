using Ayniyat.Dal.Abstract;
using Ayniyat.Models.Dtos;
using Ayniyat.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ayniyat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZimmetController : ControllerBase
    {
        private IZimmetDal _zimmetDal;
        private IKullaniciDal _kullaniciDal;
        private IZimmetLogDal _zimmetLogDal;
        public ZimmetController(IZimmetDal zimmetDal,IKullaniciDal kullaniciDal, IZimmetLogDal zimmetLogDal)
        {
            _zimmetDal = zimmetDal;
            _kullaniciDal = kullaniciDal;
            _zimmetLogDal = zimmetLogDal;
        }

        [HttpGet("getir")]
        public async Task<IActionResult> Getir(int id)
        {
            var zimmet=await _zimmetDal.Getir(id);

            if(zimmet==null)
                return NotFound();

            ZimmetDto zimmetDto = new ZimmetDto
            {
                Aciklama=zimmet.Aciklama,
                Birim=zimmet.Birim,
                MalzemeAd=zimmet.MalzemeAd,
                EnvanterNo=zimmet.EnvanterNo,
                Id=zimmet.Id,
                KullaniciId=zimmet.KullaniciId,
                Miktar=zimmet.Miktar,
                Model=zimmet.Model,
                SeriNo=zimmet.SeriNo,
                StokNo=zimmet.StokNo,
                TasinirNo=zimmet.TasinirNo
            };
            return Ok(zimmetDto);
        }

        [HttpPost("kaydet")]
        public async Task<IActionResult> Kaydet([FromBody] ZimmetDto zimmetDto)
        {
            if (zimmetDto.Id == 0)
            {
                Kullanici? kullanici = await _kullaniciDal.Getir(zimmetDto.KullaniciId);
                if (kullanici == null)
                    return BadRequest("Kullanıcı yok");

                Zimmet eklenecekZimmet = new Zimmet
                {
                    Aciklama = zimmetDto.Aciklama,
                    Birim = zimmetDto.Birim,
                    MalzemeAd = zimmetDto.MalzemeAd,
                    EnvanterNo = zimmetDto.EnvanterNo,
                    Id = zimmetDto.Id,
                    KullaniciId = zimmetDto.KullaniciId,
                    Miktar = zimmetDto.Miktar,
                    Model = zimmetDto.Model,
                    SeriNo = zimmetDto.SeriNo,
                    StokNo = zimmetDto.StokNo,
                    TasinirNo = zimmetDto.TasinirNo,
                    SubeId=kullanici.SubeId,
                    KayitTarihi=DateTime.UtcNow,
                    GuncellemeTarihi=DateTime.UtcNow,         
                };


                await _zimmetDal.Ekle(eklenecekZimmet);

                var log = new ZimmetLog
                {
                    Aciklama = eklenecekZimmet.Aciklama,
                    MalzemeAd = eklenecekZimmet.MalzemeAd,
                    Birim = eklenecekZimmet.Birim,
                    EnvanterNo = eklenecekZimmet.EnvanterNo,
                    IslemTarihi = DateTime.UtcNow,
                    Miktar = eklenecekZimmet.Miktar,
                    Model = eklenecekZimmet.Model,
                    SeriNo = eklenecekZimmet.SeriNo,
                    StokNo = eklenecekZimmet.StokNo,
                    TasinirNo = eklenecekZimmet.TasinirNo,
                    ZimmetId = eklenecekZimmet.Id,
                    Degisenler="Yeni kayıt oluşturuldu"
                };
                await _zimmetLogDal.Ekle(log);

                return Ok();
            }
            else
            {
                var guncellenecekZimmet=await _zimmetDal.Getir(zimmetDto.Id);
                if (guncellenecekZimmet == null)
                    return BadRequest("Zimmet bulunamadı");
                if (guncellenecekZimmet.KaldirilmaTarihi.HasValue)
                    return BadRequest("Kaldirilan ayniyat güncellenemez");

                string zimmetLogAciklama = ZimmetKarsilastir(guncellenecekZimmet, zimmetDto);
                if (string.IsNullOrWhiteSpace(zimmetLogAciklama))
                    return Ok();

                guncellenecekZimmet.Miktar = zimmetDto.Miktar;
                guncellenecekZimmet.StokNo = zimmetDto.StokNo;
                guncellenecekZimmet.TasinirNo=zimmetDto.TasinirNo;
                guncellenecekZimmet.MalzemeAd = zimmetDto.MalzemeAd;
                guncellenecekZimmet.EnvanterNo=zimmetDto.EnvanterNo;
                guncellenecekZimmet.SeriNo=zimmetDto.SeriNo;
                guncellenecekZimmet.Model=zimmetDto.Model;
                guncellenecekZimmet.Aciklama=zimmetDto.Aciklama;

                var log = new ZimmetLog
                {
                    Aciklama =guncellenecekZimmet.Aciklama,
                    MalzemeAd=guncellenecekZimmet.MalzemeAd,
                    Birim=guncellenecekZimmet.Birim,
                    EnvanterNo=guncellenecekZimmet.EnvanterNo,
                    IslemTarihi=DateTime.UtcNow,
                    Miktar=guncellenecekZimmet.Miktar,
                    Model=guncellenecekZimmet.Model,
                    SeriNo=guncellenecekZimmet.SeriNo,
                    StokNo=guncellenecekZimmet.StokNo,
                    TasinirNo=guncellenecekZimmet.TasinirNo,
                    ZimmetId=guncellenecekZimmet.Id,
                    Degisenler= zimmetLogAciklama
                };

                await _zimmetDal.Guncelle(guncellenecekZimmet);

                await _zimmetLogDal.Ekle(log);
                return Ok();
            }
        }

        private string ZimmetKarsilastir(Zimmet zimmet, ZimmetDto zimmetDto)
        {
            string degisim="";
          
            if (zimmet.MalzemeAd != zimmetDto.MalzemeAd)
                degisim += "Malzeme Adı, ";
            if (zimmet.Birim != zimmetDto.Birim)
                degisim += "Birim, ";
            if (zimmet.EnvanterNo != zimmetDto.EnvanterNo)
                degisim += "Envanter No, ";
            if (zimmet.Miktar != zimmetDto.Miktar)
                degisim += "Miktar, ";
            if (zimmet.Model != zimmetDto.Model)
                degisim += "Model, ";
            if (zimmet.SeriNo != zimmetDto.SeriNo)
                degisim += "Seri No, ";
            if (zimmet.StokNo != zimmetDto.StokNo)
                degisim += "Stok No, ";
            if (zimmet.TasinirNo != zimmetDto.TasinirNo)
                degisim += "Taşınır No, ";

            degisim = degisim.Remove(degisim.Length - 2, 2);
                return degisim;
        }


        [HttpGet("ayniyatgecmisigetir")]
        public async Task<IActionResult> ZimmetLoglariGetir(int zimmetId)
        {
            var loglar=await _zimmetLogDal.ZimmetLoglariGetir(zimmetId);
            List<ZimmetLogListeOgeDto> dtoList=loglar.Select(x=>new ZimmetLogListeOgeDto
            {
                Aciklama = x.Aciklama,
                MalzemeAd = x.MalzemeAd,
                Birim = x.Birim,
                EnvanterNo = x.EnvanterNo,
                IslemTarihi = DateTime.UtcNow,
                Miktar = x.Miktar,
                Model = x.Model,
                SeriNo = x.SeriNo,
                StokNo = x.StokNo,
                TasinirNo = x.TasinirNo,
                Degisenler = x.Degisenler
            }).ToList();

            return Ok(dtoList );
        }


        /// <summary>
        /// kullaniciId=0 verilirse ZimmetBazlı liste istenmiş olur
        /// subeId=0 verilirse KullanıcıBazlı liste istenmiş olur
        /// </summary>
        /// <param name="kriter"></param>
        /// <returns></returns>
        [HttpPost("listegetir")]
        public async Task<IActionResult> ListeGetir(ZimmetAraKriterDto kriter)
        {
            var liste = await _zimmetDal.ZimmetListesiGetir(kriter);
            return Ok(liste );
        }


        [HttpGet("kaldir")]
        public async Task<IActionResult> Kaldir(int zimmetId)
        {
            Zimmet? kaldirilacakZimmet=await _zimmetDal.Getir(zimmetId);
            if(kaldirilacakZimmet==null)
                return NotFound();
            if(kaldirilacakZimmet.KaldirilmaTarihi.HasValue)
            {
                return BadRequest("Ayniyat daha önce kaldırılmış");
            }
            kaldirilacakZimmet.KaldirilmaTarihi = DateTime.UtcNow;
            await _zimmetDal.Guncelle(kaldirilacakZimmet);

            var log = new ZimmetLog
            {
                Aciklama = kaldirilacakZimmet.Aciklama,
                MalzemeAd = kaldirilacakZimmet.MalzemeAd,
                Birim = kaldirilacakZimmet.Birim,
                EnvanterNo = kaldirilacakZimmet.EnvanterNo,
                IslemTarihi = DateTime.UtcNow,
                Miktar = kaldirilacakZimmet.Miktar,
                Model = kaldirilacakZimmet.Model,
                SeriNo = kaldirilacakZimmet.SeriNo,
                StokNo = kaldirilacakZimmet.StokNo,
                TasinirNo = kaldirilacakZimmet.TasinirNo,
                ZimmetId = kaldirilacakZimmet.Id,
                Degisenler = "Ayniyat kaldırıldı"
            };
            await _zimmetLogDal.Ekle(log);
            return Ok();
        }

    }
}
