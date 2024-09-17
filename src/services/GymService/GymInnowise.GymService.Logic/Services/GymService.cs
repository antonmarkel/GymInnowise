﻿using AutoMapper;
using GymInnowise.GymService.Logic.Interfaces;
using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.GymService.Persistence.Repositories.Interfaces;
using GymInnowise.GymService.Shared.Dtos.Requests.Creates;
using GymInnowise.GymService.Shared.Dtos.Requests.Updates;
using GymInnowise.GymService.Shared.Dtos.Responses.Gets;
using GymInnowise.GymService.Shared.Enums;
using OneOf;
using OneOf.Types;

namespace GymInnowise.GymService.Logic.Services
{
    public class GymService(IGymRepository _repo, IMapper _mapper) : IGymService
    {
        public async Task CreateGymAsync(CreateGymRequest request)
        {
            var gymEntity = _mapper.Map<GymEntity>(request);
            await _repo.AddGymAsync(gymEntity);
        }

        public async Task<OneOf<Success, NotFound>> UpdateGymAsync(Guid gymId, UpdateGymRequest updateRequest)
        {
            var gymEntity = await _repo.GetGymByIdAsync(gymId);
            if (gymEntity is null)
            {
                return new NotFound();
            }

            _mapper.Map(updateRequest, gymEntity);
            await _repo.UpdateGymAsync(gymEntity);

            return new Success();
        }

        public async Task<OneOf<GetGymDetailsResponse, NotFound>> GetGymDetailsByIdAsync(Guid gymId)
        {
            var gymEntity = await _repo.GetGymByIdAsync(gymId);
            if (gymEntity is null)
            {
                return new NotFound();
            }

            return _mapper.Map<GetGymDetailsResponse>(gymEntity);
        }

        public async Task<List<GetGymPreviewResponse>> GetGymPreviewsByTagsAsync(List<GymTag> tags)
        {
            if (!tags.Any())
            {
                var gyms = await _repo.GetAllGymsAsync();

                return gyms.Select(_mapper.Map<GetGymPreviewResponse>).ToList();
            }

            var gymPreviewDtos = await _repo.GetGymsByTagsAsync(tags);

            return gymPreviewDtos.Select(_mapper.Map<GetGymPreviewResponse>).ToList();
        }
    }
}