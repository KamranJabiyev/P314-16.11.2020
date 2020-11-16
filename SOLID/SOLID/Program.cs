using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    public enum InvoiceType { Invoice,FinalInvoice,DiscountInvoice}
    class Program
    {
        static void Main(string[] args)
        {
            //Fruit orange = new Orange();
            //Product product = new Product();
            //Customer customer = new Customer();
            //product.GetProData();
            //customer.GetCusData();

            Injector injector = new Injector();
            injector.customer.GetCusData();
            injector.product.GetProData();
        }
    }

    #region Single representasion
    class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Group Group { get; set; }
    }

    class Group
    {
        public string Name { get; set; }

        public string GetGroupName()
        {
            return Name;
        }
    }
    #endregion

    #region Open/Close
    //class Invoice
    //{
    //    public void GetPrice(int invoice)
    //    {
    //        switch (invoice)
    //        {
    //            case (int)InvoiceType.Invoice:
    //                Console.WriteLine("30% discount");
    //                break;
    //            case (int)InvoiceType.DiscountInvoice:
    //                Console.WriteLine("50% discount");
    //                break;
    //            default:
    //                Console.WriteLine("40% discount");
    //                break;
    //        }
    //    }
    //}

    class Invoice
    {
        public virtual void GetPrice()
        {
            Console.WriteLine("30% discount");
        }
    }

    class FinalInvoice: Invoice
    {
        public override void GetPrice()
        {
            Console.WriteLine("40% discount");
        }
    }

    class DiscountInvoice : Invoice
    {
        public override void GetPrice()
        {
            Console.WriteLine("50% discount");
        }
    }
    #endregion

    #region Liskov substitution
    abstract class Fruit
    {
        public abstract void Tasty();
    }
    class Apple: Fruit
    {
        public override void Tasty()
        {
            Console.WriteLine("As Apple");
        }
    }

    class Orange:Fruit
    {
        public override void Tasty()
        {
            Console.WriteLine("As Orange");
        }
    }
    #endregion

    #region Interface Segregation
    interface ISum
    {
        void Sum();
    }

    interface IDifference
    {
        void Difference();
    }

    interface IDivide
    {
        void Divide();
    }

    class Test : ISum
    {
        public void Sum()
        {
            throw new NotImplementedException();
        }
    }

    class Calculate : ISum,IDifference,IDivide
    {
        public void Difference()
        {
            throw new NotImplementedException();
        }

        public void Divide()
        {
            throw new NotImplementedException();
        }

        public void Sum()
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region Dependency Injection
    interface IDatabase
    {
        void Getdata(string str);
    }

    class Database : IDatabase
    {
        public void Getdata(string str)
        {
            Console.WriteLine(str);
        }
    }

    class Product
    {
        private readonly IDatabase _db;
        public Product(IDatabase db)
        {
            _db = db;
        }

        public void GetProData()
        {
            _db.Getdata("Product data");
        }
    }

    class Customer
    {
        private readonly IDatabase _db;
        public Customer(IDatabase db)
        {
            _db = db;
        }

        public void GetCusData()
        {
            _db.Getdata("Customer data");
        }
    }

    class Injector
    {
        public Customer customer;
        public Product product;
        public Injector()
        {
            Database db = new Database();
            customer = new Customer(db);
            product = new Product(db);
        }
    }
    #endregion
}
