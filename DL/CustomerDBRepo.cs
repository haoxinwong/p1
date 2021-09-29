using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = P0_M.Models;
using Entity = DL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace P0_M.DL
{
    public class CustomerDBRepo:IRepo
    {
        private Entity.P0Context _context;
        public CustomerDBRepo(Entity.P0Context context){
            _context = context;
        }

        public Model.Order AddAOrder(Model.Order order){
            // Console.WriteLine(order.Time+" "+ order.Total+" "+order.Location+" "+order.LocationId+" "+ order.CustomerId);
            Entity.Order orderToAdd = new Entity.Order(){ 
                OrderTime = order.Time,
                Total = order.Total,
                StoreLocation = order.Location,
                LocationId = order.LocationId,
                CustomerId = order.CustomerId
            };
            // Console.WriteLine(orderToAdd.OrderTime+" "+ orderToAdd.Total+" "+orderToAdd.StoreLocation+" "+orderToAdd.LocationId+" "+ orderToAdd.CustomerId);
            _context.Orders.Add(orderToAdd);
            _context.SaveChanges();

            return new Model.Order(){
                Id = orderToAdd.Id,
                Time = (System.DateTime)orderToAdd.OrderTime,
                Total = (decimal)orderToAdd.Total,
                Location = orderToAdd.StoreLocation,
                LocationId = (int)orderToAdd.LocationId,
                CustomerId = (int)orderToAdd.CustomerId
            };
        }



        public List<Model.Order> GetAllOrderbyId(int id){
            List <Model.Order> orderData = new List<Model.Order>();
            using(SqlConnection connection = new SqlConnection("Server=tcp:mp0server.database.windows.net,1433;Initial Catalog=P0;Persist Security Info=False;User ID=hao;Password=Zm111111;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                connection.Open();
                string query = $"SELECT Id,ToTal,OrderTime,StoreLocation,LocationId,CustomerId FROM Orders WHERE CustomerId={id}";
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // st<LineItem>lineItems, string location, int LocationID,int Custome
                            string query2 = $"SELECT Name,Price,Quantity,Id,OrderId FROM CustomerLineItems WHERE OrderId={reader[0]}";
                            List<Model.LineItem> lineitemData = new List<Model.LineItem>();
                            using(SqlCommand command2 = new SqlCommand(query2, connection))
                            {
                                using (SqlDataReader reader2 = command2.ExecuteReader())
                                {
                                    while (reader2.Read())
                                    {
                                        lineitemData.Add(new Model.LineItem(reader2.GetString(0),reader2.GetDecimal(1),reader2.GetInt32(2),reader2.GetInt32(4)));
                                    }
                                }
                            }
                            orderData.Add(new Model.Order(lineitemData,reader.GetString(3),reader.GetInt32(4),reader.GetInt32(5),reader.GetDateTime(2)));
                        }         
                    }
                }
            }
            return orderData;
        }
        public Model.LineItem AddALineItem(Model.LineItem lineitem){
            Entity.CustomerLineItem lineitemToAdd = new Entity.CustomerLineItem(){
                Name = lineitem.Name,
                Price = lineitem.Price,
                Quantity = lineitem.Quantity,
                OrderId = lineitem.OrderId
            };
            lineitemToAdd = _context.CustomerLineItems.Add(lineitemToAdd).Entity;
            _context.SaveChanges();

            return new Model.LineItem(){
                Id = lineitemToAdd.Id,
                Name = lineitemToAdd.Name,
                Price = (decimal)lineitemToAdd.Price,
                Quantity = (int)lineitemToAdd.Quantity,
                OrderId = (int)lineitemToAdd.OrderId
            };
        }

        // public List<Model.Order> GetOrdersbyId(int id){
        //     return _context.Orders.Where(
        //         order =>order.CustomerId == id
        //     )Select(
        //         r => new Model.Order(){
        //             // Total{get;set;}
        // public List<LineItem> LineItems{get;set;}
        // public DateTime Time{get;set;}
        // public string Location{get;set;}
        // public int LocationID{get;set;}

        //         }
        //     )
        // }
        public Model.Customer Add(Model.Customer cust){
            Entity.Customer custToAdd = new Entity.Customer(){
                // Id = cust.Id,
                Name = cust.Name,
                Address = cust.Address ?? "",
                Phonenumber = cust.PhoneNumber ?? ""
            };

            custToAdd = _context.Add(custToAdd).Entity;

            _context.SaveChanges();

            return new Model.Customer(){
                Id = custToAdd.Id,
                Name = custToAdd.Name,
                Address = custToAdd.Address,
                PhoneNumber = custToAdd.Phonenumber
            };
        }

        

        public List<Model.Customer>GetAll(){
            return _context.Customers.Select(
                customer => new Model.Customer(){
                    Id = customer.Id,
                    Name = customer.Name,
                    Address = customer.Address,
                    PhoneNumber = customer.Phonenumber
                }
            ).ToList();
        }

        public Model.Customer Update(Model.Customer customerToUpdate){
            Entity.Customer custToUpdate = new Entity.Customer(){
                Id = customerToUpdate.Id,
                Name = customerToUpdate.Name,
                Address = customerToUpdate.Address,
                Phonenumber = customerToUpdate.PhoneNumber
            };

            custToUpdate = _context.Customers.Update(custToUpdate).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Model.Customer() {
                Id = custToUpdate.Id,
                Name = custToUpdate.Name,
                Address = custToUpdate.Address,
                PhoneNumber = custToUpdate.Phonenumber
            };
        }

        

    }
}