/// <summary>
/// File: MainPage.xaml.cs
/// By: Connor Reed
/// Email: Contact@calexreed.me
/// Course: CITC 1372
/// </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Project3
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }
        // function that gets called when switch 1 is toggeled
        private void SwBitOne_Toggled(object sender, ToggledEventArgs e)
        {
			// evaultes if the event value is true or false to determine what code to run
            if (e.Value == true)
            {
                lbBitOne.TextColor = Color.Red;
                BitFun();
                
            }
            else
            {
                lbBitOne.TextColor = Color.Gray;
                BitFun();
            }
        }
		
		// function that gets called when switch 2 is toggeled
        private void SwBitTwo_Toggled(object sender, ToggledEventArgs e)
        {
			// evaultes if the event value is true or false to determine what code to run
            if (e.Value == true)
            {
                lbBitTwo.TextColor = Color.Red;
                BitFun();
            }
            else
            {
                lbBitTwo.TextColor = Color.Gray;
                BitFun();
            }
        }
		
		// Function that evaultes bits by determining if either is on or off
        private void BitFun()
        {
			// local varabiles 
            byte byte1;
            byte byte2;
            byte byte3Add;
            byte byte3OR;
            byte byte3AND;
            string bitsAdd;
            string bitsAND;
            string bitsOR;
			
			// Determines if the event firing made them true or false and assigns them a value
            if (SwBitOne.IsToggled == true)
            {
                byte1 = 0b1;
            } 
            else
            {
                byte1 = 0b0;
            }

            if(SwBitTwo.IsToggled == true)
            {
                byte2 = 1;
            }
            else
            {
                byte2 = 0;
            }
			
			// this AND's, OR's, or Add's the bits together.
            byte3AND = (byte)(byte1 & byte2); // AND
            byte3OR = (byte)(byte1 | byte2); // OR
            byte3Add = (byte)(byte1 + byte2); // ADD
			
            // changes bytes to binary form and puts it into string form and appends them to proper spot in form
            bitsAdd = Convert.ToString(byte3Add, 2);
            ADDingBits.Text = bitsAdd;
            bitsAND = Convert.ToString(byte3AND, 2);
            ANDingBits.Text = bitsAND;
            bitsOR= Convert.ToString(byte3OR, 2);
            ORingBits.Text = bitsOR;

        }
    }
}
