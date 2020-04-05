using System;
using System.Collections.Generic;
using System.Linq;

namespace Statistics
{
    class Program
    {

        class NumberCruncher
        {
            public double[] data = new double[1000];
            int count = 0;
            private double _total;

            public void addNumber(double num)
            {
                data[count] = num;
                count++;
            }

            public void addRandomNumbers(int howMany, int lowRange, int highRange)
            {
                Random rnd = new Random();
                for(int c = 0; c < howMany; c++)
                {
                    addNumber(rnd.Next(lowRange, highRange));
                }
            }

            public void displayData()
            {
                for(int n = 0; n < count; n++)
                {
                    Console.WriteLine(data[n]);
                }
            }

            public void removeLastNumber()
            {
                data[count] = 0;
                count--;
            }

            public void removeNumberAt(int index)
            {
                data[index] = 0;
                for(int n = index; n < count; ++n)
                {
                    data[n] = data[n + 1];
                }
                count--;
            }

            private void setTotal()
            {
                double theTotal = 0;
                for (int n = 0; n < count; n++)
                {
                    theTotal = theTotal + data[n];
                }
                _total = theTotal;
            }

            public double total()
            {
                setTotal();
                return _total;
            }


            public double average()
            {
                setTotal();
                double theAvg;
                theAvg = _total / count;
                return theAvg;
            }

            public double mean()
            {
                return average();
            }

            public double minimum()
            {
                double lowest = data[count];
                for (int n = 0; n < count; n++)
                {
                    if (data[n] < lowest)
                    {
                        lowest = data[n];
                    }
                }
                return lowest;
            }

            public double maximum()
            {
                double biggest = data[count];
                for (int n = 0; n < count; n++)
                {
                    if (data[n] > biggest)
                    {
                        biggest = data[n];
                    }
                }
                return biggest;
            }

            public double range()
            {
                double theRange;
                double biggest = maximum();
                double smallest = minimum();
                theRange = biggest - smallest;

                return theRange;
            }


            public string mode()
            {
                string theMode = "";
                int currentMax = 0;
                Dictionary<double, int> modes = new Dictionary<double, int>();
                                
                for (int n = 0; n < count; n++)
                {
                    // Adds key to dictionary if not there
                    if (!modes.ContainsKey(data[n]))
                    {
                        modes.Add(data[n], 1);
                    }
                    // Else increase counter if already exists
                    else modes[data[n]]++;
                }

                // Cycle keys for highest count
                foreach(var entry in modes)
                {
                    Console.WriteLine(entry);
                    // If count is higher than current highest count, update highest count
                    if (entry.Value > currentMax)
                    {                        
                        currentMax = entry.Value;
                       // theMode = entry.Key;
                    }
                } 
                
                // Cycle for all keys with the highest count, then add to theMode string
                foreach(var entry in modes)
                {
                    if(entry.Value == currentMax)
                    {
                        theMode = theMode + " " + entry.Key.ToString();
                    }
                }
                return theMode;
            }
        }

        static void Main(string[] args)
        {


            NumberCruncher test = new NumberCruncher();

            Console.WriteLine("AddNumbers");
            test.addRandomNumbers(15, 1, 15); // Adds 15 random numbers between 1 - 15
            test.displayData();
            Console.WriteLine();

            Console.WriteLine("Remove Last");
            test.removeLastNumber();
            test.displayData();
            Console.WriteLine();

            Console.WriteLine("Remove number at index 1");
            test.removeNumberAt(1);
            test.displayData();
            Console.WriteLine();            

            Console.WriteLine("Number Totals");
            Console.WriteLine(test.total());
            Console.WriteLine();

            Console.WriteLine("Number Average");
            Console.WriteLine(test.average());
            Console.WriteLine();

            Console.WriteLine("Calling for mean");
            Console.WriteLine(test.mean());
            Console.WriteLine();

            Console.WriteLine("Lowest Number");
            Console.WriteLine(test.minimum());
            Console.WriteLine();

            Console.WriteLine("Highest Number");
            Console.WriteLine(test.maximum());
            Console.WriteLine();

            Console.WriteLine("Range");
            Console.WriteLine(test.range());
            Console.WriteLine();

            Console.WriteLine("Mode, can have multiple modes.");
            Console.WriteLine(test.mode());
            Console.WriteLine();
            
        }
    }
}
