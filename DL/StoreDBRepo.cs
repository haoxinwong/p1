using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace DL
{

    public class StoreDBRepo
    {
        private P0DBContext _context;
        public StoreDBRepo(P0DBContext context){
            _context = context;
        }


        // public List<Model.Customer>GetAll(){
        //     return _context.Customers.Select(
        //         customer => new Model.Customer(){
        //             Name = customer.Name,
        //             Address = customer.Address,
        //             PhoneNumber = customer.PhoneNumber
        //         }
        //     ).ToList();
        // }

        public Inventory UpdateInventory(Inventory inventory){
            Inventory invenToUpdate = new Inventory(){
                // Id = inventory.Id,
                Name = inventory.Name,
                Price = inventory.Price,
                Quantity = inventory.Quantity,
                StoreId = inventory.StoreId
            };

            invenToUpdate = _context.Inventories.Update(invenToUpdate).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Inventory() {
                Id = invenToUpdate.Id,
                Name = invenToUpdate.Name,
                Price = (decimal)invenToUpdate.Price,
                Quantity = (int)invenToUpdate.Quantity,
                StoreId = (int)invenToUpdate.StoreId
            };
        }

        public Inventory UpdateInventory(Inventory inventory,string str){
            string constr = "Server=tcp:mp0server.database.windows.net,1433;Initial Catalog=P0;Persist Security Info=False;User ID=hao;Password=Zm111111;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand($"UPDATE Inventory  SET Quantity = {inventory.Quantity} Where Id ={inventory.Id}",con);
            cmd.ExecuteNonQuery();
            con.Close();

            return inventory;
        }

        public Inventory UpdateInventory2(Inventory inventory){
            Inventory invToUpdate = new Inventory(){
                Id = inventory.Id,
                Price = (decimal)inventory.Price,
                Name = inventory.Name,
                Quantity = (int)inventory.Quantity,
                StoreId = (int)inventory.StoreId
            };

            invToUpdate = _context.Inventories.Update(invToUpdate).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Inventory() {
                Id = invToUpdate.Id,
                Price = (decimal)invToUpdate.Price,
                Name = invToUpdate.Name,
                Quantity = (int)invToUpdate.Quantity,
                StoreId = (int)invToUpdate.StoreId
            };

            // string constr = "Server=tcp:mp0server.database.windows.net,1433;Initial Catalog=P0;Persist Security Info=False;User ID=hao;Password=Zm111111;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            // SqlConnection con = new SqlConnection(constr);
            // con.Open();
            // SqlCommand cmd = new SqlCommand($"UPDATE Inventory  SET Price = {inventory.Price} Where Id ={inventory.Id}",con);
            // cmd.ExecuteNonQuery();
            // con.Close();
            // return inventory;
        }

        public List<Store> GetAll(){

            // Entity.Store stores= _context.Stores.Include(r => r.Inventory);
            
            return _context.Stores.Select(
                store => new Store(){
                    Id = store.Id,
                    Name = store.Name,
                    Address = store.Address
                    // Inventory = stores.Inventory.Select(r => new Model.Inventory(){
                    //     Name = r.Name,
                    //     Price = r.Price,
                    //     Quantity = r.Quantity
                    // }).ToList()
                }
            ).ToList();
        }

        public Store GetOneStoreById(int id)
        {
            Store stoById = 
                _context.Stores
                //this include method joins reviews table with the restaurant table
                //and grabs all reviews that references the selected restaurant
                //by restaurantId
                // .Include("Reviews")
                .Include(r => r.Inventory)
                .FirstOrDefault(r => r.Id == id);

            return new Store() {
                Id = stoById.Id,
                Name = stoById.Name,
                Address = stoById.Address,
                Inventory = stoById.Inventory.Select(r => new Inventory(){
                        Id = r.Id,
                        Name = r.Name,
                        Price = (decimal)r.Price,
                        Quantity = (int)r.Quantity,
                        StoreId = (int)r.StoreId
                }).ToList()
            };
        }

        public Store Update(Store storeToUpdate){
            Store stoToUpdate = new Store(){
                Id = storeToUpdate.Id,
                Name = storeToUpdate.Name,
                Address = storeToUpdate.Address
            };

            stoToUpdate = _context.Stores.Update(stoToUpdate).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Store() {
                Id = stoToUpdate.Id,
                Name = stoToUpdate.Name,
                Address = stoToUpdate.Address
            };
        }

        public Inventory AddInventoryItem(Inventory inv){
            Inventory invToAdd = new Inventory() {
                Name = inv.Name,
                Price = inv.Price,
                Quantity = inv.Quantity,
                StoreId = inv.StoreId
            };
            invToAdd = _context.Add(invToAdd).Entity;
            _context.SaveChanges();

            _context.ChangeTracker.Clear();

            return new Inventory(){
                Id = invToAdd.Id,
                Name = invToAdd.Name,
                Price = (decimal)invToAdd.Price,
                Quantity = (int)invToAdd.Quantity,
                StoreId = (int)invToAdd.StoreId
            };
        }
        public Store Add(Store sto){
            Store stoToAdd = new Store(){
                Id = sto.Id,
                Name = sto.Name,
                Address = sto.Address ?? ""
            };

            stoToAdd = _context.Add(stoToAdd).Entity;

            _context.SaveChanges();

            _context.ChangeTracker.Clear();

            return new Store(){
                Id = stoToAdd.Id,
                Name = stoToAdd.Name,
                Address = stoToAdd.Address
            };
        }

        public List<Order> GetAllOrderbyId(int id){
            List <Order> orderData = new List<Order>();
            using(SqlConnection connection = new SqlConnection("Server=tcp:mp0server.database.windows.net,1433;Initial Catalog=P0;Persist Security Info=False;User ID=hao;Password=Zm111111;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                connection.Open();
                string query = $"SELECT Id,ToTal,OrderTime,StoreLocation,LocationId,CustomerId FROM Orders WHERE LocationId={id}";
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // st<LineItem>lineItems, string location, int LocationID,int Custome
                            string query2 = $"SELECT Name,Price,Quantity,Id,OrderId FROM CustomerLineItems WHERE OrderId={reader[0]}";
                            List<LineItem> lineitemData = new List<LineItem>();
                            using(SqlCommand command2 = new SqlCommand(query2, connection))
                            {
                                using (SqlDataReader reader2 = command2.ExecuteReader())
                                {
                                    while (reader2.Read())
                                    {
                                        lineitemData.Add(new LineItem(reader2.GetString(0),reader2.GetDecimal(1),reader2.GetInt32(2),reader2.GetInt32(4)));
                                    }
                                }
                            }
                            orderData.Add(new Order(lineitemData,reader.GetString(3),reader.GetInt32(4),reader.GetInt32(5),reader.GetDateTime(2)));
                        }         
                    }
                }
            }
            return orderData;
        }
        // private const string storeFilePath = "../DL/Stores.json";
        
        
        // private string jsonString;

        // public List<Store> GetAll(){
        //     jsonString = File.Read.AllText(storeFilePath);

        //     return JsonSerializer.Deserialize<List<Store>>(jsonString);
        // }
        // public Store Add(Store store){
        //     List<Store> allStores = GetAll();
        //     allStores.Add(store);
        //     jsonString = JsonSerializer.Serialize(allStores);
        //     File.WriteAllText(storeFilePath,jsonString);
        //     return store;
        // }


        // public Store Update(Store storeToUpdate){
        //     List<Store>allStores = GetAll();
        //     int storeIndex = allStores.FindIndex(r => r.Equals(storeToUpdate));
        //     allStores[storeIndex] = storeToUpdate;
        //     jsonString = JsonSerializer.Serialize(allStores);
        //     File.WriteAllText(storeFilePath, jsonString);
        //     return storeToUpdate;
        // }

        

    }
}