using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StorageMaster
{
    class Storage
    {
        private string name;             // storage's name
        private int capacity;            // maximum weight of products the storage can handle
        private int weight;                  // weight used in the storage
        private int garageSlots;         // number of slots available in the storage's garage
        private IReadOnlyCollection<Vehicle> vehicles;
        private IReadOnlyCollection<Product> products;

        public string Name { get { return this.name; } }
        public int Capacity { get { return this.capacity; } }
        public int GarageSlots { get { return this.garageSlots; } }

        public bool IsFull()
        {
            if (weight == capacity)
                return true;
            else
                return false;
        }
        public IReadOnlyCollection<Vehicle> Garage()
        {
            return this.vehicles;
        }
        public IReadOnlyCollection<Product> Products()
        {
            return this.products;
        }

        public Storage(string name, int capacity, int garageSlots, IEnumerable<Vehicle> vehicles)
        {
            this.name = name;
            this.capacity = capacity;
            this.garageSlots = garageSlots;

            foreach(Vehicle vehicle in vehicles)
            {
                this.vehicles.Append<Vehicle>(vehicle);
            }
        }

        public Vehicle GetVehicle(int garageSlot)
        {
            if ((garageSlot < 0) || (garageSlot >= this.garageSlots))
            {
                throw new InvalidOperationException("Invalid garage slot!");
            }
            if (vehicles.ElementAt<Vehicle>(garageSlot) == null)
            {
                throw new InvalidOperationException("No vehicle in this garage slot!");
            }
            return vehicles.ElementAt<Vehicle>(garageSlot);
        }

        public int FreeSlot()
        {
            for(int i = 0; i < this.capacity; i++)
            {
                if (vehicles.ElementAt<Vehicle>(i) == null)
                    return i;
            }
            return -1;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            this.vehicles.Append<Vehicle>(vehicle);
        }

        public int SendVehicleTo(int garageSlot, Storage deliveryLocation)
        {
            Vehicle vehicle;
            try
            {
                vehicle = this.GetVehicle(garageSlot);
            }
            catch
            {
                throw;
            }

            int freeSlot = deliveryLocation.FreeSlot();
            if (freeSlot < 0)
            {
                throw new InvalidOperationException("No room in garage!");
            }
            deliveryLocation.AddVehicle(vehicle);
            return freeSlot;
        }

        public int UnloadVehicle(int garageSlot)
        {
            double totalWeight = 0;

            foreach(Product product in this.vehicles[garageSlot])
            {
                totalWeight += product.Weight;
            }
            if (this.capacity == this.weight)
            {
                throw new InvalidOperationException("Storage is full!");
            }
            if(totalWeight+(double)this.weight > (double)this.capacity)
            {
                throw new InvalidOperationException("Storage is full!");
            }

        }
    }
}
