using System;
using System.Collections;
using System.Collections.Generic;

namespace MoltenUtilites
{
    public class ListUtilities<T>
    {
        // Swaps two items in a list
        public static void Swap(List<T> a_list, T a_itemOne, T a_itemTwo)
        {
            if (!a_list.Contains(a_itemOne) && !a_list.Contains(a_itemTwo))
                return;

            int itemIndex = a_list.IndexOf(a_itemOne);
            a_list[itemIndex] = a_itemTwo;
            itemIndex = a_list.IndexOf(a_itemTwo);
            a_list[itemIndex] = a_itemOne;
        }

        // Swaps two items in a list based on index
        public static void Swap(List<T> a_list, int a_indexOne, int a_indexTwo)
        {
            if (a_indexOne > a_list.Count || a_indexTwo > a_list.Count)
                return;

            Swap(a_list, a_list[a_indexOne], a_list[a_indexTwo]);
        }

        // Shuffles a list
        public static void ShuffleList(List<T> a_list)
        {
            Random rand = new Random();

            int previousIndex = a_list.Count - 1;
            int currentIndex = -1;

            // Used the Fisher-Yates-Shuffle; https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
            for (int i = 0; i < a_list.Count; i++)
            {
                currentIndex = rand.Next(0, i + 1);
                Swap(a_list, currentIndex, previousIndex);
                previousIndex = currentIndex;
            }
        }

        // Returns a string of all elements of a list, seperated by commas. (Eg. "1, 2, 3, 4, 5")
        public static string ListToString(List<T> a_list)
        {
            string result = "";
            for (int i = 0; i < a_list.Count; i++)
            {
                result += a_list[i];
                if (i < a_list.Count - 1)
                {
                    result += ", ";
                }
            }

            return result;
        }

        // Returns a shuffled copy of a list
        public static List<T> GetShuffledList(List<T> a_list)
        {
            List<T> list = a_list;
            ShuffleList(list);
            return list;
        }

        // Randomly shuffles a list in to another list
        public static void ShuffleInsert(List<T> a_masterList, List<T> a_listToAdd)
        {
            Random rand = new Random();
            List<T> newList = a_listToAdd;

            ShuffleList(newList);

            for (int i = 0; i < newList.Count; i++)
            {
                int index = rand.Next(0, a_masterList.Count);
                a_masterList.Insert(index, newList[i]);
            }
        }

        // Shuffles both lists whilst also shuffle inserting the second list in to the first?
        public static void ShuffleMerge(List<T> a_masterList, List<T> a_listToAdd)
        {
            ShuffleList(a_masterList);
            ShuffleInsert(a_masterList, GetShuffledList(a_listToAdd));
        }

        // Returns the array in the form of a list
        public static List<T> ArrayToList(T[] a_array)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < a_array.Length; i++)
            {
                list.Add(a_array[i]);
            }

            return list;
        }

        public static void RemoveSingular(List<T> a_list, T a_item)
        {
            int index = a_list.IndexOf(a_item);
            a_list.RemoveAt(index);
        }
    }
}
