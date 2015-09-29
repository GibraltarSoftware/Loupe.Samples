Find By User Extension for Gibraltar Loupe
======================================

This extension indexes the users associated with each log session. A Session Summary View control
allows Loupe Desktop to quickly identify the log sessions associated with a user on any day. 

Using this Extension
---------------

After building this project, register the Extension NuGet Package with the Loupe Server.  To
do this, you'll need to use the Loupe Server Administrator.  For more information on deploying
extensions, see:

[Loupe Extension Deployment](http://www.gibraltarsoftware.com/Support/Loupe/Documentation/WebFrame.html#AddIn_Deployment.html)

If you wish to use VistaDB as the data store for indexed data, copy FindByUser.vdb4 to this 
directory as well.  Alternately, you can run the FindByUser.sql script to initialize a
SQL Server database with the necessary schema.

To enable the extension, start Loupe Server Administrator then go to the backstage area and enable the extension.
Configure the extension to reference your data store then restart Loupe Server.

Once you have restarted Loupe, sessions can be indexed for in three ways:

1. Automatic Indexing by Loupe Server
2. Manual Indexing by Loupe Desktop

**Automatic Indexing by Loupe Server**

If you have a private Loupe Server or an Loupe Server Enterprise subscription you can run custom add-ins
in Loupe Server.  In that case, sessions will be automatically indexed by Loupe Server as they are received.

**Manual Indexing by Loupe Desktop**

You can also right-click on a multi-selected a set of downloaded sessions in Loupe Desktop and choose
the option to "Scan sessions for users". This option may be used in combination with automatic
indexing if you wish to reanalyze sessions.


Implementation Notes
--------------------

This add-in is compiled for .NET 4.0 but is compatible with both .NET 4.0 and .NET 4.5.

By default, this add-in retains 30 days of index data.  If you wish to change the data retention period, 
update DataRetentionDays in SessionFilterView.cs.

Building the Extension
-------------------

This project is designed for use with Visual Studio 2012 with NuGet package restore enabled.
When you build it the first time it will retrieve dependencies from NuGet.

Contributing
------------

Feel free to branch this project and contribute a pull request to the development branch. 
If your changes are incorporated into the master version they'll be published out to NuGet for
everyone to use!