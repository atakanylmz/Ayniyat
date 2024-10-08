﻿using Ayniyat.Dal.Abstract;
using Ayniyat.Models.Dtos;
using Ayniyat.Models.Entities;
using Ayniyat.Models.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Security.Claims;

namespace Ayniyat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZimmetController : ControllerBase
    {
        public IConfiguration _configuration;
        private IZimmetDal _zimmetDal;
        private IKullaniciDal _kullaniciDal;
        private IZimmetLogDal _zimmetLogDal;
        private Paths _paths;
        public ZimmetController(IZimmetDal zimmetDal,IKullaniciDal kullaniciDal, IZimmetLogDal zimmetLogDal, IConfiguration configuration)
        {
            _zimmetDal = zimmetDal;
            _kullaniciDal = kullaniciDal;
            _zimmetLogDal = zimmetLogDal;
            _configuration = configuration;
            _paths = configuration.GetSection("Paths").Get<Paths>();
        }

        [HttpGet("getir/{id}")]
        [Authorize(Roles = "SisYon,Admin")]

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
        [Authorize(Roles = "SisYon,Admin")]

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
                guncellenecekZimmet.GuncellemeTarihi = DateTime.UtcNow;
                guncellenecekZimmet.Birim=zimmetDto.Birim;

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
            if (zimmet.Aciklama != zimmetDto.Aciklama)
                degisim += "Açıklama, ";

                if (degisim.Length>2)
            degisim = degisim.Remove(degisim.Length - 2, 2);
                return degisim;
        }


        [HttpGet("ayniyatgecmisigetir/{zimmetId}")]
        [Authorize(Roles = "SisYon,Admin")]

        public async Task<IActionResult> ZimmetLoglariGetir(int zimmetId)
        {
            var loglar=await _zimmetLogDal.ZimmetLoglariGetir(zimmetId);
            List<ZimmetLogListeOgeDto> dtoList=loglar.Select(x=>new ZimmetLogListeOgeDto
            {
                Aciklama = x.Aciklama,
                MalzemeAd = x.MalzemeAd,
                Birim = x.Birim,
                EnvanterNo = x.EnvanterNo,
                IslemTarihi = x.IslemTarihi,
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
        [Authorize(Roles = "SisYon,Admin")]

        public async Task<IActionResult> ListeGetir(ZimmetAraKriterDto kriter)
        {
            var liste = await _zimmetDal.ZimmetListesiGetir(kriter);
            List<ZimmetListeOgeDto> dtoListe=liste.Select(x=>new ZimmetListeOgeDto 
            {
                Id = x.Id,
                StokNo=x.StokNo,
                TasinirNo=x.TasinirNo,
                MalzemeAd=x.MalzemeAd,
                EnvanterNo=x.EnvanterNo,
                Birim=x.Birim,
                Miktar=x.Miktar,
                SeriNo=x.SeriNo,
                Model = x.Model,
                KayitTarihi=x.KayitTarihi,
                GuncellemeTarihi=x.GuncellemeTarihi,
                KaldirilmaTarihi=x.KaldirilmaTarihi,
                Aciklama=x.Aciklama
            }).ToList();
            return Ok(dtoListe );
        }


        [HttpGet("kaldir/{zimmetId}")]
        [Authorize(Roles = "SisYon,Admin")]

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

        [HttpGet("excelindir/{kullaniciId}")]
        [Authorize(Roles = "SisYon,Admin")]

        public async Task<IActionResult> ExcelIndir(int kullaniciId)
        {
            var kullanici = await _kullaniciDal.SubeyleGetir(kullaniciId);
            if (kullanici == null||kullanici.Sube==null)
                return NotFound();

            var currentUserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var currentUser = await _kullaniciDal.SubeyleGetir(currentUserId);
            if (currentUser == null || currentUser.Sube == null||currentUser.Sube.Daire==null)
                return NotFound();

            var mudur=await _kullaniciDal.SubeMuduruGetir(currentUser.SubeId);
            if (mudur == null)
                return NotFound();

            var liste=await _zimmetDal.ZimmetListesiGetir(new ZimmetAraKriterDto { KullaniciId = kullaniciId,Tarih=DateTime.UtcNow });

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var path = Path.Combine(_paths.ExcelFile, "ayniyat.xlsx");
            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }
            
          

            FileInfo fileInfo=new FileInfo(path);
            using (var excelapp = new ExcelPackage(fileInfo))
            {
                DateTime ilkgun = new DateTime(DateTime.UtcNow.Year, 1, 2);
                while (ilkgun.DayOfWeek == DayOfWeek.Saturday || ilkgun.DayOfWeek == DayOfWeek.Sunday)
                    ilkgun = ilkgun.AddDays(1);

                var worksheet = excelapp.Workbook.Worksheets["Sayfa1"];
                for (int i = 0; i < 10; i++)
                {        
                    worksheet.Cells[2 + i * 25, 1].Value = currentUser.Sube.Daire.Ad;
                    worksheet.Cells[3+i*25, 1].Value = kullanici.Sube.Ad;

                    worksheet.Cells[1 + i * 25, 10].Value = ilkgun;
                    worksheet.Cells[2 + i * 25, 10].Value = DateTime.UtcNow;
                }

                int satir = 6;
                int sayac = 0;
                //excelde her 25de bir tekrar ediyor. O yüzden kaç tane 15 lik satır varsa o kadar tekrarlı yazdırılacak alan olacak
                int ciktiSyfaSayisi = (liste.Count / 16)+1;
                int silinecek =25* ciktiSyfaSayisi+1;
                foreach (var zimmet in liste)
                {
                   

                    worksheet.Cells[satir, 2].Value = zimmet.StokNo;
                    worksheet.Cells[satir, 3].Value = zimmet.TasinirNo;
                    worksheet.Cells[satir, 4].Value = zimmet.MalzemeAd+(string.IsNullOrWhiteSpace(zimmet.Model)?"":" ("+zimmet.Model+")");
                    worksheet.Cells[satir, 5].Value = zimmet.EnvanterNo;
                    worksheet.Cells[satir, 6].Value = zimmet.Birim;
                    worksheet.Cells[satir, 7].Value = zimmet.Miktar;
                    worksheet.Cells[satir, 9].Value = zimmet.SeriNo;
                    worksheet.Cells[satir, 10].Value = zimmet.Aciklama;

                    sayac++;
                    if(sayac==15)
                    {
                        sayac = 0;
                        satir = satir + 11;
                    }
                    else
                    satir += 1;
                }
                for (int i = 0; i < 9; i++)
                {
                    worksheet.Cells[23+i*25, 3].Value = kullanici.Unvan;
                    worksheet.Cells[23 + i * 25, 5].Value = kullanici.Ad + " " + kullanici.Soyad;

                    worksheet.Cells[24 + i * 25, 3].Value = currentUser.Unvan;
                    worksheet.Cells[24 + i * 25, 5].Value = currentUser.Ad + " " + currentUser.Soyad;

                    worksheet.Cells[25 + i * 25, 3].Value = mudur.Unvan;
                    worksheet.Cells[25 + i * 25, 5].Value = mudur.Ad + " " + mudur.Soyad;
                }
                worksheet.DeleteRow(silinecek, 252);

                var ms = new MemoryStream();
                excelapp.SaveAs(ms);
                var byteArray=ms.ToArray();

                return File(byteArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }


        }

    }
}
