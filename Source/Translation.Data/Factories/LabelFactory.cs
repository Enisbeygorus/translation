﻿using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Label;
using Translation.Data.Entities.Domain;

namespace Translation.Data.Factories
{
    public class LabelFactory
    {
        public Label CreateEntity(string key, Project projectEntity)
        {
            var entity = new Label();
            entity.Name = key;
            entity.LabelKey = key;
            entity.IsActive = true;

            entity.ProjectId = projectEntity.Id;
            entity.ProjectUid = projectEntity.Uid;
            entity.ProjectName = projectEntity.Name;

            entity.OrganizationId = projectEntity.OrganizationId;
            entity.OrganizationUid = projectEntity.OrganizationUid;
            entity.OrganizationName = projectEntity.OrganizationName;

            return entity;
        }

        public Label CreateEntityFromRequest(LabelCloneRequest request, Label label, Project project)
        {
            var entity = new Label();
            entity.OrganizationId = label.OrganizationId;
            entity.OrganizationUid = label.OrganizationUid;
            entity.OrganizationName = label.OrganizationName;

            entity.ProjectUid = project.Uid;
            entity.ProjectId = project.Id;
            entity.ProjectName =project.Name;

            entity.Description = request.Description;
            entity.LabelKey = request.LabelKey;
            entity.Name = request.LabelKey;
            entity.LabelTranslationCount = label.LabelTranslationCount;

            entity.IsActive = true;

            return entity;
        }

        public Label CreateEntityFromRequest(LabelEditRequest request, Label entity)
        {
            entity.UpdatedBy = request.CurrentUserId;
            entity.LabelKey = request.LabelKey;
            entity.Name = request.LabelKey;
            entity.Description = request.Description;

            return entity;
        }

        public Label CreateEntityFromRequest(LabelCreateRequest request, Project project)
        {
            var entity = new Label();

            entity.LabelKey = request.LabelKey;
            entity.Name = request.LabelKey;
            entity.Description = request.Description;

            entity.OrganizationId = project.OrganizationId;
            entity.OrganizationUid = project.OrganizationUid;
            entity.OrganizationName = project.OrganizationName;
            entity.ProjectId = project.Id;
            entity.ProjectUid = project.Uid;
            entity.ProjectName = project.Name;
            entity.IsActive = true;

            return entity;
        }

        public LabelDto CreateDtoFromEntity(Label entity)
        {
            var dto = new LabelDto();
            dto.Uid = entity.Uid;
            dto.CreatedAt = entity.CreatedAt;
            dto.UpdatedAt = entity.UpdatedAt;
            dto.Key = entity.LabelKey;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.LabelTranslationCount = entity.LabelTranslationCount;

            dto.OrganizationUid = entity.OrganizationUid;
            dto.OrganizationName = entity.OrganizationName;
            dto.ProjectUid = entity.ProjectUid;
            dto.ProjectName = entity.ProjectName;
            dto.IsActive = entity.IsActive;

            return dto;
        }

        public Label UpdateEntityForChangeActivation(Label entity)
        {
            entity.IsActive = !entity.IsActive;
            return entity;
        }
    }
}