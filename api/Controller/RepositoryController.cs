using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using challenge.Models;
using challenge.Service;

namespace challenge.Controller
{
    [ApiController]
    [Route("api/repositories")]
    
    public class RepositoryController : ControllerBase
    {
  
        public async Task<List<Repository>> Get()
        {
    
            return await DataService.ProcessRepositories();
        }

    }
}

