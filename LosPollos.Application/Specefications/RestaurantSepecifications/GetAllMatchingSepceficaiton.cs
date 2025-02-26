using LosPollos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Specefications.RestaurantSepecifications
{
    public class GetAllMatchingSpecification : Specefication<Resturant>
    {
        public GetAllMatchingSpecification(string? searchPhrase)
            : base(!string.IsNullOrEmpty(searchPhrase)
                ? x => x.Description.ToLower().Contains(searchPhrase.ToLower())
                    || x.Name.ToLower().Contains(searchPhrase.ToLower())
                : null!)
        {
        }
    }
}
