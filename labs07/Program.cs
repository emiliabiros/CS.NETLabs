using System.Diagnostics;
using System.Net.NetworkInformation;

namespace ThreadsTask
{
    class Program
    {
        const int MaxDegreeOfParallelism = 4;
        const int PingTimeout = 1000;
        const int LockTimeout = 100;
        const int RetryDelay = 10;

        static void Main(string[] args)
        {
            var servers = LoadServers("/Users/emilia/Code/C#.NET/labs07/ping.txt");

            Console.WriteLine("Sekwencyjnie");
            TimerMethod(() => PingServersSequentially(servers));
            Console.WriteLine();

            Console.WriteLine("Równolegle (AsParallel, max 4 wątki)");
            TimerMethod(() => PingServersInParallel(servers));
            Console.WriteLine();

            Console.WriteLine("Sekwencyjnie (Za pomocą tasków i Monitor.TryEnter)");
            TimerMethod(() => PingServersWithTasks(servers));

            Console.ReadKey();
        }

        static List<(string Country, string Address)> LoadServers(string filePath)
        {
            return File.ReadAllLines(filePath)
                .Select(line => line.Split(';'))
                .Where(x => x.Length == 2)
                .Select(x => (Country: x[0].Trim(), Address: x[1].Trim()))
                .ToList();
        }

        static void TimerMethod(Action action)
        {
            var stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            Console.WriteLine($"Czas: {stopwatch.ElapsedMilliseconds}ms");
        }

        static void PingServersSequentially(List<(string Country, string Address)> servers)
        {
            foreach (var server in servers)
            {
                PingServer(server);
            }
        }

        static void PingServersInParallel(List<(string Country, string Address)> servers)
        {
            servers
                .AsParallel()
                .WithDegreeOfParallelism(MaxDegreeOfParallelism)
                .ForAll(PingServer);
        }

        static void PingServer((string Country, string Address) server)
        {
            using (var ping = new Ping())
            {
                try
                {
                    var reply = ping.Send(server.Address, PingTimeout);
                    
                    if (reply.Status == IPStatus.Success)
                    {
                        Console.WriteLine($"OK {server.Country}: {reply.RoundtripTime}ms");
                    }
                    else
                    {
                        Console.WriteLine($"Błąd {server.Country}: {reply.Status}");
                    }
                }
                catch
                {
                    Console.WriteLine($"Wyjątek dla {server.Country}");
                }
            }
        }

        static void PingServersWithTasks(List<(string Country, string Address)> servers)
        {
            var queue = new Queue<(string Country, string Address)>(servers);
            var lockObject = new object();
            var tasks = new List<Task>();

            for (int i = 0; i < MaxDegreeOfParallelism; i++)
            {
                tasks.Add(Task.Run(() => WorkerThread(queue, lockObject)));
            }

            Task.WaitAll(tasks.ToArray());
        }

        static void WorkerThread(Queue<(string Country, string Address)> queue, object lockObject)
        {
            while (true)
            {
                var server = TryDequeueServer(queue, lockObject);

                if (server.HasValue)
                {
                    PingServer(server.Value);
                }
                else if (server == null)
                {
                    return;
                }
                else
                {
                    Thread.Sleep(RetryDelay);
                }
            }
        }

        static (string Country, string Address)? TryDequeueServer(
            Queue<(string Country, string Address)> queue,
            object lockObject)
        {
            bool lockAcquired = false;

            try
            {
                Monitor.TryEnter(lockObject, TimeSpan.FromMilliseconds(LockTimeout), ref lockAcquired);

                if (lockAcquired && queue.Count > 0)
                {
                    return queue.Dequeue();
                }
                else if (lockAcquired && queue.Count == 0)
                {
                    return null;
                }
            }
            finally
            {
                if (lockAcquired)
                {
                    Monitor.Exit(lockObject);
                }
            }

            return null;
        }
    }
}