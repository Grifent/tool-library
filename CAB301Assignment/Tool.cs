using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    public class Tool : iTool
    {
        private string name;
        private int quantity;
        private int availableQuantity;
        private int noBorrowings;
        private MemberCollection getBorrowers;

        public Tool(string name)
        {
            this.name = name;
            // Initialise quantities
            quantity = 1;
            availableQuantity = 1;
            noBorrowings = 0;
            getBorrowers = new MemberCollection();
        }
        
        public string Name// get and set the name of this tool
        {
            get { return name; }
            set { name = value; }
        }

        public int Quantity // get and set the quantity of this tool
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public int AvailableQuantity // get and set the quantity of this tool currently available to lend
        {
            get { return availableQuantity; }
            set { availableQuantity = value; } // 
        }

        public int NoBorrowings // get and set the number of times that this tool has been borrowed
        {
            get { return noBorrowings; }
            set { noBorrowings = value; } // 
        }

        public MemberCollection GetBorrowers  //get all the members who are currently holding this tool
        {
            get { return getBorrowers; } // 
        }

        public void addBorrower(Member aMember) //add a member to the borrower list
        {
            getBorrowers.add(aMember); // Add member to the Member Collection for this tool
        }

        public void deleteBorrower(Member aMember) //delete a member from the borrower list 
        {
            getBorrowers.delete(aMember); // MemberCollection should handle this logic (checking if member exists)
        }

        public override string ToString() //return a string containing the name and the available quantity and quantity of this tool 
        {
            return $"{name}: {availableQuantity} available, {quantity} total"; 
        }

    }
}
