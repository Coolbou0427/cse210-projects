using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    class Entry
    {
        public string Date { get; }
        public string Prompt { get; }
        public string Response { get; }

        public Entry(string date, string prompt, string response)
        {
            Date = date;
            Prompt = prompt;
            Response = response;
        }

        public void Display()
        {
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Prompt: {Prompt}");
            Console.WriteLine($"Response: {Response}");
        }
    }

    class PromptGenerator
    {
        private List<string> prompts;
        private Random random;

        public PromptGenerator()
        {
            random = new Random();
            prompts = new List<string>
            {
                "Who was the most interesting person I interacted with today?",
                "What was the best part of my day?",
                "How did I see the hand of the Lord in my life today?",
                "What was the strongest emotion I felt today?",
                "If I had one thing I could do over today, what would it be?",
                "What did you learn today?",
                "What's something that made you smile today?",
                "Describe a challenge you overcame today.",
                "What moment are you grateful for today?",
                "What did you do today that brought you closer to your goals?",
                "How did you practice kindness today?",
                "What is a new idea you had today?",
                "What was the most unexpected part of your day?",
                "How did you push yourself out of your comfort zone today?",
                "What is something you wish you'd done differently today?"
            };
        }

        public string GeneratePrompt()
        {
            int index = random.Next(prompts.Count);
            return prompts[index];
        }
    }

    class Journal
    {
        private List<Entry> entries;
        private PromptGenerator promptGenerator;
        private const string DELIMITER = "|";

        public Journal()
        {
            entries = new List<Entry>();
            promptGenerator = new PromptGenerator();
        }

        public void AddEntry()
        {
            string prompt = promptGenerator.GeneratePrompt();
            Console.WriteLine($"\nPrompt: {prompt}");
            Console.Write("> ");
            string response = Console.ReadLine();
            string date = DateTime.Now.ToShortDateString();
            Entry newEntry = new Entry(date, prompt, response);
            entries.Add(newEntry);
            Console.WriteLine("Entry added successfully!");
        }

        public void DisplayEntries()
        {
            if (entries.Count == 0)
            {
                Console.WriteLine("No entries yet.");
                return;
            }
            foreach (Entry entry in entries)
            {
                entry.Display();
            }
        }

        public void SaveToFile(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Entry entry in entries)
                {
                    writer.WriteLine($"{entry.Date}{DELIMITER}{entry.Prompt}{DELIMITER}{entry.Response}");
                }
            }
            Console.WriteLine("Journal saved successfully!");
        }

        public void LoadFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found.");
                return;
            }
            string[] lines = File.ReadAllLines(filename);
            entries.Clear();
            foreach (string line in lines)
            {
                string[] parts = line.Split(DELIMITER);
                if (parts.Length >= 3)
                {
                    string date = parts[0];
                    string prompt = parts[1];
                    string response = parts[2];
                    Entry entry = new Entry(date, prompt, response);
                    entries.Add(entry);
                }
            }
            Console.WriteLine("Journal loaded successfully.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Journal myJournal = new Journal();
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nJournal Menu:");
                Console.WriteLine("1. Write");
                Console.WriteLine("2. Display");
                Console.WriteLine("3. Save");
                Console.WriteLine("4. Load");
                Console.WriteLine("5. Quit");
                Console.Write("Choose an option (1-5): ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        myJournal.AddEntry();
                        break;
                    case "2":
                        myJournal.DisplayEntries();
                        break;
                    case "3":
                        Console.Write("\nEnter filename to save: ");
                        string saveFile = Console.ReadLine();
                        myJournal.SaveToFile(saveFile);
                        break;
                    case "4":
                        Console.Write("\nEnter filename to load: ");
                        string loadFile = Console.ReadLine();
                        myJournal.LoadFromFile(loadFile);
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("Exiting program. Bye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }
    }
}
