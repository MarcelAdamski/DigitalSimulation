using System;
using System.Collections.Generic;

namespace ProjektSymulka.Events
{
    class ServingDrink: Event
    {
        private readonly int _eventTime;
        private readonly string _type;

        public ServingDrink(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = type;
        }

        public override void Execute(List<Event> scheduler, Restaurant restaurant, int[] parameters)
        {
            ConditionalEvents.SortingByConsumptionTime(restaurant.drinkGroup);
            Console.WriteLine("### Wydarzenie: Klienci o ID {0} otrzymali napoje. ###",restaurant.drinkGroup[0].ID);
            Console.WriteLine("Kelner jest wolny");
            Client temp = restaurant.drinkGroup[0];
            restaurant.mealsQ.Enqueue(temp);
            restaurant.drinkGroup.RemoveAt(0);
            restaurant.Waiters++;
        }
    }
}
