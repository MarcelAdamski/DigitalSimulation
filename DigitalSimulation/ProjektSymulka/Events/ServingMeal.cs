using System;
using System.Collections.Generic;

namespace ProjektSymulka.Events
{
    class ServingMeal : Event
    {
        private readonly int _eventTime;
        private readonly string _type;

        public ServingMeal(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = type;
        }

        public override void Execute(List<Event> scheduler, Restaurant restaurant, int[] parameters)
        {
            var time = (int) Generators.Exponential(1900, ref Generators.Seeds[1]);
            ConditionalEvents.SortingByConsumptionTime(restaurant.mealGroup);
            Console.WriteLine("### Wydarzenie: Klienci o ID {0} dostali jedzenie. Smacznego. ###", restaurant.mealGroup[0].ID);
            Console.WriteLine("Kelner jest wolny");
            Client temp = restaurant.mealGroup[0];
            restaurant.consumptionGroup.Add(temp);
            restaurant.mealGroup.RemoveAt(0);
            temp.ConsumptionTime = SystemTime + time;
            restaurant.Waiters++;
            scheduler.Add(new Consumption(SystemTime + time, "Consumption"));
            SortingScheduler(scheduler);
        }
    }
}
