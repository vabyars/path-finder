using Autofac;
using Autofac.Core;
using PathFinder.Domain.Models.Algorithms;
using PathFinder.Domain.Models.Algorithms.Realizations.AStar;
using PathFinder.Domain.Models.Algorithms.Realizations.IDA;
using PathFinder.Domain.Models.Algorithms.Realizations.JPS;
using PathFinder.Domain.Models.Algorithms.Realizations.Lee;
using PathFinder.Domain.Models.Renders;

namespace PathFinder.Api
{
    public static class AutofacContainerBuilderExtensions
    {
        /// <summary>
        /// Registers algorithms and renders to builder.
        /// To add algorithm with custom render use "WithParameter" method.
        /// The default is AStarRender
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterAlgorithmsAndRenders(this ContainerBuilder builder)
        {
            builder.RegisterType<AStarRender>().Named<IRender>("AStar");
            builder.RegisterType<JpsRender>().Named<IRender>("JPS");
            
            builder.RegisterType<AStarRender>().As<IRender>();

            builder.RegisterType<AStarAlgorithm>().As<IAlgorithm>()
                .WithParameter(ResolvedParameter.ForNamed<IRender>("AStar"));

            builder.RegisterType<JpsDiagonal>().As<IAlgorithm>()
                .WithParameter(ResolvedParameter.ForNamed<IRender>("JPS"));
            
            builder.RegisterType<LeeAlgorithm>().As<IAlgorithm>();
            builder.RegisterType<IDA>().As<IAlgorithm>();
        }
    }
}