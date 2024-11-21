using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Logic.Handlers.Redundant;
using GymInnowise.SectionService.Logic.Handlers.RelationHandlers;
using GymInnowise.SectionService.Logic.Handlers.Sections;
using GymInnowise.SectionService.Logic.Queries;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.Shared.Sections.Base;
using GymInnowise.Shared.Sections.Base.Relations;
using GymInnowise.Shared.Sections.Dtos.Responses;
using GymInnowise.Shared.Sections.Interfaces;
using GymInnowise.Shared.Sections.Redundant;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.API.Extensions
{
    public static class ApplicationMediatrExtensions
    {
        public static WebApplicationBuilder AddMediatr(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(AddToSectionCommand<>).Assembly));
            builder.Services
                .AddRedundantHandlers<Profile, ProfileEntity>()
                .AddRedundantHandlers<Gym, GymEntity>()
                .AddRelationHandlers<MembershipBase, SectionMemberEntity, ProfileEntity>()
                .AddRelationHandlers<MentorshipBase, SectionCoachEntity, ProfileEntity>()
                .AddRelationHandlers<GymRelationBase, SectionGymEntity, GymEntity>()
                .AddSectionHandlers();

            return builder;
        }

        private static IServiceCollection AddRedundantHandlers<TRedundant, TRedundantEntity>(
            this IServiceCollection services)
            where TRedundant : class, IRedundant
            where TRedundantEntity : class, TRedundant, IEntity
        {
            return services
                .AddTransient<IRequestHandler<CreateRedundantCommand<TRedundant>>,
                    CreateRedundantHandler<TRedundant, TRedundantEntity>>()
                .AddTransient<IRequestHandler<UpdateRedundantCommand<TRedundant>>,
                    UpdateRedundantHandler<TRedundant, TRedundantEntity>>();
        }

        private static IServiceCollection AddRelationHandlers<TRelation, TRelationEntity, TEntity>(
            this IServiceCollection services)
            where TRelation : class, ISectionRelation
            where TRelationEntity : class, TRelation, IJoinEntity, ITimeStampedModel, new()
            where TEntity : class, IEntity
        {
            return services
                .AddTransient<IRequestHandler<AddToSectionCommand<TRelation>,
                        OneOf<Success, NotFound, Error<string>>>,
                    AddToSectionHandler<TRelationEntity, TEntity, TRelation>>()
                .AddTransient<IRequestHandler<GetSectionRelationQuery<TRelation>,
                        OneOf<TRelation, NotFound>>,
                    GetSectionRelationHandler<TRelation, TRelationEntity>>()
                .AddTransient<IRequestHandler<GetSectionRelationQuery<TRelation>,
                        OneOf<TRelation, NotFound>>,
                    GetSectionRelationHandler<TRelation, TRelationEntity>>()
                .AddTransient<IRequestHandler<UpdateSectionRelationCommand<TRelation>,
                        OneOf<Success, NotFound>>,
                    UpdateSectionRelationHandler<TRelationEntity, TRelation>>();
        }

        private static IServiceCollection AddSectionHandlers(this IServiceCollection services)
        {
            return services
                .AddTransient<IRequestHandler<CreateSectionCommand, Guid>,
                    CreateSectionHandler>()
                .AddTransient<IRequestHandler<GetSectionFullQuery, OneOf<GetSectionFullResponse, NotFound>>,
                    GetSectionFullHandler>()
                .AddTransient<IRequestHandler<GetSectionPreviewQuery, OneOf<SectionBase, NotFound>>,
                    GetSectionPreviewHandler>()
                .AddTransient<IRequestHandler<GetSectionsByTagsQuery, IReadOnlyList<SectionBase>>,
                    GetSectionsByTagsHandler>()
                .AddTransient<IRequestHandler<UpdateSectionCommand, OneOf<Success, NotFound>>,
                    UpdateSectionHandler>();
        }
    }
}
