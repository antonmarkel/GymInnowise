﻿using GymInnowise.Shared.Sections.Interfaces;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.Logic.Commands
{
    public sealed record AddToSectionCommand<TSectionRelation>(TSectionRelation Relation)
        : IRequest<OneOf<Success, NotFound>>
        where TSectionRelation : class, ISectionRelation;
}