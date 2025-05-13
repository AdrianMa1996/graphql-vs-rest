using HotChocolate.Authorization;
using Server.Mapper.ContributionTypes.OutputTypes;
using Server.Mapper.UserTypes.OutputTypes;
using Server.Models.Types.Contribution.OutputTypes;
using Server.Models.Types.User.OutputTypes;
using Server.Repositories;

namespace Server.Models.Types.Comment.OutputTypes
{
    public class GetCommentType
    {
        [Authorize]
        public Guid CommentID { get; set; }

        [Authorize]
        public Guid UserID { get; set; }

        [Authorize]
        public Guid ContributionID { get; set; }

        [Authorize]
        public string Text { get; set; }

        [Authorize]
        public string Date { get; set; }

        [Authorize]
        [GraphQLNonNullType]
        public async Task<GetUserType> CreatorAsync([Service] IUserRepository userRepository, [Service] IUserToGetUserTypeMapper userToGetUserTypeMapper)
        {
            var creator = await userRepository.GetUserByIdAsync(UserID);
            var getCreator = userToGetUserTypeMapper.Map(creator);
            return getCreator;
        }

        [Authorize]
        [GraphQLNonNullType]
        public async Task<GetContributionType> ContributionsAsync([Service] IContributionRepository contributionRepository, [Service] IContributionToGetContributionTypeMapper contributionToGetContributionTypeMapper)
        {
            var contribution = await contributionRepository.GetContributionByIdAsync(ContributionID);
            var getContribution = contributionToGetContributionTypeMapper.Map(contribution);
            return getContribution;
        }
    }
}
