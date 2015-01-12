Caliper Metric Wrapper
======================

A caliper is an instrument for measuring thickness, diameter or distance between surfaces.
Caliper is a handy little class you can use to measure the time consumed by a block of code.

You probably already know that you can measure the duration taken by a method in Loupe by 
applying a GTimer attribute to that method.  But what if the block of code you're interested
in measuring is just a part of a method?  Using a Caliper, you can wrap that code block like this:

```C#
    using (new Caliper("Tests.DoWork"))
    {
        // Do something
    }
```

Or maybe you want to time an asynchronous process that starts in one method and ends  in another?
You can use a Caliper instance to measure the duration of that process like this:

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

Either way, Caliper will create a custom metric for you allowing you to analyze the performance
of that block of code in Loupe Desktop, just like you'd do for any other metric.  Just give each
of your metrics a dot-delimited name and they'll be available as a hierarchy in the Metrics tab
of Loupe Desktop under the Caliper node.

Implementation Notes
--------------------

The Caliper project includes a simple WinForm app that demonstrates both usages of Caliper.
But to integrate Caliper with your programs, just add the Caliper.cs file to your project.
