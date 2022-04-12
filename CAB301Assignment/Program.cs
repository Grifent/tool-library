using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Menu system
            DataSystem data = new DataSystem(); // Create new data system to store collections
            ToolLibrarySystem library = new ToolLibrarySystem(data); // Create new library system, sending the data system to it
            Menu menuSystem = new Menu(library, data); // Create a new member system, using the library and data systems

            //if (args.Length > 0) // Check if args have been given
            //{
            //    if (args[0] == "-test") // Add default tool values for testing
            //    {
                    // Create new tools, default type is Gardening tools, Line trimmers
                    Tool tool1 = new Tool("Big Tool");
                    Tool tool2 = new Tool("Ozito 290mm Electric Line Trimmer");
                    Tool tool3 = new Tool("Ryobi Easy Stat Curved Shaft Line Trimmer");
                    Tool tool4 = new Tool("Ozito 500W Pole Edged Trimmer");
                    Tool tool5 = new Tool("Some Ridiculously Long Tool Just to Test the System");

                    // Create new members
                    Member joe = new Member("Joe", "Smith", "0123456789", "1234");

                    // Add tools to library
                    library.add(tool1);
                    data.NameExists(tool1.Name);
                    library.add(tool1, 5);
                    library.add(tool2);
                    data.NameExists(tool2.Name);
                    library.add(tool2, 3);
                    library.add(tool3);
                    data.NameExists(tool3.Name);
                    library.add(tool3, 8);
                    library.add(tool4);
                    data.NameExists(tool4.Name);
                    library.add(tool4, 2);
                    library.add(tool5);
                    data.NameExists(tool5.Name);

                    // Add new members
                    library.add(joe);
            //    }
            //}

            // Bool that checks whether the user wants to exit the menu.
            bool showMenu = true; // Initialise true to start menu

            // Keep running the main menu until the user decides to exit. 
            while (showMenu) 
            {
                // Start the main menu
                showMenu = menuSystem.MainMenu();
            }                      
        }
    }
}
