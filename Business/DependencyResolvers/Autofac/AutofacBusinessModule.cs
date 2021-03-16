//Created By Engin Yenice
//enginyenice2626@gmail.com

using Autofac;
using Business.Abstract;
using Business.Concrete;
using Business.Helpers.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Business
            builder.RegisterType<IndexerManager>().As<IIndexerService>();

            //DataAccess
            builder.RegisterType<InMemoryWordToExcludeDal>().As<IWordToExcludeDal>();
            builder.RegisterType<InMemoryTagAndPoint>().As<ITagAndPointDal>();


            //Helpers
            builder.RegisterType<HtmlClearer>().As<IHtmlClearer>();
            builder.RegisterType<KeywordOperation>().As<IKeywordOperation>();
            builder.RegisterType<WebSiteOperation>().As<IWebSiteOperation>();
        }
    }
}