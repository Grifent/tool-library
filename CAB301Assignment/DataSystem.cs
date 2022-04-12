using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Assignment
{
    /// <summary>
    /// Stores all collections and data for the Tool Library System.
    /// </summary>
    public class DataSystem
    {
        // Parallel String Array of Tool Categories
        private readonly string[] toolCategories = { "Gardening Tools", "Flooring Tools", "Fencing Tools", "Measuring Tools", "Cleaning Tools",
                                                            "Painting Tools", "Electronic Tools", "Electricity Tools", "Automotive Tools" };

        // Parallel String Arrays for each Tool Category
        private static readonly string[] gardeningToolsTypes = { "Line Trimmers", "Lawn Mowers", "Hand Tools", "Wheelbarrows", "Garden Power Tools" };
        private static readonly string[] flooringToolsTypes = { "Scrapers", "Floor Lasers", "Floor Levelling Tools", "Floor Levelling Materials", "Floor Hand Tools", "Tiling Tools" };
        private static readonly string[] fencingToolsTypes = { "Hand Tools ", "Electric Fencing", "Steel Fencing Tools", "Power Tools", "Fencing Accessories" };
        private static readonly string[] measuringToolsTypes = { "Distance Tools", "Laser Measurer", "Measuring Jugs", "Temperature & Humidity Tools", "Levelling Tools", "Markers" };
        private static readonly string[] cleaningToolsTypes = { "Draining", "Car Cleaning", "Vacuum", "Pressure Cleaners", "Pool Cleaning", "Floor Cleaning" };
        private static readonly string[] paintingToolsTypes = { "Sanding Tools", "Brushes", "Rollers", "Paint Removal Tools", "Paint Scrapers", "Sprayers" };
        private static readonly string[] electronicToolsTypes = { "Voltage Tester", "Oscilloscopes", "Thermal Imaging", "Data Test Tool", "Insulation Testers" };
        private static readonly string[] electricityToolsTypes = { "Test Equipment", "Safety Equipment", "Basic Hand Tools", "Circuit Protection", "Cable Tools" };
        private static readonly string[] automotiveToolsTypes = { "Jacks", "Air Compressors", "Battery Chargers", "Socket Tools", "Braking", "Drivetrain" };

        // Master Array of String Arrays for each Tool Type and Tool Category
        private static readonly string[][] toolTypes = { gardeningToolsTypes, flooringToolsTypes, fencingToolsTypes, measuringToolsTypes, cleaningToolsTypes,
                                                         paintingToolsTypes, electronicToolsTypes, electricityToolsTypes, automotiveToolsTypes };

        // ToolCollection arrays to store each tool category
        private ToolCollection[] gardeningTools = new ToolCollection[gardeningToolsTypes.Length]; // initialise each array with number of tool types
        private ToolCollection[] flooringTools = new ToolCollection[flooringToolsTypes.Length];
        private ToolCollection[] fencingTools = new ToolCollection[fencingToolsTypes.Length];
        private ToolCollection[] measuringTools = new ToolCollection[measuringToolsTypes.Length];
        private ToolCollection[] cleaningTools = new ToolCollection[cleaningToolsTypes.Length];
        private ToolCollection[] paintingTools = new ToolCollection[paintingToolsTypes.Length];
        private ToolCollection[] electronicTools = new ToolCollection[electronicToolsTypes.Length];
        private ToolCollection[] electricityTools = new ToolCollection[electricityToolsTypes.Length];
        private ToolCollection[] automotiveTools = new ToolCollection[automotiveToolsTypes.Length];

        // Master Array of ToolCollection arrays, indexed by [Category][Type]
        private ToolCollection[][] toolsMasterArray; 

        // Member Collection for the system
        private MemberCollection members = new MemberCollection();

        // Store current index for the tool category and type
        private int currentCategory = 0;
        private int currentType = 0;

        // Store names of existing tools in a list, so it is easy to add new tool names
        private List<string> toolNames = new List<string>(); 

        /// <summary>
        /// Creates a DataSytem structure to store all the tool and member collections for the Tool Library System, as well as the currently selected tool type.
        /// </summary>
        public DataSystem()
        {
            // Add each tool category array to the master jagged array
            toolsMasterArray = new ToolCollection[][] { gardeningTools, flooringTools, fencingTools, measuringTools, cleaningTools, paintingTools,
                                                         electronicTools, electricityTools, automotiveTools };

            // Initialise tool collections
            for (int i = 0; i < toolsMasterArray.Length; i++)
            {
                for (int j = 0; j < toolsMasterArray[i].Length; j++)
                {
                    toolsMasterArray[i][j] = new ToolCollection();
                }
            }
        }

        /// <summary>
        /// Returns the member collection for the entire system. 
        /// </summary>
        public MemberCollection Members // Members in the system (and for logins)
        {
            get { return members; }
        }

        /// <summary>
        /// Returns the names of each tool category as a string array.
        /// </summary>
        public string[] ToolCategories 
        {
            get { return toolCategories; }
        }

        /// <summary>
        /// Returns the names of the tool types in each category as an array of string arrays.
        /// </summary>
        public string[][] ToolTypes 
        {
            get { return toolTypes; }
        }

        /// <summary>
        /// Returns the Tool Collections for each category and tool type as an array of Tool Collection arrays.
        /// </summary>
        public ToolCollection[][] ToolsMasterArray 
        {
            get { return toolsMasterArray; }
        }

        /// <summary>
        /// The index of the current tool category set by the user.
        /// </summary>
        public int CurrentCategory
        {
            get { return currentCategory; }
            set { currentCategory = value; }
        }

        /// <summary>
        /// The index of the current tool type set by the user.
        /// </summary>
        public int CurrentType
        {
            get { return currentType; }
            set { currentType = value; }
        }

        /// <summary>
        /// Returns the Tool Collection that stores the tools of the currently selected tool type.
        /// </summary>
        public ToolCollection CurrentToolType
        {
            get { return toolsMasterArray[currentCategory][currentType]; }
        }

        /// <summary>
        /// Returns the name of the currently selected tool type.
        /// </summary>
        public string CurrentToolTypeStr
        {
            get { return toolTypes[currentCategory][currentType]; } 
        }

        /// <summary>
        /// Checks if the provided tool name already exists in the system.
        /// </summary>
        /// <param name="toolName">The name of the tool to check.</param>
        /// <returns>true if the tool name exists, false otherwise.</returns>
        public bool NameExists(string toolName)
        {
            if (toolNames.Contains(toolName)) // Check if the tool name exists already
            {
                return true;
            }

            else
            {
                toolNames.Add(toolName); // Add the tool name to the list if it isnt in there
                return false;
            }            
        }
    }
}
