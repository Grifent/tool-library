using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Assignment
{
    public class ToolLibrarySystem : iToolLibrarySystem
    {
        private DataSystem data; // Data system that holds all collections and memory for the library system
        private List<Tool> borrowedTools; // 1D Tool list to store each tool that has been borrowed at least once

        public ToolLibrarySystem(DataSystem data) // Constructor
        {
            // Initialise data system and borrowed tools
            this.data = data;
            borrowedTools = new List<Tool>();
        }
        
        public void add(Tool aTool) // add a new tool to the system
        {
            // Add the tool to the correct Tool Collection
            data.CurrentToolType.add(aTool);
        }

        public void add(Tool aTool, int quantity) //add new pieces of an existing tool to the system
        {
            // Increment the quantity of the tool by the provided amount
            aTool.Quantity += quantity;
            // Adjust available quantity based on this
            aTool.AvailableQuantity += quantity;
        }

        public void delete(Tool aTool) //delete a given tool from the system 
        {
            // Delete the tool from the currently selected Tool Collection (if this was implemented via the Console Menu)
            data.CurrentToolType.delete(aTool);
        }

        public void delete(Tool aTool, int quantity) //remove some pieces of a tool from the system
        {
            // Menus.cs checks if the enetered quantity is valid
            aTool.Quantity -= quantity;
            // Adjust available quantity based on this
            aTool.AvailableQuantity -= quantity;
        }

        public void add(Member aMember) //add a new member to the system
        {
            // Check if member exists already
            if (!data.Members.search(aMember))
            {
                // Add member to the Member Collection
                data.Members.add(aMember);
                Console.WriteLine();
                Console.WriteLine($"New member {aMember.FirstName} {aMember.LastName} successfully added.");
            }
            
            else
            {
                // Tell user that the member already exists.
                Console.WriteLine();
                Console.WriteLine("Member already exists!");
            }
        }

        public void delete(Member aMember) //delete a member from the system
        {
            // Get the string array of tool names currently held by the member
            string[] memberTools = aMember.Tools;

            // Check that member doesn't have any tools
            if (memberTools == null || memberTools.Length == 0)
            {
                // Delete the member
                data.Members.delete(aMember);
                Console.WriteLine();
                Console.WriteLine($"Member {aMember.FirstName} {aMember.LastName} successfully deleted.");
            }

            else
            {
                // Tell user that member still has tools on loan
                Console.WriteLine();
                Console.WriteLine("Member needs to return their tools first!"); 
            }
            
        }

        public void displayBorrowingTools(Member aMember) //given a member, display all the tools that the member are currently renting
        {
            // Get the string array of tool names currently held by the member
            string[] memberTools = aMember.Tools;

            // Check if member has no tools
            if (memberTools.Length == 0) 
            {
                Console.WriteLine("No tools currently rented.");
                return;
            }

            // Otherwise print each one with a numbered index
            for (int i = 0; i < memberTools.Length; i++)
            {
                string tool = memberTools[i];
                Console.WriteLine($"{i + 1}. {tool}.");
            }
        }

        public void displayTools(string aToolType) // display all the tools of a tool type selected by a member
        {
            // Initialise indexes for category and type
            int toolCategory = -1;
            int toolType = -1;

            Tool[] selectedTools; // Array to hold tools of selected tool type

            // Check that an actual string has been passed
            if (aToolType != null) 
            {
                // Search DataSystem for tool type based on the string (this is redundant with my implementation, but I assume it needs to be implemented somewhat like this)
                for (int i = 0; i < data.ToolCategories.Length; i++)
                {
                    // Find index of the provided tool type in ToolTypes 
                    int j = Array.FindIndex(data.ToolTypes[i], type => type == aToolType);

                    // Break the loop if tool type is found
                    if (j > -1) 
                    {
                        toolCategory = i;
                        toolType = j;
                        break;
                    }
                }

                // Ensure tool type is valid (should never be a problem when aToolType is passed correctly)
                if (toolCategory < 0 || toolType < 0)
                {
                    Console.WriteLine("Invalid Tool Type Provided.");
                    return;
                }

                // Get array of selected tools
                selectedTools = data.ToolsMasterArray[toolCategory][toolType].toArray();
            }

            // If null string is passed to aToolType, do the fast implementation instead
            else
            {
                // Get array of currently selected tools from DataSystem
                selectedTools = data.CurrentToolType.toArray();
            }

            // Print the tools to a table in Console
            Console.WriteLine();
            Console.WriteLine($"List of {data.CurrentToolTypeStr}"); // Inserts the name of the current tool type
            string border = "=================================================================================================";
            Console.WriteLine(border);
            Console.WriteLine("    Tool Name\t\t\t\t\t\t\t\tAvailable\tTotal");
            Console.WriteLine(border);

            // Check if tool collection is empty
            if (selectedTools.Length == 0)
            {
                Console.WriteLine("No tools of this type yet.");
                Console.WriteLine(border);
                return;
            }
            
            // Print each tool
            for (int i = 0; i < selectedTools.Length; i++)
            {
                // Get name and quantities of tool
                string toolName = selectedTools[i].Name;
                int toolAvailQuant = selectedTools[i].AvailableQuantity;
                int toolQuant = selectedTools[i].Quantity;

                // Print tool info with Console index that needs to be entered by user
                Console.WriteLine(string.Format("{0}.  {1,-71} {2,-13} {3}", i + 1, toolName, toolAvailQuant, toolQuant));
            }

            Console.WriteLine(border);
        }

        public void borrowTool(Member aMember, Tool aTool) //a member borrows a tool from the tool library
        {
            Console.WriteLine();

            // Check tool has allowable quantity to be borrowed
            if (aTool.AvailableQuantity < 1) 
            {
                Console.WriteLine("None of this tool is available to borrow! Please wait until more are in stock.");
                return;
            }

            // Ensure member doesn't have more than 3 tools
            if (aMember.Tools.Length < 3) 
            {
                aMember.addTool(aTool); // Add tool to member's borrowed tool list
                aTool.addBorrower(aMember); // Add member to tool's list of borrowers
                aTool.NoBorrowings++; // Increase number of borrowings
                aTool.AvailableQuantity--; // Adjust available quantity 
                Console.WriteLine($"{aTool.Name} borrowed successfully.");

                // Add aTool to borrowed tools list if it hasn't been already
                if (!borrowedTools.Contains(aTool))
                {
                    borrowedTools.Add(aTool);
                }
            }

            else
            {
                Console.WriteLine("Cannot borrow more than 3 tools!");
            }
        }

        public void returnTool(Member aMember, Tool aTool) //a member return a tool to the tool library
        {
            aMember.deleteTool(aTool); // Delete tool from member's borrowed tool list
            aTool.deleteBorrower(aMember); // Delete member from tool's list of borrowers
            aTool.AvailableQuantity++; // Update available quantity
        }

        public string[] listTools(Member aMember) //get a list of tools that are currently held by a given member
        {
            // Return the string array of tool names stored in the Member object
            return aMember.Tools;
        }

        public void displayTopThree() //Display top three most frequently borrowed tools by the members in the descending order by the number of times each tool has been borrowed.
        {
            Tool[] borrowedToolsArray = borrowedTools.ToArray(); // 1D array of all tools that have been borrowed at least once

            // Get number of tools in the borrowed tools array
            int numBorrowedTools = borrowedToolsArray.Length; 

            // Tell user and exit if no tools have been borrowed yet
            if (numBorrowedTools == 0)
            {
                Console.WriteLine("No tools have been borrowed yet.");
                return;
            }

            // Get the top 3 tools using a partial heapsort on the NoBorrowings for each tool
            Tool[] top3Tools = PartialHeapSort(borrowedToolsArray); 

            // Print these tools to the Console
            for (int i = 0; i < top3Tools.Length; i++)
            {
                Console.WriteLine(string.Format($"{i + 1}. {top3Tools[i].Name} --- Borrowed {top3Tools[i].NoBorrowings} times"));
            }

        }

        // The following code is adapted from the Wk6 tute:

        // Sort the elements in the array of borrowed tools, returns a Tool array with the top 3 tools
        private Tool[] PartialHeapSort(Tool[] borrowedTools)
        {
            Tool[] top3; // Top 3 tools array

            //Use the HeapBottomUp procedure to convert the array, data, into a heap
            HeapBottomUp(borrowedTools);

            int maxSortLength = 3; // Only need the top three, so sort 3 times
            int heapLength = borrowedTools.Length; // Store the length of the heap

            //repeatly remove the maximum key from the heap and then rebuild the heap
            for (int i = 0; i <= heapLength - 2 && i < maxSortLength; i++) // Stops when heap is fully sorted, or when it has sorted 3 times. Whichever comes first
            {
                // Delete the maximum heap
                MaxKeyDelete(borrowedTools, heapLength - i);
            }

            // Return only the sorted items:
            if (heapLength <= maxSortLength) // If the array is 3 or smaller, it is fully sorted
            {
                top3 = borrowedTools;
                Array.Reverse(top3); // Reverse the array so it is ordered highest to lowest
                return top3;
            }

            else // Otherwise trim the non-sorted items
            {
                top3 = borrowedTools.TakeLast(maxSortLength).ToArray(); // Take only the last 3 items
                Array.Reverse(top3); // Reverse the array so it is ordered highest to lowest
                return top3;
            }
        }

        // convert a complete binary tree into a heap
        private void HeapBottomUp(Tool[] borrowedTools)
        {
            int n = borrowedTools.Length;
            for (int i = (n - 1) / 2; i >= 0; i--)
            {
                int k = i;
                Tool v = borrowedTools[i];
                bool heap = false;
                while ((!heap) && ((2 * k + 1) <= (n - 1)))
                {
                    int j = 2 * k + 1;  //the left child of k
                    if (j < (n - 1))   //k has two children
                        if (borrowedTools[j].NoBorrowings < borrowedTools[j + 1].NoBorrowings) // Compare the NoBorrowings in each Tool
                            j = j + 1;  //j is the larger child of k
                    if (v.NoBorrowings >= borrowedTools[j].NoBorrowings)
                        heap = true;
                    else
                    {
                        borrowedTools[k] = borrowedTools[j];
                        k = j;
                    }
                }
                borrowedTools[k] = v;
            }
        }

        //delete the maximum key and rebuild the heap
        private void MaxKeyDelete(Tool[] borrowedTools, int size)
        {
            //1. Exchange the root’s key with the last key K of the heap;
            Tool temp = borrowedTools[0];
            borrowedTools[0] = borrowedTools[size - 1];
            borrowedTools[size - 1] = temp;

            //2. Decrease the heap’s size by 1;
            int n = size - 1;

            //3. “Heapify” the complete binary tree.
            bool heap = false;
            int k = 0;
            Tool v = borrowedTools[0];
            while ((!heap) && ((2 * k + 1) <= (n - 1)))
            {
                int j = 2 * k + 1; //the left child of k
                if (j < (n - 1))   //k has two children
                    if (borrowedTools[j].NoBorrowings < borrowedTools[j + 1].NoBorrowings)
                        j = j + 1;  //j is the larger child of k
                if (v.NoBorrowings >= borrowedTools[j].NoBorrowings)
                    heap = true;
                else
                {
                    borrowedTools[k] = borrowedTools[j];
                    k = j;
                }
            }
            borrowedTools[k] = v;
        }
    }
}
