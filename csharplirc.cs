using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using Lirc;

namespace LircTest
{
  class Program
  {
    static void Main(string[] args)
    {
      IrReceiver ir = new IrReceiver();
      ir.IrKeyPress += RecievedIrKey;
      ir.Start();

      Console.WriteLine("IR Test running press any key to exit");
      Console.ReadKey();

      ir.Stop();
      ir.IrKeyPress -= RecievedIrKey;

      Console.WriteLine("IR Test exited...");
    }

    static void RecievedIrKey(LircEventArgs e)
    {
      Console.WriteLine("IR Key pressed");
      Console.WriteLine("Key Name   : {0}", e.KeyCode);
      Console.WriteLine("Remote Name: {0}", e.RemoteName);
      Console.WriteLine();
    }

  }
}
