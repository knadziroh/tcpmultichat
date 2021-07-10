using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
public class Server
{
    public static TcpListener tcpListener;
    public static List<TcpClient> tcpClientLists = new List<TcpClient>();

    //membaca input dari client
    static void Listeners()
    {
        TcpClient tcpClient = tcpListener.AcceptTcpClient();
        tcpClientLists.Add(tcpClient);
            System.IO.StreamReader streamReader = new System.IO.StreamReader(tcpClient.GetStream());
            while (true)
            {
                string theString = streamReader.ReadLine();
                Console.WriteLine("Message recieved by client :" + theString);
                Broadcast(theString, tcpClient);
                if (theString == "exit")
                    break;
            }
            streamReader.Close();

        Console.WriteLine("Press any key to exit from server program . . .");
        Console.ReadKey();
    }

    //fungsi broadcast untuk menyebarkan chat ke client lain
    public static void Broadcast(string msg, TcpClient tcpClient)
    {
        foreach(TcpClient client in tcpClientLists)
        {
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(client.GetStream());
            if(client != tcpClient)
            {
              streamWriter.WriteLine("Client : " + msg);
            }
            streamWriter.Flush();
        }
    }

    //fungsi utama(main)
    public static void Main()
    {
        tcpListener = new TcpListener(IPAddress.Any, 5000);
        tcpListener.Start();
        
        Console.WriteLine("************ Server Program ************");
        while (true)
        {
            Thread newThread = new Thread(new ThreadStart(Listeners));
            newThread.Start();
        }
    }
}