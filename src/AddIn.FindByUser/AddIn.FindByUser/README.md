Find By User AddIn for Gibraltar Loupe
======================================

This add-in indexes the users associated with each log session. A Session Summary View control
allows Loupe Desktop to quickly identify the log sessions associated with a user on any day. 

Using this Add-In
---------------

After building this project, copy the Gibraltar.AddIn.FindByUser.dll to the public data folder
associated with Loupe Desktop and Loupe Server. Typically, this folder is located here:

    C:\ProgramData\Gibraltar\Add In

If you wish to use VistaDB as the data store for indexed data, copy FindByUser.vdb4 to this 
directory as well.  Alternately, you can run the FindByUser.sql script to initialize a
SQL Server database with the necessary schema.

To enable the add-in, restart Loupe Desktop then go to the backstage area and enable the add-in.
Configure the add-in to reference your data store then restart Loupe Desktop.

Once you have restarted Loupe, sessions can be indexed for in three ways:

1. Automatic Indexing by Loupe Desktop
2. Automatic Indexing by Loupe Server
3. Manual Indexing by Loupe Desktop

**Automatic Indexing by Loupe Desktop**

If you configured the add-in to "Scan sessions automatically", then sessions will be indexeds
whenever you download them to Loupe Desktop. Note that, by default, Loupe Desktop only downloads
session summaries, not full logs.  If you want logs to by automatically indexed, you might also
wish to configure Loupe Desktop to auomatically download sessions.

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

Building the Add-In
-------------------

This project is designed for use with Visual Studio 2012 with NuGet package restore enabled.
When you build it the first time it will retrieve dependencies from NuGet.

Contributing
------------

Feel free to branch this project and contribute a pull request to the development branch. 
If your changes are incorporated into the master version they'll be published out to NuGet for
everyone to use!