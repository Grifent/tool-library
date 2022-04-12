using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    public class ToolCollection : iToolCollection
    {
        private Tool[] toolArray; 
        private int number;

        public ToolCollection()
        {
            toolArray = new Tool[30]; // CAN HARD CODE SIZE OF ARRAY ACCORDING TO ANTHONY
            number = 0;
        }

        public int Number // get the number of the types of tools in the community library
        {
            get { return number; } //
        }

        public void add(Tool aTool) //add a given tool to this tool collection
        {
            // Find the first empty index to add to (order doesn't matter for the Member tool collection)
            int addIndex = Array.FindIndex(toolArray, element => element == null);

            // Protection if array is completely full (shouldn't happen in normal use)
            if (addIndex <= -1) 
            {
                Console.WriteLine("Tool Collection Full! Oops!");
                return;
            }
            
            toolArray[addIndex] = aTool; // Insert tool into the array
            number++; // Increment number of tools
        }

        public void delete(Tool aTool) //delete a given tool from this tool collection
        {
            // Find index of the tool in the collection (if it exists)
            int i = Array.FindIndex(toolArray, element => element != null && element.Name == aTool.Name);

            if (i > -1) // Check that tool was successfully found
            {
                toolArray[i] = null; // Deletes tool by setting instance to null
                number--; // Decreases number of tools in the tool collection
                return;
            }

            else // (shouldn't occur normally but just for safety)
            {
                Console.WriteLine("Tool does not exist.");
            }            
        }

        public Boolean search(Tool aTool) //search a given tool in this tool collection. Return true if this tool is in the tool collection; return false otherwise
        {
            // Check for a tool with the same name, as tool names are unique with the current implementation
            return Array.Exists(toolArray, element => element.Name == aTool.Name); 
        }

        public Tool[] toArray() // output the tools in this tool collection to an array of iTool
        {
            // Only return non-null variables, i.e. real tools
            return Array.FindAll(toolArray, element => element != null); 
        }
    }
}
