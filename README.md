Loupe.Samples
=============

Code samples for Loupe - including Agent usage and Add In development

Caliper
--------
Caliper is a handy little class you can use to easily add Loupe metrics to your code.

Here's how easy it is to time a bit of code:

```C#
    using (new Caliper("Tests.DoWork"))
    {
        // Do something
    }
```

or like this:

```C#
    var timer = new Caliper("Tests.StartStop");
    private void btnStartWorking_Click(object sender, EventArgs e)
    {
        timer.Start();
    }

    private void btnStopWorking_Click(object sender, EventArgs e)
    {
        timer.Stop();
    }
```

AddIn.Export
------------
This add-in allows you to export Loupe log messages and/or metrics as text files.

Files can be exported on-demand from Loupe Desktop using right-click context menus
or the add-in can be configured to export files automatically as logs are added
to the repository.  This latter usage is great for integrating Loupe with other
log analysis software such as Loggly, Splunk, PaperTrail and LogStash.

Note that the AddIn.Export project provides a more detailed README.md file.
