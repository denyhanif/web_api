using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebAPI.Repository.Interface;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity,Repository,Key> : ControllerBase
        where Entity: class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<Entity> Get()
        {

            var result = repository.Get();
            if (result.Count() < 0)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, messsage = " Data Masih Kosong" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Berhasil Ditampilkan" });
            }
         
        }

        [HttpGet("{key}")]
        public ActionResult<Entity> Get(Key key)
        {
            var result = repository.Get(key);
            if (result == null) 
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, messsage = " Data tidak ditemukan" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Berhasil Ditampilkan" });
            }
           
        }

        [HttpPost]
        public ActionResult<Entity> Post(Entity key)
        {
            var result = repository.Insert(key);
            return Ok(result);

        }

        [HttpDelete]
        public ActionResult<Entity> Delete(Key key)
        {
            var result = repository.Delete(key);
            if (result != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Berhasil DiHapus" });
               
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, messsage = $" Data {key} tidak ditemukan" });
            }
            
        }

        public ActionResult<Entity> Put(Entity entity)
        {


            var result = repository.Insert(entity);
            if (result != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Berhasil Update" });

            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.BadRequest, messsage = " Data Gagal diupdate" });
            }            
        }


    }
}
