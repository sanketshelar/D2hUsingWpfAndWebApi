using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using D2hUsingWpfAndWebApi.Command;
using D2hUsingWpfAndWebApi.Model;
using Newtonsoft.Json;

namespace D2hUsingWpfAndWebApi.ViewModel
{
    public class AddCustomerViewModel : INotifyPropertyChanged
    {
        HttpClient httpClient;

        string url = ConfigurationManager.AppSettings["URL"];

        private string _lastname;
        public string LastName
        {
            get { return _lastname; }
            set { _lastname = value; OnPropertyChanged("LastName"); }
        }
        private string _username;
        public string UserName
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged("UserName"); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
        }
        private string _fname;

        public string FirstName
        {
            get { return _fname; }
            set { _fname = value; OnPropertyChanged("FirstName"); }
        }


        private List<Package> packages;
        public List<Package> Packages
        {
            get { return packages; }
            set
            {
                packages = value;
                OnPropertyChanged("Packages");
            }
        }

        private List<City> _cities;

        public List<City> Cities
        {
            get { return _cities; }
            set { _cities = value; OnPropertyChanged("Cities"); }
        }

        private List<Group> _groups;

        public List<Group> Groups
        {
            get { return _groups; }
            set { _groups = value; OnPropertyChanged("Groups"); }
        }

        private List<Status> _statuses;
        public List<Status> Status
        {
            get { return _statuses; }
            set { _statuses = value; OnPropertyChanged("Status"); }
        }

        private Package _packagehighlighted;
        public Package PackageHighlighted
        {
            get { return _packagehighlighted; }
            set { _packagehighlighted = value; OnPropertyChanged("PackageHighlighted"); }
        }
     

        private Group _grouphighlighted;
        public Group GroupHighlighted
        {
            get { return _grouphighlighted; }
            set { _grouphighlighted = value; OnPropertyChanged("GroupHighlighted"); }
        }
       

        private City _cityhighlighted;
        public City CityHighlighted
        {
            get { return _cityhighlighted; }
            set { _cityhighlighted = value; OnPropertyChanged("CityHighlighted"); }
        }
       
        private Status _statushighlighted;
        public Status StatusHighlighted
        {
            get { return _statushighlighted; }
            set { _statushighlighted = value; OnPropertyChanged("StatusHighlighted"); }
        }
 

        public ICommand AddCustomer { get; set; }
        public AddCustomerViewModel()
        {
           
            AddCustomer = new BaseButtonCommand(AddCustomerMethod);

            httpClient = new HttpClient();
            var result = httpClient.GetAsync($"{url}/api/Package").Result;
            var pkg = result.Content.ReadAsAsync<IEnumerable<Package>>().Result;
            Packages = pkg.ToList();

            var result1=httpClient.GetAsync($"{url}/api/City").Result.Content.ReadAsAsync<IEnumerable<City>>().Result;
            Cities = result1.ToList();

            var result2= httpClient.GetAsync($"{url}/api/Group").Result.Content.ReadAsAsync<IEnumerable<Group>>().Result;
            Groups = result2.ToList();

            var result3 = httpClient.GetAsync($"{url}/api/Status").Result.Content.ReadAsAsync<IEnumerable<Status>>().Result;
            Status = result3.ToList();
        }

        public void AddCustomerMethod()
        {
          
            Customer c = new Customer();
            c.FirstName = FirstName;
            c.LastName = LastName;
            c.Username = UserName;
            c.Password = Password;
            c.GroupId =  GroupHighlighted.Id;
            c.PackageId = PackageHighlighted.Id;
            c.CityId =CityHighlighted.Id;
            c.StatusId = StatusHighlighted.Id;


            /*var myContent = JsonConvert.SerializeObject(c);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");*/

            var result1 = httpClient.PostAsJsonAsync($"{url}/api/Customer", c).Result;
            result1.EnsureSuccessStatusCode();
          


        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
