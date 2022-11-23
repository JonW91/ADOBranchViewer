namespace Shared.DTO.Repos;

public record AdoReposHolder(
    int Count,
    List<Value> Value);

// Records guaruntee immutability. This data must remain unchanged so the app can present factual information.