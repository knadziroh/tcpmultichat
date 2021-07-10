using System;
using System.Net.Sockets;
using System.Threading;
using System.Text;
public class Client
{
    private static NetworkStream stream;
    public static TcpClient socketForServer;
    
    //fungsi utama(main)
    static public void Main(string[] args)
    {
        
        try
        {
            socketForServer = new TcpClient("127.0.0.1", 5000);
        }
        catch
        {
            Console.WriteLine("Failed to connect to server at {0}:999", "localhost");
            return;
        }

        NetworkStream networkStream = socketForServer.GetStream();
        
        System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(socketForServer.GetStream());
        Console.WriteLine("******* Client Program that Connected to Localhost on port No:10 *****");

        try
        {
            Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
            receiveThread.Start();
            Console.WriteLine("Type your message : ");
            string str = Console.ReadLine();
            while (str != "EXIT")
            {
                streamWriter.WriteLine(str);
                streamWriter.Flush();
                Console.WriteLine("Type your message : ");
                str = Console.ReadLine();
            }
            if (str == "EXIT")
            {
                streamWriter.WriteLine(str);
                streamWriter.Flush();
            }
        }

        catch
        {
            Console.WriteLine("Exception reading from Server");
        }
        networkStream.Close();
        Console.WriteLine("Press any key to exit from client program . . .");
        Console.ReadKey();
    }

    //fungsi menerima chat broadcast
    static void ReceiveMessage()
    {
        System.IO.StreamReader streamReader = new System.IO.StreamReader(socketForServer.GetStream());
        while (true)
        {
            try
            {
                byte[] data = new byte[64];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                string msg = streamReader.ReadLine();
                Console.WriteLine(msg);
            }

            catch(Exception e)
            {
                Console.Write(e.Message);
                break;
            }
        }
    }

}