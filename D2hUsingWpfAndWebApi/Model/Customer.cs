using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2hUsingWpfAndWebApi.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string PackageName { get; set; }
        public string CityName { get; set; }
        public string GroupName { get; set; }
        public string StatusName { get; set; }
        public string LastName { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public int PackageId { get; set; }
        public int CityId { get; set; }
        public int GroupId { get; set; }
        public int StatusId { get; set; }
    }
}
