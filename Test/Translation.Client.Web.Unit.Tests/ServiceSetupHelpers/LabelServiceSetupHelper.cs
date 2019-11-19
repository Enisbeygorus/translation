﻿using System.Collections.Generic;

using Moq;
using StandardUtils.Enumerations;
using StandardUtils.Models.DataTransferObjects;

using Translation.Common.Contracts;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Requests.Label.LabelTranslation;
using Translation.Common.Models.Responses.Label;
using Translation.Common.Models.Responses.Label.LabelTranslation;
using Translation.Data.Repositories.Contracts;

using static Translation.Common.Tests.TestHelpers.FakeDtoTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.ServiceSetupHelpers
{
    public static class LabelServiceSetupHelper
    {
        public static void Setup_LabelCreateWithToken_Returns_LabelCreateResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateLabel(It.IsAny<LabelCreateWithTokenRequest>()))
                .ReturnsAsync(new LabelCreateResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_LabelCreateWithToken_Returns_LabelCreateResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateLabel(It.IsAny<LabelCreateWithTokenRequest>()))
                .ReturnsAsync(new LabelCreateResponse { Status = ResponseStatus.Failed });
        }

        public static void Setup_LabelCreateWithToken_Returns_LabelCreateResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateLabel(It.IsAny<LabelCreateWithTokenRequest>()))
                .ReturnsAsync(new LabelCreateResponse { Status = ResponseStatus.Invalid });
        }

        public static void Verify_LabelCreateWithToken(this Mock<ILabelService> service)
        {
            service.Verify(x => x.CreateLabel(It.IsAny<LabelCreateWithTokenRequest>()));
        }

        public static void Setup_GetLabelByKey_Returns_LabelReadByKeyResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabelByKey(It.IsAny<LabelReadByKeyRequest>()))
                   .ReturnsAsync(new LabelReadByKeyResponse() { Status = ResponseStatus.Success });
        }

        public static void Setup_GetLabelByKey_Returns_LabelReadByKeyResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabelByKey(It.IsAny<LabelReadByKeyRequest>()))
                   .ReturnsAsync(new LabelReadByKeyResponse() { Status = ResponseStatus.Failed });
        }

        public static void Setup_GetLabelByKey_Returns_LabelReadByKeyResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabelByKey(It.IsAny<LabelReadByKeyRequest>()))
                   .ReturnsAsync(new LabelReadByKeyResponse() { Status = ResponseStatus.Invalid });
        }

        public static void Setup_ChangeActivationForLabel_Returns_LabelChangeActivationResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<LabelChangeActivationRequest>()))
                   .ReturnsAsync(new LabelChangeActivationResponse() { Status = ResponseStatus.Success });
        }

        public static void Setup_ChangeActivationForLabel_Returns_LabelChangeActivationResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<LabelChangeActivationRequest>()))
                   .ReturnsAsync(new LabelChangeActivationResponse() { Status = ResponseStatus.Failed });
        }

        public static void Setup_ChangeActivationForLabel_Returns_LabelChangeActivationResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<LabelChangeActivationRequest>()))
                   .ReturnsAsync(new LabelChangeActivationResponse() { Status = ResponseStatus.Invalid });
        }

        public static void Setup_RestoreLabel_Returns_LabelRestoreResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.RestoreLabel(It.IsAny<LabelRestoreRequest>()))
                   .ReturnsAsync(new LabelRestoreResponse() { Status = ResponseStatus.Success });
        }

        public static void Setup_RestoreLabel_Returns_LabelRestoreResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.RestoreLabel(It.IsAny<LabelRestoreRequest>()))
                   .ReturnsAsync(new LabelRestoreResponse() { Status = ResponseStatus.Failed });
        }

        public static void Setup_RestoreLabel_Returns_LabelRestoreResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.RestoreLabel(It.IsAny<LabelRestoreRequest>()))
                   .ReturnsAsync(new LabelRestoreResponse() { Status = ResponseStatus.Invalid });
        }

        public static void Setup_GetLabelRevisions_Returns_LabelRevisionReadListResponse_Success(this Mock<ILabelService> service)
        {
            var items = new List<RevisionDto<LabelDto>>() { GetRevisionDtoLabelDto() };
            service.Setup(x => x.GetLabelRevisions(It.IsAny<LabelRevisionReadListRequest>()))
                   .ReturnsAsync(new LabelRevisionReadListResponse() { Status = ResponseStatus.Success, Items = items });
        }

        public static void Setup_GetLabelTranslationRevisions_Returns_LabelTranslationRevisionReadListResponse_Success(this Mock<ILabelService> service)
        {
            var items = new List<RevisionDto<LabelTranslationDto>>() { GetRevisionDtoLabelTranslationDto() };
            service.Setup(x => x.GetLabelTranslationRevisions(It.IsAny<LabelTranslationRevisionReadListRequest>()))
                   .ReturnsAsync(new LabelTranslationRevisionReadListResponse() { Status = ResponseStatus.Success, Items = items });
        }

        public static void Setup_GetLabelTranslationRevisions_Returns_LabelTranslationRevisionReadListResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabelTranslationRevisions(It.IsAny<LabelTranslationRevisionReadListRequest>()))
                   .ReturnsAsync(new LabelTranslationRevisionReadListResponse() { Status = ResponseStatus.Failed });
        }

        public static void Setup_GetLabelTranslationRevisions_Returns_LabelTranslationRevisionReadListResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabelTranslationRevisions(It.IsAny<LabelTranslationRevisionReadListRequest>()))
                   .ReturnsAsync(new LabelTranslationRevisionReadListResponse() { Status = ResponseStatus.Invalid });
        }

        public static void Setup_GetLabelRevisions_Returns_LabelRevisionReadListResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabelRevisions(It.IsAny<LabelRevisionReadListRequest>()))
                   .ReturnsAsync(new LabelRevisionReadListResponse() { Status = ResponseStatus.Failed });
        }

        public static void Setup_GetLabelRevisions_Returns_LabelRevisionReadListResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabelRevisions(It.IsAny<LabelRevisionReadListRequest>()))
                   .ReturnsAsync(new LabelRevisionReadListResponse() { Status = ResponseStatus.Invalid });
        }

        public static void Setup_GetLabels_Returns_LabelReadListResponse_Success(this Mock<ILabelService> service)
        {
            var items = new List<LabelDto>() { GetLabelDto() };
            service.Setup(x => x.GetLabels(It.IsAny<LabelReadListRequest>()))
                   .ReturnsAsync(new LabelReadListResponse { Status = ResponseStatus.Success, Items = items });
        }

        public static void Setup_GetLabel_Returns_LabelReadResponse_Success(this Mock<ILabelService> service)
        {
            var item = new LabelDto() { ProjectUid = UidOne };
            service.Setup(x => x.GetLabel(It.IsAny<LabelReadRequest>()))
                   .ReturnsAsync(new LabelReadResponse { Status = ResponseStatus.Success, Item = item });
        }

        public static void Setup_CreateLabel_Returns_LabelCreateResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateLabel(It.IsAny<LabelCreateRequest>()))
                   .ReturnsAsync(new LabelCreateResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_CreateLabelFromList_Returns_LabelCreateListResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateLabelFromList(It.IsAny<LabelCreateListRequest>()))
                   .ReturnsAsync(new LabelCreateListResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_EditLabel_Returns_LabelEditResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.EditLabel(It.IsAny<LabelEditRequest>()))
                   .ReturnsAsync(new LabelEditResponse { Status = ResponseStatus.Success, Item = new LabelDto() { Uid = UidOne } });
        }

        public static void Setup_DeleteLabel_Returns_LabelDeleteResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.DeleteLabel(It.IsAny<LabelDeleteRequest>()))
                   .ReturnsAsync(new LabelDeleteResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_CloneLabel_Returns_LabelCloneResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CloneLabel(It.IsAny<LabelCloneRequest>()))
                   .ReturnsAsync(new LabelCloneResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_GetTranslations_Returns_LabelTranslationReadListResponse_Success(this Mock<ILabelService> service)
        {
            var items = new List<LabelTranslationDto>() { GetLabelTranslationDto() };
            service.Setup(x => x.GetTranslations(It.IsAny<LabelTranslationReadListRequest>()))
                   .ReturnsAsync(new LabelTranslationReadListResponse { Status = ResponseStatus.Success, Items = items });
        }

        public static void Setup_GetLabelsWithTranslations_Returns_AllLabelReadListResponse_Success(this Mock<ILabelService> service)
        {
            var items = GetLabelFatDtoList();
            service.Setup(x => x.GetLabelsWithTranslations(It.IsAny<AllLabelReadListRequest>()))
                   .ReturnsAsync(new AllLabelReadListResponse { Status = ResponseStatus.Success, Labels = items });
        }

        public static void Setup_CreateTranslation_Returns_LabelTranslationCreateResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateTranslation(It.IsAny<LabelTranslationCreateRequest>()))
                   .ReturnsAsync(new LabelTranslationCreateResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_CreateTranslationFromList_Returns_LabelTranslationCreateListResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateTranslationFromList(It.IsAny<LabelTranslationCreateListRequest>()))
                   .ReturnsAsync(new LabelTranslationCreateListResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_EditTranslation_Returns_LabelTranslationEditResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.EditTranslation(It.IsAny<LabelTranslationEditRequest>()))
                   .ReturnsAsync(new LabelTranslationEditResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_DeleteTranslation_Returns_LabelTranslationDeleteResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.DeleteTranslation(It.IsAny<LabelTranslationDeleteRequest>()))
                   .ReturnsAsync(new LabelTranslationDeleteResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_ChangeActivation_Returns_LabelChangeActivationResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<LabelChangeActivationRequest>()))
                   .ReturnsAsync(new LabelChangeActivationResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_GetTranslation_Returns_LabelTranslationReadResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetTranslation(It.IsAny<LabelTranslationReadRequest>()))
                   .ReturnsAsync(new LabelTranslationReadResponse() { Status = ResponseStatus.Success });

        }

        public static void Setup_GetTranslation_Returns_LabelTranslationReadResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetTranslation(It.IsAny<LabelTranslationReadRequest>()))
                   .ReturnsAsync(new LabelTranslationReadResponse() { Status = ResponseStatus.Failed });

        }

        public static void Setup_GetTranslation_Returns_LabelTranslationReadResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetTranslation(It.IsAny<LabelTranslationReadRequest>()))
                   .ReturnsAsync(new LabelTranslationReadResponse() { Status = ResponseStatus.Invalid });
        }

        public static void Setup_GetLabelsWithTranslations_Returns_AllLabelReadListResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabelsWithTranslations(It.IsAny<AllLabelReadListRequest>()))
                   .ReturnsAsync(new AllLabelReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetLabels_Returns_LabelReadListResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabels(It.IsAny<LabelReadListRequest>()))
                   .ReturnsAsync(new LabelReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetLabel_Returns_LabelReadResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabel(It.IsAny<LabelReadRequest>()))
                   .ReturnsAsync(new LabelReadResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetLabels_Returns_LabelSearchListResponse_Success(this Mock<ILabelService> service)
        {
            var items = new List<LabelDto>() { GetLabelDto() };
            service.Setup(x => x.GetLabels(It.IsAny<LabelSearchListRequest>()))
                   .ReturnsAsync(new LabelSearchListResponse() { Status = ResponseStatus.Success, Items = items });
        }

        public static void Setup_GetLabels_Returns_LabelSearchListResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabels(It.IsAny<LabelSearchListRequest>()))
                   .ReturnsAsync(new LabelSearchListResponse() { Status = ResponseStatus.Failed });
        }

        public static void Verify_GetLabels_LabelSearchListRequest(this Mock<ILabelService> service)
        {
            service.Verify(x => x.GetLabels(It.IsAny<LabelSearchListRequest>()));
        }

        public static void Setup_GetLabels_Returns_LabelSearchListResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabels(It.IsAny<LabelSearchListRequest>()))
                   .ReturnsAsync(new LabelSearchListResponse() { Status = ResponseStatus.Invalid });
        }

        public static void Setup_CreateLabel_Returns_LabelCreateResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateLabel(It.IsAny<LabelCreateRequest>()))
                   .ReturnsAsync(new LabelCreateResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_CreateLabelFromList_Returns_LabelCreateListResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateLabelFromList(It.IsAny<LabelCreateListRequest>()))
                   .ReturnsAsync(new LabelCreateListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_EditLabel_Returns_LabelEditResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.EditLabel(It.IsAny<LabelEditRequest>()))
                   .ReturnsAsync(new LabelEditResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_DeleteLabel_Returns_LabelDeleteResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.DeleteLabel(It.IsAny<LabelDeleteRequest>()))
                   .ReturnsAsync(new LabelDeleteResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_CloneLabel_Returns_LabelCloneResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CloneLabel(It.IsAny<LabelCloneRequest>()))
                   .ReturnsAsync(new LabelCloneResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetTranslations_Returns_LabelTranslationReadListResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetTranslations(It.IsAny<LabelTranslationReadListRequest>()))
                   .ReturnsAsync(new LabelTranslationReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_RestoreLabelTranslation_Returns_LabelTranslationRestoreResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.RestoreLabelTranslation(It.IsAny<LabelTranslationRestoreRequest>()))
                   .ReturnsAsync(new LabelTranslationRestoreResponse() { Status = ResponseStatus.Success });
        }

        public static void Setup_RestoreLabelTranslation_Returns_LabelTranslationRestoreResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.RestoreLabelTranslation(It.IsAny<LabelTranslationRestoreRequest>()))
                   .ReturnsAsync(new LabelTranslationRestoreResponse() { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_RestoreLabelTranslation_Returns_LabelTranslationRestoreResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.RestoreLabelTranslation(It.IsAny<LabelTranslationRestoreRequest>()))
                   .ReturnsAsync(new LabelTranslationRestoreResponse() { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_CreateTranslation_Returns_LabelTranslationCreateResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateTranslation(It.IsAny<LabelTranslationCreateRequest>()))
                   .ReturnsAsync(new LabelTranslationCreateResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_CreateTranslationFromList_Returns_LabelTranslationCreateListResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateTranslationFromList(It.IsAny<LabelTranslationCreateListRequest>()))
                   .ReturnsAsync(new LabelTranslationCreateListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_EditTranslation_Returns_LabelTranslationEditResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.EditTranslation(It.IsAny<LabelTranslationEditRequest>()))
                   .ReturnsAsync(new LabelTranslationEditResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_DeleteTranslation_Returns_LabelTranslationDeleteResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.DeleteTranslation(It.IsAny<LabelTranslationDeleteRequest>()))
                   .ReturnsAsync(new LabelTranslationDeleteResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_ChangeActivation_Returns_LabelChangeActivationResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<LabelChangeActivationRequest>()))
                   .ReturnsAsync(new LabelChangeActivationResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetLabelsWithTranslations_Returns_AllLabelReadListResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabelsWithTranslations(It.IsAny<AllLabelReadListRequest>()))
                   .ReturnsAsync(new AllLabelReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetLabels_Returns_LabelReadListResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabels(It.IsAny<LabelReadListRequest>()))
                   .ReturnsAsync(new LabelReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetLabel_Returns_LabelReadResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabel(It.IsAny<LabelReadRequest>()))
                   .ReturnsAsync(new LabelReadResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_CreateLabel_Returns_LabelCreateResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateLabel(It.IsAny<LabelCreateRequest>()))
                   .ReturnsAsync(new LabelCreateResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_CreateLabelFromList_Returns_LabelCreateListResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateLabelFromList(It.IsAny<LabelCreateListRequest>()))
                   .ReturnsAsync(new LabelCreateListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_EditLabel_Returns_LabelEditResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.EditLabel(It.IsAny<LabelEditRequest>()))
                   .ReturnsAsync(new LabelEditResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_DeleteLabel_Returns_LabelDeleteResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.DeleteLabel(It.IsAny<LabelDeleteRequest>()))
                   .ReturnsAsync(new LabelDeleteResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_CloneLabel_Returns_LabelCloneResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CloneLabel(It.IsAny<LabelCloneRequest>()))
                   .ReturnsAsync(new LabelCloneResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetTranslations_Returns_LabelTranslationReadListResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetTranslations(It.IsAny<LabelTranslationReadListRequest>()))
                   .ReturnsAsync(new LabelTranslationReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_CreateTranslation_Returns_LabelTranslationCreateResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateTranslation(It.IsAny<LabelTranslationCreateRequest>()))
                   .ReturnsAsync(new LabelTranslationCreateResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_CreateTranslationFromList_Returns_LabelTranslationCreateListResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateTranslationFromList(It.IsAny<LabelTranslationCreateListRequest>()))
                   .ReturnsAsync(new LabelTranslationCreateListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_EditTranslation_Returns_LabelTranslationEditResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.EditTranslation(It.IsAny<LabelTranslationEditRequest>()))
                   .ReturnsAsync(new LabelTranslationEditResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_DeleteTranslation_Returns_LabelTranslationDeleteResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.DeleteTranslation(It.IsAny<LabelTranslationDeleteRequest>()))
                   .ReturnsAsync(new LabelTranslationDeleteResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_ChangeActivation_Returns_LabelChangeActivationResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<LabelChangeActivationRequest>()))
                   .ReturnsAsync(new LabelChangeActivationResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Verify_ChangeActivationForLabel(this Mock<ILabelService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<LabelChangeActivationRequest>()));
        }

        public static void Verify_GetLabelsWithTranslations(this Mock<ILabelService> service)
        {
            service.Verify(x => x.GetLabelsWithTranslations(It.IsAny<AllLabelReadListRequest>()));
        }

        public static void Verify_GetLabels(this Mock<ILabelService> service)
        {
            service.Verify(x => x.GetLabels(It.IsAny<LabelReadListRequest>()));
        }

        public static void Verify_GetLabel(this Mock<ILabelService> service)
        {
            service.Verify(x => x.GetLabel(It.IsAny<LabelReadRequest>()));
        }

        public static void Verify_CreateLabel(this Mock<ILabelService> service)
        {
            service.Verify(x => x.CreateLabel(It.IsAny<LabelCreateRequest>()));
        }

        public static void Verify_CreateLabelFromList(this Mock<ILabelService> service)
        {
            service.Verify(x => x.CreateLabelFromList(It.IsAny<LabelCreateListRequest>()));
        }

        public static void Verify_EditLabel(this Mock<ILabelService> service)
        {
            service.Verify(x => x.EditLabel(It.IsAny<LabelEditRequest>()));
        }

        public static void Verify_DeleteLabel(this Mock<ILabelService> service)
        {
            service.Verify(x => x.DeleteLabel(It.IsAny<LabelDeleteRequest>()));
        }

        public static void Verify_CloneLabel(this Mock<ILabelService> service)
        {
            service.Verify(x => x.CloneLabel(It.IsAny<LabelCloneRequest>()));
        }

        public static void Verify_GetTranslations(this Mock<ILabelService> service)
        {
            service.Verify(x => x.GetTranslations(It.IsAny<LabelTranslationReadListRequest>()));
        }

        public static void Verify_CreateTranslation(this Mock<ILabelService> service)
        {
            service.Verify(x => x.CreateTranslation(It.IsAny<LabelTranslationCreateRequest>()));
        }

        public static void Verify_CreateTranslationFromList(this Mock<ILabelService> service)
        {
            service.Verify(x => x.CreateTranslationFromList(It.IsAny<LabelTranslationCreateListRequest>()));
        }

        public static void Verify_EditTranslation(this Mock<ILabelService> service)
        {
            service.Verify(x => x.EditTranslation(It.IsAny<LabelTranslationEditRequest>()));
        }

        public static void Verify_DeleteTranslation(this Mock<ILabelService> service)
        {
            service.Verify(x => x.DeleteTranslation(It.IsAny<LabelTranslationDeleteRequest>()));
        }

        public static void Verify_ChangeActivation(this Mock<ILabelService> service)
        {
            service.Verify(x => x.ChangeActivation(It.IsAny<LabelChangeActivationRequest>()));
        }

        public static void Verify_GetTranslation(this Mock<ILabelService> service)
        {
            service.Verify(x => x.GetTranslation(It.IsAny<LabelTranslationReadRequest>()));
        }

        public static void Verify_GetLabelRevisions(this Mock<ILabelService> service)
        {
            service.Verify(x => x.GetLabelRevisions(It.IsAny<LabelRevisionReadListRequest>()));

        }

        public static void Verify_RestoreRevision(this Mock<ILabelRepository> repository)
        {
            repository.Verify(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()));
        }

        public static void Verify_SelectRevisions(this Mock<ILabelRepository> repository)
        {
            repository.Verify(x => x.SelectRevisions(It.IsAny<long>()));
        }

        public static void Verify_RestoreLabel(this Mock<ILabelService> service)
        {
            service.Verify(x => x.RestoreLabel(It.IsAny<LabelRestoreRequest>()));
        }

        public static void Verify_GetLabelByKey(this Mock<ILabelService> service)
        {
            service.Verify(x => x.GetLabelByKey(It.IsAny<LabelReadByKeyRequest>()));

        }

        public static void Verify_RestoreLabelTranslation(this Mock<ILabelService> service)
        {
            service.Verify(x => x.RestoreLabelTranslation(It.IsAny<LabelTranslationRestoreRequest>()));
        }

        public static void Verify_GetLabelTranslationRevisions(this Mock<ILabelService> service)
        {
            service.Verify(x => x.GetLabelTranslationRevisions(It.IsAny<LabelTranslationRevisionReadListRequest>()));
        }
    }
}