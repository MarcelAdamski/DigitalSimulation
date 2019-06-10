using System;
using System.Collections.Generic;
using System.IO;

namespace ProjektSymulka
{
    class Statistics
    {
        private static int _clientsInRestaurant;
        public static List<int> clientsInRestaurant = new List<int>();
        public static List<int> tableQueueTime = new List<int>();
        public static List<int> tableServiceTime = new List<int>();
        public static List<double> tableQueueLength = new List<double>();   
        public static List<double> cashQueueLength = new List<double>();

        public static int GetAverageInt(List<int> list)
        {
            int sum = 0;
            foreach (var x in list)
            {
                sum += x;
            }
            return sum / list.Count;
        }
        public static double GetAverageDouble(List<double> list)
        {
            double sum = 0;
            foreach (var x in list)
            {
                sum += x;
            }
            return sum / list.Count;
        }
        public static void SaveStatistics()
        {
            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string averageTableQueueTime = pathDesktop + "\\AverageTableQueueTime.csv";
            string averageTableQueueLength = pathDesktop + "\\AverageTableQueueLength.csv";
            string averageTableServiceTime = pathDesktop + "\\AverageTableServiceTime.csv";
            string averageCashQueueLength = pathDesktop + "\\AverageCashQueueLength.csv";
            string averageClientsInRestaurant = pathDesktop + "\\AverageClientsInRestaurant.csv";

            if (!File.Exists(averageTableQueueTime))
            {
                File.Create(averageTableQueueTime).Close();
            }
            if (!File.Exists(averageTableQueueLength))
            {
                File.Create(averageTableQueueLength).Close();
            }
            if (!File.Exists(averageTableServiceTime))
            {
                File.Create(averageTableServiceTime).Close();
            }
            if (!File.Exists(averageCashQueueLength))
            {
                File.Create(averageCashQueueLength).Close();
            }
            if (!File.Exists(averageClientsInRestaurant))
            {
                File.Create(averageClientsInRestaurant).Close();
            }

            string delimeter = "\n";

            using (TextWriter writer = File.CreateText(averageTableQueueTime))
            {
                for (int i = 0; i < tableQueueTime.Count; i++)
                {
                    writer.WriteLine(string.Join(delimeter, tableQueueTime[i]));
                }
            }
            
            using (TextWriter writer = File.CreateText(averageTableQueueLength))
            {
                for (int i = 0; i < tableQueueLength.Count; i++)
                {
                    writer.WriteLine(string.Join(delimeter, tableQueueLength[i]));
                }
            }

            using (TextWriter writer = File.CreateText(averageTableServiceTime))
            {
                for (int i = 0; i < tableServiceTime.Count; i++)
                {
                    writer.WriteLine(string.Join(delimeter, tableServiceTime[i]));
                }
            }

            using (TextWriter writer = File.CreateText(averageCashQueueLength))
            {
                for (int i = 0; i < cashQueueLength.Count; i++)
                {
                    writer.WriteLine(string.Join(delimeter, cashQueueLength[i]));
                }
            }

            using (TextWriter writer = File.CreateText(averageClientsInRestaurant))
            {
                for (int i = 0; i < clientsInRestaurant.Count; i++)
                {
                    writer.WriteLine(string.Join(delimeter, clientsInRestaurant[i]));
                }
            }
        }
        public static int ClientsInRestaurant
        {
            get { return _clientsInRestaurant; }
            set
            {
                _clientsInRestaurant = value;
                _clientsInRestaurant = ClientsInRestaurant;
            }
        }
    }
}
