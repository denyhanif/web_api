using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Context;
//using WebAPI.Base;
using WebAPI.Model;
using WebAPI.Repository;
using WebAPI.Repository.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : BaseController<University,UniversityRepository,int>
    {
        public UniversitiesController(UniversityRepository universityrepository) : base(universityrepository)
        {

        }
    }
}
