using System;
using System.Collections.Generic;

namespace ProjektSymulka.Events
{
    public class Event
    {
        private int _eventTime;
        private string _type;
        private static int _systemTime;

        public Event() { }

        public Event(int eventTime, string type)
        {
            _eventTime = eventTime;
            _type = type;
            //Console.WriteLine("### Wydarzenie zaplanowane. Typ wydarzenia: {0} ###",type);
        }

        public static readonly Random Rnd =new Random();

        public static void ShowSchedule(List<Event> scheduler)
        {
            int i=1;
            Console.WriteLine("Aktualne wydarzenia: ");
            foreach (var sched in scheduler)
            {
                Console.WriteLine("{0}. Nazwa: {1} || Nastąpi w czasie: {2} || Czas systemowy: {3}",i,sched._type,sched._eventTime, _systemTime);
                i++;
            }
            Console.WriteLine();
        }

        public static void SortingScheduler(List<Event> scheduler)
        {
            scheduler.Sort((x,y)=>x._eventTime.CompareTo(y._eventTime));
        }

        public static int SystemTime
        {
            get { return _systemTime;}
            set
            {
                _systemTime = value;
                _systemTime = SystemTime;
            }
        }

        public int EventTime
        {
            get { return _eventTime; }
            set
            {
                _eventTime = value;
                _eventTime = EventTime;
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                _type = Type;
            }
        }

        public virtual void Execute(List<Event> scheduler, Restaurant restaurant, int[] parameters)
        {
        }
    }
}
