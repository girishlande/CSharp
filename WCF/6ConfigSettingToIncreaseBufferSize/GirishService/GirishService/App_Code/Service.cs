using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
	public string GetData(int value)
	{
		return string.Format("You entered: {0}", value);
	}

    public void Process(byte[] array)
    {
        Console.WriteLine("Service received byte array:" + array.Length);
        string result = System.Text.Encoding.UTF8.GetString(array);
        Console.WriteLine("Content:" + result);
    }
}
