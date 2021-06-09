using Autofac;
using Autofac.Core;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.Algorithms.IDA;
using PathFinder.Domain.Models.Algorithms.JPS;
using PathFinder.Domain.Models.Algorithms.Lee;
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
            builder.RegisterType<AStarRender>().As<IRender>();

            builder.RegisterType<AStarAlgorithm>().As<IAlgorithm>()
                .WithParameter(ResolvedParameter.ForNamed<IRender>("AStar"));
            
            builder.RegisterType<JpsDiagonal>().As<IAlgorithm>();
            builder.RegisterType<LeeAlgorithm>().As<IAlgorithm>();
            builder.RegisterType<IDA>().As<IAlgorithm>();
        }
    }
}