using System;
using System.Collections.Generic;

namespace ProjektSymulka.Events
{
    class ConditionalEvents : Event
    {
        public static List<Event> GetTable(List<Event> scheduler, Restaurant restaurant)
        {
            restaurant.ManagerAvailable = false;
            Client temp = restaurant.tableQ.Peek();
            temp.WaitingForTableTime = SystemTime - temp.WaitingForTableTime;
            Statistics.tableQueueTime.Add(temp.WaitingForTableTime);
            if (temp.AmountPeople <= 2 && restaurant.TwoSeat > 0)
            {
                Console.WriteLine("Menedzer prowadzi grupe {0} o liczebnosci {1} do stolika 2os. Będzie dostępny w czasie: {2}", restaurant.tableQ.Peek().ID, restaurant.tableQ.Peek().AmountPeople,SystemTime + Restaurant.ManagerServiceTime);
                restaurant.TwoSeat--;
                temp.Table = 2;
            }
            else if (temp.AmountPeople <= 3 && restaurant.ThreeSeat > 0)
            {
                Console.WriteLine("Menedzer prowadzi grupe {0} o liczebnosci {1} stolika 3os. Będzie dostępny w czasie: {2}", restaurant.tableQ.Peek().ID, restaurant.tableQ.Peek().AmountPeople,SystemTime + Restaurant.ManagerServiceTime);
                restaurant.ThreeSeat--;
                temp.Table = 3;
            }
            else if (temp.AmountPeople <= 4 && restaurant.FourSeat > 0)
            {
                Console.WriteLine("Menedzer prowadzi grupe {0} o liczebnosci {1} stolika 4os. Będzie dostępny w czasie: {2}", restaurant.tableQ.Peek().ID, restaurant.tableQ.Peek().AmountPeople,SystemTime + Restaurant.ManagerServiceTime);
                restaurant.FourSeat--;
                temp.Table = 4;
            }
            scheduler.Add(new ManagerWork(SystemTime + Restaurant.ManagerServiceTime, "ManagerWork"));
            SortingScheduler(scheduler);
            return scheduler;
        }

        public static List<Event> GetBuffetStand(List<Event> scheduler, Restaurant restaurant, int[] parameters)
        {
            var time = (int)Generators.Normal(ref Generators.Seeds[1], ref Generators.Seeds[2], 3200, parameters[2]);
            restaurant.buffetQ.Peek().ConsumptionTime = SystemTime+time;
            switch (restaurant.buffetQ.Peek().AmountPeople)
            {
                case 1:
                    restaurant.BuffetStands--;
                    break;
                case 2:
                    restaurant.BuffetStands -= 2;
                    break;
                case 3:
                    restaurant.BuffetStands -= 3;
                    break;
                case 4:
                    restaurant.BuffetStands -= 4;
                    break;
            }

            Console.WriteLine("Grupa klientów o ID {0} i liczebności {1} idzie do Bufetu samoobsługowego.", restaurant.buffetQ.Peek().ID, restaurant.buffetQ.Peek().AmountPeople);
            restaurant.buffetGuests.Add(restaurant.buffetQ.Dequeue());
            scheduler.Add(new BuffetHandling(SystemTime+time,"BuffetHandling"));
            SortingScheduler(scheduler);
            return scheduler;
        }
        public static List<Event> GetCashier(List<Event> scheduler, Restaurant restaurant, int[] parameters)
        {
            var time = (int)Generators.Exponential(parameters[5], ref Generators.Seeds[1]);
            restaurant.Cashiers--;
            Console.WriteLine("Klient o ID {0} jest obsługiwany przez kasjera.", restaurant.cashRegisterQ.Peek().ID);
            scheduler.Add(new CashierWork(SystemTime+time,"CashierWork"));
            SortingScheduler(scheduler);
            restaurant.cashRegisterQ.Dequeue();
            return scheduler;
        }
        public static List<Event> GetDrink(List<Event> scheduler, Restaurant restaurant, int[] parameters)
        {
            var time = (int)Generators.Exponential(parameters[3], ref Generators.Seeds[1]);
            restaurant.Waiters--;
            Client temp = restaurant.drinkQ.Peek();
            temp.WaitingForWaiterTime = SystemTime - temp.WaitingForWaiterTime;
            Statistics.tableServiceTime.Add(temp.WaitingForWaiterTime);
            restaurant.drinkQ.Peek().ConsumptionTime = SystemTime+time;
            Console.WriteLine("Kelner podszedł do grupy {0}. Czekają teraz na napój. Na kelnera czekali {1} jednostek czasu", temp.ID, temp.WaitingForWaiterTime);
            restaurant.drinkGroup.Add(restaurant.drinkQ.Dequeue());
            scheduler.Add(new ServingDrink(SystemTime+time,"ServingDrink"));
            SortingScheduler(scheduler);
            return scheduler;
        }
        public static List<Event> GetMeal(List<Event> scheduler, Restaurant restaurant, int[] parameters)
        {
            var time = (int)Generators.Exponential(parameters[4], ref Generators.Seeds[1]);
            restaurant.Waiters--;
            restaurant.mealsQ.Peek().ConsumptionTime = SystemTime + time;
            Console.WriteLine("Kelner podszedł do grupy o ID {0} Czekają teraz na posiłek.", restaurant.mealsQ.Peek().ID);
            restaurant.mealGroup.Add(restaurant.mealsQ.Dequeue());
            scheduler.Add(new ServingMeal(SystemTime + time, "ServingMeal"));
            SortingScheduler(scheduler);
            return scheduler;
        }

