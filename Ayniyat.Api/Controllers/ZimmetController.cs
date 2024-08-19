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

        public async Task<IActionResult> Kaydet(ZimmetDto zimmetDto)
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
                    KayitTarihi=DateTime.Now,
                    GuncellemeTarihi=DateTime.Now,         
                };
                await _zimmetDal.Ekle(eklenecekZimmet);
                return Ok();
            }
            else
            {
                var guncellenecekZimmet=await _zimmetDal.Getir(zimmetDto.Id);
                if (guncellenecekZimmet == null)
                    return BadRequest("Zimmet bulunamadı");

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
                    IslemTarihi=DateTime.Now,
                    Miktar=guncellenecekZimmet.Miktar,
                    Model=guncellenecekZimmet.Model,
                    SeriNo=guncellenecekZimmet.SeriNo,
                    StokNo=guncellenecekZimmet.StokNo,
                    TasinirNo=guncellenecekZimmet.TasinirNo,
                    ZimmetId=guncellenecekZimmet.Id,
                };

                await _zimmetDal.Guncelle(guncellenecekZimmet);

                await _zimmetLogDal.Ekle(log);
                return Ok();
            }
        }

        private string ZimmetKarsilastir(Zimmet zimmet, ZimmetDto zimmetDto)
        {
            string aciklama="";
            if (zimmet.Miktar != zimmetDto.Miktar)
                aciklama += "Eki Miktar: " + zimmet.Miktar + ", Yeni Miktar: " + zimmetDto.Miktar + Environment.NewLine;
            if (zimmet.StokNo != zimmetDto.StokNo)
                aciklama += "Eski Stok No: " + zimmet.StokNo + ", Yeni Stok No: " + zimmetDto.StokNo + Environment.NewLine;
            //if(zimmet.TasinirNo!=zimmetDto.TasinirNo)
            return aciklama;
        }
    }
}
