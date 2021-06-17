using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    class Contact
    {
        private string Name;
        private string Email;
        private string Adress;
        private string Phone;

        //optional get set
        public string name
        {
            get => Name;

            set => this.Name = value;
        }
        public string email
        {
            get => Email;

            set => this.Email = value;
        }
        public string adress
        {
            get => Adress;

            set => this.Adress = value;
        }
        public string phone
        {
            get => Phone;
            set => this.Phone = value;
        }

        //construct
        public Contact(string n, string p, string e, string a)
        {
            this.Name = n;
            this.Phone = p;
            this.Email = e;
            this.Adress = a;
        }
        public Contact() { }
    }
}
