using Autofac;
using Business.Abstract;
using Business.Concrete;
using Business.Helpers.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;

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
            builder.RegisterType<InMemoryTagAndPointDal>().As<ITagAndPointDal>();
            //Helpers
            builder.RegisterType<HtmlCleaner>().As<IHtmlCleaner>();
            builder.RegisterType<KeywordOperation>().As<IKeywordOperation>();
            builder.RegisterType<WebSiteOperation>().As<IWebSiteOperation>();
        }
    }
}