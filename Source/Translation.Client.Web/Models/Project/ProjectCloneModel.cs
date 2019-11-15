﻿using System;

using StandardUtils.Helpers;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;

namespace Translation.Client.Web.Models.Project
{
    public class ProjectCloneModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public Guid CloningProjectUid { get; set; }
        public string Name { get; set; }

        public string Url { get; set; }
        public string Description { get; set; }

        public int LabelCount { get; set; }
        public int LabelTranslationCount { get; set; }
        public bool IsSuperProject { get; set; }

        public string Slug { get; set; }

        public Guid LanguageUid { get; set; }
        public string LanguageName { get; set; }

        public HiddenInputModel OrganizationInput { get; }
        public HiddenInputModel CloningProjectInput { get; }

        public InputModel NameInput { get; }
        public InputModel SlugInput { get; }
        public UrlInputModel UrlInput { get; }
        public LongInputModel DescriptionInput { get; }

        public HiddenInputModel LabelCountInput { get; set; }
        public HiddenInputModel LabelTranslationCountInput { get; set; }
        public CheckboxInputModel IsSuperProjectInput { get; set; }

        public SelectInputModel LanguageInput { get; }

        public ProjectCloneModel()
        {
            Title = "project_clone_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");
            CloningProjectInput = new HiddenInputModel("CloningProjectUid");
            NameInput = new InputModel("Name", "name");
            SlugInput = new InputModel("Slug", "slug");

            UrlInput = new UrlInputModel("Url", "url");
            DescriptionInput = new LongInputModel("Description", "description");

            LabelCountInput = new HiddenInputModel("LabelCount");
            LabelTranslationCountInput = new HiddenInputModel("LabelTranslationCount");
            IsSuperProjectInput = new CheckboxInputModel("IsSuperProject", "is_super_project");
            LanguageInput = new SelectInputModel("Language", "language", "/Language/SelectData");
            LanguageInput.IsOptionTypeContent = true;
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();
            CloningProjectInput.Value = CloningProjectUid.ToUidString();
            NameInput.Value = Name;
            SlugInput.Value = Slug;

            UrlInput.Value = Url;
            DescriptionInput.Value = Description;

            LabelCountInput.Value = LabelCount.ToString();
            LabelTranslationCountInput.Value = LabelTranslationCount.ToString();
            IsSuperProjectInput.Value = IsSuperProject;

            if (LanguageUid.IsNotEmptyGuid())
            {
                LanguageInput.Value = LanguageUid.ToUidString();
                LanguageInput.Text = LanguageName;
            }

            InfoMessages.Clear();
            InfoMessages.Add("the_project_language_will_use_as_the_source_language_during_the_automatic_translation_of_the_labels");
        }

        public override void SetInputErrorMessages()
        {
            if (OrganizationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("organization_uid_is_not_valid");
            }

            if (CloningProjectUid.IsEmptyGuid())
            {
                ErrorMessages.Add("cloning_project_uid_is_not_valid");
            }

            Name = Name.TrimOrDefault();
            if (Name.IsEmpty())
            {
                NameInput.ErrorMessage.Add("project_name_required_error_message");
                InputErrorMessages.AddRange(NameInput.ErrorMessage);
            }

            Slug = Slug.TrimOrDefault();
            if (Slug.IsEmpty())
            {
                SlugInput.ErrorMessage.Add("project_slug_required_error_message");
                InputErrorMessages.AddRange(SlugInput.ErrorMessage);
            }

            Url = Url.TrimOrDefault();
            if (Url.IsNotEmpty()
                && Url.IsNotUrl())
            {
                UrlInput.ErrorMessage.Add("url_is_not_valid_error_message");
                InputErrorMessages.AddRange(UrlInput.ErrorMessage);
            }

            if (LanguageUid.IsEmptyGuid())
            {
                LanguageInput.ErrorMessage.Add("language_uid_not_valid");
                InputErrorMessages.AddRange(LanguageInput.ErrorMessage);
            }
        }
    }
}