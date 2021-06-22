using System;
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
        /// To add algorithm with custom render use "RegisterAlgorithmWithRender" method.
        /// The default is AStarRender
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterAlgorithmsAndRenders(this ContainerBuilder builder)
        {
            builder.RegisterScoped<AStarRender, IRender>();

            builder.RegisterAlgorithmWithRender<JpsDiagonal, JpsRender>();
            builder.RegisterAlgorithmWithRender<AStarAlgorithm, AStarRender>();

            builder.RegisterScoped<LeeAlgorithm, IAlgorithm>();
            builder.RegisterScoped<IDA, IAlgorithm>();
        }

        private static void RegisterAlgorithmWithRender<TAlgorithm, TRender>(this ContainerBuilder builder)
            where TAlgorithm : IAlgorithm
            where TRender : IRender
        {
            var tempId = Guid.NewGuid().ToString();
            builder.RegisterType<TRender>()
                .Named<IRender>(tempId)
                .InstancePerLifetimeScope();
            builder.RegisterType<TAlgorithm>()
                .As<IAlgorithm>()
                .WithParameter(ResolvedParameter.ForNamed<IRender>(tempId))
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