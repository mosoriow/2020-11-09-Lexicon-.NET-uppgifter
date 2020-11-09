using System;
using System.Collections.Generic;
using System.Linq;

namespace StorageMaster
{
    class Vehicle
    {
        public int capacity;
        public IReadOnlyCollection<Product> Trunk;
        private bool isFull;
        private bool isEmpty;
        public int Capacity { get => capacity; set => capacity = value; }
        public Vehicle(int Capacity)
        {
            this.Capacity = Capacity;
        }

       public bool IsFull()
        {
            double totalWeight = 0;
            foreach (Product p in Trunk)
            {
                totalWeight = totalWeight + p.Weight;
            }
            if (totalWeight< Capacity)
            { 
                return false; 
            }
            else
            {
                return true;
            }
        }
        public bool IsEmpty()
        {
            int checkTrunk = Trunk.Count;
            if (checkTrunk > 0)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }
        void LoadProduct(Product product)
        {
           
            if(isFull)
            {
                throw new InvalidOperationException("Vehicle is full");
            }
            else
            {
                Trunk.Append(product);               
            }

        }
        Product Unload()
        {
            int checkTrunk = Trunk.Count;
            if (isEmpty)
            {
                throw new InvalidOperationException("No products left in vehicle!");
            }
            else if (checkTrunk == 1)
            {
                Product unloadProd = Trunk.ElementAt(checkTrunk);
                Trunk = null;
                return unloadProd;
            }
            else
            {
                Product unloadProd=Trunk.ElementAt(checkTrunk);
                Trunk = (IReadOnlyCollection<Product>)Trunk.Take(checkTrunk-1);
                return unloadProd;
            }
        }
    }
    class Van : Vehicle
    {
        public Van(int Capacity) : base(Capacity)
        {
            this.Capacity = 2;
        }
    }
    class Truck  : Vehicle
    {
        public Truck(int Capacity) : base(Capacity)
        {
            this.Capacity = 5;
        }
    }
    class Semi  : Vehicle
    {
        public Semi(int Capacity) : base(Capacity)
        {
            this.Capacity = 10;
        }
    }

}
