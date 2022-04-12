using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    /// <summary>
    /// Controls all the menus and user inputs for the Tool Library System.
    /// </summary>
    public class Menu
    {
        private ToolLibrarySystem library; // Library system for collection logic
        private DataSystem data; // Data system to hold all the collections and data needed

        /// <summary>
        /// Create a Menu system for the library.
        /// </summary>
        /// <param name="library">The Tool Library System of the current program.</param>
        /// <param name="data">The Data System of the current program.</param>
        public Menu(ToolLibrarySystem library, DataSystem data)
        {
            // Set both systems
            this.library = library;
            this.data = data;
        }

        /// <summary>
        /// Displays the main menu interface to the console. 
        /// </summary>
        /// <returns>true if the main menu should be repeated, false if exiting.</returns>
        public bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Tool Library!");
            Console.WriteLine("============Main Menu============");
            Console.WriteLine("1. Staff Login");
            Console.WriteLine("2. Member Login");
            Console.WriteLine("0. Exit");
            Console.WriteLine("=================================");
            Console.WriteLine();
            Console.Write("Please make a selection - ");

            while (true) // Loop until user gives valid input
            {
                // Switch case for each menu option
                switch (Console.ReadLine().Trim()) // Trim whitespace from the user's input
                {
                    case "1": // Staff Login
                        // Run the staff login and check if login was valid
                        bool showStaffMenu = StaffLogin();

                        // Loop staff menu until user wants to return
                        while (showStaffMenu) 
                        {
                            showStaffMenu = StaffMenu();
                        }
                        return true;

                    case "2": // Member Login
                        // Run the member login and check if login was valid, also get the logged in member object
                        bool showMemberMenu = MemberLogin(out Member loggedInMember);

                        // Loop member menu until user wants to return
                        while (showMemberMenu) 
                        {
                            showMemberMenu = MemberMenu(loggedInMember);
                        }
                        return true;

                    case "0":  // Exit
                        Console.Clear();
                        Console.WriteLine("Thanks for using the Tool Library!");
                        PressAnyKey();
                        return false;

                    default:
                        InvalidInput();
                        break;
                }
            }
        }

        /// <summary>
        /// Displays the staff login interface to the console. 
        /// </summary>
        /// <returns>true if the login was successful, false otherwise.</returns>
        private bool StaffLogin()
        {
            // Hardcode the staff login
            string staffLogin = "staff";
            string staffPassword = "today123";

            Console.Clear();
            Console.WriteLine("Tool Library System");
            Console.WriteLine("================Staff Login================");
            Console.WriteLine();

            Console.Write("Enter staff username - ");
            string userLogin = Console.ReadLine();

            Console.Write("Enter password - ");
            string userPassword = Console.ReadLine();

            // Check if the login matches
            if (userLogin == staffLogin && userPassword == staffPassword)
            {
                // Successful login
                return true;
            }

            else
            {
                Console.WriteLine();
                Console.WriteLine("Incorrect username or password.");
                PressAnyKey();

                return false;
            }
        }

        /// <summary>
        /// Displays the member login interface to the console.
        /// </summary>
        /// <param name="loggedInMember">Member object of the logged in member.</param>
        /// <returns>true if the login was successful, false otherwise.</returns>
        private bool MemberLogin(out Member loggedInMember)
        {
            // Initialise logged in member as null
            loggedInMember = null;

            // Get array of current members
            Member[] currentMembers = data.Members.toArray();
            
            Console.Clear();
            Console.WriteLine("Tool Library System");
            Console.WriteLine("================Member Login================");
            Console.WriteLine();

            Console.Write("Enter member username (LastnameFirstname) - ");
            string userLogin = Console.ReadLine();

            Console.Write("Enter password - ");
            string userPassword = Console.ReadLine();

            // Check if username matches with any members
            int memberIndex = Array.FindIndex(currentMembers, member => string.Concat(member.LastName, member.FirstName) == userLogin);

            // Check whether a member match was found
            if (memberIndex > -1) {
                // Check if password matches
                if (userPassword == currentMembers[memberIndex].PIN)
                {
                    // Successful login, assign the logged in member
                    loggedInMember = currentMembers[memberIndex];
                    return true;
                }                
            }
            
            // Else tell user their details are wrong.
            Console.WriteLine();
            Console.WriteLine("Incorrect username or password.");
            PressAnyKey();

            return false;
        }

        /// <summary>
        /// Displays the staff menu interface to the console. 
        /// </summary>
        /// <returns>true if the staff menu should be repeated, false if exiting.</returns>
        private bool StaffMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Tool Library!");
            Console.WriteLine("=================Staff Menu=================");
            Console.WriteLine("1. Add a new tool");
            Console.WriteLine("2. Add new pieces of an existing tool");
            Console.WriteLine("3. Remove some pieces of a tool");
            Console.WriteLine("4. Register a new member");
            Console.WriteLine("5. Remove a member");
            Console.WriteLine("6. Find the contact number of a member");
            Console.WriteLine("0. Return to main menu");
            Console.WriteLine("============================================");
            Console.WriteLine();
            Console.Write("Please make a selection (1-6, or 0 to return to main menu) - ");

            // Choose submenu
            while (true) // Run until valid input is given
            {
                // Switch case for each menu option
                switch (Console.ReadLine().Trim()) // Get user input
                {
                    case "1": // Add new tool
                        Console.Clear();
                        Console.WriteLine("Tool Library System");
                        Console.WriteLine("===========Add New Tool to Library===========");
                        Console.WriteLine();
                        Console.Write("Enter the name of the new tool (0 to return to main menu) - ");
                        string toolName = Console.ReadLine().Trim(); // get name of tool

                        if (toolName == "0") // Return to main menu
                        {
                            return true;
                        }

                        // Check if tool of this name exists already (to prevent issues)
                        if (data.NameExists(toolName))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Tool with same name already exists! Use the 'Add new pieces of an existing tool' function, or a different tool name.");
                            PressAnyKey();

                            return true; // Exit to menu
                        }

                        // Set category and tool type
                        if (!ChooseToolType())
                        {
                            return true; // Exit to menu
                        }

                        // Create new tool to send to Library System
                        Tool newTool = new Tool(toolName);
                        // Add tool to library
                        library.add(newTool);

                        Console.WriteLine();
                        Console.WriteLine("Tool successfully added to library.");
                        PressAnyKey();

                        return true;// Exit to menu

                    case "2": // Add new pieces of existing tool
                        bool adding = true; // True for adding tools

                        Console.Clear();
                        Console.WriteLine("Tool Library System");
                        Console.WriteLine("===========Increase Stock of Existing Tool===========");

                        // Set category and tool type
                        if (!ChooseToolType())
                        {
                            return true; // Exit to menu
                        }

                        // Get Tool and quantity to add from user
                        if (!ChooseTool(out Tool selectedTool) || !ChooseQuantity(selectedTool, adding))
                        {
                            return true; // Exit to menu
                        }

                        Console.WriteLine();
                        Console.WriteLine("Tool stock successfully updated.");

                        // Display the tools again with updated quantities
                        library.displayTools(null); // USES NULL FOR FAST IMPLEMENTATION, but passing the tool type string also works

                        PressAnyKey();

                        return true; // Exit to menu

                    case "3": // Remove some pieces of a tool
                        adding = false; // True for adding tools, false for deleting

                        Console.Clear();
                        Console.WriteLine("Tool Library System");
                        Console.WriteLine("===========Decrease Stock of Existing Tool===========");

                        // Set category and tool type
                        if (!ChooseToolType())
                        {
                            return true; // Exit to menu
                        }

                        // Get Tool and quantity to remove from user
                        if (!ChooseTool(out selectedTool) || !ChooseQuantity(selectedTool, adding))
                        {
                            return true; // Exit to menu
                        }

                        Console.WriteLine();
                        Console.WriteLine("Tool stock successfully updated.");

                        // Display the tools again with updated quantities
                        library.displayTools(null); // USES NULL FOR FAST IMPLEMENTATION, but passing the tool type string also works

                        PressAnyKey();

                        return true; // Exit to menu

                    case "4": // Register a new member
                        Console.Clear();
                        Console.WriteLine("Tool Library System");
                        Console.WriteLine("===========Register New Member===========");
                        Console.WriteLine();
                        Console.Write("Enter the first name of the member (0 to return to main menu) - ");

                        string memberFirstName = Console.ReadLine().Trim();
                        if (memberFirstName == "0")
                        {
                            return true; // Exit to menu
                        }

                        Console.Write("Enter the last name of the member (0 to return to main menu) - ");
                        string memberLastName = Console.ReadLine().Trim();
                        if (memberLastName == "0")
                        {
                            return true; // Exit to menu
                        }

                        Console.Write("Enter the mobile number of the member (0 to return to main menu) - ");
                        string memberContactNumber;
                        while (true)
                        {
                            memberContactNumber = Console.ReadLine().Trim();
                            if (long.TryParse(memberContactNumber, out _)) // Verify that it is a number
                            {
                                break;
                            }
                            else
                            {
                                InvalidInput();
                                continue;
                            }
                        }

                        if (memberContactNumber == "0")
                        {
                            return true; // Exit to menu
                        }

                        Console.Write("Enter PIN (0 to return to main menu) - ");
                        string memberPIN;
                        while (true)
                        {
                            memberPIN = Console.ReadLine().Trim();
                            if (int.TryParse(memberPIN, out _)) // Verify that it is a number (and not too long)
                            {
                                break;
                            }
                            else
                            {
                                InvalidInput();
                                continue;
                            }
                        }

                        if (memberPIN == "0")
                        {
                            return true; // Exit to menu
                        }

                        // Create new member object and add to the library
                        Member newMember = new Member(memberFirstName, memberLastName, memberContactNumber, memberPIN);
                        library.add(newMember);

                        PressAnyKey();

                        return true;

                    case "5": // Remove a member
                        Member deletedMember; // Member to delete
                        int memberIndex; // Index of member in member array

                        Console.Clear();
                        Console.WriteLine("Tool Library System");
                        Console.WriteLine("===========Remove a Member===========");
                        Console.WriteLine(); 

                        // get array of current members
                        Member[] currentMembers = data.Members.toArray();

                        Console.WriteLine($"List of Members");
                        string border =   "========================================================================";
                        Console.WriteLine(border);
                        Console.WriteLine("    First Name        Last Name        Contact Number        PIN        ");
                        Console.WriteLine(border);

                        for (int i = 0; i < currentMembers.Length; i++)
                        {
                            // Get details of member
                            Member currentMember = currentMembers[i];

                            memberFirstName = currentMember.FirstName;
                            memberLastName = currentMember.LastName;
                            memberContactNumber = currentMember.ContactNumber;
                            memberPIN = currentMember.PIN;

                            // Print member info with Console index that needs to be entered
                            Console.WriteLine(string.Format("{0}.  {1,-18}{2,-17}{3,-22}{4}", i + 1, memberFirstName, memberLastName, memberContactNumber, memberPIN));
                        }

                        Console.WriteLine(border);
                        Console.WriteLine();

                        // Get member selection from user
                        Console.Write("Select a member to remove (0 to return to main menu) - ");

                        while (true) // Loop until valid response given
                        {
                            string memberIndexStr = Console.ReadLine().Trim();
                            if (int.TryParse(memberIndexStr, out memberIndex) && memberIndex > 0 && memberIndex <= currentMembers.Length) // Check input is valid 
                            {
                                // Member successfully selected
                                memberIndex--; // Convert number into the array index

                                // Store the member
                                deletedMember = currentMembers[memberIndex];

                                // Delete the member
                                library.delete(deletedMember);
                                break; // Completed
                            }

                            else if (memberIndexStr == "0") // Return to staff menu
                            {
                                return true;
                            }

                            else // Else input is invalid, loop until valid input is given
                            {
                                InvalidInput();
                            }
                        }

                        PressAnyKey();

                        return true;

                    case "6": // Find the contact number of a member
                        Console.Clear();
                        Console.WriteLine("Tool Library System");
                        Console.WriteLine("===========Find Contact Number of a Member===========");
                        Console.WriteLine();

                        Console.Write("Enter the first name of the member (0 to return to main menu) - ");
                        memberFirstName = Console.ReadLine().Trim();
                        if (memberFirstName == "0")
                        {
                            return true; // Exit to menu
                        }

                        Console.Write("Enter the last name of the member (0 to return to main menu) - ");
                        memberLastName = Console.ReadLine().Trim();
                        Console.WriteLine();
                        if (memberLastName == "0")
                        {
                            return true; // Exit to menu
                        }

                        // Create dummy member to search with using the name of the member
                        Member selectedMember = new Member(memberFirstName, memberLastName);

                        // Get array of current members
                        currentMembers = data.Members.toArray();

                        // Check that member exists, if so return the index of them in the member array
                        memberIndex = Array.FindIndex(currentMembers, member => member.CompareTo(selectedMember) == 0);
                        if (memberIndex > -1) // Member exists
                        {
                            // Get contact number
                            memberContactNumber = currentMembers[memberIndex].ContactNumber;
                            Console.WriteLine($"Contact number of {memberFirstName} {memberLastName} is {memberContactNumber}.");
                        }

                        else
                        {
                            Console.WriteLine("Member not found.");
                        }

                        PressAnyKey();

                        return true;

                    case "0":  // Return to main menu
                        return false;

                    default:  // Invalid input
                        InvalidInput();
                        break;
                }
            }
        }

        /// <summary>
        /// Displays the member menu interface to the console. 
        /// </summary>
        /// <param name="loggedInMember">Member object of the currently logged in member.</param>
        /// <returns>true if the member menu should be repeated, false if exiting.</returns>
        private bool MemberMenu(Member loggedInMember)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Tool Library!");
            Console.WriteLine("=================Member Menu=================");
            Console.WriteLine("1. Display all the tools of a tool type");
            Console.WriteLine("2. Borrow a tool");
            Console.WriteLine("3. Return a tool");
            Console.WriteLine("4. List all the tools that I am renting");
            Console.WriteLine("5. Display top three (3) most frequently borrowed tools");
            Console.WriteLine("0. Return to main menu");
            Console.WriteLine("=============================================");
            Console.WriteLine();
            Console.WriteLine("Please make a selection (1-5, or 0 to return to main menu):");

            // Choose submenu
            while (true) // Run until valid input is given
            {
                // Switch case for each menu option
                switch (Console.ReadLine().Trim()) // Get user input
                {
                    case "1": // Display all the tools of a tool type
                        Console.Clear();
                        Console.WriteLine("Tool Library System");
                        Console.WriteLine("===========Display All Tools of Tool Type===========");

                        // Get tool category and type from the user
                        if (!ChooseToolType())
                        {
                            return true; // Exit to menu
                        }

                        // Get current tool selection
                        library.displayTools(null); // USES NULL FOR FAST IMPLEMENTATION, but passing the tool type string also works

                        PressAnyKey();

                        return true;

                    case "2": // Borrow a Tool
                        Console.Clear();
                        Console.WriteLine("Tool Library System");
                        Console.WriteLine("===========Borrow a Tool===========");

                        // Set category and tool type
                        if (!ChooseToolType())
                        {
                            return true; // Exit to menu
                        }

                        // Get desired tool from user
                        if (!ChooseTool(out Tool selectedTool))
                        {
                            return true; // Exit to menu
                        }

                        // Try borrow the tool
                        library.borrowTool(loggedInMember, selectedTool);

                        PressAnyKey();

                        return true;

                    case "3": // Return a Tool
                        string[] memberTools = library.listTools(loggedInMember); // Get a list of the tools currently held by member
                        string returnedToolName; // The name of the tool to return
                        Tool returnedTool; // The tool object of the tool to return

                        Console.Clear();
                        Console.WriteLine("Tool Library System");
                        Console.WriteLine("===========Return a Tool===========");
                        Console.WriteLine();

                        // Display the tools currently borrowed by the member
                        library.displayBorrowingTools(loggedInMember);

                        Console.WriteLine();
                        Console.Write("Select a tool to return (0 to return to main menu) - ");

                        while (true) // Loop until valid response given
                        {
                            string toolIndexStr = Console.ReadLine().Trim(); // Get user input (trim whitespace)

                            if (int.TryParse(toolIndexStr, out int toolIndex) && toolIndex > 0 && toolIndex <= memberTools.Length) // Check input is valid 
                            {
                                // Tool successfully chosen
                                toolIndex--; // Convert number into the array index

                                // Store the tool name
                                returnedToolName = memberTools[toolIndex];
                                break;
                            }

                            else if (toolIndexStr == "0") // Return to staff menu
                            {
                                return true;
                            }

                            else // Else input is invalid, loop until valid input is given
                            {
                                InvalidInput();
                            }
                        }

                        // Initialise dummy tool to search with
                        returnedTool = new Tool(returnedToolName);

                        // Find the real tool based on the tool name
                        for (int toolCategoryIndex = 0; toolCategoryIndex < data.ToolsMasterArray.Length; toolCategoryIndex++)
                        {
                            // Search each Tool Type tool collection for the tool 
                            for (int toolTypeIndex = 0; toolTypeIndex < data.ToolsMasterArray[toolCategoryIndex].Length; toolTypeIndex++)
                            {                                
                                // Get array of tools of this type
                                Tool[] toolArray = data.ToolsMasterArray[toolCategoryIndex][toolTypeIndex].toArray();

                                // Search for the tool in the array, return the index if found
                                int toolIndex = Array.FindIndex(toolArray, tool => tool.Name == returnedTool.Name);
                                if (toolIndex > -1)
                                {
                                    // Tool is found, so update the selected tool with the real one
                                    returnedTool = toolArray[toolIndex];
                                    goto LoopFinish; // break out of loop
                                }
                            }                            
                        }

                    // Label to tell the program where to skip to once the tool is found
                    LoopFinish:

                        // Try to Return the tool
                        library.returnTool(loggedInMember, returnedTool);

                        Console.WriteLine();
                        Console.WriteLine($"{returnedTool.Name} returned successfully.");

                        PressAnyKey();

                        return true;

                    case "4": // Display All Rented Tools
                        Console.Clear();
                        Console.WriteLine("Tool Library System");
                        Console.WriteLine("===========Display All Rented Tools===========");
                        Console.WriteLine();

                        // Display the tools currently borrowed by the member
                        library.displayBorrowingTools(loggedInMember);

                        PressAnyKey();

                        return true;

                    case "5": // Display Top 3 Most Frequently Used Tools
                        Console.Clear();
                        Console.WriteLine("Tool Library System");
                        Console.WriteLine("===========Top 3 Most Frequently Borrowed Tools===========");
                        Console.WriteLine();

                        // Compute and display the top 3 tools
                        library.displayTopThree();

                        PressAnyKey();

                        return true;

                    case "0":  // Return to main menu
                        return false;

                    default:  // Invalid input
                        InvalidInput();
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the tool category and tool type from the user via the console interface. 
        /// This is then set in the DataSystem.
        /// </summary>
        /// <returns>true if successful, false if returning to menu.</returns>
        private bool ChooseToolType()
        {
            Console.WriteLine();
            Console.WriteLine("Select the category:");
            for (int i = 0; i < data.ToolCategories.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {data.ToolCategories[i]}");
            }
            Console.Write("Select option from menu (0 to return to main menu) - ");

            while (true) // Loop until valid input given
            {
                string toolCategoryStr = Console.ReadLine().Trim();

                if (int.TryParse(toolCategoryStr, out int toolCategory) && toolCategory > 0
                    && toolCategory <= data.ToolCategories.Length) // Check input is valid 
                {
                    toolCategory--; // Convert number into the array index
                    Console.WriteLine();
                    Console.WriteLine("Select the tool type:");
                    for (int i = 0; i < data.ToolTypes[toolCategory].Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {data.ToolTypes[toolCategory][i]}");
                    }
                    Console.Write("Select option from menu (0 to return to main menu) - ");

                    while (true) // Loop until valid input given
                    {
                        string toolTypeStr = Console.ReadLine().Trim();

                        if (int.TryParse(toolTypeStr, out int toolType) && toolType > 0
                        && toolType <= data.ToolTypes[toolCategory].Length) // Check input is valid 
                        {
                            // Tool type successfully found
                            toolType--; // Convert number into the array index

                            // Set current tool category and type in DataSystem
                            data.CurrentCategory = toolCategory;
                            data.CurrentType = toolType;
                            return true;
                        }

                        else if (toolTypeStr == "0") // Return to menu
                        {
                            return false;
                        }

                        else // Else input is invalid, loop until valid input is given
                        {
                            InvalidInput();
                        }
                    }
                }

                else if (toolCategoryStr == "0") // Return to menu
                {
                    return false;
                }

                else // Else input is invalid, loop until valid input is given
                {
                    InvalidInput();
                }
            }
        }

        /// <summary>
        /// Gets the selected tool from the user via the console interface.
        /// </summary>
        /// <param name="selectedTool">The Tool object that was selected by the user.</param>
        /// <returns>true if successful, false if returning to menu.</returns>
        private bool ChooseTool(out Tool selectedTool)
        {
            selectedTool = null; // Initialise variable to store the tool once selected

            // Get current tool selection
            library.displayTools(null); // USES NULL FOR FAST IMPLEMENTATION, but passing the tool type string also works

            Console.WriteLine();
            Console.Write("Select a tool (0 to return to main menu) - ");

            while (true) // Loop until valid response given
            {
                string toolIndexStr = Console.ReadLine().Trim();

                if (int.TryParse(toolIndexStr, out int toolIndex) && toolIndex > 0 && toolIndex <= data.CurrentToolType.Number) // Check input is valid 
                {
                    // Tool successfully found
                    toolIndex--; // Convert number into the array index

                    // Store the tool
                    selectedTool = data.CurrentToolType.toArray()[toolIndex];
                    return true;
                }

                else if (toolIndexStr == "0") // Return to staff menu
                {
                    return false;
                }

                else // Else input is invalid, loop until valid input is given
                {
                    InvalidInput();
                }
            }            
        }

        /// <summary>
        /// Gets the quantity to add or remove from the user via the console interface, checking for valid inputs.
        /// </summary>
        /// <param name="selectedTool">The Tool object that was selected by the user.</param>
        /// <param name="adding">Whether the user is adding or removing the quantity from a tool.</param>
        /// <returns>true if successful, false if returning to menu.</returns>
        private bool ChooseQuantity(Tool selectedTool, bool adding)
        {
            // Get quantity to add or remove
            Console.WriteLine();
            if (adding)
            {
                Console.Write("Enter the quantity to add to the library (0 to return to main menu) - ");
            }

            else
            {
                Console.Write("Enter the quantity to remove from the library (0 to return to main menu) - ");
            }

            while (true) // Loop until valid response given
            {
                string quantityStr = Console.ReadLine().Trim();

                if (int.TryParse(quantityStr, out int quantity) && quantity > 0) // Check input is valid 
                {
                    // Valid quantity found for adding, can now update the tool.
                    if (adding)
                    {
                        library.add(selectedTool, quantity);
                    }

                    else
                    {
                        // Check that tool has available quantity to be deleted
                        if (quantity <= selectedTool.AvailableQuantity)
                        {
                            library.delete(selectedTool, quantity);
                        }

                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Cannot delete more than available quantity!");
                            InvalidInput();
                            continue; // Get user input again
                        }

                    }

                    return true; // Completed
                }

                else if (quantityStr == "0") // Return to staff menu
                {
                    return false;
                }

                else // Else input is invalid, loop until valid input is given
                {
                    InvalidInput();
                }
            }
        }

        /// <summary>
        /// Prints a string to the console informing the user that their input was invalid.
        /// </summary>
        private static void InvalidInput() 
        {
            Console.Write("Please enter a valid input! - ");
        }

        /// <summary>
        /// Prints a string to the console asking the user to press any key to continue, then waits for a keyboard input.
        /// </summary>
        private static void PressAnyKey()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
        }
    }
}
