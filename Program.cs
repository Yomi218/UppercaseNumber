using System;

namespace UppercaseNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入需要转换的数字：");
            while (true)
            {
                var num = Console.ReadLine();
                var result = Number2English(num).Trim().Replace("  ", " ").ToUpper();
                Console.WriteLine(result);
                Console.WriteLine();
            }
        }

        public static string Number2English(string nu)
        {
            string dollars;
            string cents = "";
            string tp = "";
            string[] tx = { "", "Thousand", "Million", "Billion", "Trillion" };

            if (decimal.Parse(nu) == 0) return "0";

            if (decimal.Parse(nu) <= 0) return "Error!! ";

            //處理小數點(通常是兩位)
            var temp = nu.Split('.');
            if (temp.Length > 2)
                return "Error!!";
            if (temp.Length == 2)
            {
                string strx = temp[1];

                foreach (var pointNum in strx)
                {
                    cents = cents + GetEnglish(pointNum.ToString());
                }

                if (!cents.Equals("")) cents = "point" + cents;
            }

            //處理整數部分
            //先將資料格式化，只取出整數
            decimal x = Math.Truncate(decimal.Parse(nu));
            //格式化整數部分
            temp = x.ToString("#,0").Split(',');
            //利用整數,號檢查千、萬、百萬....
            int j = temp.Length - 1;

            foreach (var t in temp)
            {
                tp += GetEnglish(t);
                if (tp != "")
                {
                    tp = tp + tx[j] + " ";
                }
                else
                {
                    tp += " ";
                }

                j -= 1;
            }
            if (x == 0 && cents != "") // 如果整數部位= 0 ，且有小數
            {
                dollars = cents;
            }
            else
            {
                if (cents == "")
                {
                    dollars = tp;
                }
                else
                {
                    dollars = tp + cents;
                }
            }
            return dollars;
        }

        public static string GetEnglish(string nu)
        {
            string x;
            string str2;
            string[] tr = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] ty = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            //處理百位數
            var str1 = tr[int.Parse(nu) / 100] + " Hundred and";
            if (str1.Equals(" Hundred and")) str1 = ""; //如果結果是空值，表示沒有百分位
            //處理十位數
            var temp = int.Parse(nu) % 100;   //  當315 除100 會剩餘 15 

            if (temp < 20)
            {
                str2 = tr[temp]; //取字串陣列 
            }
            else
            {
                str2 = ty[temp / 10];  //十位數  10/20/30的數量確認 

                if (str2.Equals(""))
                {
                    str2 = tr[temp % 10];
                }
                else
                {
                    str2 = str2 + "-" + tr[temp % 10];  //十位數組成
                }

            }
            if (str1 == "" && str2 == "")
            {
                x = "";
            }
            else
            {
                x = str1 + " " + str2 + " ";
            }

            return x;
        }
    }
}
