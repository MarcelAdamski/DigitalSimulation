using System;
using System.Collections.Generic;
using ProjektSymulka.Events;

namespace ProjektSymulka
{
    public class Restaurant
    {
        private static int _twoSeat; // number of two tables
        private static int _threeSeat; // number of three tables
        private static int _fourSeat; // number of four tables
        private static bool _managerAvailable; // checks if Manager is available or not
        private static int _buffetStands; // number of buffet stands available in Buffet
        private static int _cashiers; //number of cashiers
        private static int _clientsCounter; // number of clients that somehow ended this simulation
        private static int _waiters; // number of waiters
        public const int ManagerServiceTime = 100; // manager service time

        public Queue<Client> cashRegisterQ = new Queue<Client>(); // initialization queue of clients that are waiting for cashier
        public Queue<Client> tableQ = new Queue<Client>();  // initialization queue of clients that are waiting for table
        public Queue<Client> buffetQ = new Queue<Client>(); // initialization queue of clients that are waiting for buffet stands
        public Queue<Client> drinkQ = new Queue<Client>(); // initialization queue of clients that are waiting for waiter to come and serve drink
        public Queue<Client> mealsQ = new Queue<Client>(); // initialization queue of clients that are waiting for waiter to come and serve meal
        public List<Client> buffetGuests = new List<Client>(); // list of clients that ended consumption in buffet. Need to know which group ended first
        public List<Client> drinkGroup = new List<Client>(); // list of clients that ended drinking in restaurant. Need to know which ongroupe ended first
        public List<Client> mealGroup = new List<Client>(); // list of clients that ended eating meal in restaurant. Need to know which group ended first
        public List<Client> consumptionGroup = new List<Client>(); // list of clients that ended whole consumption in restaurant. Need to know which group ended first

        public Restaurant()
        {
            _twoSeat = 4;
            _threeSeat = 10;
            _fourSeat = 4;
            _managerAvailable = true;
            _buffetStands = 14;
            _cashiers = 4;
            _waiters = 5;
        }
        public List<Event> Begin(List<Event> scheduler, Restaurant restaurant, int[] parameters)
        {
            int seed = 12334;
            int x = 0;
            for (int i = 0; i < 10 * 10000; i++)
            {
                Generators.Uniform(ref seed);
                if ((i % 10000) == 0)
                    Generators.Seeds[x++] = seed;
            }

            Event.SystemTime = 0;
            var time = (int) Generators.Normal(ref Generators.Seeds[1], ref Generators.Seeds[2], parameters[0], parameters[1]);
            ClientArrival client = new ClientArrival(time, "ClientArrival");
            client.Execute(scheduler, restaurant, parameters);
            return scheduler;
        }

        public int TwoSeat
        {
            get { return _twoSeat; }
            set
            {
                _twoSeat = value;
                _twoSeat = TwoSeat;
            }
        }

        public int ThreeSeat
        {
            get { return _threeSeat; }
            set
            {
                _threeSeat = value;
                _threeSeat = ThreeSeat;
            }
        }

        public int FourSeat
        {
            get { return _fourSeat; }
            set
            {
                _fourSeat = value;
                _fourSeat = FourSeat;
            }
        }
        public bool ManagerAvailable
        {
            get { return _managerAvailable; }
            set
            {
                _managerAvailable = value;
                _managerAvailable = ManagerAvailable;
            }
        }
        public int BuffetStands
        {
            get { return _buffetStands; }
            set
            {
                _buffetStands = value;
                _buffetStands = BuffetStands;
            }
        }
        public int Cashiers
        {
            get { return _cashiers; }
            set
            {
                _cashiers = value;
                _cashiers = Cashiers;
            }
        }
        public int ClientsCounter
        {
            get { return _clientsCounter; }
            set
            {
                _clientsCounter = value;
                _clientsCounter = ClientsCounter;
            }
        }
        public int Waiters
        {
            get { return _waiters; }
            set
            {
                _waiters = value;
                _waiters = Waiters;
            }
        }
    }
}
