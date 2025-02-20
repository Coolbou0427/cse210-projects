using System;

class Program
{
    static void Main(string[] args)
    {
        Assignment a1 = new Assignment("Jaxon Terrill", "Division");
        Console.WriteLine(a1.GetSummary());

        MathAssignment a2 = new MathAssignment("Jaxon Terrill", "Pythagorean Theorem", "8.1", "10-20");
        Console.WriteLine(a2.GetSummary());
        Console.WriteLine(a2.GetHomeworkList());

        WritingAssignment a3 = new WritingAssignment("Jaxon Terrill", "Personal Narrative", "My life story");
        Console.WriteLine(a3.GetSummary());
        Console.WriteLine(a3.GetWritingInformation());
    }
}