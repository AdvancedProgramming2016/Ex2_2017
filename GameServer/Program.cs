using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Views.Handlers;
using GameServer.Controllers.Invokers;
using GameServer.Controllers.Servers;
using GameServer.Models;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Connection configurations.
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            string ip = ConfigurationManager.AppSettings["ip"];

            //Logic handlers.
            IController controller = new MainController();
            IClientHandler clientHandler = new ClientHandler(controller);
            IModel model = new Model(controller);
            controller.SetClientHandler(clientHandler);
            controller.SetModel(model);
            IServer server = new TcpServer();

            //Activates the server.
            server.Start(ip, port, clientHandler);
            server.Wait();
        }
    }
}