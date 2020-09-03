using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Utils
{
    public static class Listener
    {
        const int FLOAT_SIZE = sizeof(float);
        private static IEnumerable<IEnumerable<T>> Split<T>(this T[] array, int size)
        {
            for (int i = 0; i < array.Length / size; i++)
                yield return array.Skip(i * size).Take(size);
        }

        public static Thread Listen()
        {
            return new Thread(new ThreadStart(Udp));
        }
        private static void Udp()
        {

            using (var client = new UdpClient(9001))
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12001); // endpoint where server is listening
                client.Connect(ep);

                while (true)
                {
                    var receivedData = client.Receive(ref ep).Split(FLOAT_SIZE);
                    var data = receivedData.Select(joint => BitConverter.ToSingle(joint.ToArray(), 0));

                    DataTransfer.queue1.Enqueue(data.Take(5).ToArray());
                    DataTransfer.queue2.Enqueue(data.Skip(5).ToArray());

                    // DataTransfer.queue1.Enqueue(data.Take(14).ToArray());
                    // DataTransfer.queue2.Enqueue(data.Skip(14).ToArray());

                    // DataTransfer.queue1.Enqueue(data.Take(48).ToArray());
                    // // for 2 arms
                    // DataTransfer.queue2.Enqueue(data.Skip(48).ToArray());

                }
            }
        }
    }
}