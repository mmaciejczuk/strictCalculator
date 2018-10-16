using System;
using System.Text.RegularExpressions;

namespace TestRecrutApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write expression you want to calculate (numbers and +-/* operators):");
            string exp = (Console.ReadLine()).ToString();            

            // validation
            if (!Regex.IsMatch(exp, @"^\s*(\d+)(?:\s*([-+*\/])\s*((?:\s[-+])?\d+)\s*)+$"))
            {
                Console.WriteLine("Expression is not correct");
                Console.ReadLine();
            }

            else
            {
                string result = "";
                string resultAS = "";
                string temp = "";
                string tempAS = "";
                string op = "";
                int j = 0;
                string resDM = "";
                string resDMTemp = "";

                // there are two for loops - one for dividing and multiplication, second for adding and subtraction
                for (int i = 0; i < exp.Length; i++)
                {
                    // if number has two or more digits
                    if (Char.IsNumber(exp[i]))
                    {
                        temp += exp[i];
                        if (i + 1 == exp.Length)
                        {
                            result += temp;
                            temp = "";
                        }
                    }

                    // calculations for dividing and multiplication
                    else if (exp[i] == '/' || exp[i] == '*')
                    {
                        j = i;
                        // if number has two or more digits
                        while (j + 1 < exp.Length && Char.IsNumber(exp[j + 1]))
                        {
                            resDM += exp[j + 1].ToString();
                            j++;
                        }

                        if (temp == "")
                        {
                            temp = resDMTemp;
                            int k = result.Length;
                            for (int l = k -1; l > -1 && (Char.IsNumber(result[l]) || result[l] == ','); l--)
                            {
                                result = result.Remove(l);
                            }
                        }
                        // method to calculate two values
                        resDM = CalculateValues(exp[i].ToString(), temp, resDM);

                        i = j;
                        result += resDM;
                        temp = "";
                        resDMTemp = resDM;
                        resDM = "";
                    }

                    // concatenate for adding and subtraction
                    else if (exp[i] == '-' || exp[i] == '+')
                    {
                        result += temp + exp[i];
                        temp = "";
                    }
                }

                // if result isn't calculated yet
                if(!Regex.IsMatch(result.Replace(',', '.'), @"^-?\d+(?:\.\d+)?$"))
                {
                    // second loop for adding and subtraction
                    for (int i = 0; i < result.Length; i++)

                    {
                        if (Char.IsNumber(result[i]) || result[i] == ',')
                        {
                            temp += result[i];
                            if (i + 1 == result.Length)
                            {
                                // method to calculate two values
                                resultAS = CalculateValues(op, tempAS, temp);
                            }
                        }
                        else
                        {
                            if (tempAS == "")
                            {
                                op = result[i].ToString();
                                tempAS = temp;
                                temp = "";
                            }
                            else
                            {
                                // method to calculate two values
                                resultAS = CalculateValues(op, tempAS, temp);
                                op = result[i].ToString();
                                tempAS = resultAS;
                                temp = "";
                            }
                        }
                    }

                    Console.WriteLine("Result is: {0}", resultAS);
                    Console.ReadLine();
                }

                // if result is calculated already
                else
                {
                    Console.WriteLine("Result is: {0}", result);
                    Console.ReadLine();
                }

            }
        }

        static string CalculateValues(string op, string firstVal, string stringSecondVal)
        {
            switch (op)
            {
                case "/": return Math.Round(double.Parse(firstVal) / double.Parse(stringSecondVal), 1).ToString();
                case "*": return Math.Round(double.Parse(firstVal) * double.Parse(stringSecondVal), 1).ToString();
                case "-": return (double.Parse(firstVal) - double.Parse(stringSecondVal)).ToString();
                case "+": return (double.Parse(firstVal) + double.Parse(stringSecondVal)).ToString();
                default: return "";
            }
        }
    }
}
