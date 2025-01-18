using System;

class Program
{
    static void Main(string[] args)
    {
        int input = -1;
        List<int> listOfNumbers = new List<int>();
        Console.WriteLine("Enter a list of numbers, type 0 when finished. ");
        do
        {
            {
                Console.Write("Enter number: ");
                input = int.Parse(Console.ReadLine());
                listOfNumbers.Add(input);
            }
        }   while (input != 0);
        
        int total = 0;
        int largest = 0;
        foreach (int number in listOfNumbers)
        {
            total += number;
            if (number > largest)
            {
                largest = number;
            }
        }

        Console.WriteLine($"The sum is: {total}");
        Console.WriteLine($"The average is: {total/listOfNumbers.Count}");
        Console.WriteLine($"The largest number is: {largest}");
        
    }
}