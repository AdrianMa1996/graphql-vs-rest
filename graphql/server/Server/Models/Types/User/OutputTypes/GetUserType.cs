using HotChocolate.Authorization;
using Server.Mapper.CommentTypes.OutputTypes;
using Server.Mapper.ContributionTypes.OutputTypes;
using Server.Mapper.ProjectAndUserBindingTypes.OutputTypes;
using Server.Mapper.VoteTypes.OutputTypes;
using Server.Models.Types.Comment.OutputTypes;
using Server.Models.Types.Contribution.OutputTypes;
using Server.Models.Types.ProjectAndUserBinding.OutputTypes;
using Server.Models.Types.Vote.OutputTypes;
using Server.Repositories;
using System.Security.Claims;

namespace Server.Models.Types.User.OutputTypes
{
    public class GetUserType
    {
        [Authorize]
        public Guid UserID { get; set; }

        [Authorize]
        public string Name { get; set; }

        [Authorize]
        public string ProfilPicture { get; set; }

        [Authorize]
        public string Email { get; set; }

        [GraphQLIgnore]
        public string Password { get; set; }

        [Authorize]
        public string GetPassword(ClaimsPrincipal currentUser)
        {
            if (!currentUser.HasClaim("Role", "Admin") && !currentUser.HasClaim("CanEditUser", UserID.ToString()))
            {
                throw new GraphQLException("You are not authorized to get this password.");
            }
            return Password;
        }

        [Authorize]
        public string Role { get; set; }

        [Authorize]
        [GraphQLNonNullType]
        public async Task<IEnumerable<GetProjectAndUserBindingType>> ProjectAndUserBindingsAsync([Service] IProjectAndUserBindingRepository projectAndUserBindingRepository, [Service] IProjectAndUserBindingToGetProjectAndUserBindingTypeMapper projectAndUserBindingToGetProjectAndUserBindingTypeMapper)
        {
            var projectAndUserBindingList = await projectAndUserBindingRepository.GetProjectAndUserBindingsByUserIdAsync(UserID);
            var getProjectAndUserBindingList = projectAndUserBindingList.Select(projectAndUserBindingToGetProjectAndUserBindingTypeMapper.Map).ToList();
            return getProjectAndUserBindingList;
        }

        [Authorize]
        [GraphQLNonNullType]
        public async Task<IEnumerable<GetContributionType>> ContributionsAsync([Service] IContributionRepository contributionRepository, [Service] IContributionToGetContributionTypeMapper contributionToGetContributionTypeMapper)
        {
            var contributionList = await contributionRepository.GetContributionsByUserIdAsync(UserID);
            var getContributionList = contributionList.Select(contributionToGetContributionTypeMapper.Map).ToList();
            return getContributionList;
        }

        [Authorize]
        [GraphQLNonNullType]
        public async Task<IEnumerable<GetVoteType>> VotesAsync([Service] IVoteRepository voteRepository, [Service] IVoteToGetVoteTypeMapper voteToGetVoteTypeMapper)
        {
            var voteList = await voteRepository.GetVotesByUserIdAsync(UserID);
            var getVoteList = voteList.Select(voteToGetVoteTypeMapper.Map).ToList();
            return getVoteList;
        }

        [Authorize]
        [GraphQLNonNullType]
        public async Task<IEnumerable<GetCommentType>> CommentsAsync([Service] ICommentRepository commentRepository, [Service] ICommentToGetCommentTypeMapper commentToGetCommentTypeMapper)
        {
            var commentList = await commentRepository.GetCommentsByUserIdAsync(UserID);
            var getCommentList = commentList.Select(commentToGetCommentTypeMapper.Map).ToList();
            return getCommentList;
        }
    }
}
