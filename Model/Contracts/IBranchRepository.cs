using Shared.DTO.Branches;
using Shared.Models;

namespace Model.Contracts;

public interface IBranchRepository
{
    public Task<List<BranchModel>> GetBranches();

    Task InsertBranchData(List<BranchHolder> adoBranchesHolder);
    public Task<List<DuplicateBranchModel>?> MultipleBranches();
}