using HotChocolate.Authorization;
using Server.Mapper.VoteTypes.InputTypes;
using Server.Models.Types.Vote.InputTypes;
using Server.Repositories;

namespace Server.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class VoteMutation
    {
        [Authorize]
        public async Task<string> CreateVoteAsync(CreateVoteType input, [Service] IVoteRepository voteRepository, [Service] ICreateVoteTypeToVoteMapper createVoteTypeToVoteMapper)
        {
            var vote = createVoteTypeToVoteMapper.Map(input);
            await voteRepository.CreateVoteAsync(vote);
            return "Vote created successfully";
        }

        [Authorize]
        public async Task<string> UpdateVoteAsync(UpdateVoteType input, [Service] IVoteRepository voteRepository, [Service] IUpdateVoteTypeToVoteMapper updateVoteTypeToVoteMapper)
        {
            var existingVote = await voteRepository.GetVoteByIdAsync(input.VoteID);
            var vote = updateVoteTypeToVoteMapper.Map(existingVote, input);
            await voteRepository.UpdateVoteAsync(vote);
            return "Vote updated successfully";
        }

        [Authorize]
        public async Task<string> DeleteVoteByIdAsync(Guid voteId, [Service] IVoteRepository voteRepository)
        {
            await voteRepository.DeleteVoteByIdAsync(voteId);
            return "Vote deleted successfully";
        }
    }
}
