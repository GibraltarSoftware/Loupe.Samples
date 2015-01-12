Session Export AddIn for Gibraltar Loupe
========================================

Loupe collects a wealth of information in an extremely compact binary format. This add-in
gives you a way of exporting log messages and metrics from Loupe to text files that can
be easily parsed by other programs such as Excel or Splunk. 

Using this Add-In
---------------

After building this project, copy the Gibraltar.AddIn.Export.dll to the public data folder
associated with Loupe Desktop and Loupe Server. Typically, this folder is located here:

    C:\ProgramData\Gibraltar\Add In

To enable the add-in restart Loupe Desktop then go to the backstage area and enable the add-in.
This will prompt you to select a folder into which exported files will be stored.  If you are
using Loupe Server, you'll want to configure a path that is legal both on your local Loupe Desktop
and also on your Loupe Server.  By default, exported log files will be stored here:

    C:\Loupe Exports


Implementation Notes
--------------------

This add-in is compiled for .NET 4.0 but is compatible with both .NET 4.0 and .NET 4.5.

Building the Add-In
-------------------

This project is designed for use with Visual Studio 2012 with NuGet package restore enabled.
When you build it the first time it will retrieve dependencies from NuGet.

Contributing
------------

Feel free to branch this project and contribute a pull request to the development branch. 
If your changes are incorporated into the master version they'll be published out to NuGet for
everyone to use!