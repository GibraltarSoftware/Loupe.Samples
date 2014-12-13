Loupe.Samples
=============

Code samples for Loupe - including Agent usage and Add In development

Caliper
--------
Caliper is a handy little class you can use to easily add Loupe metrics to your code.

Here's how easy it is to time a bit of code:

```cs
    using (new Caliper("Tests.DoWork"))
    {
        // Do something
    }
```

or like this:

```cs
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
