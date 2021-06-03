using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using challenge.Service;

namespace challenge.Controller
    //Author: TallesC Repository async
{
    [ApiController]
    [Route("api/repositories")]
    
    public class RepositoryController : ControllerBase
    {
  
        public async Task<String> Get()
        {
    
            return await DataService.ProcessRepositories();
        }

    }
}