        public static bool AvailableTable(Restaurant restaurant)
        {
            bool flag = false;
            switch (restaurant.tableQ.Peek().AmountPeople)
            {
                case 1: case 2:
                    if (restaurant.TwoSeat > 0 || restaurant.ThreeSeat > 0 || restaurant.FourSeat > 0) flag = true;
                    break;
                case 3:
                    if (restaurant.ThreeSeat > 0 || restaurant.FourSeat > 0) flag = true;
                    break;
                case 4:
                    if (restaurant.FourSeat > 0) flag = true;
                    break;
            }
            return flag;
        }
        public static void SortingByConsumptionTime(List<Client> consumptionGroup) // sorting by consumption time (people in buffet and people eating in restaurant
        {
            consumptionGroup.Sort((x, y) => x.ConsumptionTime.CompareTo(y.ConsumptionTime));
        }
        public static List<Event> ShowStatistics(List<Event> scheduler, Restaurant restaurant)
        {
            Console.WriteLine("\nStatystyki: ");
            Console.WriteLine("Czas systemowy: " + SystemTime);
            Console.WriteLine("Liczba wydarzeń: " + scheduler.Count);
            //ShowSchedule(scheduler);
            Console.WriteLine("\nDługość kolejki do stolików: " + restaurant.tableQ.Count);
            Console.WriteLine("Długość kolejki do bufetu: " + restaurant.buffetQ.Count);
            Console.WriteLine("Długość kolejki do kasy: " + restaurant.cashRegisterQ.Count);
            Console.WriteLine("\nLiczba grup w Bufecie: " + restaurant.buffetGuests.Count);
            Console.WriteLine("Liczba grup oczekujacych na kelnera [DRINK]: " + restaurant.drinkQ.Count);
            Console.WriteLine("Liczba grup oczekujacych na napój: " + restaurant.drinkGroup.Count);
            Console.WriteLine("Liczba grup oczekujących na kelnera [MEAL]: " + restaurant.mealsQ.Count);
            Console.WriteLine("Liczba grup oczekujących na posiłek: "+ restaurant.mealGroup.Count);
            Console.WriteLine("\nManager wolny: "+ restaurant.ManagerAvailable);
            Console.WriteLine("Liczba kelnerów: " + restaurant.Waiters);
            Console.WriteLine("Liczba stolików 2os: " + restaurant.TwoSeat);
            Console.WriteLine("Liczba stolików 3os: " + restaurant.ThreeSeat);
            Console.WriteLine("Liczba stolików 4os: " + restaurant.FourSeat);
            Console.WriteLine("Liczba stanowisk w bufecie: " + restaurant.BuffetStands);
            Console.WriteLine("Liczba kasjerów: " + restaurant.Cashiers);
            Console.WriteLine("Liczba osób w restauracji: "+ Statistics.ClientsInRestaurant);
            Console.WriteLine("Liczba zadowolonych klienów: " + restaurant.ClientsCounter);
            Console.WriteLine(" \n### CLICK ENTER TO CONTINUE ###\n####################################################################################");
            return scheduler;
        }
    }
}
