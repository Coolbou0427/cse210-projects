using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What is your grade percentage: ");
        Double grade = Double.Parse(Console.ReadLine());
        String letter;
        if (grade >= 90)
        {
            letter = ("A");
        }
        else if (grade >= 80)
        {
            letter = ("B");
        }
        else if (grade >= 70)
        {
            letter = ("C");
        }
        else if (grade >= 60)
        {
            letter = ("D");
        }
        else if (grade < 60)
        {
            letter = ("F");
        }
        else
        {
            letter = (null);
        }
        Console.Write($"You recieved a {letter}. ");
        if (grade >= 70)
        {
            Console.Write("You passed! Congratulations!");
        }
        else
        {
            Console.Write("You did not pass, but don't worry. You got it next time!");
        }
    }
}