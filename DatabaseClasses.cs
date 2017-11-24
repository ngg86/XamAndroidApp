using System;
using System.Net.Http;

namespace AnotherApp
{
    public class DatabaseClasses
    {
        public class Customer
        {
            public long Uid { get; set; }
            public int Code { get; set; }
            public string Name { get; set; }
            public string Adres { get; set; }
            public string PostCode { get; set; }
            public string Plaats { get; set; }
        }

        public class PackingList
        {
            public long Uid { get; set; }
            public DateTime? DeliverDate { get; set; }
            public string Status { get; set; }
            public long PackingNo { get; set; }
            public string ShippingAddress { get; set; }
            public long CustomerId { get; set; }
        }
    }

    public class User
    {
        public string Name;
        public string Pass;
        public HttpClient Client;
        public bool Save;
        public string Url;

        public User(string name, string pass, HttpClient client = null)
        {
            Name = name;
            Pass = pass;
            Client = client;
        }
    }
}
