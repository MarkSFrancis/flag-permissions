# Flag Permissions

This repository demonstrates how to manage permissions using bits

## The problem
Storing a user's permissions often involves storing a fair amount of data, including a long list of every feature they might have access to. Particularly when dealing with authentication schemas like JWT Bearer tokens, if you're storing this information with the client, it can quickly grow to become a lot of data in larger applications, which causes performance problems, and in some cases, even application crashes.

## How it works

Computers work with binary, which means all data is stored in a 1 or 0, or "on" or "off" state. This suits permissions really well.

This system converts each permission into what's called a flag - a bit(1 or 0) that represents the permission, and stores all the user's flags together in binary, creating a single long number that represents all the user's permissions.

If each permission is given a unique ID - for example:
```
Read = 0,
Write = 1,
Create / Delete = 2,
Audit = 3
```
then each permission can be turned into a flag with a mathematical operation of 2<sup>n</sup> where n is the unique ID of the permission.

This example would result in the following values:
```
Read = 1,
Write = 2,
Create / Delete = 4,
Audit = 8
```

As binary equivalents, these are stored in the computer as:
```
Read = 0001,
Write = 0010,
Create / Delete = 0100,
Audit = 1000
```

Once stored as flags, you can performan an `OR` (keeps all the digits set to 1 when any of the user's permissions have the digit set to 1) operation between all permissions to create a new number that uniquely identifies a user's permissions.

For example, if the user had the following permissions:
```
0001 (Read, 1)
0010 (Write, 2)
1000 (Audit, 8)
````
They could be combined via an `OR` operator to create
```
1011
```
which is the binary equivalent to the number 11 (8 + 2 + 1). This uniquely identifies this permissions combination. No other combination of permissions will evaluate to the number 11.

Later on, to check if a user has certain permissions, you can perform an `AND` operation on the user's permission, and check if it matches the permission you're looking for.

For example, if you want to know if a user has the Read permission
```
0001
```

and they have the permission value of
```
1011 (11)
```

You can perform an AND operation between the two (keeps all the digits set to 1 when both the unique identifier and the user permission have the digit set to 1) which results in the value
```
0001
```

This is the same as the Read permission that we checked for, which means that this user has the permission.

The same can be used for a check for a combination of permissions.
For example, checking that user has Read and Write permissions is a simple matter of an OR for Read and Write, and then AND that with the user's permission

OR for Read and Write
```
0001 (Read)
0010 (Write)
```
which then OR together make
```
0011
```

which then AND against the user permissions of
```
1011
```
makes
```
0011
```
which is the same as the OR of Read and Write, and therefore the user has both permissions.

If a user doesn't have permission, the result will not be the same as the permission value. For example, checking for the user to have Write and Create / Delete:

```
0010 (Write)
0100 (Create / Delete)
```
OR makes
```
0110
```

which then AND against the user permissions of
```
1011
```
makes
```
0010
```

which is **not** the same as 0110 (Write and Create / Delete permissions)
