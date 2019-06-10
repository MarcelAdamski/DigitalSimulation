using System;
using System.Collections.Generic;

namespace ProjektSymulka.Events     
{
    class Consumption: Event
    {
        private readonly int _eventTime;
        private readonly string _type;

        public Consumption(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = type;
        }

        public override void Execute(List<Event> scheduler, Restaurant restaurant, int[] parameters)
        {
            ConditionalEvents.SortingByConsumptionTime(restaurant.consumptionGroup);
            Console.WriteLine("### Wydarzenie: Klienci o ID {0} zjedli swoj posiłek. Poszli do kasy ###",restaurant.consumptionGroup[0].ID);
            Client temp = restaurant.consumptionGroup[0];
            switch (temp.Table)
            {
                case 2:
                    restaurant.TwoSeat++;
                    break;
                case 3:
                    restaurant.ThreeSeat++;
                    break;
                case 4:
                    restaurant.FourSeat++;
                    break;
            }
            restaurant.consumptionGroup.RemoveAt(0);
            restaurant.cashRegisterQ.Enqueue(temp);
        }
    }
}
