using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.IdentityModels
{
    public record IdentityRegModel(string Email, bool NewAdmin);    
}
