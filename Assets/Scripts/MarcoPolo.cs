public static class MarcoPolo 
{
    public static string Solve()
    {
        string solution = string.Empty;

        for (int i = 1; i <= 100; i++)
        {
            string marcoPolo = string.Empty;

            if (i % 3 == 0)
                marcoPolo += "Marko";

            if (i % 5 == 0)
                marcoPolo += "Polo";

            solution += $"{i} : {marcoPolo} \n";
        }

        return solution;
    }
}
