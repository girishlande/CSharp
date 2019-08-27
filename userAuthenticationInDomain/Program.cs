using System;
using System.DirectoryServices.AccountManagement;

namespace userContext
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            PrincipalContext thisPrincipalContext =
    new PrincipalContext(ContextType.Domain);  // default doma

            if(thisPrincipalContext.ValidateCredentials("gaurav.datir","hjkl@123"))
            {
                Console.WriteLine("user is valid");
            } else
            {
                Console.WriteLine("user is INVALID");

            }

        }
    }
}
