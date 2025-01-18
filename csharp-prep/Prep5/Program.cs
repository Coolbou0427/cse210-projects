using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcome();

        string userName = PromptUserName();
        int userNumber = PromptUserNumber();
        int squareNumber = SquareNumber(userNumber);
        DisplayResult(userName, squareNumber);
    }
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }
    static String PromptUserName() 
    {
        Console.Write("Please enter your name: ");
        String UserName = Console.ReadLine();

        return UserName;
    }
    static int PromptUserNumber() 
    {
        Console.Write("Please enter your favorite number: ");
        int UserNumber = int.Parse(Console.ReadLine());

        return UserNumber;
    }
    static int SquareNumber(int number) 
    {
        int SquareNumber = number * number;

        return SquareNumber;
    }
    static void DisplayResult(string name, int square) 
    {
        Console.WriteLine($"{name}, the square of your number is {square}");
    }
}