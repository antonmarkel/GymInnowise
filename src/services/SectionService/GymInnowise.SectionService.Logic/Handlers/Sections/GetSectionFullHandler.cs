﻿using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Logic.Queries;
using GymInnowise.SectionService.Persistence.Entities.JoinEntities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Dtos.Responses;
using GymInnowise.Shared.Sections.SectionRelations.Information;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.Logic.Handlers.Sections
{
    public class GetSectionFullHandler : IRequestHandler<GetSectionFullQuery, OneOf<GetSectionFullResponse, NotFound>>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper<SectionGymEntity, GymRelationInformation> _gymInformationMapper;
        private readonly IMapper<SectionCoachEntity, MentorshipInformation> _mentorInformationMapper;
        private readonly IMapper<SectionMemberEntity, MembershipInformation> _memberInformationMapper;

        public GetSectionFullHandler(ISectionRepository sectionRepository,
            IMapper<SectionGymEntity, GymRelationInformation> gymInformationMapper,
            IMapper<SectionCoachEntity, MentorshipInformation> mentorInformationMapper,
            IMapper<SectionMemberEntity, MembershipInformation> memberInformationMapper)
        {
            _sectionRepository = sectionRepository;
            _gymInformationMapper = gymInformationMapper;
            _mentorInformationMapper = mentorInformationMapper;
            _memberInformationMapper = memberInformationMapper;
        }

        public async Task<OneOf<GetSectionFullResponse, NotFound>> Handle(GetSectionFullQuery request,
            CancellationToken cancellationToken)
        {
            var entity =
                await _sectionRepository.GetSectionIncludeReferencesByIdAsync(request.SectionId, cancellationToken);
            if (entity is null)
            {
                return new NotFound();
            }

            var sectionRequest = new GetSectionFullResponse
            {
                Name = entity.Name,
                CostPerTraining = entity.CostPerTraining,
                Description = entity.Description,
                IsActive = entity.IsActive,
                Tags = entity.Tags,
                ThumbnailId = entity.ThumbnailId,

                GymsInfo = entity.Gyms.Select(_gymInformationMapper.Map).ToList(),
                MembersInfo = entity.Members.Select(_memberInformationMapper.Map).ToList(),
                MentorsInfo = entity.Coaches.Select(_mentorInformationMapper.Map).ToList()
            };

            return sectionRequest;
        }
    }
}