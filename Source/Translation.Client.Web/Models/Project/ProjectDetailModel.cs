﻿using System;

using StandardUtils.Helpers;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;

namespace Translation.Client.Web.Models.Project
{
    public sealed class ProjectDetailModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public Guid ProjectUid { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string OrganizationName { get; set; }
        public string Url { get; set; }
        public int LabelCount { get; set; }
        public bool IsActive { get; set; }
        public string LanguageName { get; set; }
        public string LanguageIconUrl { get; set; }

        public HiddenInputModel OrganizationInput { get; }
        public HiddenInputModel ProjectInput { get; }
        public CheckboxInputModel IsActiveInput { get; set; }

        public ProjectDetailModel()
        {
            Title = "project_detail_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");
            ProjectInput = new HiddenInputModel("ProjectUid");
            IsActiveInput = new CheckboxInputModel("IsActive", "is_active", false, true);
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();
            ProjectInput.Value = ProjectUid.ToUidString();

            InfoMessages.Clear();
            InfoMessages.Add("the_project_language_will_use_as_the_source_language_during_the_automatic_translation_of_the_labels");
        }
    }
}