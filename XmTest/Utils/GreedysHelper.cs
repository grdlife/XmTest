using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace XmTest.Utils
{
    public class GreedysHelper
    {
        //1.活动优先级
        /*
          2.描述：一间教室被用来举行活动，在某个时间段，有N个活动需要占用此教室，如何安排，效果、效率最好。
          3.转化变量： 活动集合：e{1,2....n}  区间[si,fi]  表示活动起始时间、结束时间
        
         */

        public static  List<Integer> ActivityPriority()
        {
            int[] start = { 1, 3, 0, 5, 3, 5, 6, 8, 8, 2, 12 };
            int[] end = { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            List<int> ay = start.ToList();

            List<Integer> lgs = ActivityPriority(start, end);
            return lgs;        
        }


        /// <summary>
        ///  安排:集合
        /// </summary>
        public static List<Integer> ActivityPriority(int[] start, int[] end)
        {         
            List<int> ary = SortActivity(start, end);
            List<Integer> arry=new List<Integer>();
             Integer Ig=new Integer();
             for (int i = 0; i < ary.Count; i++)
            {
               Ig=new Integer();
               int k = ary[i];
               Ig.index = ary[i];
               Ig.StartTime = start[k];
               Ig.EndTime = end[k];
               arry.Add(Ig);
            }
            return arry;
        }
        /// <summary>
        /// 活动安排 起始：s  结束 e
        /// </summary>
        public static List<int> SortActivity(int[] s,int[] e)
        {
       
            Integer lg=new Integer();
            List<Integer> lgs=new List<Integer>();

            List<int> ary = new List<int>();
            ary.Add(0);

            int endtime = e[0];
            int aCount = s.Length;

            for(int i=0;i<aCount;i++)
           {
              if(endtime<s[i])//结束时间小于起始时间 相容
              {
                  ary.Add(i);
                  endtime = e[i];
                  //e.ToList().Except(ary);
              }
           }
            //foreach(var item in ary)
            //{
            //    s.SkipWhile(x => x ==item);
            //    e.SkipWhile(x => x == item);
            //}
           
            return ary;
        }


        //纸牌移动： 移动最少次数，使每堆上面的牌一致。
        /*
         示例一: int[] ary={9，8，17，6};
         示例二: int[] ary={1，2，27};
         avg：平均数
         
         a[i]=avg;
         a[i+1]=a[i+1]+a[i]-avg;
         
         堆数 count
         */

        public static int MoveCardCount()
        {
            int[] ary={9,8,17,6};
            int Count = ary.Length;//堆数
            int mcCount = MoveCard(ary, Count);
            return mcCount;
        }

        public static int MoveCard(int[] ary,int Count)
        {
            int sums = 0;//总牌数
          
            for (int i = 0; i < ary.Length; i++)
            {
                sums += ary[i];
            }
            int avg = sums / Count;//平均数


            //移动次数
            int aCount = 0;
            for(int i=0;i<ary.Length;i++)
            {
                if(ary[i]!=avg)
                {
                    int  lt=  ary[i]-avg;//差
                    ary[i] -= lt;
                    ary[i + 1] = ary[i+1] + lt;
                    aCount++;
                }
            }
            return aCount;
            
        }

    }
    //public class IntegerS
    //{
    //    public string day { get; set; }
    //    public List<Integer> IntegerList { get; set; }      
    //}
    public class Integer
    {
        public int index { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}