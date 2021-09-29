using System;
using P0_M.Models;
using P0_M.BL;
using P0_M.DL;
using System.Collections.Generic;
using Serilog;
namespace P0_M.UI
{
    public class StoresMenu:IMenu
    {
        private StoreBL _sbl;
        private Customer _currentCustomer;

        public StoresMenu(StoreBL sbl,Customer customer){
            _sbl = sbl;
            _currentCustomer = customer;
        }

        public void Start(){
            bool exit = false;
            string input = "";
            int result = -1;
            List<Store> allStores = _sbl.GetAll();
            do
            {
                Console.WriteLine("This is Stores Menu");
                if(allStores.Count!=0){
                    for ( int i = 0; i < allStores.Count; i++)
                    {
                        Store TempStore = allStores[i];
                        Console.WriteLine($"[{i}] Store Name:{TempStore.Name}, Store Address:{TempStore.Address}");
                    }
                }else{
                    Console.WriteLine("No available store");
                }
                Console.WriteLine("[<] Go Back To Customer Home Menu");
                Console.WriteLine("[x] Exit");

                bool enterStoreNumber =false;
                do
                {
                    Console.WriteLine("Enter Store Number to view that store");
                    input = Console.ReadLine().ToLower();

                    if (input.Equals("<")){
                        exit = true;
                        enterStoreNumber = true;
                    }else if(input.Equals("x")){
                        Console.WriteLine("bye");
                        System.Environment.Exit(1);
                    }else{
                        try
                        {
                            result = Int32.Parse(input);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Unable to parse '{input}', reenter");
                            enterStoreNumber = false;
                        }
                        if (result >= 0 & result < allStores.Count){
                            GoToStoreMenu(allStores[result],_currentCustomer);
                        }else{
                            Console.WriteLine("Input Error, reenter");
                            enterStoreNumber = false;
                        }
                    }
                } while (!enterStoreNumber);
                
            } while (!exit);
        }

        private void GoToStoreMenu(Store currentStore,Customer currentCustomer){
            MenuFactory.GetMenu("thatstore",currentStore,currentCustomer).Start();
        }
    }
}