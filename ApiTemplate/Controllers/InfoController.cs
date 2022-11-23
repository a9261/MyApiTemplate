using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using ApiLayer.ControllerFilters;
using Application.Merchants;
using Domain.Merchant;

namespace SPG.PaymentPublicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidMerchantRequestFilter]
    [ServiceFilter(typeof(UnBoxingRequestFilter))]
    [BoxingResponseFilter]
    public class InfoController : ControllerBase
    {
        private readonly GetMerchantInfoService _getMerchantInfoService;

        public InfoController(GetMerchantInfoService getMerchantInfoService)
        {
            _getMerchantInfoService = getMerchantInfoService;
        }

        // HTTP Get Info Response OK
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Payment Public API is running");
        }

        [HttpGet]
        [Route("test")]
        public Task<Student> GetTest()
        {
            return Task.FromResult(new Student());
        }

        [HttpPost]
        [Route("test")]
        public Student PostTest([FromBody] Student student)
        {
            student.Name = "Change is Changed " + student.Name;
            return student;
        }
    }

    public class Student
    {
        public string Name { get; set; } = "Jaja";
        public int Age { get; set; } = 18;

        public List<string> Course { get; set; } = new List<string> { "AAA", "BBB" };
    }
}