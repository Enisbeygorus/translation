﻿using System;

using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.User
{
    public class UserRestoreRequest : BaseAuthenticatedRequest
    {
        public Guid UserUid { get; set; }
        public int Revision { get; set; }

        public UserRestoreRequest(long currentUserId, Guid userUid, int revision) : base(currentUserId)
        {
            UserUid = userUid;
            Revision = revision;
        }
    }
}