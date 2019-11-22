﻿using System.Collections.Generic;

using Moq;
using StandardUtils.Enumerations;

using Translation.Common.Contracts;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Journal;
using Translation.Common.Models.Responses.Journal;

using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeDtoTestHelper;

namespace Translation.Client.Web.Unit.Tests.ServiceSetupHelpers
{
    public static class JournalServiceSetupHelper
    {
        public static void Setup_GetJournalsOfOrganization_Returns_OrganizationJournalReadListResponse_Success(this Mock<IJournalService> service)
        {
            var items = new List<JournalDto>() { GetJournalDto() };

            service.Setup(x => x.GetJournalsOfOrganization(It.IsAny<OrganizationJournalReadListRequest>()))
                   .ReturnsAsync(new JournalReadListResponse { Status = ResponseStatus.Success, Items = items });
        }

        public static void Setup_GetJournalsOfOrganization_Returns_OrganizationJournalReadListResponse_UserUidAndIntegrationUidEmpty_Success(this Mock<IJournalService> service)
        {
            var items = new List<JournalDto>() { GetUserUidAndIntegrationUidEmptyJournalDto() };

            service.Setup(x => x.GetJournalsOfOrganization(It.IsAny<OrganizationJournalReadListRequest>()))
                   .ReturnsAsync(new JournalReadListResponse { Status = ResponseStatus.Success, Items = items });
        }

        public static void Setup_GetJournalsOfUser_Returns_UserJournalReadListResponse_Success(this Mock<IJournalService> service)
        {
            var items = new List<JournalDto>() { GetJournalDto() };

            service.Setup(x => x.GetJournalsOfUser(It.IsAny<UserJournalReadListRequest>()))
                   .ReturnsAsync(new JournalReadListResponse { Status = ResponseStatus.Success, Items = items });
        }

        public static void Setup_GetJournalsOfOrganization_Returns_OrganizationJournalReadListResponse_Failed(this Mock<IJournalService> service)
        {
            service.Setup(x => x.GetJournalsOfOrganization(It.IsAny<OrganizationJournalReadListRequest>()))
                   .ReturnsAsync(new JournalReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetJournalsOfUser_Returns_UserJournalReadListResponse_Failed(this Mock<IJournalService> service)
        {
            service.Setup(x => x.GetJournalsOfUser(It.IsAny<UserJournalReadListRequest>()))
                   .ReturnsAsync(new JournalReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetJournalsOfOrganization_Returns_OrganizationJournalReadListResponse_Invalid(this Mock<IJournalService> service)
        {
            service.Setup(x => x.GetJournalsOfOrganization(It.IsAny<OrganizationJournalReadListRequest>()))
                   .ReturnsAsync(new JournalReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetJournalsOfUser_Returns_UserJournalReadListResponse_Invalid(this Mock<IJournalService> service)
        {
            service.Setup(x => x.GetJournalsOfUser(It.IsAny<UserJournalReadListRequest>()))
                   .ReturnsAsync(new JournalReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Verify_GetJournalsOfOrganization(this Mock<IJournalService> service)
        {
            service.Verify(x => x.GetJournalsOfOrganization(It.IsAny<OrganizationJournalReadListRequest>()));
        }

        public static void Verify_GetJournalsOfUser(this Mock<IJournalService> service)
        {
            service.Verify(x => x.GetJournalsOfUser(It.IsAny<UserJournalReadListRequest>()));
        }
    }
}