Session Export Extension for Gibraltar Loupe
========================================

Loupe collects a wealth of information in an extremely compact binary format. This add-in
gives you a way of exporting log messages and metrics from Loupe to text files that can
be easily parsed by other programs such as Excel or Splunk. 

Using this Extension
---------------

After building this project, register the Extension NuGet Package with the Loupe Server.  To
do this, you'll need to use the Loupe Server Administrator.  For more information on deploying
extensions, see:

[Loupe Extension Deployment](http://www.gibraltarsoftware.com/Support/Loupe/Documentation/WebFrame.html#AddIn_Deployment.html)


Implementation Notes
--------------------

This extension is compiled for .NET 4.0 but is compatible with both .NET 4.0 and .NET 4.5.

Building the Extension
-------------------

This project is designed for use with Visual Studio 2012 with NuGet package restore enabled.
When you build it the first time it will retrieve dependencies from NuGet.

Contributing
------------

Feel free to branch this project and contribute a pull request to the development branch. 
If your changes are incorporated into the master version they'll be published out to NuGet for
everyone to use!