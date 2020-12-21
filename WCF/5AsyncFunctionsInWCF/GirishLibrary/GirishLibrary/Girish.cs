using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace GirishLibrary
{
    

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Girish : IGirish
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public Student GetStudent(int roll)
        {
            return new Student() { Roll = roll, Name = "Girish" };
        }

        public string SampleMethod(string msg)
        {
            Console.WriteLine("Called synchronous sample method with \"{0}\"", msg);
            return "The sychronous service greets you: " + msg;
        }

        // This asynchronously implemented operation is never called because
        // there is a synchronous version of the same method.
        public IAsyncResult BeginSampleMethod(string msg, AsyncCallback callback, object asyncState)
        {
            Console.WriteLine("BeginSampleMethod called with: " + msg);
            return new CompletedAsyncResult<string>(msg);
        }

        public string EndSampleMethod(IAsyncResult r)
        {
            CompletedAsyncResult<string> result = r as CompletedAsyncResult<string>;
            Console.WriteLine("EndSampleMethod called with: " + result.Data);
            return result.Data;
        }

        public IAsyncResult BeginServiceAsyncMethod(string msg, AsyncCallback callback, object asyncState)
        {
            Console.WriteLine("BeginServiceAsyncMethod called with: \"{0}\"", msg);
            Thread.Sleep(2000);
            return new CompletedAsyncResult<string>(msg+ "async called");
        }

        public string EndServiceAsyncMethod(IAsyncResult r)
        {
            CompletedAsyncResult<string> result = r as CompletedAsyncResult<string>;
            Console.WriteLine("EndServiceAsyncMethod called with: \"{0}\"", result.Data);
            return result.Data;
        }
    }


    // Simple async result implementation.
    class CompletedAsyncResult<T> : IAsyncResult
    {
        T data;

        public CompletedAsyncResult(T data)
        { this.data = data; }

        public T Data
        { get { return data; } }

        #region IAsyncResult Members
        public object AsyncState
        { get { return (object)data; } }

        public WaitHandle AsyncWaitHandle
        { get { throw new Exception("The method or operation is not implemented."); } }

        public bool CompletedSynchronously
        { get { return true; } }

        public bool IsCompleted
        { get { return true; } }
        #endregion
    }
}
