using System.Collections.Generic;

namespace ProjektSymulka.Events
{
    class CashierWork:Event
    {
        private readonly int _eventTime;
        private readonly string _type;

        public CashierWork(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = type;
        }

        public override void Execute(List<Event> scheduler, Restaurant restaurant, int[] parameters)
        {
            //Console.WriteLine("### Wydarzenie: kasjer obsłużył klienta. Dziękujemy za wizytę ###");
            Statistics.ClientsInRestaurant--;
            restaurant.Cashiers++;
            restaurant.ClientsCounter++;
        }
    }
}
