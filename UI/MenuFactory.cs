using P0_M.DL;
using P0_M.BL;
using DL.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;
using P0_M.Models;
using System;

namespace P0_M.UI
{
    public class MenuFactory
    {
        public static IMenu GetMenu(string menuString){
            string connectionString = File.ReadAllText(@"../connectionString.txt");
            DbContextOptions<P0Context> options = new DbContextOptionsBuilder<P0Context>()
            .UseSqlServer(connectionString).Options;
            P0Context context = new P0Context(options);

            switch (menuString.ToLower())
            {
                case "customerlogin":
                    return new CustomerLoginMenu(new CustomerBL(new CustomerDBRepo(context)));
                default:
                    return null;
            }
        }
        public static IMenu GetMenu(string menuString,string name, string code){
            string connectionString = File.ReadAllText(@"../connectionString.txt");
            DbContextOptions<P0Context> options = new DbContextOptionsBuilder<P0Context>()
            .UseSqlServer(connectionString).Options;
            P0Context context = new P0Context(options);

            switch (menuString.ToLower())
            {
                case "adminlogin":
                    return new AdminLogin(new StoreBL(new StoreDBRepo(context)),new CustomerBL(new CustomerDBRepo(context)),name, code);
                default:
                    return null;
            }
        }

        public static IMenu GetMenu(string menuString,P0_M.Models.Store store){
            string connectionString = File.ReadAllText(@"../connectionString.txt");
            DbContextOptions<P0Context> options = new DbContextOptionsBuilder<P0Context>()
            .UseSqlServer(connectionString).Options;
            P0Context context = new P0Context(options);
            switch (menuString.ToLower())
            {
                case "adminstore":
                    return new AdminStoreMenu(new StoreBL(new StoreDBRepo(context)),store);
                default:
                    return null;
            }
        }

        public static IMenu GetMenu(string menuString,P0_M.Models.Customer customer){
            string connectionString = File.ReadAllText(@"../connectionString.txt");
            DbContextOptions<P0Context> options = new DbContextOptionsBuilder<P0Context>()
            .UseSqlServer(connectionString).Options;
            P0Context context = new P0Context(options);
            switch (menuString.ToLower())
            {
                case "customer":
                    return new CustomerMenu(customer);
                case "profile":
                    return new ProfileMenu(new CustomerBL(new CustomerDBRepo(context)),customer);
                case "stores":
                    return new StoresMenu(new StoreBL(new StoreDBRepo(context)),customer);
                default:
                    return null;
            }
        }

        public static IMenu GetMenu(string menuString, P0_M.Models.Store currentStore,P0_M.Models.Customer currentCustomer){
            string connectionString = File.ReadAllText(@"../connectionString.txt");
            DbContextOptions<P0Context> options = new DbContextOptionsBuilder<P0Context>()
            .UseSqlServer(connectionString).Options;
            P0Context context = new P0Context(options);

            switch (menuString.ToLower())
            {
                case "thatstore":
                    return new StoreMenu(new StoreBL(new StoreDBRepo(context)), new CustomerBL(new CustomerDBRepo(context)),currentStore,currentCustomer);
                default:
                    Console.WriteLine("lol");
                    return null;
            }
        }
    }
}