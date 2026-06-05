using System;
using AdventureHorizonsBD.Models;
using AdventureHorizonsBD.DAL;

namespace AdventureHorizonsBD.BLL
{
    public class MembershipBLL
    {
        private readonly MembershipDAL _membershipDAL = new MembershipDAL();
        private readonly UserDAL _userDAL = new UserDAL();

        public bool SubmitMembershipRequest(int userId, string plan, out string message)
        {
            try
            {
                var request = new MembershipRequestModel
                {
                    UserID = userId,
                    MembershipType = plan
                };
                
                _membershipDAL.SubmitRequest(request);
                message = "Membership request submitted successfully.";
                return true;
            }
            catch (Exception ex)
            {
                message = "Error submitting request: " + ex.Message;
                return false;
            }
        }

        public bool ApproveRequest(int requestId, out string message)
        {
            try
            {
                var request = _membershipDAL.GetRequestById(requestId);
                if (request != null)
                {
                    _membershipDAL.UpdateRequestStatus(requestId, "Approved");
                    _userDAL.UpdateUserApproval(request.UserID, true);
                    
                    message = "Membership approved successfully.";
                    return true;
                }
                message = "Request not found.";
                return false;
            }
            catch (Exception ex)
            {
                message = "Error approving request: " + ex.Message;
                return false;
            }
        }

        public bool RejectRequest(int requestId, out string message)
        {
            try
            {
                _membershipDAL.UpdateRequestStatus(requestId, "Rejected");
                message = "Membership rejected successfully.";
                return true;
            }
            catch (Exception ex)
            {
                message = "Error rejecting request: " + ex.Message;
                return false;
            }
        }
    }
}
