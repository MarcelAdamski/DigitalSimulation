using System;
using System.Collections.Generic;

namespace ProjektSymulka.Events
{
    class ManagerWork : Event
    {
        private readonly int _eventTime;
        private readonly string _type;

        public ManagerWork(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = type;
        }

        public override void Execute(List<Event> scheduler, Restaurant restaurant, int[] parameters)
        {
            Client temp = restaurant.tableQ.Peek();
            Console.WriteLine("Klient o ID {0} oczekiwał na stolik {1} jednostek czasu",temp.ID,temp.WaitingForTableTime);
            temp.WaitingForWaiterTime = SystemTime;
            restaurant.drinkQ.Enqueue(restaurant.tableQ.Dequeue());
            restaurant.ManagerAvailable = true;
            Console.WriteLine("### Wydarzenie: Menedzer jest wolny ###");
            Console.WriteLine("Grupa o ID {0} oczekuje na kelnera [DRINK]", restaurant.drinkQ.Peek().ID);
        }
    }
}
