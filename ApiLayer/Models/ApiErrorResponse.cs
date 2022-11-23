using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Environment.Model.Enums;

namespace ApiLayer.Models
{
    public class ApiErrorResponse
    {
        public SystemResponseCodeEnum ResponseCode { get; set; }
        public string Message { get; set; }
    }
}