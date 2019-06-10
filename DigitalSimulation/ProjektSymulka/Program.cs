using System;
using System.Collections.Generic;
using ProjektSymulka.Events;

namespace ProjektSymulka
{
    public class Program : Event
    {
        static void Main(string[] args)
        {
            ExecuteSimulation(340, 50, 100, 545, 2300, 700); //uA[0] uAvar[1] Bvar[2] lamDri[3] lamMeal[4] lCash[5] starting params
            Console.ReadKey();
        }
        public static void ExecuteSimulation(int uAmean, int Avar, int Bvar, int lambdaDrink, int lambdaMeal, int lambdaCashRegister)
        {
            int counter = 1000;
            string Thousand = "";
            bool flag = true; // if false program ends
            bool eventTrig; // conditional event flag
            int[] parameters = { uAmean, Avar, Bvar, lambdaDrink, lambdaMeal, lambdaCashRegister };
            while (flag)
            {
                // ### INITIALIZATION OF THE SYSTEM ###
                Restaurant restaurant = new Restaurant(); //restaurant - here we have all information about queues, clients, manager, waiters etc.
                List<Event> scheduler = new List<Event>(); //scheduler - list of events
                Console.WriteLine("##################\n####RESTAURANT####\n#####HAS BEEN#####\n#####CREATED######\n##################\n");
                scheduler = restaurant.Begin(scheduler, restaurant, parameters);
                // ### END OF INITIALIZATION ###
                Console.Write("\n###############\n1.Tryb krokowy\n2.Tryb ciągły\n###############\n");
                Console.Write("Jaki tryb? ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1: // stepping loop
                        {
                            while (restaurant.ClientsCounter < 100)
                            {
                                SystemTime = scheduler[0].EventTime;
                                scheduler[0].Execute(scheduler, restaurant, parameters);
                                scheduler.RemoveAt(0);
                                ConditionalEvents.ShowStatistics(scheduler, restaurant);
                                Console.ReadKey();

                                eventTrig = true;
                                while (eventTrig)
                                {
                                    eventTrig = false;
                                    if (restaurant.tableQ.Count > 0 && restaurant.ManagerAvailable == true &&
                                        ConditionalEvents.AvailableTable(restaurant) == true)
                                    {
                                        scheduler = ConditionalEvents.GetTable(scheduler, restaurant);
                                        eventTrig = true;
                                        ConditionalEvents.ShowStatistics(scheduler, restaurant);
                                        Console.ReadKey();
                                    } // getting table 

                                    if (restaurant.buffetQ.Count > 0 && restaurant.BuffetStands >=
                                        restaurant.buffetQ.Peek().AmountPeople)
                                    {
                                        scheduler = ConditionalEvents.GetBuffetStand(scheduler, restaurant, parameters);
                                        eventTrig = true;
                                        ConditionalEvents.ShowStatistics(scheduler, restaurant);
                                        Console.ReadKey();
                                    } // getting buffet stand

                                    if (restaurant.cashRegisterQ.Count > 0 && restaurant.Cashiers > 0)
                                    {
                                        scheduler = ConditionalEvents.GetCashier(scheduler, restaurant, parameters);
                                        eventTrig = true;
                                        ConditionalEvents.ShowStatistics(scheduler, restaurant);
                                        Console.ReadKey();
                                    } // getting a cashier

                                    if (restaurant.mealsQ.Count > 0 && restaurant.Waiters > 0)
                                    {
                                        scheduler = ConditionalEvents.GetMeal(scheduler, restaurant, parameters);
                                        eventTrig = true;
                                        ConditionalEvents.ShowStatistics(scheduler, restaurant);
                                        Console.ReadKey();
                                    } // taking order and getting main meal
                                    else if (restaurant.drinkQ.Count > 0 && restaurant.Waiters > 0)
                                    {
                                        scheduler = ConditionalEvents.GetDrink(scheduler, restaurant, parameters);
                                        eventTrig = true;
                                        ConditionalEvents.ShowStatistics(scheduler, restaurant);
                                        Console.ReadKey();
                                    } // taking order and getting drink
                                }
                            }
                            flag = false;
                            break;
                        }
                    case 2: //constant loop
                        {
                            while (restaurant.ClientsCounter < 2000)
                            {
                                SystemTime = scheduler[0].EventTime;
                                scheduler[0].Execute(scheduler, restaurant, parameters);
                                scheduler.RemoveAt(0);
                                eventTrig = true;
                                while (eventTrig)
                                {
                                    eventTrig = false;
                                    if (restaurant.tableQ.Count > 0 && restaurant.ManagerAvailable == true &&
                                        ConditionalEvents.AvailableTable(restaurant) == true)
                                    {
                                        scheduler = ConditionalEvents.GetTable(scheduler, restaurant);
                                        eventTrig = true;
                                    } // getting table

                                    if (restaurant.buffetQ.Count > 0 &&
                                        restaurant.BuffetStands >= restaurant.buffetQ.Peek().AmountPeople)
                                    {
                                        scheduler = ConditionalEvents.GetBuffetStand(scheduler, restaurant, parameters);
                                        eventTrig = true;
                                    } // getting buffet stand

                                    if (restaurant.cashRegisterQ.Count > 0 && restaurant.Cashiers > 0)
                                    {
                                        scheduler = ConditionalEvents.GetCashier(scheduler, restaurant, parameters);
                                        eventTrig = true;
                                    } // getting a cashier

                                    if (restaurant.mealsQ.Count > 0 && restaurant.Waiters > 0)
                                    {
                                        scheduler = ConditionalEvents.GetMeal(scheduler, restaurant, parameters);
                                        eventTrig = true;
                                    } // taking order and getting main meal
                                    else if (restaurant.drinkQ.Count > 0 && restaurant.Waiters > 0)
                                    {
                                        scheduler = ConditionalEvents.GetDrink(scheduler, restaurant, parameters);
                                        eventTrig = true;
                                    } // taking order and getting drink
                                }

                                Statistics.tableQueueLength.Add(restaurant.tableQ.Count);
                                Statistics.cashQueueLength.Add(restaurant.cashRegisterQ.Count);
                                if (restaurant.ClientsCounter == counter)
                                {
                                    Thousand = "Średnia oczekiwania na stolik:  " +
                                               Statistics.GetAverageInt(Statistics.tableQueueTime) + "\n";
                                    Thousand += "Średnia oczekiwania na kelnera:  " +
                                                Statistics.GetAverageInt(Statistics.tableServiceTime) + "\n";
                                    Thousand += "Średnia kolejka do stolików:  " +
                                                Statistics.GetAverageDouble(Statistics.tableQueueLength) + "\n";
                                    Thousand += "Średnia kolejka do kas:  " +
                                                Statistics.GetAverageDouble(Statistics.cashQueueLength) + "\n";
                                    Thousand += "Liczba osob w restauracji:  " +
                                                Statistics.ClientsInRestaurant;
                                    Console.WriteLine("{0}k: \n\n" + Thousand, counter / 1000);
                                    counter += 1000;
                                    Thousand = "";
                                }

                                //SystemTime = scheduler[0].EventTime;
                            }

                            ConditionalEvents.ShowStatistics(scheduler, restaurant);
                            Statistics.SaveStatistics();
                            flag = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Błąd. Spróbuj ponownie");
                            break;
                        }
                }
            }
            Console.WriteLine("Dziękujemy za symulację");
            Console.ReadKey();
        }
    }
}
