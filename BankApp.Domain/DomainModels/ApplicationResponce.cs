using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.DomainModels
{
    public class ApplicationResponce
    {
        public int ResponceCode { get; set; }

        public string ResponceText { get; set; }

        public Object ResponceBody { get; set; }

    }
}
