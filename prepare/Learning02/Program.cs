using System;

class Program
{
    static void Main(string[] args)
    {
        Job Job1 = new Job();
        Job1._company = "Blizzard";
        Job1._startYear = 2005;
        Job1._endYear = 2015;
        Job1._jobTitle = "Software Engineer";
        
        Job Job2 = new Job();
        Job2._company = "Take Two Interactive";
        Job2._startYear = 2015;
        Job2._endYear = 2020;
        Job2._jobTitle = "Lead Project Debugger";

        Job1.Display();
        Job2.Display();
    
        Resume myResume = new Resume();
        myResume._name = "Jaxon Terrill";

        myResume._jobs.Add(Job1);
        myResume._jobs.Add(Job2);

        myResume.Display();
    }
}