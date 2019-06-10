using System;
using System.Collections.Generic;

namespace ProjektSymulka.Events
{
    class BuffetHandling : Event
    {
        private readonly int _eventTime;
        private readonly string _type;

        public BuffetHandling(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = type;
        }

        public override void Execute(List<Event> scheduler, Restaurant restaurant, int[] parameters)
        {
            ConditionalEvents.SortingByConsumptionTime(restaurant.buffetGuests);
            Console.WriteLine("### Wydarzenie: klient o ID {0} kończy konsumpcję w Bufecie. Trafia do kolejki do kasy ###", restaurant.buffetGuests[0].ID);
            Client temp = restaurant.buffetGuests[0];
            switch (temp.AmountPeople)
            {
                case 1:
                    restaurant.BuffetStands++;
                    break;
                case 2:
                    restaurant.BuffetStands+=2;
                    break;
                case 3:
                    restaurant.BuffetStands+=3;
                    break;
                case 4:
                    restaurant.BuffetStands+=4;
                    break;
            }
            restaurant.cashRegisterQ.Enqueue(temp);
            restaurant.buffetGuests.RemoveAt(0);
        }
    }
}
