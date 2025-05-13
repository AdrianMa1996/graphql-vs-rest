using HotChocolate.Authorization;
using Server.Mapper.ContributionTypes.OutputTypes;
using Server.Mapper.UserTypes.OutputTypes;
using Server.Models.Types.Contribution.OutputTypes;
using Server.Models.Types.User.OutputTypes;
using Server.Repositories;

namespace Server.Models.Types.Vote.OutputTypes
{
    public class GetVoteType
    {
        [Authorize]
        public Guid VoteID { get; set; }

        [Authorize]
        public Guid UserID { get; set; }

        [Authorize]
        public Guid ContributionID { get; set; }

        [Authorize]
        [GraphQLNonNullType]
        public async Task<GetUserType> UserAsync([Service] IUserRepository userRepository, [Service] IUserToGetUserTypeMapper userToGetUserTypeMapper)
        {
            var user = await userRepository.GetUserByIdAsync(UserID);
            var getUser = userToGetUserTypeMapper.Map(user);
            return getUser;
        }

        [Authorize]
        [GraphQLNonNullType]
        public async Task<GetContributionType> ContributionAsync([Service] IContributionRepository contributionRepository, [Service] IContributionToGetContributionTypeMapper contributionToGetContributionTypeMapper)
        {
            var contribution = await contributionRepository.GetContributionByIdAsync(ContributionID);
            var getContribution = contributionToGetContributionTypeMapper.Map(contribution);
            return getContribution;
        }
    }
}
