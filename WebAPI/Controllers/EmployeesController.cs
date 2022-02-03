using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebAPI.Model;
using WebAPI.Repository.Data;
using WebAPI.ViewModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository,string>//string?tipe data PK
    {
        public EmployeeRepository employerepo;
        public EmployeesController(EmployeeRepository employeeRepository):base(employeeRepository)
        {
           this.employerepo = employeeRepository;
        }

        [HttpPost("{RegisterVM}")]

        public IActionResult Register(RegisterVM registervm)
        {
            var result = employerepo.Register(registervm);
            if (result == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Berhasil di insert" });
            } else if (result == 2)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Nomor HP sudah ada" });
            }
            else if(result == 3)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email dan hp sudah ada" });
            }
            else if (result ==4)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email sudah ada" });
            }
            return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Insert gagal" });

        }
        [Route("registeredData")]
        [HttpGet]
        //[Route("api/[controller]/registeredData")]

        
        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("test cors berhasil");
        }


        public ActionResult RegisteredData()
        {
            var result = employerepo.GetRegister();
            if (result !=null ) 
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "data berhasil ditampilkan" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.NotFound, result, message = "Data kosong" });
            }
            
        }
    }
}
