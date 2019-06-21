using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmTest.Data.Factory
{

    /// <summary>
    /// 工厂实例类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DALFactory<T> where T : new()
    {
        public static T _instance;

        public static T Instance
        {
            get
            {
                if (DALFactory<T>._instance == null)
                {
                    DALFactory<T>._instance = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
                }
                return DALFactory<T>._instance;
            }
        }

    }
}
