﻿using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Organization
{
    public class OrganizationRevisionReadListRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get; }

        public OrganizationRevisionReadListRequest(long currentUserId, Guid organizationUid) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            OrganizationUid = organizationUid;
        }
    }
}