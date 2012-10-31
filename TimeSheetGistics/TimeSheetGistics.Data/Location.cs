using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeSheetGistics.Data
{
    public class Location
    {
        private int _id;
        private string _address;
        private string _city;
        private string _country;
        private string _zip;

        public string Zip
        {
            get { return _zip; }
            set { _zip = value; }
        }
        

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }
        
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        
        
    }
}
