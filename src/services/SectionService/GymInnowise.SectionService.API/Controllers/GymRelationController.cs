﻿using GymInnowise.SectionService.API.Controllers.Base;
using GymInnowise.Shared.Sections.SectionRelations;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace GymInnowise.SectionService.API.Controllers
{
    [Route("api/gym-relation")]
    public class GymRelationController : SectionRelationController<GymRelation>
    {
        public GymRelationController(ISender sender) : base(sender)
        {
        }
    }
}