# raspberrypi-csharp-lirc
Quick example written in C# to read the Lirc (Infrared Daemon) on a Raspberry Pi configured with a receiver and the Lirc software

Nothing special, just a quick bit of code to read the lirc unix domain socket on an RPi

I got the unix domain endpoint code from this stackoverflow post:

*http://stackoverflow.com/questions/40195290/how-to-connect-to-a-unix-domain-socket-in-net-core-in-c-sharp*

the rest I cobbled together myself.

The **lirc.cs** file is the main class, it will raise a thread driven event when a key is pressed on the remote.

**csharplirc.cs** is a console mode program showing how to use it.

This was written and debugged entirely on my RPi, I didn't use Visual Studio or anything like that, simply:

*sudo apt-get install mono-complete*

then used a text editor of choice (In my case mcedit from midnight commander)
everything was then compiled using:

*dmcs csharplirc.cs lirc.cs UnixEndPoint.cs -out:lirctest*

and then

*./lirctest*

to run it.

**Please note, this code has not been tested under dotnet core, but as it's fairly standard C# it should probably work ok**
