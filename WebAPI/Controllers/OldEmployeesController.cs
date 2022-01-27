using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebAPI.Model;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldEmployeesController : ControllerBase
    {
        private OldEmployeeRepository employeeRepository;

        public OldEmployeesController( OldEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost]
        public IActionResult Post(Employee emp)//
        {
            var result = employeeRepository.Insert(emp);
            if (result!=0)
            {
                if(result ==2)
                {
                    return StatusCode(400, new {status = HttpStatusCode.BadRequest,result,message="Email sudah digunakan!" });
                }else if (result == 3)
                {
                    return StatusCode(400, new {status = HttpStatusCode.BadRequest,result,message="Nomor Telepon sudah digunakan!" });
                }else if(result == 4)
                {
                    return StatusCode(400, new {status= HttpStatusCode.BadRequest, result, message="Email dan Nomor Telepon sudah di gunakan" });
                }
                return Ok(new {status = HttpStatusCode.OK, message = "Berhasil di insert" });

            }
            return StatusCode(404,new { status= HttpStatusCode.BadRequest,result,message =" Insert data gagal" });
        }
        [HttpGet]
        public ActionResult Get()
        {
            var result = employeeRepository.Get();
            if (employeeRepository.Get().Count() < 0 )
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, messsage = " Data tidak ditemukan" }); 
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Berhasil Ditampilkan" });
            }
         
        }
        [HttpGet("{NIK}")]
        public ActionResult Get(Employee emp)//
        {
            var result = employeeRepository.Get(emp.NIK);
            if (result !=null)
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Berhasil DiTemukan" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, messsage ="Data tidak ditemukan"});
            }
        }

        [HttpDelete]
        public ActionResult Delete(Employee emp)//
        {
            var result = employeeRepository.Delete(emp.NIK);
            if (result>0)
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Berhasil DiHapus" });
            }
            else
            {
                return StatusCode(404, new{status = HttpStatusCode.BadRequest, result, messsage = "sData tidak ditemukan"});
            }
        }

        [HttpPut]
        public ActionResult Put(Employee emp)
        {
            var result = employeeRepository.Update(emp);
            if (result>0)
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Berhasil di Update" });
            }
            var get = employeeRepository.Get(emp.NIK);
            if (get == null) {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Data Tidak Ditemukan" });
            }
            else
            { 
                return StatusCode(404 , new { status = HttpStatusCode.BadRequest,result,message ="Data Tidak valid" });
            }
        }

    }
}
