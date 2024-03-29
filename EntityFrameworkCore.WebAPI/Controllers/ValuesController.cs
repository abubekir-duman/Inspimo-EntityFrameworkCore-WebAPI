﻿using AutoMapper;
using EntityFrameworkCore.WebAPI.Context;
using EntityFrameworkCore.WebAPI.DTOs;
using EntityFrameworkCore.WebAPI.Models;
using EntityFrameworkCore.WebAPI.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;





namespace EntityFrameworkCore.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController(ApplicationDbContext context,IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
           // var response = context.Personels.ToList();//select * from personels
            //select top1 * from personels
            //select * from personels where x=x

            List<Personel> response = context.Personels
                .AsNoTracking()
                .OrderBy(p=>p.FirstName).ToList();//ToList() ve FirstOrDefault()
            //response.Where(s => s.LastName == "Saydam");
            //response.OrderBy(s => s.LastName);
            //response.ToList();

            return Ok(response);
        }

        //private void LinQMethods()
        //{
        //    List<string> names = new();

        //    names.Add("Abubekir Duman");
        //    names.Remove("Abubekir Duman");
        //    //names.Where(p => p.StartsWith("A")).FirstOrDefault();
        //    string? response=names.FirstOrDefault(p => p.StartsWith("A"));
        //    List<string> response2=names.Where(Filter => Filter == "Abubekir").ToList();
        //}


        [HttpPost]
        public IActionResult Create([FromForm] CreatePersonelDto request)
        {
            //CreatePersonelDtoValidator validator = new();

            //ValidationResult validationResult = validator.Validate(request);

            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.Errors.Select(s => s.ErrorMessage));
            //}



            
                bool isEmailExists = context.Personels.Any(p => p.Email == request.Email);
                if (isEmailExists)
                {
                    return BadRequest(new { Message = "Bu mail adresi daha önce kullanılmış" });
                }
            
            


            Personel personel = mapper.Map<Personel>(request);
            //{
            //    Id=Guid.NewGuid(),
            //    FirstName=firstName,
            //    LastName=lastName,
            //    Email=email,

            //}


            context.Personels.Add(personel);
            int result = context.SaveChanges();

            return Ok(new { Message = "Kayıt başarı ile tamamlandı" });
        }





        [HttpPost]
        public IActionResult Update(UpdatePersonelDto request)
        {
            //UpdatePersonelDtoValidator validator = new();

            //ValidationResult validationResult = validator.Validate(request);

            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.Errors.Select(s=>s.ErrorMessage));
            //}




            Personel? personel = context.Personels.FirstOrDefault(p => p.Id == request.Id);
          
            if(personel is null)
            {
                return StatusCode(500, new { Message = "Bu personel kaydı bulunamadı" });
            }


            if (personel.Email != request.Email)
            {
                bool isEmailExists = context.Personels.Any(p => p.Email == request.Email);
                if (isEmailExists)
                {
                    return BadRequest(new { Message = "Bu mail adresi daha önce kullanılmış" });
                }
            }


            mapper.Map(request, personel);

            //context.Personels.Update(personel);
            context.SaveChanges();

            return Ok(new { Message = "Personel kaydı başarı ile güncellendi" });
        }

        [HttpGet("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            Personel? personel = context.Personels.Find(id);
            if(personel is null)
            {
                return StatusCode(500,new  {Message="Bu personel kaydı bulunamadı" });
            }

            context.Personels.Remove(personel);

            context.SaveChanges();

            return Ok(new { Message = "Silme işlemi başarı ile tamamlandı" });

        }




       


    }
}
