
// --------------------------------------------------
// Read following article for Threads safety 
// --------------------------------------------------
Please READ following article for understanding thread safety

https://stackoverflow.com/questions/505515/c-sharp-thread-safety-with-get-set

https://stackoverflow.com/questions/7161413/thread-safe-properties-in-c-sharp

accessing variable in C# is atomic operation if its smaller than 32 bits
https://stackoverflow.com/questions/9666/is-accessing-a-variable-in-c-sharp-an-atomic-operation

general rules for making methods thread safe
https://stackoverflow.com/questions/9848067/what-makes-a-method-thread-safe-what-are-the-rules

// ----------------
// Pointe 1
// ----------------
I am trying to create thread safe properties in C# and I want to make sure that I am on the correct path - here is what I have done -

private readonly object AvgBuyPriceLocker = new object();
private double _AvgBuyPrice;
private double AvgBuyPrice 
{
    get
    {
        lock (AvgBuyPriceLocker)
        {
            return _AvgBuyPrice;
        }
    }
    set
    {
        lock (AvgBuyPriceLocker)
        {
            _AvgBuyPrice = value;
        }
    }
}

Answer : 
The locks, as you have written them are pointless. The thread reading the variable, for example, will:

Acquire the lock.
Read the value.
Release the lock.
Use the read value somehow.
There is nothing to stop another thread from modifying the value after step 3. As variable access in .NET is atomic (see caveat below), the lock is not actually achieving much here: merely adding an overhead. Contrast with the unlocked example:

Read the value.
Use the read value somehow.
Another thread may alter the value between step 1 and 2 and this is no different to the locked example.

If you want to ensure state does not change when you are doing some processing, you must read the value and do the processing using that value within the contex of the lock:

Acquire the lock.
Read the value.
Use the read value somehow.
Release the lock.
Having said that, there are cases when you need to lock when accessing a variable. These are usually due to reasons with the underlying processor: a double variable cannot be read or written as a single instruction on a 32 bit machine, for example, so you must lock (or use an alternative strategy) to ensure a corrupt value is not read.

//////////////////////////////////////////
// Point 2
//////////////////////////////////////////
Thread safety is not something you should add to your variables, it is something you should add to your "logic". If you add locks to all your variables, your code will still not necessarily be thread safe, but it will be slow as hell. To write a thread-safe program, Look at your code and decide where multiple threads could be using the same data/objects. Add locks or other safety measures to all those critical places.

For instance, assuming the following bit of pseudo code:

void updateAvgBuyPrice()
{
    float oldPrice = AvgBuyPrice;
    float newPrice = oldPrice + <Some other logic here>
    //Some more new price calculation here
    AvgBuyPrice = newPrice;
}
If this code is called from multiple threads at the same time, your locking logic has no use. Imagine thread A getting AvgBuyPrice and doing some calculations. Now before it is done, thread B is also getting the AvgBuyPrice and starting calculations. Thread A in the meantime is done and will assign the new value to AvgBuyPrice. However, just moments later, it will be overwritten by thread B (which still used the old value) and the work of thread A has been lost completely.

So how do you fix this? If we were to use locks (which would be the ugliest and slowest solution, but the easiest if you're just starting with multithreading), we need to put all the logic which changes AvgBuyPrice in locks:

void updateAvgBuyPrice()
{
    lock(AvgBuyPriceLocker)
    {
        float oldPrice = AvgBuyPrice;
        float newPrice = oldPrice + <Some other code here>
        //Some more new price calculation here
        AvgBuyPrice = newPrice;
    }
}
Now, if thread B wants to do the calculations while thread A is still busy, it will wait until thread A is done and then do its work using the new value. Keep in mind though, that any other code that also modifies AvgBuyPrice should also lock AvgBuyPriceLocker while it's working!

Still, this will be slow if used often. Locks are expensive and there are a lot of other mechanism to avoid locks, just search for lock-free algorithms.



