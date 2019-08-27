What is LDAP used for?

I know that LDAP is used to provide some information and to help facilitate authorization.
But what are the other usages of LDAP?

LDAP stands for Lightweight Directory Access Protocol.
The use model is similar like how people use library cards or phonebooks. When you have a task that requires “write/update once, read/query many times”, you might consider using LDAP. LDAP is designed to provide extremely fast read/query performance for a large scale of dataset. Typically you want to store only a small piece of information for each entry. The add/delete/update performance is relatively slower compared with read/query because the assumption is that you don’t do “update” that often.

Imagine you have a website that has a million registered users with thousands of page requests per second. Without LDAP, every time users click a page, even for static page viewing, you will probably need to interact with your database to validate the user ID and its digital signature for this login session. Obviously, the query to your database for user-validation will become your bottleneck. By using LDAP, you can easily offload the user validation and gain significant performance improvement. Essentially, in this example, LDAP is another optimization layer outside your database to enhance performance, not replacing any database functions.
LDAP is not just for user validation, any task that has the following properties might be a good use case for LDAP:
You need to locate ONE piece of data many times and you want it fast
You don’t care about the logic and relations between different data
You don’t update, add, or delete the data very often
The size of each data entry is small
You don’t mind having all these small pieces of data at a centralized place

LDAP is a protocol for accessing a directory. A directory contains objects; generally those related to users, groups, computers, printers and so on; company structure information (although frankly you can extend it and store anything in there).
LDAP gives you query methods to add, update and remove objects within a directory (and a bunch more, but those are the central ones).
What LDAP does not do is provide a database; a database provides LDAP access to itself, not the other way around. It is much more than signup.

In Windows Server LDAP is a protocol which is used for access Active Directory object, user authentication, authorization.
