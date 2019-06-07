using System;
using System.Numerics;

namespace FlagPermissions.Console
{
    using Console = System.Console;

    internal class Program
    {
        private static void Main()
        {
            UserPermissions myPermissions;

            Console.Write("Create permissions for user? ");

            if (YesNo())
            {
                myPermissions = GetPermissionsFromUser();
            }
            else
            {
                Console.WriteLine("Enter your current permissions as a number: ");
                myPermissions = new UserPermissions(GetBigIntFromUser());
            }

            Console.WriteLine("Your permissions value is " + myPermissions);

            Console.WriteLine("Your permissions include access to the following: ");
            if (myPermissions.HasAccessTo(ExamplePermissions.Read))
            {
                Console.WriteLine("Read");
            }
            if (myPermissions.HasAccessTo(ExamplePermissions.Write))
            {
                Console.WriteLine("Write");
            }
            if (myPermissions.HasAccessTo(ExamplePermissions.CreateDelete))
            {
                Console.WriteLine("Create and Delete");
            }
            if (myPermissions.HasAccessTo(ExamplePermissions.ViewAudit))
            {
                Console.WriteLine("Audit");
            }

            Console.WriteLine($"Press {nameof(ConsoleKey.Escape)} or {nameof(ConsoleKey.Enter)} to exit");
            ConsoleKey pressedKey;
            do
            {
                pressedKey = Console.ReadKey(true).Key;
            } while (pressedKey != ConsoleKey.Escape && pressedKey != ConsoleKey.Enter);
        }

        private static bool YesNo()
        {
            ConsoleKey keyPress;
            do
            {
                keyPress = Console.ReadKey(true).Key;
            } while (keyPress != ConsoleKey.Y && keyPress != ConsoleKey.N);

            Console.WriteLine(keyPress);

            return keyPress == ConsoleKey.Y;
        }

        private static BigInteger GetBigIntFromUser()
        {
            bool isValid;
            BigInteger flag;
            do
            {
                var enteredText = Console.ReadLine();
                isValid = BigInteger.TryParse(enteredText, out flag);

                if (!isValid)
                {
                    Console.Write(new string('\b', enteredText.Length + Environment.NewLine.Length));
                }
            } while (!isValid);

            return flag;
        }

        private static UserPermissions GetPermissionsFromUser()
        {
            var myPermissions = new UserPermissions();

            Console.Write("Do you have create, delete, write and read permissions? ");
            if (YesNo())
            {
                myPermissions.Permissions |= ExamplePermissions.CreateDelete;
            }
            else
            {
                Console.Write("Do you have write and read permissions? ");
                if (YesNo())
                {
                    myPermissions.Permissions |= ExamplePermissions.Write;
                }
                else
                {
                    Console.Write("Do you have read permissions? ");
                    if (YesNo())
                    {
                        myPermissions.Permissions |= ExamplePermissions.Read;
                    }
                }
            }

            Console.Write("Do you have auditing permissions? ");
            if (YesNo())
            {
                myPermissions.Permissions |= ExamplePermissions.ViewAudit;
            }

            return myPermissions;
        }
    }
}
