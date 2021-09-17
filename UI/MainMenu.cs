using System;
using P0_M.Models;
using P0_M.BL;
using P0_M.DL;

namespace P0_M.UI
{
    public class MainMenu:IMenu
    {
        public void Start(){
            bool exit = false;
            string input = "";
            do{
                Console.WriteLine("Welcome to liquor store");
                Console.WriteLine("Author Haoxin wang");
                Console.WriteLine("[0] Start Login with a customer name");
                Console.WriteLine("[1] Start Login with a admin name and code");
                Console.WriteLine("[x] Exit");

                input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "0":
                        new CustomerLoginMenu(new CustomerBL(new CustomerDBRepo())).Start();
                        break;
                    case "1":
                        Console.WriteLine("what's your name2");
                        break;
                    case "x":
                        Console.WriteLine("bye");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("?");
                        break;
                }
                
            }while (!exit);
            
        }
    }
}