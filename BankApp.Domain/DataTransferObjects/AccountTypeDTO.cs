using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Enteties.DataTransferObjects
{
    public class AccountTypeDTO
    {
        public int AccountTypesId { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
    }
}