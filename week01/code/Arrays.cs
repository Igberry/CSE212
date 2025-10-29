using System;
using System.Collections.Generic;
using System.Diagnostics;

public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Solution Plan (step-by-step):
        // 1. Create a container (List<double> or array) to hold 'length' multiples.
        // 2. Loop an index i from 1 up to 'length' (inclusive).
        // 3. For each i compute multiple = number * i.
        // 4. Add the computed multiple to the container.
        // 5. After the loop convert the container to a double[] and return it.
        //
        // This is explicit enough for another person to implement.

        // Implementation:
        List<double> multiples = new List<double>(length);
        for (int i = 1; i <= length; i++)
        {
            multiples.Add(number * i);
        }

        return multiples.ToArray();
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Solution Plan (step-by-step):
        // 1. If data is null or empty, do nothing and return.
        // 2. Let n = data.Count.
        // 3. Normalize amount with amount = amount % n so amounts equal to n wrap to 0.
        // 4. If amount == 0 after normalization, nothing to do; return.
        // 5. Compute splitIndex = n - amount. The last 'amount' items move to the front.
        // 6. Use GetRange to take the rightPart = data.GetRange(splitIndex, amount).
        // 7. Use GetRange to take the leftPart = data.GetRange(0, splitIndex).
        // 8. Clear the original list and AddRange(rightPart) then AddRange(leftPart).
        // This modifies `data` in-place as required.

        if (data == null || data.Count == 0)
            return;

        int n = data.Count;
        amount = amount % n;
        if (amount == 0)
            return;

        int splitIndex = n - amount;
        List<int> rightPart = data.GetRange(splitIndex, amount); // last 'amount' items
        List<int> leftPart = data.GetRange(0, splitIndex);       // first n-amount items

        data.Clear();
        data.AddRange(rightPart);
        data.AddRange(leftPart);
    }
}
