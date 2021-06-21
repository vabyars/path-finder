using System.ComponentModel.Design;
using Autofac;
using Autofac.Builder;
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
        /// To add algorithm with custom render use "RegisterAlgorithmWithRender" method.
        /// The default is AStarRender
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterAlgorithmsAndRenders(this ContainerBuilder builder)
        {
            builder.RegisterScoped<AStarRender, IRender>();

            builder.RegisterAlgorithmWithRender<AStarAlgorithm, AStarRender>();
            builder.RegisterAlgorithmWithRender<JpsDiagonal, JpsRender>();

            builder.RegisterScoped<LeeAlgorithm, IAlgorithm>();
            builder.RegisterScoped<IDA, IAlgorithm>();
        }

        private static void RegisterAlgorithmWithRender<TAlgorithm, TRender>(this ContainerBuilder builder)
            where TAlgorithm : IAlgorithm
            where TRender : IRender
        {
            builder.RegisterType<TRender>().Named<IRender>(nameof(TRender))
                .InstancePerLifetimeScope();
            builder.RegisterType<TAlgorithm>().As<IAlgorithm>()
                .WithParameter(ResolvedParameter.ForNamed<IRender>(nameof(TRender)))
                .InstancePerLifetimeScope();
        }
        
        private static void RegisterScoped<TFrom, TTo>(this ContainerBuilder builder)
        {
            builder.RegisterType<TFrom>()
                .As<TTo>()
                .InstancePerLifetimeScope();
        }
    }
}