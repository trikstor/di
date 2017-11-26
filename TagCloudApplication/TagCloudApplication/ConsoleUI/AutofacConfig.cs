using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using TagCloud;
using TagCoudRenderer;
using TextReader;

namespace ConsoleUI
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<Reader>().As<IReader>();
            builder.RegisterType<Renderer>().As<IReader>().WithParameters(new List<Parameter>
            {
               // new NamedParameter("color", ),
               //new NamedParameter("font", ),
               //new NamedParameter("size", )
            });

            var container = builder.Build();


        }
    }
}
