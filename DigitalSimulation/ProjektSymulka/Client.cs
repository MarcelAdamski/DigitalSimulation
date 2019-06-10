using System;

namespace ProjektSymulka
{
    public class Client
    {
        private int _clientId; // clients ID number
        private int _amountPeople; // number of people in group
        private readonly string _type; // buffet client or restaurant client
        private int _consumptionTime; // consumption time. In restaurant - for the first time is drink, then meal, in buffet - time spent in buffet
        private int _table; // when clients sit in restaurant they get this to know which table they took
        private int _waitingForTableTime; // time needed for statistic to get average waiting time for table
        private int _waitingForWaiterTime; // time needed for statistic to get average waiting time for waiter

        public Client(int id, string type)
        {
            _clientId = id;
            _type = type;
            _consumptionTime = 0;
            _table = 0;
            _waitingForTableTime = 0;
            _waitingForWaiterTime = 0;
            double probability = Generators.Uniform(ref Generators.Seeds[1]);
            if (probability >=0 && probability< 0.11) _amountPeople = 1;
            else if(probability >=0.11 && probability< 0.44) _amountPeople = 2;
            else if(probability >=0.44 && probability< 0.77) _amountPeople = 3;
            else _amountPeople = 4;
        }
        public int ID
        {
            get { return _clientId; }
            set
            {
                _clientId = value;
                _clientId = ID;
            }
        }
        
        public int AmountPeople
        {
            get { return _amountPeople; }
            set
            {
                _amountPeople = value;
                _amountPeople = AmountPeople;
            }
        }

        public int ConsumptionTime
        {
            get { return _consumptionTime; }
            set
            {
                _consumptionTime = value;
                _consumptionTime = ConsumptionTime;
            }
        }
        public int Table
        {
            get { return _table; }
            set
            {
                _table = value;
                _table = Table;
            }
        }
        public int WaitingForTableTime
        {
            get { return _waitingForTableTime; }
            set
            {
                _waitingForTableTime = value;
                _waitingForTableTime = WaitingForTableTime;
            }
        }
        public int WaitingForWaiterTime
        {
            get { return _waitingForWaiterTime; }
            set
            {
                _waitingForWaiterTime = value;
                _waitingForWaiterTime = WaitingForWaiterTime;
            }
        }
    }
}
