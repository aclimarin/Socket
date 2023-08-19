using System.Net.Sockets;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hi! I'm the client! Please insert the server's access data:");
        Console.WriteLine("IP Address:");
        var serverIp = Console.ReadLine();

        Console.WriteLine("Port:");
        var port = Convert.ToInt32(Console.ReadLine());

        // Create TCP/IP socket
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        // Connect
        clientSocket.Connect(serverIp, port);
        
        Console.WriteLine("OK, i'm in. Now write a message to the server");
        var message = Console.ReadLine();
        
        var data = Encoding.UTF8.GetBytes(message);
        clientSocket.Send(data);

        var responseData = new byte[1024];
        var bytesRead = clientSocket.Receive(responseData);
        var response = Encoding.UTF8.GetString(responseData, 0, bytesRead);
        Console.WriteLine($"Server response: { response }");

        clientSocket.Close();
        
    }
}