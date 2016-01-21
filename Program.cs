using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FirstApp
{
    class Program
    {
        /*
	     * !!!При первом запуске файл входных данных генерируется автоматически!!!
	     *
         * Считать из файла последовательность целых чисел. Вычислить 90 
         * персентиль, медиану, максимальное, минимальное и среднее значения. 
         * На вход программа получает файл с числами. Каждое число в файле 
         * находится на новой строке. Вывод в консоль должен быть следующим:
         * 90 percentile <значение>
         * median <значение>
         * average <значение>
         * max <значение>
         * min <значение> 
         */
        static void Main(string[] args)
        {
            double[] array = GetArray();
            Console.WriteLine("90 percentile " + Percentile(array, 0.9));
            Console.WriteLine("median " + Percentile(array, 0.5));
            Console.WriteLine("average " + Average(array));
            Console.WriteLine("max " + array.Max());
            Console.WriteLine("min " + array.Min());
            
            Console.WriteLine("press any key to exit...");
            Console.ReadKey();
        }

        static void SetArray() {
            Random r = new Random();
            try { 
                using (StreamWriter generate = new StreamWriter("input.txt")) { 
                    for (int i = 0; i < 100; i++) {
                        generate.WriteLine(((double) r.Next(0, 10000) / 100).ToString());
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine("Houston we have a problem: " + e.Message);
            }
        }

        static double[] GetArray() {
            List<double> list = new List<double>();
            bool isInit = false;

            while (!isInit) { 
                try { 
                    using (StreamReader input = new StreamReader("input.txt"))
                    {
                        string line;
                        isInit = true;

                        while ((line = input.ReadLine()) != null)
                        {
                            list.Add(Convert.ToDouble(line));
                        }
                    }
                }
                catch (FileNotFoundException e) {
                    SetArray();
                    Console.WriteLine("Массив сгенерирован.");
                }
            }

            return list.ToArray();
        }

        static double Percentile(double[] sequence, double excelPercentile)
        {
            double[] sortedSequence = (double[])sequence.Clone();
            Array.Sort(sortedSequence);
            int N = sortedSequence.Length;
            double n = (N - 1) * excelPercentile + 1;
            if (n == 1d) return sortedSequence[0];
            else if (n == N) return sortedSequence[N - 1];
            else
            {
                int k = (int)n;
                double d = n - k;
                return sortedSequence[k - 1] + d * (sortedSequence[k] - sortedSequence[k - 1]);
            }
        }

        static double Sum(double[] array)
        {
            double result = 0;

            for (int i = 0; i < array.Length; i++)
            {
                result += array[i];
            }

            return result;
        }

        static double Average(double[] array)
        {
            double sum = Sum(array);
            double result = (double)sum / array.Length;
            return result;
        }
    }
}
