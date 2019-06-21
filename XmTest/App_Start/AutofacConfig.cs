
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using System.Reflection;

namespace XmTest.App_Start
{
    /// <summary>
    /// 控制反转和依赖注入的调用
    /// </summary>
    public class AutofacConfig
    {
        public static void RegisterDependency()
        {
           
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            var iService = Assembly.Load("XmTest.IService");
            var Service = Assembly.Load("XmTest.Service");

            var iRepository = Assembly.Load("XmTest.IRepository");
            var Repository = Assembly.Load("XmTest.Repository");

            builder.RegisterAssemblyTypes(iRepository, Repository).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(iService, Service).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces();
            //创建容器
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        
        }



    }
}