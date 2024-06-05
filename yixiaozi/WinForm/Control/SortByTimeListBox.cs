using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using yixiaozi.Model.DocearReminder;

namespace yixiaozi.WinForm.Control
{
    public class SortByTimeListBox : ListBox
    {
        public SortByTimeListBox() : base()
        {
        }

        // Overrides the parent class Sort to perform a simple
        // bubble sort on the length of the string contained in each item.
        protected override void Sort()
        {
            return;
            ObjectCollection itemsCopy = this.Items;
            if (itemsCopy.Count > 1)
            {
                bool swapped;
                do
                {
                    int counter = itemsCopy.Count - 1;
                    swapped = false;

                    while (counter > 0)
                    {
                        if (((MyListBoxItemRemind)itemsCopy[counter]).Time
                            < ((MyListBoxItemRemind)itemsCopy[counter - 1]).Time)
                        {
                            object temp = itemsCopy[counter];
                            itemsCopy[counter] = itemsCopy[counter - 1];
                            itemsCopy[counter - 1] = temp;
                            swapped = true;
                        }
                        counter -= 1;
                    }
                }
                while ((swapped == true));
            }
            //for (int i = 0; i < itemsCopy.Count; i++)
            //{
            //    Items[i] = itemsCopy[i];
            //}
        }
    }
}
