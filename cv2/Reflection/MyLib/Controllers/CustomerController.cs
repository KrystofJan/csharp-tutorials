using MyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.Controllers
{
    class CustomerController
    {

        private static List<Customer> customers = new List<Customer>()
        {
            new Customer(){ Id = 1, Age = 34, IsActive = true, Name = "Jan" },
            new Customer(){ Id = 2, Age = 46, IsActive = false, Name = "Tom" },
            new Customer(){ Id = 3, Age = 36, IsActive = true, Name = "Michala" }
        };


        public string Add(string Name, int Age, bool IsActive) {
            int id = customers.Select(x => x.Id).DefaultIfEmpty(0).Max() + 1;
            customers.Add(new Customer {
                Id = id,
                Age= Age,
                IsActive = IsActive,
                Name = Name
            });
            return id.ToString();
        }
        public string List(int limit)
        {
            if (limit > customers.Count) {
                throw new Exception("Limit out of bounds of list");
            }
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < limit; i++) // or foreach (var customer in customers.Take(limit))
            {
                result.AppendLine(customers[i].Name);
            }
            return result.ToString();

        }

        
    }
}
