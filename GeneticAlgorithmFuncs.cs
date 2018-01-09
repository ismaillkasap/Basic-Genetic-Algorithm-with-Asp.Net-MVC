using GeneticAlgorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeneticAlgorithm
{
    public class GeneticAlgorithmFuncs
    {      
        public static int count_ones(string value)
        {
            int counter = 0;
            foreach(var val in value)
            {
                if (val == '1')
                    counter++;
            }
            return counter;
        }

        public static List<float> sort_avarages(List<float>avarages_of_values)
        {
            for (int i = 0; i < 4; i++)
            {
                for(int j = 1; j<4; j++)
                {
                    if (avarages_of_values[j] < avarages_of_values[j - 1])
                    {
                        var temp_value = avarages_of_values[j - 1];
                        avarages_of_values[j - 1] = avarages_of_values[j];
                        avarages_of_values[j] = temp_value;
                    }
                }
            }
            return avarages_of_values;
        }

        public static int[] sort_int_values (int[] avarages_of_values)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    if (avarages_of_values[j] < avarages_of_values[j - 1])
                    {
                        var temp_value = avarages_of_values[j - 1];
                        avarages_of_values[j - 1] = avarages_of_values[j];
                        avarages_of_values[j] = temp_value;
                    }
                }
            }
            return avarages_of_values;
        }

        public static List<float> generate_random_dec()
        {
            List<float> Random_Decs = new List<float>();
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                float temp_val = rnd.Next(1, 100);
                Random_Decs.Add(temp_val/100);
            }
            return Random_Decs;
        }

        public static int[] choose_element(List<float> rand_decs, List<float> avarages_ones)
        {
            int[] result = new int[] { -1, -1, -1, -1 };
            for (int i = 0; i < 4; i++)
            {
                if (rand_decs[i] <= avarages_ones[0])
                    result[i] = 0;
                if (rand_decs[i] <= avarages_ones[1] && rand_decs[i] > avarages_ones[0])
                    result[i] = 1;
                if (rand_decs[i] <= avarages_ones[2] && rand_decs[i] > avarages_ones[1])
                    result[i] = 2;
                if (rand_decs[i] <= avarages_ones[3] && rand_decs[i] > avarages_ones[2])
                    result[i] = 3;
            }
            result = sort_int_values(result);
            return result;
        }

        public static List<string> crosswise(string value1, string value2)
        {
            List<string> result_values = new List<string>();
            string crosswised_value1 = value1[0].ToString() + value1[1].ToString() + value2[2].ToString() + value1[3].ToString() + value1[4].ToString() + value1[5].ToString() + value1[6].ToString() + value1[7].ToString();
            string crosswised_value2 = value2[0] + value2[1].ToString() + value1[2].ToString() + value2[3].ToString() + value2[4].ToString() + value2[5].ToString() + value2[6].ToString() + value2[7].ToString();
            result_values.Add(crosswised_value1);
            result_values.Add(crosswised_value2);
            return result_values;
        }

        public static List<string> mutation(List<string> crosswised_values)
        {            
            Random rnd = new Random();
            int mutation_index = rnd.Next(4);
            int index = rnd.Next(8);
            string temp_value = crosswised_values[mutation_index];
            string result_value = "";
            
            for(int i = 0; i<8; i++)
            {
                if (i != index)
                    result_value += temp_value[i].ToString();
                else
                {
                    if (temp_value[i] == '1')
                        result_value += 0.ToString();
                    else
                        result_value += 1.ToString();
                }
            }           

            crosswised_values[mutation_index] = result_value;
            return crosswised_values;
        }

        public static List<string> run(List<string> values)
        {
            try
            {
                if (values[0] != "11111111" && values[1] != "11111111" && values[2] != "11111111" && values[3] != "11111111")
                {
                    float ones_of_a = count_ones(values[0]); //a'da bulunan 1 sayısı hesaplanıyor.
                    float ones_of_b = count_ones(values[1]); //b'de bulunan 1 sayısı hesaplanıyor.
                    float ones_of_c = count_ones(values[2]); //c'de bulunan 1 sayısı hesaplanıyor.
                    float ones_of_d = count_ones(values[3]); //d'de bulunan 1 sayısı hesaplanıyor.
                    float ones_of_total = ones_of_a + ones_of_b + ones_of_c + ones_of_d; //a,b,c ve d'deki toplam 1 sayısı hesaplanıyor.
                    List<float> averages_ones = new List<float>();
                    averages_ones.Add(ones_of_a / ones_of_total); //a'daki ortalama 1 sayısı hesaplanıyor ve listeye atılıyor.
                    averages_ones.Add(ones_of_b / ones_of_total); //b'deki ortalama 1 sayısı hesaplanıyor ve listeye atılıyor.
                    averages_ones.Add(ones_of_c / ones_of_total); //c'deki ortalama 1 sayısı hesaplanıyor ve listeye atılıyor.
                    averages_ones.Add(ones_of_d / ones_of_total); //d'deki ortalama 1 sayısı hesaplanıyor ve listeye atılıyor.
                    averages_ones = sort_avarages(averages_ones); //Ortalamalara göre liste küçükten büyüğe sıralanıyor.
                    averages_ones[0] = averages_ones[0]; //a'nın 0-1 aralığındaki üst konumu hesaplanıyor.
                    averages_ones[1] = averages_ones[1] + averages_ones[0]; //b'nın 0-1 aralığındaki üst konumu hesaplanıyor.
                    averages_ones[2] = averages_ones[2] + averages_ones[1]; //c'nın 0-1 aralığındaki üst konumu hesaplanıyor.
                    averages_ones[3] = 1; //d'nın 0-1 aralığındaki üst konumu hesaplanıyor.
                    var random_decs = generate_random_dec(); //4 adet ondalık sayı üretiliyor.
                    int[] chosen_elements = choose_element(random_decs, averages_ones); //Ondalık sayılara karşılık gelen değer seçilir.

                    var crosswise12 = crosswise(values[chosen_elements[0]], values[chosen_elements[1]]); //İlk değer çifti çaprazlanıyor.
                    var crosswise34 = crosswise(values[chosen_elements[2]], values[chosen_elements[3]]); //İkinci değer çifti çaprazlanıyor.
                    List<string> crosswised_values = new List<string>();
                    crosswised_values.AddRange(crosswise12);
                    crosswised_values.AddRange(crosswise34); //Çaprazlanan değerler tek listede toplanıyor.

                    values = mutation(crosswised_values); //Rastgele seçilen değerde mutasyon yapılıyor ve son değerler listeye atılıyor.

                    return values;
                }
                else
                {                 
                    List<string> state = new List<string>();
                    state.Add("Done");
                    return (state);                 
                }
            }
            catch (Exception)
            {
                List<string> err = new List<string>();
                err.Add("NotOk");
                return (err);
            }                                      
        }         
    }           
}