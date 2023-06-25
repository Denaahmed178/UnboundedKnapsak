using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class AliBabaInParadise
    {
        #region YOUR CODE IS HERE
        #region FUNCTION#1: Calculate the Value
        //Your Code is Here:
        //==================
        /// <summary>
        /// Given the Camels maximum load and N items, each with its weight and profit 
        /// Calculate the max total profit that can be carried within the given camels' load
        /// </summary>
        /// <param name="camelsLoad">max load that can be carried by camels</param>
        /// <param name="itemsCount">number of items</param>
        /// <param name="weights">weight of each item</param>
        /// <param name="profits">profit of each item</param>
        /// <returns>Max total profit</returns>
        static int[,] storedValues;
        static string[,] traceMax;
        static public int SolveValue(int camelsLoad, int itemsCount, int[] weights, int[] profits)
        {
            //REMOVE THIS LINE BEFORE START CODING
            storedValues = new int[itemsCount + 1, camelsLoad + 1];
            traceMax = new string[itemsCount + 1, camelsLoad + 1];
            int max = SolveSubValue(camelsLoad, itemsCount, weights, profits);

            return max;
            //throw new NotImplementedException();

        }
        static public int SolveSubValue(int camelsLoad, int itemsCount, int[] weights, int[] profits)
        {

            if (camelsLoad == 0 || itemsCount == 0)
                return storedValues[itemsCount, camelsLoad] = 0;  // no items or no weight 

            if (storedValues[itemsCount, camelsLoad] != 0)
                return storedValues[itemsCount, camelsLoad]; //already calculated

            if (weights[itemsCount - 1] > camelsLoad) //skip  it 
            {
                storedValues[itemsCount, camelsLoad] = SolveSubValue(camelsLoad, itemsCount - 1, weights, profits);
                traceMax[itemsCount, camelsLoad] = "<-";
            }
            else
            {
                int e = 0, i = 0;    // e for exclude  i for include
                if (weights[itemsCount - 1] <= camelsLoad)
                {
                    i = profits[itemsCount - 1] + SolveSubValue(camelsLoad - weights[itemsCount - 1], itemsCount, weights, profits);
                    // W- w[n] , N
                }
                if (itemsCount > 0)
                {
                    e = SolveSubValue(camelsLoad, itemsCount - 1, weights, profits);
                    // W , N-1 
                }
                //storedValues[itemsCount, camelsLoad] = Math.Max(i, e);
                if (i > e)
                {
                    storedValues[itemsCount, camelsLoad] = i;
                    traceMax[itemsCount, camelsLoad] = "*";
                }
                else if (e > i)
                {
                    storedValues[itemsCount, camelsLoad] = e;
                    traceMax[itemsCount, camelsLoad] = "<-";
                }
            }
            return storedValues[itemsCount, camelsLoad]; //memoize
        }
        #endregion

        #region FUNCTION#2: Construct the Solution
        //Your Code is Here:
        //==================
        /// <returns>Tuple array of the selected items to get MAX profit (stored in Tuple.Item1) together with the number of instances taken from each item (stored in Tuple.Item2)
        /// OR NULL if no items can be selected</returns>
        static List<Tuple<int, int>> values = new List<Tuple<int, int>>();
        static public Tuple<int, int>[] ConstructSolution(int camelsLoad, int itemsCount, int[] weights, int[] profits)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            AliBabaInParadise.values.Clear();  //clear to empty the global var
            Solutionitems(camelsLoad, itemsCount, weights);
            var values = AliBabaInParadise.values.ToArray();
            return values;
        }
        static public void Solutionitems(int camelsLoad, int itemsCount, int[] weights)
        {

            if (camelsLoad == 0 || itemsCount == 0)
                return;
            if (traceMax[itemsCount, camelsLoad].Equals("<-"))
            {
                Solutionitems(camelsLoad, itemsCount - 1, weights);
            }
            else if (traceMax[itemsCount, camelsLoad].Equals("*"))
            {
                if (values.Count == 0 || itemsCount != values.Last().Item1)// if new item , add it
                {
                    var t = new Tuple<int, int>(itemsCount, 1);

                    values.Add(t);
                }
                else
                {
                    // if the item already exist , change the number of instances
                    var newTup = new Tuple<int, int>(itemsCount, values.FirstOrDefault(t => t.Item1 == itemsCount).Item2 + 1);
                    Tuple<int, int> tupleToRemove = values.FirstOrDefault(t => t.Item1 == itemsCount);
                    bool v = values.Remove(tupleToRemove);
                    values.Add(newTup);
                }
                Solutionitems(camelsLoad - weights[itemsCount - 1], itemsCount, weights);
            }

        }
        #endregion

        #endregion
    }
}
