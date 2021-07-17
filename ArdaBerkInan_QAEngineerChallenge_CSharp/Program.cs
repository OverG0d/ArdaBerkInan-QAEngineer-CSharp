using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ArdaBerkInan_QAEngineerChallenge_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"src";
            Dictionary<int, int> numbers = new Dictionary<int, int>();
            DirectoryInfo d = new DirectoryInfo(filePath); 

            if(!d.Exists) //Check if the directory exists
            {
                throw new DirectoryNotFoundException(
                "Directory does not exist or could not be found: "
                + filePath);
            }

            FileInfo[] files = d.GetFiles("*.txt"); //Get text files

            int counter = 0;
            int lowestFreqNumber = 0;
            string[] contents;
            for (int i = 0; i < files.Length; i++)
            {
                counter = 0;
                lowestFreqNumber = 0;
                contents = File.ReadAllLines(files[i].FullName); //Read all the lines in the current text file
                while(counter < contents.Length)  
                {
                    if (!numbers.ContainsKey(Int32.Parse(contents[counter]))) //If the key does not exist, add it
                    {
                        numbers.Add(Int32.Parse(contents[counter]), 1); 
                    }
                    else
                    {
                        numbers[Int32.Parse(contents[counter])]++; //If the key exists, add the value by 1
                    }
                    counter++; 
                }
                lowestFreqNumber = CheckForLowestFrequency(numbers); //Returns the number with lowest frequency
                Console.WriteLine("Lowest frequency count for file " + files[i].Name +
                "is" + ":"  + " " + lowestFreqNumber);
            }
        }

        private static int CheckForLowestFrequency(Dictionary<int, int> numbers)
        {
            List<int> tempDictValues = new List<int>();
            List<int> tempDictKeys = new List<int>();

            //Sort the values
            foreach (var pair in numbers)
            {
                tempDictValues.Add(pair.Value);
            }
            tempDictValues.Sort();

            //Sort the keys
            foreach(var pair in numbers)
            {
                if (numbers[pair.Key] == tempDictValues[0])
                {
                    tempDictKeys.Add(pair.Key);
                }
            }
            tempDictKeys.Sort();

            return tempDictKeys[0]; 
        }
    }
}