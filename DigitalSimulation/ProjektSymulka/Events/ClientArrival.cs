using System;
using System.Collections.Generic;

namespace ProjektSymulka.Events
{
    class ClientArrival : Event
    {
        private readonly int _eventTime;
        private readonly string _type;
        private static int _id; // id that new group gets
        private static int _choice; // for change buffet client come or restaurant client come

        public ClientArrival(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = type;
        }

        public override void Execute(List<Event> scheduler, Restaurant restaurant, int[] parameters)
        {
            var time = (int) Generators.Normal(ref Generators.Seeds[1], ref Generators.Seeds[2], parameters[0], parameters[1]);
            _id++;
            if (_choice == 0)
            {
                var temp = new Client(_id, "RC") {WaitingForTableTime = SystemTime};
                restaurant.tableQ.Enqueue(temp);
                _choice = 1;
                Console.WriteLine("### Wydarzenie: nowa grupa klientów. ID grupy: {0}, liczba osób w grupie: {1}. Typ klienta: RC ###", temp.ID, temp.AmountPeople);
            }
            else
            {
                Client temp = new Client(_id, "BC");
                restaurant.buffetQ.Enqueue(temp);
                _choice = 0;
                Console.WriteLine("### Wydarzenie: nowa grupa klientów. ID grupy: {0}, liczba osób w grupie: {1}. Typ klienta: BC ###", temp.ID, temp.AmountPeople);
            }
            //Planning new event - new group of clients will arrive
            Statistics.ClientsInRestaurant++;
            Statistics.clientsInRestaurant.Add(Statistics.ClientsInRestaurant);
            scheduler.Add(new ClientArrival(SystemTime + time, "ClientArrival"));
            SortingScheduler(scheduler);
        }
    }
}
