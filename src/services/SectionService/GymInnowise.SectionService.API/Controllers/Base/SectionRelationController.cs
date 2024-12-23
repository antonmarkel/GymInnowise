﻿using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Logic.Queries;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.Sections.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymInnowise.SectionService.API.Controllers.Base
{
    public class SectionRelationController<TRelation> : ControllerBase
        where TRelation : class, ISectionRelation
    {
        private readonly ISender _sender;

        public SectionRelationController(ISender sender)
        {
            _sender = sender;
        }

        [ActionName(nameof(GetRelationByIdAsync))]
        [HttpGet("{sectionId}/{relatedId}")]
        public virtual async Task<IActionResult> GetRelationByIdAsync([FromRoute] Guid sectionId,
            [FromRoute] Guid relatedId)
        {
            var result = await _sender.Send(new GetSectionRelationQuery<TRelation>(sectionId, relatedId));

            return result.Match<IActionResult>(
                Ok,
                _ => NotFound()
            );
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public virtual async Task<IActionResult> CreateRelationAsync([FromBody] TRelation relation)
        {
            var result = await _sender.Send(new AddToSectionCommand<TRelation>(relation));

            return result.Match<IActionResult>(
                _ => CreatedAtAction(nameof(GetRelationByIdAsync), new { relation.SectionId, relation.RelatedId },
                    relation),
                _ => NotFound(),
                err => BadRequest(err.Value)
            );
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut]
        public virtual async Task<IActionResult> UpdateRelationAsync([FromBody] TRelation relation)
        {
            var result = await _sender.Send(new UpdateSectionRelationCommand<TRelation>(relation));

            return result.Match<IActionResult>(
                _ => NoContent(),
                _ => NotFound()
            );
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{sectionId}/{relatedId}")]
        public virtual async Task<IActionResult> RemoveRelationAsync([FromRoute] Guid sectionId,
            [FromRoute] Guid relatedId)
        {
            await _sender.Send(new RemoveFromSectionCommand<TRelation>(sectionId, relatedId));

            return NoContent();
        }
    }
}
