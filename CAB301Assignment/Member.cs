using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    public class Member : iMember, IComparable<Member>
    {
        private string firstName;        
        private string lastName;        
        private string contactNumber;        
        private string pin;
        private ToolCollection tools;
        
        public Member(string firstName, string lastName) // Constructor for searching just based on name
        {
            FirstName = firstName;
            LastName = lastName;
            ContactNumber = "";
            PIN = "";
            tools = new ToolCollection();
        }
        
        public Member(string firstName, string lastName, string contactNumber, string pin) // General Constructor
        {
            FirstName = firstName;
            LastName = lastName;
            ContactNumber = contactNumber;
            PIN = pin;
            tools = new ToolCollection();
        }

        public string FirstName  //get and set the first name of this member
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName //get and set the last name of this member
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string ContactNumber //get and set the contact number of this member
        {
            get { return contactNumber; }
            set { contactNumber = value; }
        }

        public string PIN //get and set the password of this member
        {
            get { return pin; }
            set { pin = value; }
        }

        public string[] Tools //get a list of tools that this member is currently holding
        {
            get 
            {
                Tool[] toolsArray = tools.toArray(); // Get array form of tool collection
                return Array.ConvertAll(toolsArray, item => item.Name); // Convert to a string array of tool names
            }
        }

        public void addTool(Tool aTool) //add a given tool to the list of tools that this member is currently holding
        {
            tools.add(aTool); // Add tool to the member's tool collection
        }

        public void deleteTool(Tool aTool) //delete a given tool from the list of tools that this member is currently holding
        {
            tools.delete(aTool); // Delete tool from the member's tool collection
        }

        public override string ToString() //return a string containing the first name, lastname, and contact phone number of this memeber
        {
            return $"{FirstName} {LastName}, {ContactNumber}";
        }

        public int CompareTo(Member obj)
        {
            Member another = obj; // other member to compare to

            // Compare names by last name, then first name
            if (LastName.CompareTo(another.LastName) < 0)
                return -1;
            else
                if (LastName.CompareTo(another.LastName) == 0)
                return FirstName.CompareTo(another.FirstName);
            else
                return 1;
        }
    }
}
