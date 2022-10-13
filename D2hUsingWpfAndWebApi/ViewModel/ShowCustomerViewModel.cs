using D2hUsingWpfAndWebApi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using D2hUsingWpfAndWebApi.Command;
using System.Configuration;

namespace D2hUsingWpfAndWebApi.ViewModel
{
    public class ShowCustomerViewModel : INotifyPropertyChanged
    {
        string url = ConfigurationManager.AppSettings["URL"];
        HttpClient httpClient;
        private string _name;

        public string Name
        {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged("Name");
            }
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
        private List<Customer> _customers;
        public List<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                OnPropertyChanged("Customers");
            }
        }

        private string _id="";
        public string Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("Id"); }
        }

        private Package _highlighted;

        public Package Highlighted
        {
            get { return _highlighted; }
            set { _highlighted = value; OnPropertyChanged("Highlighted"); }
        }

        private Visibility _visibility = Visibility.Hidden;

        public Visibility Visibility
        {
            get { return _visibility; }
            set { _visibility = value; OnPropertyChanged("Visibility"); }
        }

     


        public ICommand SubmitCommand { get; set; }
        public ICommand CustomerAdd { get; set; }
        public ICommand SearchCommand { get; set; }

        public ShowCustomerViewModel()
        {
                      
            SubmitCommand = new BaseButtonCommand(SubmitMethod);
            CustomerAdd = new BaseButtonCommand(CustomerAddMethod);
            SearchCommand = new BaseButtonCommand(SearchCustomerByPackage);

            httpClient = new HttpClient();
            var result = httpClient.GetAsync($"{url}/api/Package").Result;
            var pkg = result.Content.ReadAsAsync<IEnumerable<Package>>().Result;
            Packages = pkg.ToList();
        }
       
        public void SubmitMethod()
        {
            var result1 = httpClient.GetAsync($"{url}/api/Customer?username={Name}").Result;
            var cust= result1.Content.ReadAsAsync<IEnumerable<Customer>>().Result;
            Customers = cust.ToList();
            this.Visibility = Visibility.Visible;

        }
        public void CustomerAddMethod()
        {
            var Customer = (AddCustomer)Application.Current.Resources["AddCustomer"];
            Customer.Visibility = Visibility.Visible;
        }

        public void SearchCustomerByPackage()
        {
            MessageBox.Show(Highlighted.Id.ToString());
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
