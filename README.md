# Loupe.Samples #

Code samples for Loupe - including Agent usage and Extension development

## AddIn.Export ##
This add-in allows you to export Loupe log messages and/or metrics as text files.

Files can be exported on-demand from Loupe Desktop using right-click context menus
or the add-in can be configured to export files automatically as logs are added
to the repository.  This latter usage is great for integrating Loupe with other
log analysis software such as Loggly, Splunk, PaperTrail and LogStash.

Note that the AddIn.Export project provides a more detailed README.md file.

## AddIn.FindByUser ##

*Note*: this extension duplicates functionality built into Loupe 4.0 and is provided
as an example of how to implement similar functionality only.

This extension indexes all the users associated with each log session so that you
can quickly find the logs associated with a particular user.  The indexed data can
be stored in either a VistaDB or SQL Server database.  

Full details are provided in a README.md file associated with the AddIn.FindByUser project.

## Caliper ##

Caliper is a handy little class you can use to easily add Loupe metrics to your code.

Here's how easy it is to measure the duration of a block of code:

```C#
    using (new Caliper("Tests.DoWork"))
    {
        // Do something
    }
```

or you could measure an asynchronous process like this:

```C#
    var timer = new Caliper("Tests.StartStop");
    private void Start()
    {
        timer.Start();
        // Start doing something...
    }

    private void Stop()
    {
        // ... all done!
        timer.Stop();
    }
```

## Loupe.Export ##

This is a command line utility that does a simplified version of what the Export Extension does.
Call it to dump the log messages from a single Loupe log file (.glf) into a text file.  Use -help
to get a list of all command options.

## Loupe API ##

A sample HTML and jQuery project that shows how to call the Loupe API.

The project includes examples of logging on and off, fetching and
updating data, searching for users and sessions, and remove accounts.