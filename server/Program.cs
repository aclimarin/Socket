using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var ipAdress = IPAddress.Any;
        var port = 1234;

        //Create TCP/IP socket
        var serverSocket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        //Bind and listen
        serverSocket.Bind(new IPEndPoint(ipAdress, port));
        serverSocket.Listen(10);


        var localIp = Dns.GetHostAddresses(Dns.GetHostName())
            .Where(x => x.AddressFamily == AddressFamily.InterNetwork)
            .FirstOrDefault();

        Console.WriteLine($"Hi! I'm the Server and this is my access data:");
        Console.WriteLine($"IP Address: { localIp }");
        Console.WriteLine($"Port: { port }");
        
        
        Console.WriteLine("");
        Console.WriteLine("Now i'm waithing for a client...");

        //Accept connection from client
        var clientSocket = serverSocket.Accept();

        Console.WriteLine("Client connected!");

        //Receive data from client
        var data = new byte[1024];
        var byteRead = clientSocket.Receive(data);
        var message = Encoding.UTF8.GetString(data, 0, byteRead);
        Console.WriteLine($"Received: { message }");

        //Respond back
        var response = "Message received successfully!";
        var responseData = Encoding.UTF8.GetBytes(response);
        clientSocket.Send(responseData);

        clientSocket.Close();
        serverSocket.Close();
    }
}