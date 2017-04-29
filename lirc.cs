using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Lirc
{
  public class IrReceiver
  {
    public delegate void IrKeyPressHandler(LircEventArgs e);

    public event IrKeyPressHandler IrKeyPress;

    Thread receiveThread;
    bool receiveRunning = false;

    public void Start()
    {
      receiveThread = new Thread(new ThreadStart(receiveThreadRunner));
      receiveThread.Name = "CSharpLircReceiver";

      receiveRunning = true;
      receiveThread.Start();
    }

    public void Stop()
    {
      receiveRunning = false;
      receiveThread.Join();
    }

    private void receiveThreadRunner()
    {
      string lircSocketName = "/var/run/lirc/lircd";

      byte[] buffer = new byte[500];
      Socket socket  = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
      UnixEndPoint unixEp = new UnixEndPoint(lircSocketName); // NOTE: This is NOT a .NET base class it's in the UnixEndPoint.cs file

      socket.Connect(unixEp);

      while(receiveRunning)
      {
        if(socket.Poll(1000, SelectMode.SelectRead)) // poll socket once per second
        {
          int size = socket.Receive(buffer);
          string command = Encoding.ASCII.GetString(buffer, 0, size);
          string[] commandParts = command.Split(' ');
          if(IrKeyPress != null)
          {
            IrKeyPress(new LircEventArgs(commandParts[2], commandParts[3]));
          }
        }
      }
      socket.Close();
    }

  }

  public class LircEventArgs:EventArgs
  {
    public string KeyCode { get; private set; }
    public string RemoteName { get; private set; }

    public LircEventArgs(string keyCode, string remoteName)
    {
      KeyCode = keyCode;
      RemoteName = remoteName;
    }

  }

}
