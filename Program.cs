using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

string measurement = "";
string divider = "--------------------------------------------------";
string ynQuery = "\nPlease type Y for yes and N for no.";
int coats;
double price;
double length;
double height;
bool queryBool = false;
string[,] paintColours = { { "Pickles Premium", "12" }, { "Ritual Red", "11" }, { "Albino White", "10" }, { "Bingo Boingo", "8" }, { "Trouble", "5" } };
List<string> currentWalls = new List<string>();
string[,] measurementOptions = { { "Meters", "m"}, { "Centimeters", "cm"} };
double cubicMeterTotal = 0;
double metresPerLitre;


// Welcome
Console.WriteLine("\nWelcome to PAINT CALCULATOR 9000, SUPREME EDITION.\n");
AddDivider();

// Pick paint type & set price
Console.WriteLine("Pick your paint type from the following: \n");
GetPaintColours();
Console.WriteLine("Prices given are in pounds per litre.\n");
SelectPaint();

// Select Unit of measurement for walls
Console.WriteLine("\nTo start, please type your unit of measurement used for measuring walls then press enter.\n");
SelectMeasurement();

// Add wall ?
AddWall();

// done adding walls: 
RunningTotal();
AddDivider();


// Close on button press
Console.Write("Press any key to close the Calculator console app...");

void CalculateCans(){
    Console.WriteLine("Assuming 5 litres of paint per can, and one meter cubed per litre of paint.");
    int temp;
    // ask how many paint coats are needed.
    Console.WriteLine("How many coats of paint do you need?");
    bool coatsString = int.TryParse(Console.ReadLine(), out temp);


    if (coatsString)
    {
        coats = temp;
        double cansNeeded = 0;
        if (measurement.Equals("m"))
        {
            cansNeeded = cubicMeterTotal / 5;
        }
        else if (measurement.Equals("cm"))
        {
            cansNeeded = cubicMeterTotal / 5;
            cansNeeded /= 1000;
        }
        
        Console.WriteLine("You currently need: " + cansNeeded + " cans of paint.");
        Console.WriteLine("This will cost: ");
    }
}

void LitreCalculator()
{
    Console.WriteLine("How many squared " + measurement + " can one litre of paint cover in your project?");
    string metresPerLitres = Console.ReadLine();
    double math;
    if (CheckDouble(metresPerLitres))
    {
        CalculateCans();
    }

    else
    {
        Console.WriteLine("Invalid input. Try again.");
        LitreCalculator();
    }
}



void SelectMeasurement()
{
    bool matchFound = false;
    measurement = Console.ReadLine().ToLower();
    for (int i = 0; i < measurementOptions.GetLength(0); i++)
    {
        for (int j = 0; j < measurementOptions.GetLength(1); j++)
        {
            if (measurement.Equals(measurementOptions[i, j].ToLower()))
            {
                measurement = measurementOptions[i, 1];
                Console.WriteLine("\n" + divider + "\nMeasurement selected: " + measurementOptions[i, 0]);
                matchFound = true;
                AddDivider();
                break;
            }
        }
    }

    if (!matchFound)
    {
        Console.WriteLine("\nTry another measurement unit.");
        SelectMeasurement();
    }
}

void SelectPaint()
{
    String paintSelected = Console.ReadLine().ToLower();
    double price = 0;
    for (int i = 0; i<paintColours.GetLength(0); i++)
    {
        if (paintSelected == paintColours[i, 0].ToLower())
        {
            price = Double.Parse(paintColours[i, 1]);
            paintSelected = paintColours[i,0];
            Console.WriteLine("\n" + divider+ "\n\nPaint selected: " + paintSelected + "\nPrice: £" + price + " per litre.\n");
            AddDivider();
        } 
    }

    if(price == 0)
    {
        // Select paint again
        Console.WriteLine("\nInvalid paint. Please try again.\n");
        SelectPaint();
    }

}

void GetPaintColours()
{
    for (int i = 0; i < paintColours.GetLength(0); i++)
    {
        Console.WriteLine(paintColours[i, 0] + ": £" + paintColours[i, 1]);
        Console.WriteLine();
    }
    Console.WriteLine();
}

void AddDivider()
{
    Console.WriteLine(divider);
}

void QueryResult()
{
    Console.WriteLine(ynQuery);
    queryBool = false;
    switch (Console.ReadLine().ToLower())
    {
        case "y":
            AddDivider();
            Console.WriteLine("You answered: Yes");
            queryBool = true;
            AddDivider();
            break;

        case "n":
            AddDivider();
            Console.WriteLine("You answered: No");
            AddDivider();
            break;

        default:
            Console.WriteLine("try again");
            QueryResult();
            break;
    }
}

bool CheckDouble(string checkMe)
{
    double checkedDouble;
    bool isValid = Double.TryParse(checkMe, out checkedDouble);

    if (isValid)
    {
        return isValid;
    }
    else
    {
        return isValid;
    }
}

void RunningTotal()
{
    // current walls:
    string[] wallArray = currentWalls.ToArray();
    bool start = true;
    double total = 0;
    for (int i = 0; i < wallArray.Length; i++)
    {
        string[] lengthHeight = wallArray[i].Split();

        length = Double.Parse(lengthHeight[0]);
        height = Double.Parse(lengthHeight[1]);
        if(start)
        {
            Console.WriteLine("\nWalls so far: ");
            start = false;
        }
        // keeps score of cubic meters so far
        Console.WriteLine(length + measurement + " x " + height + measurement);
        total += length*height;
        cubicMeterTotal += total;
    }
    Console.WriteLine();
    
}


void AddWall()
{
    // Add a wall
    Console.WriteLine("\nWould you like to add a wall?");
    QueryResult();
    if (queryBool) {

    // if no it displays current total
   

        Console.WriteLine("Please enter the length and height of your wall of your wall, seperated by a space. There is a maximum value of 999.");
        Console.WriteLine("" +
            "\nTo remove a wall section:" +
            "\n  First, add the whole wall." +
            "\n  Then, add another wall and prefix a minus sign to one of the numbers." +
            "\n  This will subtract that section from your total." +
            "\n\nNote: If you put two negatives, the space will be added rather than subtracted.");
        // check both numbers are doubles
        string input = Console.ReadLine();
        if (input != null)
        {
            
            string[] inputSplit = input.Split();

            if (inputSplit.Length != 2)
            {
                Console.WriteLine("Invalid entry. please try again.");
                AddWall();
            }

            else if (CheckDouble(inputSplit[0]) && CheckDouble(inputSplit[1]))
            {
                double length2 = Double.Parse(inputSplit[0]);
                length = Math.Clamp(length2, -999f, 999);
                // print after each change. Get this dang fixen done
                Console.WriteLine();
                height = Math.Clamp(Double.Parse(inputSplit[1]), -999.0, 999.0);
                currentWalls.Add(input);
                RunningTotal();
                AddWall();
            }

        }

        else {  // enter a valid value idiot
            Console.WriteLine("Nothing detected.");
            AddWall();
        }
    }

    else
    {
        LitreCalculator();
    }
}

