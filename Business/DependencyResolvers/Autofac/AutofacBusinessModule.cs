//Created By Engin Yenice
//enginyenice2626@gmail.com

using Autofac;
using Business.Abstract;
using Business.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IndexerManager>().As<IIndexerService>();
        }
    }
}