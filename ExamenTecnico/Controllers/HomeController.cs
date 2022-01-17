using ExamenTecnico.Models;
using ExamenTecnico.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ExamenTecnico.Controllers
{

    [Route("contracts")]
    public class HomeController : Controller
    {
        private IMemoryCache _cache;

        public HomeController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult GetContracts()
        {
            return View("Index", GetStoredContracts());
        }

        [HttpPost]
        public IActionResult PostContract([FromBody] ContractRequest contract)
        {
            List<Contract> contracts = GetStoredContracts();

            contracts.Add(new Contract
            {
                Id = Guid.NewGuid().ToString(),
                Description = contract.Description,
                ActiveState = false
            });

            StoreContracts(contracts);
            return Ok(new { Message = $"Contract succesfully created.", Id = contracts.Last().Id });
        }

        [HttpPut("{idContract}")]
        public IActionResult PutContract([FromBody] ContractRequest contract, string idContract)
        {
            List<Contract> contracts = GetStoredContracts();

            contracts.Find(c => c.Id == idContract).ActiveState = contract.ActiveState;

            StoreContracts(contracts);

            return Ok(new { Message = $"Contract id ({idContract}) succesfully updated." });
        }

        private List<Contract> GetStoredContracts()
        {
            if (_cache.Get("contracts") is null)
            {
                var contracts = new List<Contract>
                {
                    new Contract
                    {
                        Id = Guid.NewGuid().ToString(),
                        Description = "test",
                        ActiveState = false
                    },
                    new Contract
                    {
                        Id = Guid.NewGuid().ToString(),
                        Description = "test2",
                        ActiveState = false
                    },
                    new Contract
                    {
                        Id = Guid.NewGuid().ToString(),
                        Description = "test3",
                        ActiveState = false
                    }
                };

                _cache.Set("contracts", contracts);
            }

            return (List<Contract>)_cache.Get("contracts");
        }

        private void StoreContracts(List<Contract> contracts)
        {
            _cache.Set("contracts", contracts);
        }

    }
}
