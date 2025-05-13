using HotChocolate.Authorization;
using Server.Mapper.CommentTypes.OutputTypes;
using Server.Mapper.ProjectTypes.OutputTypes;
using Server.Mapper.UserTypes.OutputTypes;
using Server.Mapper.VoteTypes.OutputTypes;
using Server.Models.Types.Comment.OutputTypes;
using Server.Models.Types.Project.OutputTypes;
using Server.Models.Types.User.OutputTypes;
using Server.Models.Types.Vote.OutputTypes;
using Server.Repositories;

namespace Server.Models.Types.Contribution.OutputTypes
{
    public class GetContributionType
    {
        [Authorize]
        public Guid ContributionID { get; set; }

        [Authorize]
        public Guid ProjectID { get; set; }

        [Authorize]
        public Guid UserID { get; set; }

        [Authorize]
        public string Category { get; set; }

        [Authorize]
        public string Title { get; set; }

        [Authorize]
        public string Text { get; set; }

        [Authorize]
        public string Date { get; set; }

        [Authorize]
        public string Status { get; set; }

        [Authorize]
        [GraphQLNonNullType]
        public async Task<GetProjectType> ProjectAsync([Service] IProjectRepository projectRepository, [Service] IProjectToGetProjectTypeMapper projectToGetProjectTypeMapper)
        {
            var project = await projectRepository.GetProjectByIdAsync(ProjectID);
            var getProject = projectToGetProjectTypeMapper.Map(project);
            return getProject;
        }

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
        public async Task<IEnumerable<GetVoteType>> VotesAsync([Service] IVoteRepository voteRepository, [Service] IVoteToGetVoteTypeMapper voteToGetVoteTypeMapper)
        {
            var voteList = await voteRepository.GetVotesByContributionIdAsync(ContributionID);
            var getVoteList = voteList.Select(voteToGetVoteTypeMapper.Map).ToList();
            return getVoteList;
        }

        [Authorize]
        [GraphQLNonNullType]
        public async Task<IEnumerable<GetCommentType>> CommentsAsync([Service] ICommentRepository commentRepository, [Service] ICommentToGetCommentTypeMapper commentToGetCommentTypeMapper)
        {
            var commentList = await commentRepository.GetCommentsByContributionIdAsync(ContributionID);
            var getCommentList = commentList.Select(commentToGetCommentTypeMapper.Map).ToList();
            return getCommentList;
        }
    }
}
