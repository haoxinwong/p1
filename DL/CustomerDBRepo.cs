using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace DL
{
    public class CustomerDBRepo:IRepo
    {
        private P0DBContext _context;
        public CustomerDBRepo(P0DBContext context){
            _context = context;
        }

        public Customer GetOneCustomerById(int custID)
        {
            Customer customertoView = _context.Customers.Include(x => x.Orders).FirstOrDefault(u => u.Id == custID);
            return new Customer()
            {
                Id = customertoView.Id,
                Name = customertoView.Name,
                Address = customertoView.Address ?? "",
                PhoneNumber = customertoView.PhoneNumber ?? ""
                
            };
        }

        public Order AddAOrder(Order order){
            // Console.WriteLine(order.Time+" "+ order.Total+" "+order.Location+" "+order.LocationId+" "+ order.CustomerId);
            Order orderToAdd = new Order(){ 
                Time = order.Time,
                Total = order.Total,
                Location = order.Location,
                StoreId = order.StoreId,
                CustomerId = order.CustomerId
            };
            // Console.WriteLine(orderToAdd.OrderTime+" "+ orderToAdd.Total+" "+orderToAdd.StoreLocation+" "+orderToAdd.LocationId+" "+ orderToAdd.CustomerId);
            _context.Orders.Add(orderToAdd);
            _context.SaveChanges();

            return new Order(){
                Id = orderToAdd.Id,
                Time = (System.DateTime)orderToAdd.Time,
                Total = (decimal)orderToAdd.Total,
                Location = orderToAdd.Location,
                StoreId = (int)orderToAdd.StoreId,
                CustomerId = (int)orderToAdd.CustomerId
            };
        }



        public List<Order> GetAllOrderbyId(int id)
        {

            return _context.Orders.Where(x => x.CustomerId == id).Select(
                order => new Order()
                {
                    Id = order.Id,
                    Time = order.Time,
                    Location = order.Location,
                    StoreId = order.StoreId,
                    CustomerId = order.CustomerId,
                    Total = order.Total
                }
            )
                .ToList();
            /*return _context.Orders.Where(x => x.CustomerId == id).Select(r => new Models.Order()).ToList();*/
        }
        public LineItem AddALineItem(LineItem lineitem){
            LineItem lineitemToAdd = new LineItem(){
                Name = lineitem.Name,
                Price = lineitem.Price,
                Quantity = lineitem.Quantity,
                OrderId = lineitem.OrderId
            };
            lineitemToAdd = _context.CustomerLineItems.Add(lineitemToAdd).Entity;
            _context.SaveChanges();

            return new LineItem(){
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
        public Customer Add(Customer cust){
            Customer custToAdd = new Customer(){
                // Id = cust.Id,
                Name = cust.Name,
                Address = cust.Address ?? "",
                PhoneNumber = cust.PhoneNumber ?? ""
            };

            custToAdd = _context.Add(custToAdd).Entity;

            _context.SaveChanges();

            return new Customer(){
                Id = custToAdd.Id,
                Name = custToAdd.Name,
                Address = custToAdd.Address,
                PhoneNumber = custToAdd.PhoneNumber
            };
        }

        

        public List<Customer>GetAll(){
            return _context.Customers.Select(
                customer => new Customer(){
                    Id = customer.Id,
                    Name = customer.Name,
                    Address = customer.Address,
                    PhoneNumber = customer.PhoneNumber
                }
            ).ToList();
        }

        public Inventory GetOneInventory(int id)
        {
            return _context.Inventories.FirstOrDefault(x => x.Id == id);
        }

        public void RemoveInventory(int id)
        {
            _context.Inventories.Remove(GetOneInventoryById(id));
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }
        public Inventory GetOneInventoryById(int id)
        {
            return _context.Inventories
                //this include method joins reviews table with the restaurant table
                //and grabs all reviews that references the selected restaurant
                //by restaurantId
                // .Include("Reviews")
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == id);
        }
        public Customer Update(Customer customerToUpdate){
            Customer custToUpdate = new Customer(){
                Id = customerToUpdate.Id,
                Name = customerToUpdate.Name,
                Address = customerToUpdate.Address,
                PhoneNumber = customerToUpdate.PhoneNumber
            };

            custToUpdate = _context.Customers.Update(custToUpdate).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Customer() {
                Id = custToUpdate.Id,
                Name = custToUpdate.Name,
                Address = custToUpdate.Address,
                PhoneNumber = custToUpdate.PhoneNumber
            };
        }

        public Inventory UpdateInventory(Inventory inventory)
        {
            Inventory invenToUpdate = new Inventory()
            {
                //Id = inventory.Id,
                Name = inventory.Name,
                Price = inventory.Price,
                Quantity = inventory.Quantity,
                StoreId = inventory.StoreId
            };

            invenToUpdate = _context.Inventories.Update(invenToUpdate).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Inventory()
            {
                Id = invenToUpdate.Id,
                Name = invenToUpdate.Name,
                Price = (decimal)invenToUpdate.Price,
                Quantity = (int)invenToUpdate.Quantity,
                StoreId = (int)invenToUpdate.StoreId
            };
        }


        public Inventory UpdateInventory2(Inventory inventory)
        {
           
            Inventory invToUpdate =  _context.Inventories.Update(inventory).Entity;

             _context.SaveChanges();
            _context.ChangeTracker.Clear();


            return invToUpdate;

        }

        public List<Store> GetAllStore()
        {

            // Entity.Store stores= _context.Stores.Include(r => r.Inventory);

            return _context.Stores.Select(
                store => new Store()
                {
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

            return new Store()
            {
                Id = stoById.Id,
                Name = stoById.Name,
                Address = stoById.Address,
                Inventory = stoById.Inventory.Select(r => new Inventory()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Price = (decimal)r.Price,
                    Quantity = (int)r.Quantity,
                    StoreId = (int)r.StoreId
                }).ToList()
            };
        }
        public void InventorToUpdate(List<Inventory> items)
        {

            foreach (Inventory item in items)
            {

                Inventory updatedInventory = (from i in _context.Inventories
                                              where i.Id == item.Id
                                              select i).SingleOrDefault();

                updatedInventory.Quantity = item.Quantity;
            }
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        public Store Update(Store storeToUpdate)
        {
            Store stoToUpdate = new Store()
            {
                Id = storeToUpdate.Id,
                Name = storeToUpdate.Name,
                Address = storeToUpdate.Address
            };

            stoToUpdate = _context.Stores.Update(stoToUpdate).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Store()
            {
                Id = stoToUpdate.Id,
                Name = stoToUpdate.Name,
                Address = stoToUpdate.Address
            };
        }

        public Inventory AddInventoryItem(Inventory inv)
        {
            Inventory invToAdd = new Inventory()
            {
                Name = inv.Name,
                Price = inv.Price,
                Quantity = inv.Quantity,
                StoreId = inv.StoreId
            };
            invToAdd = _context.Add(invToAdd).Entity;
            _context.SaveChanges();

            _context.ChangeTracker.Clear();

            return new Inventory()
            {
                Id = invToAdd.Id,
                Name = invToAdd.Name,
                Price = (decimal)invToAdd.Price,
                Quantity = (int)invToAdd.Quantity,
                StoreId = (int)invToAdd.StoreId
            };
        }
        public Store Add(Store sto)
        {
            Store stoToAdd = new Store()
            {
                Id = sto.Id,
                Name = sto.Name,
                Address = sto.Address ?? ""
            };

            stoToAdd = _context.Add(stoToAdd).Entity;

            _context.SaveChanges();

            _context.ChangeTracker.Clear();

            return new Store()
            {
                Id = stoToAdd.Id,
                Name = stoToAdd.Name,
                Address = stoToAdd.Address
            };
        }

        public List<Order> GetAllOrderbyStoreId(int id)
        {
            return _context.Orders.Where(x => x.StoreId == id).Select(
                order => new Order()
                {
                    Id = order.Id,
                    Time = order.Time,
                    Location = order.Location,
                    StoreId = order.StoreId,
                    CustomerId = order.CustomerId,
                    Total = order.Total
                }
            ).ToList();
        }

        public List<LineItem> GetLineItemsbyOrderId(int id)
        {
            return _context.CustomerLineItems.Where(x => x.OrderId == id).Select(
                lineItem => new LineItem()
                {
                    Id = lineItem.Id,
                    OrderId = lineItem.OrderId,
                    Name = lineItem.Name,
                    Price = lineItem.Price,
                    Quantity = lineItem.Quantity
                }
            ).ToList();
        }

        public Order GetOneOrderbyId(int id)
        {
            Order orderById =
                _context.Orders
                //this include method joins reviews table with the restaurant table
                //and grabs all reviews that references the selected restaurant
                //by restaurantId
                // .Include("Reviews")
                .Include(r => r.LineItems)
                .FirstOrDefault(r => r.Id == id);

            return new Order()
            {
                Id = orderById.Id,
                Time = orderById.Time,
                Location = orderById.Location,
                StoreId = orderById.StoreId,
                CustomerId = orderById.CustomerId,
                Total = orderById.Total,
                LineItems = orderById.LineItems.Select(r => new LineItem()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Price = (decimal)r.Price,
                    Quantity = (int)r.Quantity,
                    OrderId = r.OrderId
                    
                }).ToList()
            };
        }



    }
}